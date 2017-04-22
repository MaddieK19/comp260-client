using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

/*!
 * Program class that creates and runs instance of Game class
 */

namespace Client
{
    class Program
    {
        //! Game class to run game
        static Game game;

        static void Main(string[] args)
        {
            game = new Game();
            game.run();
        }  
    }
}
