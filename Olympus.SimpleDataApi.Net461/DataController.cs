namespace Olympus.SimpleDataApi
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using Fanex.Data.Repository;
    using Fanex.NAP.Core.Configuration;
    using Fanex.NAP.Core.Context;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Web.Session;

    [RoutePrefix("state-api/simple-data")]
    public class DataController : ApiController
    {
        private readonly SimpleCriteriaFactory simpleCriteriaFactory;

        private readonly SimpleFunctionPermissionFactory simpleFunctionPermissionFactory;

        private readonly SimpleMapperFactory simpleMapperFactory;

        private readonly CurrentSession currentSession;

        private readonly CurrentContext currentContext;

        private readonly UserService userService;

        public DataController(SimpleCriteriaFactory simpleCriteriaFactory, SimpleFunctionPermissionFactory simpleFunctionPermissionFactory, SimpleMapperFactory simpleMapperFactory)
        {
            this.simpleCriteriaFactory = simpleCriteriaFactory;
            this.simpleFunctionPermissionFactory = simpleFunctionPermissionFactory;
            this.simpleMapperFactory = simpleMapperFactory;
            currentSession = new CurrentSession();
            currentContext = new CurrentContext();
            userService = new UserService();
        }

        [Route("query")]
        public IHttpActionResult Query(Dictionary<string, object> queryModel)
        {
            queryModel = queryModel ?? new Dictionary<string, object>();
            queryModel.SafeAdd(ParameterConfiguration.ActorId, currentSession.UserId)
                .SafeAdd(ParameterConfiguration.ActorUsername, currentSession.Username)
                .SafeAdd("IP", currentContext.IP);

            queryModel = queryModel.CloneCaseInSensitive();

            var apiKey = queryModel.ContainsKey("apikey") && queryModel["apikey"] != null ? queryModel["apikey"].ToString() : "";
            if (string.IsNullOrEmpty(apiKey))
            {
                return Content(HttpStatusCode.NotFound, "apikey is empty");
            }

            var functionPermission = simpleFunctionPermissionFactory.GetFunctionPermission(apiKey);
            if (functionPermission != null)
            {
                if (currentSession.UserId == 0)
                {
                    return Content(HttpStatusCode.Unauthorized, "You are not authorized to access this data.");
                }

                var currentPolicy = userService.GetPermission(currentSession.UserId, NapSettings.Settings.SiteId, functionPermission.Key);
                var hasPermission = currentPolicy >= (int)functionPermission.Policy;
                if (!hasPermission)
                {
                    return Content(HttpStatusCode.Unauthorized, "You are not authorized to access this data.");
                }
            }

            var criteriaType = simpleCriteriaFactory.GetCriteriaType(apiKey);
            if (criteriaType == null)
            {
                return Content(HttpStatusCode.NotFound, "Data api is not found. Please check the method GetCriteriaType");
            }

            var criteria = simpleCriteriaFactory.CreateCriteria(criteriaType, queryModel);
            if (criteria == null)
            {
                return Content(HttpStatusCode.NotFound, "Data api is not found. Please check the method CreateCriteria");
            }

            if (!criteria.IsValid())
            {
                return Content(HttpStatusCode.BadRequest, "Bad request. Invalid request message parameters");
            }

            var repository = new DynamicRepository();
            var data = repository.Fetch<dynamic>(criteria).ToList();

            var mapperService = simpleMapperFactory.GetSimpleMapperService(apiKey);
            if (mapperService != null)
            {
                data.ForEach(d => mapperService.Decorate(d));
            }

            return Ok(data);
        }

        [Route("update")]
        public IHttpActionResult Update(Dictionary<string, object> queryModel)
        {
            queryModel = queryModel ?? new Dictionary<string, object>();
            queryModel.SafeAdd(ParameterConfiguration.ActorId, currentSession.UserId)
                .SafeAdd(ParameterConfiguration.ActorUsername, currentSession.Username)
                .SafeAdd("IP", currentContext.IP);

            queryModel = queryModel.CloneCaseInSensitive();

            var apiKey = queryModel.ContainsKey("apikey") && queryModel["apikey"] != null ? queryModel["apikey"].ToString() : "";
            if (string.IsNullOrEmpty(apiKey))
            {
                return Content(HttpStatusCode.NotFound, "apikey is empty");
            }

            var functionPermission = simpleFunctionPermissionFactory.GetFunctionPermission(apiKey);
            if (functionPermission != null)
            {
                if (currentSession.UserId == 0)
                {
                    return Content(HttpStatusCode.Unauthorized, "You are not authorized to access this data.");
                }

                var currentPolicy = userService.GetPermission(currentSession.UserId, NapSettings.Settings.SiteId, functionPermission.Key);
                var hasPermission = currentPolicy >= (int)functionPermission.Policy;
                if (!hasPermission)
                {
                    return Content(HttpStatusCode.Unauthorized, "You are not authorized to access this data.");
                }
            }

            var commandType = simpleCriteriaFactory.GetCommandType(apiKey);
            if (commandType == null)
            {
                return Content(HttpStatusCode.NotFound, "Data api is not found. Please check the method GetCriteriaType");
            }

            var command = simpleCriteriaFactory.CreateNonQueryCommand(commandType, queryModel);
            if (command == null)
            {
                return Content(HttpStatusCode.NotFound, "Data api is not found. Please check the method CreateCriteria");
            }

            if (!command.IsValid())
            {
                return Content(HttpStatusCode.BadRequest, "Bad request. Invalid request message parameters");
            }

            var repository = new DynamicRepository();
            repository.Execute(command);
            return Ok(command);

        }
    }
}
