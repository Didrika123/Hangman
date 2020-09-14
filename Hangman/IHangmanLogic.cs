using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    public interface IHangmanLogic
    {
        //Adds a word to a list which the game then can choose from
        //Word has to be between 2-20 letters (A-Z) or longer.   //"uncharacteristically" (20 chars) is the worlds longest commonly used english word according to a study: https://web.archive.org/web/20090427054251/http://www.maltron.com/words/words-longest-modern.html
        //If Word already exists, nothing happens
        //Words are not case sensitive.
        void AddWord(string word);

        //Wipes all data and starts a new game.
        //Selects a random word from added words.
        //numGuesses in 1-26      //26 letters in alphabet
        void NewGame(int numGuesses);

        //Guess the entire sought word or a letter in the sought word
        //Not case sensitive
        //returns true if it matches
        bool Guess(string guess);


        //Returns the number of remaining guesses
        int GetNumRemainingGuesses();


        //Returns true when either player or computer wins
        bool IsGameFinished();

        //Returns true if player won the game
        //Returns false if game isnt over or game is over and player lost
        bool PlayerWon();

    }
}
