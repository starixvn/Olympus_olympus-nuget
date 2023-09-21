namespace Olympus.SimpleDataApi
{
    using System;
    using System.Collections.Generic;

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
            return SimpleMapperServices.TryGetValue(apiKey, out var service) ? Activator.CreateInstance(service) as SimpleMapperService : null;
        }
    }
}
