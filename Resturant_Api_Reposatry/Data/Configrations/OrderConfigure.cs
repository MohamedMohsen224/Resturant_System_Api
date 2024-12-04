using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resturant_Api_Core.Entites.Enum;
using Resturant_Api_Core.Entites.Order_Mangment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Reposatry.Data.Configrations
{
    public class OrderConfigure : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.Address, shippingAddress => shippingAddress.WithOwner()); // 1:1[Total]
            builder.Property(o => o.status)
                   .HasConversion(

                        OStatus => OStatus.ToString(),

                        OStatus => (Status)Enum.Parse(typeof(Status), OStatus)

                    );
            builder.Property(o => o.SubTotal)
                 .HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
