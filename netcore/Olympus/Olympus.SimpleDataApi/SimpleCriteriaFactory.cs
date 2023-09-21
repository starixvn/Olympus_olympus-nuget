using System;
using System.Collections.Generic;
using Fanex.Data.Repository;
using Newtonsoft.Json;

namespace Olympus.SimpleDataApi
{
    public interface SimpleCriteriaFactory
    {
        Type GetCriteriaType(string apiKey);

        Type GetCommandType(string apiKey);

        ICriteria CreateCriteria(Type criteriaType, Dictionary<string, object> queryModel);

        INonQueryCommand CreateNonQueryCommand(Type commandType, Dictionary<string, object> queryModel);
    }

    public abstract class SimpleCriteriaFactoryBase : SimpleCriteriaFactory
    {
        protected static readonly Dictionary<string, Type> CriteriaTypes = new Dictionary<string, Type>();

        protected static readonly Dictionary<string, Type> CommandTypes = new Dictionary<string, Type>();

        public virtual ICriteria CreateCriteria(Type criteriaType, Dictionary<string, object> queryModel)
        {
            var criteria = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryModel), criteriaType);

            return (ICriteria)criteria;
        }

        public virtual INonQueryCommand CreateNonQueryCommand(Type commandType, Dictionary<string, object> queryModel)
        {
            var nonQueryCommand = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryModel), commandType);

            return (INonQueryCommand)nonQueryCommand;
        }

        public virtual Type GetCriteriaType(string apiKey)
        {
            return CriteriaTypes.ContainsKey(apiKey) ? CriteriaTypes[apiKey] : null;
        }

        public virtual Type GetCommandType(string apiKey)
        {
            return CommandTypes.ContainsKey(apiKey) ? CommandTypes[apiKey] : null;
        }

    }
}
