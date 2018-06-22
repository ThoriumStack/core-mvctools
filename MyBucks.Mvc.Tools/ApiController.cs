using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MyBucks.Core.Model;
using MyBucks.Mvc.Tools.Model;

namespace MyBucks.Mvc.Tools
{
    public class ApiController : ControllerBase
    {

        public BadRequestObjectResult BadRequest(string message)
        {
            return BadRequest(new ApiResponse(message));
        }

        public ObjectResult InternalServerError(string message)
        {
            var result = new ObjectResult(new ApiResponse(message)) {StatusCode = 500};
            return result;
        }

        public StatusCodeResult Unauthorized(string message)
        {
            return new UnauthorizedResult();
        }

        // todo: more methods

        public IActionResult FromReply(ReplyBase reply)
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

        public IActionResult FromReply<TData>(ReplyBase reply, TData data)
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
       

    }
}