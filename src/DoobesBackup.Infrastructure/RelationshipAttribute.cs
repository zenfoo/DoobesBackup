namespace DoobesBackup.Infrastructure
{
    using System;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property )]
    public class RelationshipAttribute : Attribute
    {
        public bool IsOwner { get; set; }

        public RelationshipAttribute(bool IsOwner = true)
        {
            this.IsOwner = IsOwner;
        }
    }
}
