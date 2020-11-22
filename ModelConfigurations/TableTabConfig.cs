using System.Data.Entity.ModelConfiguration;
using Peoples.Dal.Models;

namespace Peoples.Dal.ModelConfigurations
{
	public class TableTabConfig : EntityTypeConfiguration<TableTab>
	{
		public TableTabConfig( )
		{
			HasKey( x => x.Id );

			HasRequired( x => x.DataStore )
				.WithMany( x => x.TableTabs )
				.HasForeignKey( x => x.DataStoreId );
		}
	}
}