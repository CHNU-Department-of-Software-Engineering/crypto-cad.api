using AutoMapper;
using CryptoCAD.API.Models.Methods;
using CryptoCAD.Domain.Entities.Methods;
using CryptoCAD.Domain.Entities.Methods.Base;

namespace CryptoCAD.API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StandardMethod, MethodModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToFriendlyString()))
                .ForMember(dest => dest.Family, opt => opt.MapFrom(src => src.Family.ToFriendlyString()))
                .ForMember(dest => dest.Relation, opt => opt.MapFrom(src => src.Relation.ToFriendlyString()));

            CreateMap<MethodModel, StandardMethod>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToMethodType()))
                .ForMember(dest => dest.Family, opt => opt.MapFrom(src => src.Family.ToMethodFamily()))
                .ForMember(dest => dest.Relation, opt => opt.MapFrom(src => src.Relation.ToMethodRelation()));
        }
    }
}