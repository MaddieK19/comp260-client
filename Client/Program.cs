using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;



namespace Client
{
    class Program
    {
        static Game game;

        static void Main(string[] args)
        {
            game = new Game();
            game.run();
        }  
    }
}
