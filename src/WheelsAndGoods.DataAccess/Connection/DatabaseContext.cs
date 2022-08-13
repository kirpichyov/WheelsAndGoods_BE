#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.EntityConfigurations;

namespace WheelsAndGoods.DataAccess.Connection;

public class DatabaseContext : DbContext
{
	public DbSet<User> Users { get; set; }

	public DatabaseContext(DbContextOptions<DatabaseContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
	}
}
