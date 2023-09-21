namespace Olympus.SimpleDataApi
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;
    using Fanex.Data.Repository;

    public interface SimpleCriteriaFactory
    {
        Type GetCriteriaType(string apiKey);

        Type GetCommandType(string apiKey);

        ICriteria CreateCriteria(Type criteriaType, Dictionary<string, object> queryModel);

        INonQueryCommand CreateNonQueryCommand(Type commandType, Dictionary<string, object> queryModel);
    }

    public abstract class SimpleCriteriaFactoryBase : SimpleCriteriaFactory
    {
        protected readonly JavaScriptSerializer serializer = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };

        protected static readonly Dictionary<string, Type> CriteriaTypes = new Dictionary<string, Type>();

        protected static readonly Dictionary<string, Type> CommandTypes = new Dictionary<string, Type>();

        public virtual ICriteria CreateCriteria(Type criteriaType, Dictionary<string, object> queryModel)
        {
            var criteria = serializer.Deserialize(serializer.Serialize(queryModel), criteriaType);

            return (ICriteria)criteria;
        }

        public virtual INonQueryCommand CreateNonQueryCommand(Type commandType, Dictionary<string, object> queryModel)
        {
            var nonQueryCommand =  serializer.Deserialize(serializer.Serialize(queryModel), commandType);
            return (INonQueryCommand)nonQueryCommand;
        }

        public virtual Type GetCriteriaType(string apiKey)
        {
            return CriteriaTypes.TryGetValue(apiKey, out var type) ? type : null;
        }

        public virtual Type GetCommandType(string apiKey)
        {
            return CommandTypes.TryGetValue(apiKey, out var type) ? type : null;
        }

    }
}
