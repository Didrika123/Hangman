using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    /// <summary>
    ///     Interface for the logic of the word guessing game Hangman.
    /// </summary> 
    public interface IHangman
    {
        //"uncharacteristically" (20 chars) is the worlds longest commonly used english word according to a study: https://web.archive.org/web/20090427054251/http://www.maltron.com/words/words-longest-modern.html
        /// <summary>
        ///      Starts a new game.
        ///      With specified secret word.
        /// </summary>     
        /// <param name="numGuesses">
        ///      numGuesses in range 1-26 (26 letters in alphabet)
        /// </param>
        /// <param name="secretWord">
        ///      2-20 characters and only letters (A-Z)
        /// </param>
        void NewGame(int numGuesses, string secretWord);


        /// <summary>
        ///      Starts a new game.
        ///      Selects a random word from the wordlist hangman maintains.
        /// </summary>     
        /// <param name="numGuesses">
        ///      numGuesses in range 1-26 (26 letters in alphabet)
        /// </param>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        void NewGame(int numGuesses);


        /// <summary>
        ///      Guess the entire secret word or a single letter in the secret word
        /// </summary>     
        /// <returns>
        ///      returns true if argument matches a single letter or the entire word
        ///      return false otherwise OR if game is over
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="GameIsFinishedException"></exception>
        bool Guess(string guess);


        /// <summary>
        ///      Guess a single letter in the secret word
        /// </summary>     
        /// <returns>
        ///      returns true if argument matches a single letter
        ///      return false otherwise OR if game is over
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="GameIsFinishedException"></exception>
        bool Guess(char charGuess);



        /// <returns>
        ///      Returns true when either player or computer has won
        /// </returns>
        bool IsGameFinished();


        /// <returns>
        ///      Returns true if player won the game
        ///      Returns false if player lost OR if game isnt over yet
        /// </returns>
        bool PlayerWon();

        /// <returns>
        ///      Returns the number of remaining guesses
        /// </returns>
        int GetNumRemainingGuesses();


        /// <returns>
        ///      Returns the number of guesses you started out with.
        /// </returns>
        int GetStartingNumberOfGuesses();


        /// <returns>
        ///      Returns the current progress on the secret word
        /// </returns>
        string GetSecretWordProgress();


        /// <returns>
        ///      Returns the incorrect guesses
        /// </returns>
        string GetIncorrectGuesses();


        /// <summary>
        ///      Replaces default wordlist (If no error) with a new one containing words from specified file.
        /// </summary>     
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="BadGameDataException"></exception>
        void UseWordsFromTextFile(string path);
    }
}
