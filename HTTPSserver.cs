using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CSharpServer
{
    public class HTTPSserver
    {
        public const String VERSION = "HTTP/1.1";
        public const String NAME = "TESTING123-Server";
        public const String MESSAGEDIRECTORY = "/html/";
        private int portNum;
        private bool running;
        private TcpListener listener;

        /**
         * Constructor for HTTPSserver.
         * 
         * @param port The port number the HTTPSserver should run on.
         */
        public HTTPSserver(int port)
        {
            this.portNum = port;
            this.running = false;
            this.listener = new TcpListener(IPAddress.Any, portNum);
        }

        /**
         * Starts running the server
         */
        public void Start()
        {
            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
        }

        /**
         * Run the server thread
         */
        private void Run()
        {
            running = true;
            listener.Start();

            while (running)
            {
                Console.WriteLine("Waiting for connection...");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected");

                HandleClient(client);

                client.Close();
            }

            listener.Stop();
            running = false;
        }

        /**
         * Handle requests from the client, and send out the correct response.
         * 
         * @param client The client we are listening to.
         */
        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());

            String message = "";
            while (reader.Peek() != -1)
            {
                message += reader.ReadLine() + "\n";
            }

            Debug.WriteLine("Request: \n " + message);

            Request request = Request.GetRequest(message);
            Response response = Response.RespondTo(request);

            response.Post(client.GetStream());
        }
    }
}
