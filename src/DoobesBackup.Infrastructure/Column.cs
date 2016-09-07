namespace DoobesBackup.Infrastructure
{
    public class Column
    {
        public string Name { get; set; }
        public ColumnType Type { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
    }
}
