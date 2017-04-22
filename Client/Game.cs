using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
/*!
 * Game class 
 * Creates and instance of TCPClient and runs game
 */

namespace Client
{
    class Game
    {
        //! TCPClient to connect to server
        TCPClient client;
        //! Timer to determine when to send data to server
        Timer timer;
        //! int for how many times the timers ticked
        int ticks = 0;
        //! int for how many ticks had passed on the previous update
        int previousTick = 0;
        //! float for how often to send to server
        float serverUpdateInterval = 1000;
        //! String to store the players last command
        String inputText;
        //! bool for whether the main loop should be running
        bool gameRunning = true;
        //! String for the players name
        String playerName = null;
          
 
        //! Constructor
        public Game()
        {
            client = new TCPClient();
            client.initServerConnection();
        }

        //! intialises the timer
        void initTimer()
        {
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(tick);
            timer.Interval = serverUpdateInterval;
            timer.Enabled = true;
        }

        //! gets the players name and stores it in playerName
        void getPlayerName()
        {
            while(playerName == null)
                playerName = Console.ReadLine();
        }

        //! Runs the game
        public void run()
        {
            client.readFromStream();
            getPlayerName();
            client.writeToStream(playerName);
            client.readFromStream();
            Console.WriteLine("Press enter to continue...");
            
            initTimer();

            while (gameRunning)
            {
                client.readFromStream();
                if (ticks > previousTick)
                {
                    inputCommand();
                    previousTick = ticks;
                }
            }
        }

        //! Increases ticks
        private void tick(object source, ElapsedEventArgs e) {
            ticks++;
        }

        //! Takes player input and adds it to commands
        void inputCommand()
        {
            Console.Write("Enter Text: ");
            inputText = Console.ReadLine();
            
            if (inputText == "quit")
            {
                exitProgram();
            }
            if (inputText != null)
            {
                client.writeToStream(inputText);
            }
            inputText = null;

        }
        //! Closes the connection with the server, the tcpclient and the application
        void exitProgram()
        {
            gameRunning = false;
            client.writeToStream(inputText);
            client.readFromStream();
            client.disconnectFromServer();
            Environment.Exit(0);
        }
    }
}
