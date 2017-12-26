using MVC.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext()
            : base("AppDB")
        {

        }
        public virtual void Commit()
        {
            base.SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Assembly.GetExecutingAssembly().GetTypes()
                 .Where(type => type.Namespace == "MVC.Data.Mappings")
                 .ToList()
                 .ForEach(type =>
                 {
                     dynamic instance = Activator.CreateInstance(type);
                     modelBuilder.Configurations.Add(instance);
                 });
        }

        public DbSet<Cost> Costs { get; set; }
    }
}
