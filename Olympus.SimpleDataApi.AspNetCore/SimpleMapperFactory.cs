using System;
using System.Collections.Generic;

namespace Olympus.SimpleDataApi
{
    public interface SimpleMapperFactory
    {
        SimpleMapperService GetSimpleMapperService(string apiKey);
    }

    public class SimpleMapperFactoryBase : SimpleMapperFactory
    {
        protected static Dictionary<string, Type> SimpleMapperServices = new Dictionary<string, Type>();

        protected static void AddSimpleMapperService(string apiKey, Type type)
        {
            SimpleMapperServices.Add(apiKey, type);
        }

        public virtual SimpleMapperService GetSimpleMapperService(string apiKey)
        {
            return SimpleMapperServices.ContainsKey(apiKey) ? Activator.CreateInstance(SimpleMapperServices[apiKey]) as SimpleMapperService : null;
        }
    }
}
