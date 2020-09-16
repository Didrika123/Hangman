using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            HangmanConsole hangman = new HangmanConsole();

            hangman.Init();
            do
            {
                hangman.Update();
                hangman.Draw();
            } while (hangman.IsRunning());
        }

    }
}
