using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CryptoCAD.Common.Helpers;
using CryptoCAD.Common.Configurations.Ciphers;
using CryptoCAD.API.Models.Methods;
using CryptoCAD.Domain.Repositories;
using CryptoCAD.Domain.Entities.Methods;
using CryptoCAD.Domain.Entities.Methods.Base;
using CryptoCAD.Domain.Entities.Ciphers;
using CryptoCAD.Core.Services.Abstractions;

namespace CryptoCAD.API.Controllers
{
    [ApiController]
    [Route("api/methods")]
    public class MethodsController : ControllerBase
    {
        private readonly ICipherService CipherService;
        private readonly IHashService HashService;
        private readonly IStandardMethodsRepository StandardMethodsRepository;
        private readonly ILogger<MethodsController> Logger;
        private readonly IMapper Mapper;

        public MethodsController(ICipherService cipherService, IHashService hashService, IStandardMethodsRepository methodsRepository, ILogger<MethodsController> logger, IMapper mapper)
        {
            CipherService = cipherService;
            HashService = hashService;
            StandardMethodsRepository = methodsRepository;
            Logger = logger;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<MethodModel>> GetMethods()
        {
            try
            {
                var standardMethods = StandardMethodsRepository.GetAll();
                var methodModels = Mapper.Map<IEnumerable<MethodModel>>(standardMethods);

                Logger.LogInformation("GetMethods");
                return Ok(methodModels);
            }
            catch (Exception exception)
            {
                Logger.LogError("GetMethods", exception);
                return BadRequest($"Exception occured: {exception}");
            }
        }

        [HttpPost]
        [Route("process")]
        public ActionResult<ProcessResponse> Process(ProcessRequest request)
        {
            try
            {
                if (request.Id == Guid.Empty)
                {
                    throw new ArgumentNullException("Method Id is required!");
                }

                var type = request.Type.ToMethodType();

                if (type == MethodTypes.SymmetricCipher)
                {
                    var mode = request.Mode.ToCipherMode();

                    var key = request.Secret.ToBytes();
                    var data = request.Data.ToBytes(mode == CipherModes.Decrypt ? ConvertMode.BASE64 : ConvertMode.UTF8);

                    var method = StandardMethodsRepository.Get(request.Id);

                    var result = CipherService.Process(key, data, mode, method.Family, request.Configuration);

                    var dataResult = result.Data.ToString(mode == CipherModes.Encrypt ? ConvertMode.BASE64 : ConvertMode.UTF8);

                    return Ok(new ProcessResponse
                    {
                        Id = request.Id,
                        Type = request.Type,
                        Family = request.Family,
                        Mode = request.Mode,
                        Data = dataResult.Trim('\0'),
                        IntermediateResults = result.IntermediateResults
                    });
                }
                else if (type == MethodTypes.Hash)
                {
                    var method = StandardMethodsRepository.Get(request.Id);
                    var result = HashService.Hash(request.Data.ToBytes(), method.Name.ToMethodFamily(), request.Configuration);

                    return Ok(new ProcessResponse()
                    {
                        Id = request.Id,
                        Type = request.Type,
                        Family = request.Family,
                        Data = result.Data.ToString(ConvertMode.UTF8)
                    });
                }
                else
                {
                    throw new NotSupportedException($"Method type {request.Type} is not supported!");
                }
            }
            catch (Exception exception)
            {
                Logger.LogError("Process", exception);
                return BadRequest($"Exception occured: {exception}");
            }
        }

        [HttpPost]
        [Route("savechanges")]
        public ActionResult<MethodModel> SaveChanges(SaveRequest request)
        {
            try
            {
                var configuration = request.Configuration.DESConfigurationFromJsonString().ToJsonString();

                MethodModel result;
                var method = new StandardMethod
                {
                    Name = request.Name,
                    IsModifiable = true,
                    Relation = StandardMethodRelations.Child,
                    ParentId = request.ParentId,
                    Configuration = configuration
                };

                var parent = StandardMethodsRepository.Get(request.ParentId.Value);

                if (parent is null)
                {
                    throw new ArgumentException($"Parent with id '{method.ParentId}' is not exist!");
                }

                method.Family = parent.Family;
                method.SecretLength = parent.SecretLength;

                if (request.Id == Guid.Empty)
                {
                    method.Id = Guid.NewGuid();
                    StandardMethodsRepository.Add(method);
                    result = Mapper.Map<MethodModel>(method);
                }
                else
                {
                    method.Id = request.Id;
                    var current = StandardMethodsRepository.Get(method.Id);
                    if (current is null)
                    {
                        StandardMethodsRepository.Add(method);
                        result = Mapper.Map<MethodModel>(method);
                    }
                    else
                    {
                        if (current.Relation == StandardMethodRelations.Parent)
                        {
                            throw new InvalidOperationException($"Method with id'{current.Id}' can't be edited! It's a parent method!");
                        }
                        else
                        {
                            StandardMethodsRepository.Update(method);
                            result = Mapper.Map<MethodModel>(method);
                        }
                    }
                }

                StandardMethodsRepository.SaveChanges();
                return Ok(result);
            }
            catch (Exception exception)
            {
                Logger.LogError("SaveChanges", exception);
                return BadRequest($"Exception occured: {exception}");
            }
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete(DeleteRequest request)
        {
            try
            {
                if (request.Id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(request.Id));
                }

                var method = StandardMethodsRepository.Get(request.Id);

                if (method is null)
                {
                    throw new ArgumentException($"Method with id '{request.Id}' not exist!");
                }

                StandardMethodsRepository.Remove(method);
                StandardMethodsRepository.SaveChanges();

                return Ok("Successfully deleted!");
            }
            catch (Exception exception)
            {
                Logger.LogError("Delete", exception);
                return BadRequest($"Exception occured: {exception}");
            }
        }
    }
}