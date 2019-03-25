using System;
using System.Collections.Generic;
using Thorium.Core.Model;
using Thorium.Mvc.Tools;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

//            var apiController = new ApiController();
//
//            var lst = new ListReply<SelectItem<int>>
//            {
//                ResultList = new List<SelectItem<int>>
//                {
//                    new SelectItem<int>
//                    {
//                        Description = "asd",
//                        Id = 1
//                    },
//                    new SelectItem<int>
//                    {
//                        Description = "cxvxcv",
//                        Id = 2
//                    },
//                },
//                ReplyStatus = ReplyStatus.Successful
//            };

          //  var result = apiController.FromReply(lst);
            return;
        }
        
        public class SelectItem<T>
        {
            public string Description { get; set; }

            public T Id { get; set; }
        }
    }
}