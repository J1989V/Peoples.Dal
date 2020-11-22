using System.Collections.Generic;

namespace Peoples.Dal.Models
{
	public class TableTab
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int DataStoreId { get; set; }

		public DataStore DataStore { get; set; }

		public virtual ICollection<Field> Fields { get; set; }
	}
}