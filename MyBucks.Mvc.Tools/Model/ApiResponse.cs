﻿namespace MyBucks.Mvc.Tools.Model
{
    public class ApiResponse
    {
        public ApiResponse(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}