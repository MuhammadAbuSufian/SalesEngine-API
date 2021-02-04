    using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class BusinessDbContext:DbContext
    {
        public BusinessDbContext() : base("name=DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            //Configuration.AutoDetectChangesEnabled = true;
        }

        public static BusinessDbContext Create()
        {
            return new BusinessDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        //add your models to DbSet here
        
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<PermissionMap> PermissionMaps { get; set; }

        public virtual DbSet<Token> Tokens { get; set; }

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }

        public virtual DbSet<SalesDetail> SalesDetails { get; set; }

        public virtual DbSet<Purchase> Purchases { get; set; }

        public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Journal> Journals { get; set; }

        public virtual DbSet<JournalType> JournalTypes { get; set; }
    }
}
