//using Microsoft.AspNetCore.Mvc;
//using MyBucks.Core.Model;

namespace MyBucks.Mvc.Tools
{
    public class ApiController //: ControllerBase
    {

//        public BadRequestObjectResult BadRequest(string message)
//        {
//            return BadRequest(new ApiResponse(message));
//        }
//
//        public ObjectResult InternalServerError(string message)
//        {
//            return new ObjectResult(new ApiResponse(message));
//        }
//
//        public ObjectResult Unauthorized(string message)
//        {
//            return new ObjectResult(new ApiResponse(message));
//        }
//
//        // todo: more methods
//
//        public IActionResult FromReply(ReplyBase reply)
//        {
//            if (reply.ReplyStatus == ReplyStatus.Failed)
//            {
//                return InternalServerError(reply.ReplyMessage);
//            }
//
//            if (reply.ReplyStatus == ReplyStatus.NotFound)
//            {
//                return NotFound(reply.ReplyMessage);
//            }
//
//            return Ok();
//        }
//
//        public IActionResult FromReply<TData>(ReplyBase reply, TData data)
//        {
//            if (reply.ReplyStatus == ReplyStatus.Failed)
//            {
//                return InternalServerError(reply.ReplyMessage);
//            }
//
//            if (reply.ReplyStatus == ReplyStatus.NotFound)
//            {
//                return NotFound(reply.ReplyMessage);
//            }
//
//            if (reply.ReplyStatus == ReplyStatus.Unauthorized)
//            {
//                return Unauthorized(reply.ReplyMessage);
//            }
//
//            return Ok(data);
//
//        }

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