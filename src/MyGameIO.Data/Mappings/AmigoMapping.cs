using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGameIO.Business.Models;

namespace MyGameIO.Data.Mappings
{
    public class AmigoMapping : IEntityTypeConfiguration<Amigo>
    {

        public void Configure(EntityTypeBuilder<Amigo> builder)
        {
            //Primary Key
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome).IsRequired().HasColumnType("varchar(100)");
            builder.Property(a => a.Documento).IsRequired().HasColumnType("varchar(11)");

            //Config Relationship 1 : 1 => Amigo : Endereco
            builder.HasOne(a => a.Endereco).WithOne(e => e.Amigo);

            //Config Relationship 1 : N => Amigo : Games
            builder.HasMany(a => a.Jogos).WithOne(g => g.Amigo);

            builder.ToTable("Amigos");
        }
    }
}
