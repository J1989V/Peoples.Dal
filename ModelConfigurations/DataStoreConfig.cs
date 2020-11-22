using System.Data.Entity.ModelConfiguration;
using Peoples.Dal.Models;

namespace Peoples.Dal.ModelConfigurations
{
	public class DataStoreConfig : EntityTypeConfiguration<DataStore>
	{
		public DataStoreConfig( )
		{
			HasKey( x => x.Id );
		}
	}
}