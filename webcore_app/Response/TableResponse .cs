using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webcore_app.Response
{
    public class TableResponse : BaseResponse
    {
        public int total { get; set; }

        public int page { get; set; }

        public int perPage { get; set; }

        public object data { get; set; }
    }


}
