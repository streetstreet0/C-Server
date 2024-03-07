using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /**
         * Private constructor to create a request object.
         * 
         * @param type The type of request.
         * @param url The url of the server.
         * @param host The host of the server.
         */
        private Request(String type, String url, String host)
        {
            this.Type = type;
            this.Url = url;
            this.Host = host;
            TestRequest();
        }

        /**
         * Display the variables of the request to the debug console.
         * 
         */
        private void TestRequest()
        {
            Debug.WriteLine("Type = " + Type);
            Debug.WriteLine("url = " + Url);
            Debug.WriteLine("host = " + Host);
        }

        /**
         * Generate a request object from a string request.
         * 
         * @param request The String request.
         * @return The generated request object.
         */
        public static Request GetRequest(String request)
        {
            if (String.IsNullOrEmpty(request))
            {
                return null;
            }

            String[] tokens = request.Split(' ');
            String type = tokens[0];
            String url = tokens[1];
            String host = tokens[4];
            return new Request(type, url, host);
        }
    }
}
