using KWH.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;

namespace KWH.DAL.DataContext
{
    public partial class KWHDBContext : DbContext
    {
        public KWHDBContext(DbContextOptions<KWHDBContext> options) : base(options)
        {
        }
        public DbSet<RFId> RFId { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<ClassMaster> ClassMaster { get; set; }
        public DbSet<Category> Category { get; set; }   

    }
}
