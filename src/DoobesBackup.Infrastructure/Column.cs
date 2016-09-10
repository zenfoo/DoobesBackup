namespace DoobesBackup.Infrastructure
{
    public class Column
    {
        public string Name { get; set; } // Name of the column
        public ColumnType Type { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public string PropertyPath { get; set; } // Path to access the value of the column from the source
    }
}
