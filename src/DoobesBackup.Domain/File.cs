namespace DoobesBackup.Domain
{
    using System;

    public abstract class File : Entity
    {
        public virtual string Name { get; protected set; }
        public virtual DateTime LastModifiedUtc { get; protected set; }
        public virtual string Path { get; protected set; }
    }
}
