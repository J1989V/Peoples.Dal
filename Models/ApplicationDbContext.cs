﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Peoples.Dal.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<PopiMetadata> PopiMetadata { get; set; }
		
		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}