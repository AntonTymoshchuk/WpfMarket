using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMarket.DatabaseContext
{
    public class WpfMarketContext : DbContext
    {
        public WpfMarketContext() : base("Data Source=LAPTOP-OCJDU2KO;Initial Catalog=WpfMarket;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        { }

        public DbSet<Product> Products { get; set; }
    }
}
