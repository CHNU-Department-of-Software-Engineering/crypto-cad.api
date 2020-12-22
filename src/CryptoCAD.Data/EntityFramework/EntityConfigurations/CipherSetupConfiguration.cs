using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoCAD.Domain.Entities;

namespace CryptoCAD.Data.EntityFramework.EntityConfigurations
{
    class CipherSetupConfiguration : IEntityTypeConfiguration<CipherSetup>
    {
        public void Configure(EntityTypeBuilder<CipherSetup> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}