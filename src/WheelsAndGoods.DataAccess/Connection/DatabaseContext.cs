using Microsoft.EntityFrameworkCore;
using WheelsAndGoods.DataAccess.EntityConfigurations;

namespace WheelsAndGoods.DataAccess.Connection;

public class DatabaseContext : DbContext
{
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
