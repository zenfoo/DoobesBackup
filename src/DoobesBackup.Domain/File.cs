namespace DoobesBackup.Domain
{
    using System;

    /// <summary>
    /// Describes a file to be synchronised
    /// </summary>
    public class File : Entity
    {
        public string Name { get; protected set; }
        public DateTime LastModifiedUtc { get; protected set; }
        public string Path { get; protected set; }
        public File(string name, DateTime lastModifiedUtc, string path)
        {
            this.Name = name;
            this.LastModifiedUtc = lastModifiedUtc;
            this.Path = path;
        }
    }
}
