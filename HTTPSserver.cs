using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CSharpServer
{
    public class HTTPSserver
    {
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

        private void HandleClient(TcpClient client)
        {
            Console.WriteLine("HandleClient hasn't been implemented, lol");
            StreamReader reader = new StreamReader(client.GetStream());

            String message = "";
            while (reader.Peek() != -1)
            {
                message += reader.ReadLine() + "\n";
            }

            Console.WriteLine("Request: \n " + message);
        }
    }
}
