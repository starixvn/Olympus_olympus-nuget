using System.Text.Json;
using Olympus.SimpleDataApi;

namespace SimpleDataApi.UnitTests
{
    public class BooleanCriteria
    {
        public bool IsDay { get; set; }

        public string Username { get; set; }
    }
    

    public class JsonSerializerUnitTests
    {
        protected static readonly JsonSerializerOptions JsonSerializerOptions = new();
        static JsonSerializerUnitTests()
        {
            JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverterUsingDateTimeParse());
            JsonSerializerOptions.Converters.Add(new DateTimeConverterUsingDateTimeParse());
            JsonSerializerOptions.Converters.Add(new BooleanConverter());
        }

        [Fact]
        public void ConvertBooleanFalseTests()
        {
            var json =
                """{"apikey":"pre-match-assignment-get-last-updated","isday":false,"actorid":8,"username":"sa","ip":"10.0.0.5"}""";

           var criteria = JsonSerializer.Deserialize(json, typeof(BooleanCriteria), JsonSerializerOptions);

           Assert.NotNull(criteria);
        }

        [Fact]
        public void ConvertBooleanAsStringTests()
        {
            var json =
                """{"apikey":"pre-match-assignment-get-last-updated","isday":"true","actorid":8,"username":"sa","ip":"10.0.0.5"}""";

            var criteria = JsonSerializer.Deserialize(json, typeof(BooleanCriteria), JsonSerializerOptions);

            Assert.NotNull(criteria);
        }

        [Fact]
        public void ConvertBooleanAsIntTests()
        {
            var json =
                """{"apikey":"pre-match-assignment-get-last-updated","isday":1,"actorid":8,"username":"sa","ip":"10.0.0.5"}""";

            var criteria = JsonSerializer.Deserialize(json, typeof(BooleanCriteria), JsonSerializerOptions);

            Assert.NotNull(criteria);
        }
    }
}
