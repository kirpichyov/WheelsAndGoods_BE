using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.DataAccess.EntityConfigurations
{
    public class OrderRequestConfiguration : EntityConfigurationBase<OrderRequest, Guid>
    {
		public override void Configure(EntityTypeBuilder<OrderRequest> builder)
		{
			base.Configure(builder);

			builder.Property(entity => entity.Comment);
			builder.Property(entity => entity.CreatedAtUtc).IsRequired();

			builder.HasOne(entity => entity.User)
				.WithMany()
				.HasForeignKey(entity => entity.UserId);

			builder.HasOne(entity => entity.Order)
				.WithMany()
				.HasForeignKey(entity => entity.OrderId);
		}
	}
}
