using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Thorium.Core.Model;
using Thorium.Core.Model.Abstractions;
using Thorium.Mvc.Models;

namespace MyBucks.Mvc.Tools
{
    public abstract class ApiController : ControllerBase, IActionFilter
    {
        private readonly IServiceBase[] _services;
        
        protected ApiController(params IServiceBase[] services)
        {
            this._services = services;
        }

        private string _currentUserId;
        protected virtual string CurrentUserId
        {
            get => _currentUserId;
            set
            {
                _currentUserId = value;
                foreach (var service in this._services)
                {
                    service.CurrentUserId = _currentUserId;
                }
            }
        }

        private string _currentContext;
        protected virtual string CurrentContext
        {
            get => _currentContext;
            set
            {
                _currentContext = value;
                foreach (var service in this._services)
                {
                    service.CurrentContext = _currentContext;
                }
            }
        }
        
        private int _currentTimeZoneOffset;
        protected virtual int CurrentTimeZoneOffset
        {
            get => _currentTimeZoneOffset;
            set
            {
                _currentTimeZoneOffset = value;
                foreach (var service in this._services)
                {
                    service.CurrentTimeZoneOffset = _currentTimeZoneOffset;
                }
            }
        }
        
        protected BadRequestObjectResult BadRequest(string message)
        {
            return BadRequest(new ApiResponse(message));
        }

        protected ObjectResult InternalServerError(string message)
        {
            var result = new ObjectResult(new ApiResponse(message)) {StatusCode = 500};
            return result;
        }
        
        protected ObjectResult NotFound(string message)
        {
            var result = new ObjectResult(new ApiResponse(message)) {StatusCode = 404};
            return result;
        }

        protected StatusCodeResult Unauthorized(string message)
        {
            return new UnauthorizedResult();
        }
        
       

        // todo: more methods

        protected IActionResult FromReply(ReplyBase reply)
        {
            switch (reply.ReplyStatus)
            {
                case ReplyStatus.Failed:
                    return InternalServerError(reply.ReplyMessage);
                case ReplyStatus.NotFound:
                    return NotFound(reply.ReplyMessage);
                case ReplyStatus.Unauthorized:
                    return Unauthorized(reply.ReplyMessage);
                case ReplyStatus.InvalidInput:
                    return BadRequest(reply.ReplyMessage);
                case ReplyStatus.Successful:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (reply is IPaginatedReply paginatedReply)
            {
//                var d1 = typeof(PaginatedResponse<>);
//                Type[] typeArgs = { ((ListReply)paginatedReply).ResultList.GetType().GetElementType() };
//                var makeme = d1.MakeGenericType(typeArgs);
//                object o = Activator.CreateInstance(makeme);
                
                
                return Ok(new PaginatedResponse
                {
                    Items = ((ListReply)paginatedReply).ResultList,
                    TotalItems = paginatedReply.TotalItems,
                    TotalPages = paginatedReply.TotalPages,
                });
                
            }

            switch (reply)
            {
                case IdReply idReply:
                    return Ok(new CreatedResponse<long?> { Id = idReply.RefId });
                case SingleValueReply singleValueReply:
                    return Ok(singleValueReply.Value);
                case ListReply listReply:
                    return Ok(listReply.ResultList);
            }
            return Ok();
        }

        protected IActionResult FromReply<TData>(ReplyBase reply, TData data)
        {
            switch (reply.ReplyStatus)
            {
                case ReplyStatus.Failed:
                    return InternalServerError(reply.ReplyMessage);
                case ReplyStatus.NotFound:
                    return NotFound(reply.ReplyMessage);
                case ReplyStatus.Unauthorized:
                    return Unauthorized(reply.ReplyMessage);
                case ReplyStatus.InvalidInput:
                    return BadRequest(reply.ReplyMessage);
                case ReplyStatus.Successful:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Ok(data);

        }

        [NonAction]
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("MyBucks-Context", out StringValues contextStrings))
            {
                CurrentContext = string.IsNullOrWhiteSpace(contextStrings.FirstOrDefault()) ? null : contextStrings.FirstOrDefault();
            }
            else
            {
                CurrentContext = null;
            }
            
            if (context.HttpContext.Request.Headers.TryGetValue("MyBucks-UserId", out StringValues userIds))
            {
                CurrentUserId = string.IsNullOrWhiteSpace(userIds.FirstOrDefault()) ? null : userIds.FirstOrDefault();
            }
            else
            {
                CurrentUserId = null;
            } 
            if (context.HttpContext.Request.Headers.TryGetValue("MyBucks-TimeZone", out StringValues timeZones))
            {
                var tz = timeZones.FirstOrDefault();

                int.TryParse(tz, out int timezoneOffset);
                
                CurrentTimeZoneOffset = timezoneOffset;
            }
            else
            {
                CurrentTimeZoneOffset = 0;
            }

            OnActionExecutingCustom(context);
        }

        protected virtual void OnActionExecutingCustom(ActionExecutingContext context)
        {
            
        }

        [NonAction]
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
    }
}