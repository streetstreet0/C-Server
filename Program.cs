

namespace CSharpServer
{
    public class MainServer
    {
        static void Main(string[] args)
        {
            int port = 8081;
            Console.WriteLine("Starting the server...!");
            Console.WriteLine("Server running on port " + port);
            HTTPSserver server = new HTTPSserver(port);
            server.Start();
        }
    }
}



