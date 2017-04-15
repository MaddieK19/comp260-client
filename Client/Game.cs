using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Game
    {
        TCPClient client;
        
        public Game()
        {
            client = new TCPClient();
            client.initServerConnection();
        }

        public void run()
        {
            client.readFromStream();
            String input = Console.ReadLine();
            client.writeToStream(input);
            client.readFromStream();
            client.readFromStream();
            Console.ReadLine();
        }
    }
}
