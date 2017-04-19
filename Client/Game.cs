﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Client
{
    class Game
    {
        //! TCPClient to connect to server
        TCPClient client;
        //! Timer to determine when to send data to server
        Timer timer;

        int ticks = 0;
        int previousTick = 0;
        //! float for how often to send to server
        float serverUpdateInterval = 200;
        //! List of the commands the player has entered
        List<String> commands;
        //! String to store the players last command
        String inputText;
        //! int for the maximum number of commands to be stored
        int maxCommands = 5;

        bool gameRunning = true;
          
 
        //! Constructor
        public Game()
        {
            client = new TCPClient();
            client.initServerConnection();
            commands = new List<String>();
            initTimer();
        }
        //! intialises the timer
        void initTimer()
        {
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(tick);
            timer.Interval = serverUpdateInterval;
            timer.Enabled = true;
        }
        //!
        public void run()
        {
            while (gameRunning)
            {
                if (ticks > previousTick)
                {
                    update();
                    previousTick++;
                }
                //inputCommand();
            }
        }

        //! sends every entry in commands to the server
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
            inputText = Console.ReadLine();
            if (inputText == "quit")
            {
                exitProgram();
            }
            if (commands.Count < maxCommands && inputText != "")
            {
                commands.Add(inputText);
            }
           
        }

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
