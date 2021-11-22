using SQLite;

namespace SampleAppDemo.Models
{
    public class ScanModel
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public bool IsChecked { get; set; }
        public string Name { get; set; }
    }
}
