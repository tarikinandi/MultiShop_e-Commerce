using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Persistence.Context
{
    public class OrderContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-1F6TS0S\\SQLEXPRESS; initial catalog = MultiShopOrderDb; integrated security = true ");
        }

        DbSet<Address> Addresses { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<Ordering> Orderings { get; set; }
    }
}
