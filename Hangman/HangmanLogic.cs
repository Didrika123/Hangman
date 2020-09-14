using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    public class HangmanLogic : IHangmanLogic
    {
        readonly private string[] _words;
        private StringBuilder _word;
         //Make a flipper, so if a word been used, flip it to 1 so its not chosen until all are 1, then flip it to 0
        public void AddWord(string word)
        {
            throw new NotImplementedException();
        }

        public bool ComputerWon()
        {
            throw new NotImplementedException();
        }

        public int GetNumRemainingGuesses()
        {
            throw new NotImplementedException();
        }

        public bool Guess(char guess)
        {
            return false;
        }
        public bool Guess(string guess)
        {
            return false;
        }

        public bool IsGameFinished()
        {
            throw new NotImplementedException();
        }

        public void NewGame(int numGuesses)
        {
            throw new NotImplementedException();
        }

        public bool PlayerWon()
        {
            throw new NotImplementedException();
        }
    }
}
