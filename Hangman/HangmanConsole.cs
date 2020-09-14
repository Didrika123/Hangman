using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    class HangmanConsole : IHangman
    {
        private IHangmanLogic _hangmanLogic = new HangmanLogic();
        private bool _isRunning = false;
        public HangmanConsole()
        {
        }
        public void Draw()
        {
        }

        public void init()
        {
            _hangmanLogic.NewGame(10);
            //_hangmanLogic.addWord()
        }

        public bool IsRunning()
        {
            return _isRunning;
        }

        public void Update()
        {
            _isRunning = true;

            /* Console.WriteLine("Hang the Man!");
             Console.WriteLine("To the Gallows!");
             Console.WriteLine("Off with his head!");

             string input = "";
             while (input.ToLower() != "exit")
             {
                 input = Console.ReadLine();
                 hangman.Guess(input);

                 if (hangman.IsGameFinished())
                 {
                     Console.WriteLine("Press <Enter> to start a new game. ");
                     Console.Read();
                     hangman.NewGame();
                 }
             }*/
        }
        //Displays the game visually in the console
        void ShowGame()
        {

        }

        //Displays the rules of the game in the console
        void ShowRules()
        {

        }
    }
}
