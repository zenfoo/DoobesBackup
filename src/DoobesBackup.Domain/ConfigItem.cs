namespace DoobesBackup.Domain
{
    using System;

    public class ConfigItem : Entity
    {
        public ConfigItem(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public string Key { get; private set; }

        public string Value { get; private set; }
    }
}
