using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpServer
{
    public class Request
    {
        private String Type { get; set; }
        private String Url { get; set; }
        private String Host { get; set; }


        private Request(String type, String url, String host)
        {
            Type = type;
            Url = url;
            Host = host;
        }

        public static Request GetRequest(String request)
        {
            if (String.IsNullOrEmpty(request))
            {
                return null;
            }

            String[] tokens = request.Split(' ');
            return new Request("", "", "");
        }
    }
}
