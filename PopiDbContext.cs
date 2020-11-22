using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Peoples.Dal.Models;

namespace Peoples.Dal
{
	public class PopiDbContext : IdentityDbContext<ApplicationUser>
	{
		// public DbSet<PopiMetadata> PopiMetadatas { get; set; }
		// public DbSet<IdentificationNumber> IdentificationNumbers { get; set; }
		// public DbSet<IdentificationNumberPopiMetadataMap> IdentificationNumberPopiMetadataMaps { get; set; }

		public DbSet<DataStore> DataStores { get; set; }
		public DbSet<TableTab> TableTabs { get; set; }
		public DbSet<Field> Fields { get; set; }

		public PopiDbContext( )
			: base( "DefaultConnection", throwIfV1Schema: false ) { }

		public static PopiDbContext Create( )
		{
			return new PopiDbContext( );
		}
	}
}