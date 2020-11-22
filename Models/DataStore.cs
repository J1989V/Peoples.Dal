using System.Collections.Generic;

namespace Peoples.Dal.Models
{
	public class DataStore
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Type { get; set; }

		public string Location { get; set; }

		public virtual ICollection<TableTab> TableTabs { get; set; }
	}
}