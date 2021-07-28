using System;

namespace Olympus.OWP.InfoSiteClient.Extensions.XMLExtensions
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class XMLName : Attribute
    {
        private string name;

        public XMLName(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
    }
}