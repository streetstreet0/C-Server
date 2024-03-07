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

            if (request.RequestType == "GET")
            {
                String fileName = Environment.CurrentDirectory + HTTPSserver.MESSAGEDIRECTORY + request.Url;
                FileInfo file = new FileInfo(fileName);
                if (file.Exists && file.Extension.Contains("."))
                {
                    return CreateResponseFrom(file);
                }
                else
                {
                    return CreateNotFoundRequest();
                }
            }
            else
            {
                return CreateWrongRequest();
            }

            return null;
        }

        /**
         * Create a Response from a file
         * 
         */
        public static Response CreateResponseFrom(FileInfo file)
        {
            FileStream fileStream = file.OpenRead();

            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] outputBytes = new Byte[fileStream.Length];
            reader.Read(outputBytes, 0, outputBytes.Length);

            fileStream.Close();

            return new Response("200 OK", "text/html", outputBytes);
        }

        /**
         * Create a Response for a null request
         * 
         */
        public static Response CreateNullRequest()
        {
            String fileName = Environment.CurrentDirectory + HTTPSserver.MESSAGEDIRECTORY + "400.html";
            FileInfo file = new FileInfo(fileName);
            FileStream fileStream = file.OpenRead();

            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] outputBytes = new Byte[fileStream.Length];
            reader.Read(outputBytes, 0, outputBytes.Length);

            fileStream.Close();

            return new Response("400 Bad Request", "text/html", outputBytes);
        }

        /**
         * Create a Response for a request to a page that doesn't exist
         * 
         */
        public static Response CreateNotFoundRequest()
        {
            String fileName = Environment.CurrentDirectory + HTTPSserver.MESSAGEDIRECTORY + "404.html";
            FileInfo file = new FileInfo(fileName);
            FileStream fileStream = file.OpenRead();

            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] outputBytes = new Byte[fileStream.Length];
            reader.Read(outputBytes, 0, outputBytes.Length);

            fileStream.Close();

            return new Response("404 Page Not Found", "text/html", outputBytes);
        }

        /**
         * Create a Response for a request for a method that isn't allowed
         * 
         */
        public static Response CreateWrongRequest()
        {
            String fileName = Environment.CurrentDirectory + HTTPSserver.MESSAGEDIRECTORY + "405.html";
            FileInfo file = new FileInfo(fileName);
            FileStream fileStream = file.OpenRead();

            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] outputBytes = new Byte[fileStream.Length];
            reader.Read(outputBytes, 0, outputBytes.Length);

            fileStream.Close();

            return new Response("405 Method Not Allowed", "text/html", outputBytes);
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
