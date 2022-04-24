using KWH.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.DAL.DataContext
{
    public class KWHContext : DbContext
    {
        public KWHContext(DbContextOptions<KWHContext> options) : base(options)
        {
        }
        public DbSet<RFId> RFId { get; set; }

    }
}
