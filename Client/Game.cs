using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
/*!
 * Game class 
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
        //! List of the commands the player has entered
        List<String> commands;
        //! String to store the players last command
        String inputText;
        //! int for the maximum number of commands to be stored
        int maxCommands = 5;

        bool gameRunning = true;

        String playerName = null;
          
 
        //! Constructor
        public Game()
        {
            client = new TCPClient();
            client.initServerConnection();
            commands = new List<String>();
        }
        //! intialises the timer
        void initTimer()
        {
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(tick);
            timer.Interval = serverUpdateInterval;
            timer.Enabled = true;
        }

        void getPlayerName()
        {
            playerName = Console.ReadLine();
        }

        //!
        public void run()
        {
            client.readFromStream();
            getPlayerName();
            client.writeToStream(playerName);
            Console.WriteLine("Press enter to continue...");
            Console.WriteLine("");
            initTimer();

            while (gameRunning)
            {
                client.readFromStream();
                if (ticks > previousTick)
                {
                    inputCommand();
                    //update();
                    previousTick = ticks;
                }
                
            }
        }

        //! Increases ticks
        private void tick(object source, ElapsedEventArgs e) {
            ticks++;
        }

        void update()
        {
            client.readFromStream();
            sendCommands();
            inputCommand();
        }

        void sendCommands()
        {
            for (int i = 0; i < commands.Count; i++)
            {
                client.writeToStream(commands[i]);
                Console.WriteLine(commands[i]);
            }
            commands.Clear();
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
            if (commands.Count < maxCommands && inputText != "")
            {
                commands.Add(inputText);
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
