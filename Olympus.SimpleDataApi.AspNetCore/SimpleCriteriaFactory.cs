namespace Olympus.SimpleDataApi;

using System;
using System.Collections.Generic;
using System.Text.Json;
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
    protected static readonly Dictionary<string, Type> CriteriaTypes = new();

    protected static readonly Dictionary<string, Type> CommandTypes = new();

    public virtual ICriteria CreateCriteria(Type criteriaType, Dictionary<string, object> queryModel)
    {
        var json = JsonSerializer.Serialize(queryModel);
        var criteria = JsonSerializer.Deserialize(json, criteriaType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return (ICriteria)criteria;
    }

    public virtual INonQueryCommand CreateNonQueryCommand(Type commandType, Dictionary<string, object> queryModel)
    {
        var json = JsonSerializer.Serialize(queryModel);
        var nonQueryCommand = JsonSerializer.Deserialize(json, commandType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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