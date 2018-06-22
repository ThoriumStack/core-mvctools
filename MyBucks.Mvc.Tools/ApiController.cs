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
            var result=  new ObjectResult(new ApiResponse(message));
            result.StatusCode = 500;
            return result;
        }

        public StatusCodeResult Unauthorized(string message)
        {
            return new UnauthorizedResult();
        }

        // todo: more methods

        public IActionResult FromReply(ReplyBase reply)
        {
            if (reply.ReplyStatus == ReplyStatus.Failed)
            {
                return InternalServerError(reply.ReplyMessage);
            }

            if (reply.ReplyStatus == ReplyStatus.NotFound)
            {
                return NotFound(reply.ReplyMessage);
            }

            if (reply.ReplyStatus == ReplyStatus.Unauthorized)
            {
                return Unauthorized(reply.ReplyMessage);
            }

            if (reply.ReplyStatus == ReplyStatus.InvalidInput)
            {
                return BadRequest(reply.ReplyMessage);
            }

            if (reply is IdReply idReply)
            {
                return Ok(new CreatedResponse<long?> { Id = idReply.RefId });
            }
            return Ok();
        }

        public IActionResult FromReply<TData>(ReplyBase reply, TData data)
        {
            if (reply.ReplyStatus == ReplyStatus.Failed)
            {
                return InternalServerError(reply.ReplyMessage);
            }

            if (reply.ReplyStatus == ReplyStatus.NotFound)
            {
                return NotFound(reply.ReplyMessage);
            }

            if (reply.ReplyStatus == ReplyStatus.Unauthorized)
            {
                return Unauthorized(reply.ReplyMessage);
            }

            if (reply.ReplyStatus == ReplyStatus.InvalidInput)
            {
                return BadRequest(reply.ReplyMessage);
            }
            
            return Ok(data);

        }

    }
    
    public class ApiResponse
    {
        public ApiResponse(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}