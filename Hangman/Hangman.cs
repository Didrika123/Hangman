using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hangman
{
    public class GameIsFinishedException : Exception
    {
        public GameIsFinishedException(string message)
            : base(message) { }
    }
    public class BadGameDataException : Exception
    {
        public BadGameDataException(string message)
            : base(message) { }
    }

    public class Hangman : IHangman
    {
        private string[] _words = { "sinner", "heretic", "anarchist", "devil", "hell", "demon", "cucumber", "asparagus", "soybean", "tofu", "orange", "banana" }; //default words
        private StringBuilder _incorrectCharGuesses;
        readonly private List<string> _incorrectWordGuesses = new List<string>();
        private char[] _secretWordProgress;
        private string _secretWord;
        private bool _playerWon;
        private int _numRemainingGuesses;
        private int _numStartingGuesses;



        private bool ContainsOnlyLowerCaseAlphabeticalLetters(string word)
        {
            foreach (char ch in word)
            {
                if (ch < 'a' || ch > 'z')
                    return false;
            }
            return true;
        }
        private string GetRandomWord()
        {
            Random rand = new Random();
            int index = rand.Next(0, _words.Length);
            return _words[index];
        }
        public int GetNumRemainingGuesses()
        {
            return _numRemainingGuesses;
        }

        public int GetStartingNumberOfGuesses()
        {
            return _numStartingGuesses;
        }

        public string GetSecretWordProgress()
        {
            StringBuilder spacesBetweenChars = new StringBuilder();
            foreach (char ch in _secretWordProgress) //This is purely for aesthetic reasons (so output is like _ _ _ _ _ instead of _____)
            {
                spacesBetweenChars.Append(Char.ToUpper(ch));
                spacesBetweenChars.Append(' ');
            }
            return spacesBetweenChars.ToString(); ;
        }

        public string GetIncorrectGuesses()
        {
            string niceStringOfIncorrectCharGuesses = String.Join(", ", _incorrectCharGuesses.ToString().ToCharArray()).ToUpper();
            string niceStringOfIncorrectWordGuesses = String.Join(", ", _incorrectWordGuesses).ToUpper();
            if (niceStringOfIncorrectWordGuesses.Length > 0 && niceStringOfIncorrectCharGuesses.Length > 0)
                niceStringOfIncorrectWordGuesses = niceStringOfIncorrectWordGuesses + ", ";

            return niceStringOfIncorrectWordGuesses + niceStringOfIncorrectCharGuesses; 
        }
        public bool Guess(string guess)
        {
            if (IsGameFinished())
                throw new GameIsFinishedException("You cant Guess when The game is finished, call NewGame(int) to start over.");

            if (String.IsNullOrEmpty(guess))
                throw new ArgumentNullException("", "You must enter a guess.");

            guess = guess.ToLower();

            if (guess.Length == 1)
                return Guess(guess[0]); //Use guess by char instead

            if (!ContainsOnlyLowerCaseAlphabeticalLetters(guess))
                throw new FormatException("You must enter a guess that only contains alphabetical letters.");

            if (guess.Length != _secretWord.Length && guess.Length != 1)
                throw new FormatException("You must enter a guess of 1 letter or the same length as the secret word.");


            if (guess == _secretWord)
            {
                _secretWordProgress = guess.ToCharArray();
                _playerWon = true;
                return true;
            }
            else
            {
                if (!_incorrectWordGuesses.Contains(guess))
                {
                    _incorrectWordGuesses.Add(guess);
                    _numRemainingGuesses--;
                }
                return false;
            }
        }
        public bool Guess(char charGuess)
        {
            if (IsGameFinished())
                throw new GameIsFinishedException("You cant Guess when The game is finished, call NewGame(int) to start over.");

            charGuess = Char.ToLower(charGuess);

            if (charGuess < 'a' || charGuess > 'z')
                throw new FormatException("You must enter a guess that only contains alphabetical letters.");

            bool itsACorrectGuess = false;
            for (int i = 0; i < _secretWord.Length; i++)
            {
                if (_secretWord[i] == charGuess)
                {
                    _secretWordProgress[i] = charGuess;
                    itsACorrectGuess = true;
                }

            }

            if (itsACorrectGuess)
            { 
                if (_secretWord == new string(_secretWordProgress))
                {
                    _playerWon = true;
                }
                return true;
            }
            else
            {
                if (!_incorrectCharGuesses.ToString().Contains(charGuess))
                {
                    _incorrectCharGuesses.Append(charGuess);
                    _numRemainingGuesses--;
                }
                return false;
            }
        }

        public bool IsGameFinished()
        {
            return GetNumRemainingGuesses() == 0 || _playerWon;
        }

        public void NewGame(int numGuesses, string secretWord)
        {
            if (String.IsNullOrEmpty(secretWord))
                throw new ArgumentNullException("", "The Secret Word cannot be empty or null.");

            secretWord = secretWord.ToLower();

            if (numGuesses < 1 || numGuesses > 26)
                throw new ArgumentOutOfRangeException("Number of guesses must be between 1 and 26.");

            if (!ContainsOnlyLowerCaseAlphabeticalLetters(secretWord) || secretWord.Length < 2 || secretWord.Length > 20)
                throw new FormatException("The secret word must be between 2 and 20 letters and must consist exclusively of alphabetical letters.");

            _playerWon = false;
            _numStartingGuesses = _numRemainingGuesses = numGuesses;
            _incorrectCharGuesses = new StringBuilder();
            _incorrectWordGuesses.Clear();
            _secretWord = secretWord;
            _secretWordProgress = new char[_secretWord.Length];

            for (int i = 0; i < _secretWord.Length; i++)
                _secretWordProgress[i] = '_';
        }
        public void NewGame(int numGuesses)
        {
            NewGame(numGuesses, GetRandomWord());
        }

        public bool PlayerWon()
        {
            return _playerWon;
        }

        public void UseWordsFromTextFile(string path) //Ideally I think I would've preferred having the file loading outside this class and instead passed an Array or something of words
        {
            List<string> uncheckedWords = new List<string>();
            StreamReader streamReader = null;

            try
            {
                streamReader = File.OpenText(path);

                uncheckedWords.AddRange(streamReader.ReadToEnd().ToLower().Replace(" ", "").Split(',')); //System.IO.File.ReadAllText(path).Split(','); would also work with less bloat but wanted to try the finally statement
            }
            finally
            {
                if (streamReader != null)
                    streamReader.Close();
            }

            for (int i = uncheckedWords.Count - 1; i >= 0; i--)
            {
                string word = uncheckedWords[i];
                if (!ContainsOnlyLowerCaseAlphabeticalLetters(word) || word.Length < 2 || word.Length > 20)
                    uncheckedWords.RemoveAt(i);
            }

            if (uncheckedWords.Count == 0)
                throw new BadGameDataException($"The File {path} contains no valid words, or the format is wrong. Will revert back to Default Wordlist.");
            _words = uncheckedWords.ToArray();
        }

    }
}
