using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    public interface IHangmanLogic
    {

        /// <summary>
        ///      Adds a word to a list which the game then can choose from
        ///      Word has to be between 2-20 letters (A-Z) or longer.   //"uncharacteristically" (20 chars) is the worlds longest commonly used english word according to a study: https://web.archive.org/web/20090427054251/http://www.maltron.com/words/words-longest-modern.html
        ///      If Word already exists, nothing happens
        ///      Words are not case sensitive.
        /// </summary>     
        void AddWord(string word);

        /// <summary>
        ///      Wipes all data and starts a new game.
        ///      Selects a random word from added words.
        ///      numGuesses in 1-26      //26 letters in alphabet
        /// </summary>     
        void NewGame(int numGuesses);

        /// <summary>
        ///      Guess the entire sought word or a letter in the sought word
        ///      Not case sensitive
        ///      returns true if it matches
        /// </summary>     
        bool Guess(string guess);


        /// <summary>
        ///      Returns the number of remaining guesses
        /// </summary>     
        int GetNumRemainingGuesses();

        /// <summary>
        ///      Returns the secret word
        /// </summary>     
        string GetSecretWord();

        string GetGuesses();


        /// <summary>
        ///      Returns true when either player or computer won until NewGame() has been called.
        /// </summary>     
        bool IsGameFinished();

        /// <summary>
        ///      Returns true if player won the game
        ///      Returns false if game isnt over or game is over and player lost
        /// </summary>     
        bool PlayerWon();

    }
}
