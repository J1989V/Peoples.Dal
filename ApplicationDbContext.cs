using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Peoples.Dal.Models;

namespace Peoples.Dal
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<PopiMetadata> PopiMetadatas { get; set; }
		public DbSet<IdentificationNumber> IdentificationNumbers { get; set; }
		public DbSet<IdentificationNumberPopiMetadataMap> IdentificationNumberPopiMetadataMaps { get; set; }

		public ApplicationDbContext( )
			: base( "DefaultConnection", throwIfV1Schema: false ) { }

		public static ApplicationDbContext Create( )
		{
			return new ApplicationDbContext( );
		}
	}
}