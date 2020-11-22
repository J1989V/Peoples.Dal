namespace Peoples.Dal.Models
{
	public class Field
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int Row { get; set; }

		public int Category { get; set; }

		public int TableTabId { get; set; }

		public virtual TableTab TableTab { get; set; }
	}
}