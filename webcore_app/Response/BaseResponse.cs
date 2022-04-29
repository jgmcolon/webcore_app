using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webcore_app.Response
{
    public class BaseResponse
    {

        [JsonProperty("error")]
        public bool Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public BaseResponse()
        {
            this.Error = false;
            this.Message = "";
        }

        public BaseResponse(bool Error, string Message)
        {
            this.Error = Error;
            this.Message = Message;
        }
    }
}
