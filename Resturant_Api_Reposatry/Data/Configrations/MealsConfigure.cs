using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resturant_Api_Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Reposatry.Data.Configrations
{
    public class MealsConfigure : IEntityTypeConfiguration<Meals>
    {
        public void Configure(EntityTypeBuilder<Meals> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(60).IsFixedLength();
            builder.Property(x => x.Components).IsRequired().HasMaxLength(200).IsFixedLength();
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.PictureUrl).IsRequired();
            builder.HasOne(x => x.Category).WithMany();
        }
    }
}
