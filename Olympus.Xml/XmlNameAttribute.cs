using System;

namespace Olympus.Xml
{
    [AttributeUsage(AttributeTargets.Property)]
    public class XmlNameAttribute : Attribute
    {
        public XmlNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
