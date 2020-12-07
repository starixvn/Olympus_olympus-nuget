using System.Collections.Generic;
using System.Linq;
using System.Net;
using Fanex.Data.Repository;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Web;
using Fanex.NAP.Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace Olympus.SimpleDataApi
{
    [Route("state-api/simple-data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly SimpleCriteriaFactory simpleCriteriaFactory;

        private readonly SimpleFunctionPermissionFactory simpleFunctionPermissionFactory;

        private readonly SimpleMapperFactory simpleMapperFactory;

        private readonly INapSession napSession;

        private readonly INapSettings napSettings;

        private readonly IUserService userService;

        public DataController(
            INapSession napSession,
            INapSettings napSettings,
            IUserService userService,
            SimpleCriteriaFactory simpleCriteriaFactory, 
            SimpleFunctionPermissionFactory simpleFunctionPermissionFactory, 
            SimpleMapperFactory simpleMapperFactory)
        {
            this.simpleCriteriaFactory = simpleCriteriaFactory;
            this.simpleFunctionPermissionFactory = simpleFunctionPermissionFactory;
            this.simpleMapperFactory = simpleMapperFactory;
            this.napSession = napSession;
            this.napSettings = napSettings;
            this.userService = userService;
        }

        [Route("query")]
        public IActionResult  Query(Dictionary<string, object> queryModel)
        {
            queryModel = queryModel ?? new Dictionary<string, object>();
            queryModel.SafeAdd(ParameterConfiguration.ActorId, napSession.UserId)
                .SafeAdd(ParameterConfiguration.ActorUsername, napSession.UserName)
                .SafeAdd("IP", GetClientIP());

            queryModel = queryModel.CloneCaseInSensitive();

            var apiKey = queryModel.ContainsKey("apikey") && queryModel["apikey"] != null ? queryModel["apikey"].ToString() : "";
            if (string.IsNullOrEmpty(apiKey))
            {
                return Content(HttpStatusCode.NotFound.ToString(), "apikey is empty");
            }

            var functionPermission = simpleFunctionPermissionFactory.GetFunctionPermission(apiKey);
            if (functionPermission != null)
            {
                if (napSession.UserId == 0)
                {
                    return Content(HttpStatusCode.Unauthorized.ToString(), "You are not authorized to access this data.");
                }

                var currentPolicy = userService.GetPermission(napSession.UserId, napSettings.SiteId, functionPermission.Key);
                var hasPermission = currentPolicy >= (int)functionPermission.Policy;
                if (!hasPermission)
                {
                    return Content(HttpStatusCode.Unauthorized.ToString(), "You are not authorized to access this data.");
                }
            }

            var criteriaType = simpleCriteriaFactory.GetCriteriaType(apiKey);
            if (criteriaType == null)
            {
                return Content(HttpStatusCode.NotFound.ToString(), "Data api is not found. Please check the method GetCriteriaType");
            }

            var criteria = simpleCriteriaFactory.CreateCriteria(criteriaType, queryModel);
            if (criteria == null)
            {
                return Content(HttpStatusCode.NotFound.ToString(), "Data api is not found. Please check the method CreateCriteria");
            }

            if (!criteria.IsValid())
            {
                return Content(HttpStatusCode.BadRequest.ToString(), "Bad request. Invalid request message parameters");
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
        public IActionResult  Update(Dictionary<string, object> queryModel)
        {
            queryModel = queryModel ?? new Dictionary<string, object>();
            queryModel.SafeAdd(ParameterConfiguration.ActorId, napSession.UserId)
                .SafeAdd(ParameterConfiguration.ActorUsername, napSession.UserName)
                .SafeAdd("IP", GetClientIP());

            queryModel = queryModel.CloneCaseInSensitive();

            var apiKey = queryModel.ContainsKey("apikey") && queryModel["apikey"] != null ? queryModel["apikey"].ToString() : "";
            if (string.IsNullOrEmpty(apiKey))
            {
                return Content(HttpStatusCode.NotFound.ToString(), "apikey is empty");
            }

            var functionPermission = simpleFunctionPermissionFactory.GetFunctionPermission(apiKey);
            if (functionPermission != null)
            {
                if (napSession.UserId == 0)
                {
                    return Content(HttpStatusCode.Unauthorized.ToString(), "You are not authorized to access this data.");
                }

                var currentPolicy = userService.GetPermission(napSession.UserId, napSettings.SiteId, functionPermission.Key);
                var hasPermission = currentPolicy >= (int)functionPermission.Policy;
                if (!hasPermission)
                {
                    return Content(HttpStatusCode.Unauthorized.ToString(), "You are not authorized to access this data.");
                }
            }

            var commandType = simpleCriteriaFactory.GetCommandType(apiKey);
            if (commandType == null)
            {
                return Content(HttpStatusCode.NotFound.ToString(), "Data api is not found. Please check the method GetCriteriaType");
            }

            var command = simpleCriteriaFactory.CreateNonQueryCommand(commandType, queryModel);
            if (command == null)
            {
                return Content(HttpStatusCode.NotFound.ToString(), "Data api is not found. Please check the method CreateCriteria");
            }

            if (!command.IsValid())
            {
                return Content(HttpStatusCode.BadRequest.ToString(), "Bad request. Invalid request message parameters");
            }

            var repository = new DynamicRepository();
            repository.Execute(command);
            return Ok(command);
        }

        private string GetClientIP()
        {
            var clientIP = HttpContext.Request?.Headers["x-forwarded-for"].ToString();
            if (string.IsNullOrWhiteSpace(clientIP))
            {
                clientIP = HttpContext?.Connection?.RemoteIpAddress?.ToString();
            }

            return clientIP;
        }
    }
}
