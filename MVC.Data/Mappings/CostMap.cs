using MVC.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace MVC.Data.Mappings
{
    public class CostMap : EntityTypeConfiguration<Cost>
    {
        public CostMap()
        {
            ToTable("Cost");
            Property(b => b.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(b => b.Name).HasMaxLength(100);
            Property(b => b.Dated).HasColumnType("date");
            Property(b => b.EstimatedCost).HasPrecision(18, 2);
            Property(b => b.Description).HasMaxLength(200);
        }
    }
}
