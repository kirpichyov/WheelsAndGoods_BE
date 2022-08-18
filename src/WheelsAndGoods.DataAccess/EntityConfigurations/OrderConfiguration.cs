using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.DataAccess.EntityConfigurations
{
    public class OrderConfiguration : EntityConfigurationBase<Order, Guid>
	{
		public override void Configure(EntityTypeBuilder<Order> builder)
		{
			base.Configure(builder);

			builder.Property(entity => entity.Title).IsRequired();
			builder.Property(entity => entity.Cargo).IsRequired();
			builder.Property(entity => entity.Description);
			builder.Property(entity => entity.From).IsRequired();
			builder.Property(entity => entity.To).IsRequired();
			builder.Property(entity => entity.DeliveryDeadlineAtUtc).IsRequired();
			builder.Property(entity => entity.Price).IsRequired();

			builder.HasOne(entity => entity.Customer)
				.WithMany()
				.HasForeignKey(entity => entity.CustomerId);
		}
	}
}
