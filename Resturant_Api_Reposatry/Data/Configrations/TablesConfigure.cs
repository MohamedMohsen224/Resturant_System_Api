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
    public class TablesConfigure : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
           builder.Property(x => x.Capacity).IsRequired();
           builder.Property(x => x.IsAvailable).IsRequired();
           builder.Property(x => x.smoking).IsRequired();
            builder.Property(x => x.Floor).IsRequired();
            builder.HasOne(x => x.Reservation).WithOne(x => x.Table).HasForeignKey<Table>(x => x.ReservationId);
        }
    }
}
