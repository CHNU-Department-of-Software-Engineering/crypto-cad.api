using CryptoCAD.Domain.Entities;
using CryptoCAD.Domain.Repositories;
using CryptoCAD.Data.EntityFramework;

namespace CryptoCAD.Data.Repositories
{
    public class CipherSetupRepository : Repository<CipherSetup>, ICipherSetupRepository
    {
        public CipherSetupRepository(PostgreSqlContext context) : base(context)
        {
            
        }
    }
}