using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    public class HangmanLogic : IHangmanLogic
    {
        readonly private List<string> _words = new List<string>();
        private StringBuilder _incorrectGuesses = new StringBuilder();
        private char[] _correctGuesses;
        private string _secretWord;
        private bool _playerWon;
        private int _numRemainingGuesses;

        public void AddWord(string word)
        {
            if (String.IsNullOrEmpty(word))
                throw new ArgumentNullException("Words added to the game cant be empty.");

            word = word.ToLower();

            if (!ContainsOnlyLetters(word) || word.Length < 2 || word.Length > 20)
                throw new FormatException("Words added to the game must be between 2 and 20 letters and must consist exclusively of alphabetical letters.");

            _words.Add(word);
        }
        private bool ContainsOnlyLetters(string word)
        {
            foreach (char ch in word)
            {
                if (ch < 'a' || ch > 'z')
                    return false;
            }
            return true;
        }

        public int GetNumRemainingGuesses()
        {
            return _numRemainingGuesses;
        }

        public string GetSecretWord()
        {
            string str = new string(_correctGuesses);
            str = String.Join(",", str.Split());
            return str;
        }

        public string GetGuesses()
        {
            return String.Join(", ", _incorrectGuesses.ToString().Split("")); 
        }

        public bool Guess(string guess)
        {
            if (String.IsNullOrEmpty(guess))
                throw new ArgumentNullException("You must enter a guess.");

            if (!ContainsOnlyLetters(guess))
                throw new FormatException("You must enter a guess that only contains alphabetical letters.");

            if (guess.Length != _secretWord.Length &&  guess.Length != 1)
                throw new FormatException("You must enter a guess of 1 letter or the same length as the secret word.");
            

            guess = guess.ToLower();

            if (guess.Length > 1)
            {
                if (guess == _secretWord)
                {
                    _correctGuesses = guess.ToCharArray();
                    _playerWon = true;
                    return true;
                }
            }
            else 
            {
                bool exists = false;
                char guessChar = guess[0];
                for (int i = 0; i < _secretWord.Length; i++)
                {
                    if (_secretWord[i] == guessChar)
                    {
                        _correctGuesses[i] = guessChar;
                        exists = true;
                    }

                }
                if (exists)
                {
                    if(_secretWord == new string(_correctGuesses))
                    {
                        _playerWon = true;
                    }
                    return true;
                }
                else
                {
                    if (!_incorrectGuesses.ToString().Contains(guessChar))
                    {
                        _incorrectGuesses.Append(guessChar);
                    }
                    else return false;
                }
            }

            _numRemainingGuesses--;
            return false;
        }

        public bool IsGameFinished()
        {
            return GetNumRemainingGuesses() == 0 || _playerWon;
        }

        public void NewGame(int numGuesses)
        {
            if (numGuesses < 1 || numGuesses > 26)
                throw new ArgumentOutOfRangeException("Number of guesses must be between 1 and 26.");

            if (_words.Count == 0)
                throw new AccessViolationException("You must add words before starting a game."); //Fix custom exc

            _playerWon = false;
            _numRemainingGuesses = numGuesses;
            _incorrectGuesses = new StringBuilder();
            _secretWord = _words[new Random().Next(0, _words.Count)];
            _correctGuesses = new char[_secretWord.Length];
            for (int i = 0; i < _secretWord.Length; i++)
                _correctGuesses[i] = '_';
        }

        public bool PlayerWon()
        {
            return _playerWon;
        }
    }
}
