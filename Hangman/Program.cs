using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            IHangman hangman = new HangmanConsole();
            hangman.init();
            do
            {
                hangman.Update();
                hangman.Draw();
            } while (hangman.IsRunning());
        }

    }
}
