using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace CSharpServer
{
    public class Response
    {
        private Byte[] ByteData;
        private String Status;
        private String InputDataType;

        /**
         * Private constructor to create a Response object
         * 
         * @param byteData The data to be sent in the response.
         * @param status The current status of the server.
         * @param inputDataType The type of data send in the request.
          */
        private Response(String status, String inputDataType, Byte[] byteData)
        {
            this.ByteData = byteData;
            this.Status = status;
            this.InputDataType = inputDataType;
        }

        /**
         * Create a new Response for a given request
         * 
         * @param request The request to respond to
         */
        public static Response RespondTo(Request request)
        {
            if (request == null)
            {
                return CreateNullRequest();
            }

            return null;
        }

        /**
         * Create a Response for a null request
         * 
         */
        public static Response CreateNullRequest()
        {
            return new Response("400 Bad Request", "text/html", new byte[0]);
        }

        /**
         * Post data back to the client.
         * 
         */
        public void Post(NetworkStream outputStream)
        {
            StreamWriter writer = new StreamWriter(outputStream);

            writer.WriteLine(HTTPSserver.VERSION + " " + Status);
            writer.WriteLine("Server: " + HTTPSserver.NAME);
            writer.WriteLine("Content-Type: " + InputDataType);
            writer.WriteLine("Accept-Ranges: bytes");
            writer.WriteLine("Content-Length: " + ByteData.Length);

            outputStream.Write(ByteData, 0, ByteData.Length);
        }
    }
}
