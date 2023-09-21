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

    protected static readonly JsonSerializerOptions JsonSerializerOptions = new();

    static SimpleCriteriaFactoryBase()
    {
        JsonSerializerOptions.PropertyNameCaseInsensitive =true;
        JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverterUsingDateTimeParse());
        JsonSerializerOptions.Converters.Add(new DateTimeConverterUsingDateTimeParse());
        JsonSerializerOptions.Converters.Add(new BooleanConverter());
    }

    public virtual ICriteria CreateCriteria(Type criteriaType, Dictionary<string, object> queryModel)
    {
        var json = JsonSerializer.Serialize(queryModel);
        try
        {
            var criteria = JsonSerializer.Deserialize(json, criteriaType, JsonSerializerOptions);
            return (ICriteria)criteria;
        }
        catch (Exception ex)
        {
            throw new Exception($"JSON:`{json}`", ex);
        }

    }

    public virtual INonQueryCommand CreateNonQueryCommand(Type commandType, Dictionary<string, object> queryModel)
    {
        var json = JsonSerializer.Serialize(queryModel);
        try
        {
            var nonQueryCommand = JsonSerializer.Deserialize(json, commandType, JsonSerializerOptions);
            return (INonQueryCommand)nonQueryCommand;
        }
        catch (Exception ex)
        {
            throw new Exception($"JSON:`{json}`", ex);
        }
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
