﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.DataAccess.EntityConfigurations;

public abstract class EntityConfigurationBase<TEntity, TId> : IEntityTypeConfiguration<TEntity>
	where TEntity : EntityBase<TId>
{
	public virtual void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.HasKey(entity => entity.Id);
	}
}
