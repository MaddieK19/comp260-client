using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Client
{
    class Game
    {
        TCPClient client;
        Timer timer;

        float serverUpdateInterval = 200;
 
// Implement a call with the right signature for events going off
private void myEvent(object source, ElapsedEventArgs e) { }

        public Game()
        {
            client = new TCPClient();
            client.initServerConnection();
            initTimer();
        }

        void initTimer()
        {
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(myEvent);
            timer.Interval = serverUpdateInterval;
            timer.Enabled = true;
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
