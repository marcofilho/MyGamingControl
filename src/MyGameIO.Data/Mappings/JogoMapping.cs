using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGameIO.Business.Models;

namespace MyGameIO.Data.Mappings
{
    public class JogoMapping : IEntityTypeConfiguration<Jogo>
    {

        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            //Primary Key
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Nome).IsRequired().HasColumnType("varchar(100)");

            builder.ToTable("Jogos");
        }
    }
}
