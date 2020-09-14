using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hangman.Tests
{
    public class HangmanLogicTest
    {
        //ADD WORD
        [Fact]
        public void AddWord_GivenStringContainingCharactersOutsideAlphabet_ThrowsException ()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.Throws<Exception>(() => hangmanLogic.AddWord("uncharacteristically" + "x"));
        }

        [Fact]
        public void AddWord_GivenStringOutsideAcceptedLength_ThrowsException()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.Throws<Exception>(() => hangmanLogic.AddWord("uncharacteristically" + "x"));
            Assert.Throws<Exception>(() => hangmanLogic.AddWord("x"));
        }

        [Fact]
        public void AddWord_GivenNullString_ThrowsException()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.Throws<Exception>(() => hangmanLogic.AddWord(null));
        }

        //NEW GAME
        [Fact]
        public void NewGame_GivenIntGivenOutsideBounds_ThrowsException()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.Throws<Exception>(() => hangmanLogic.NewGame(0));
            Assert.Throws<Exception>(() => hangmanLogic.NewGame(27));
        }
        [Fact]
        public void NewGame_CalledBeforeAddingWordsToTheGame_ThrowsException()
        {
            IHangmanLogic hangmanLogic = new HangmanLogic();
            Assert.Throws<Exception>(() => hangmanLogic.NewGame(10));
        }

        //Guess
        [Fact]
        public void Guess_GivenStringWithImproperLength_ThrowsException()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.Throws<Exception>(() => hangmanLogic.Guess("applesaucesauce"));
            Assert.Throws<Exception>(() => hangmanLogic.Guess("ap"));
            Assert.Throws<Exception>(() => hangmanLogic.Guess(""));
        }

        [Fact]
        public void Guess_GivenStringContainingCharactersOutsideAlphabet_ThrowsException()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.Throws<Exception>(() => hangmanLogic.Guess("!"));
        }

        [Fact]
        public void Guess_GivenNullString_ThrowsException()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.Throws<NullReferenceException>(() => hangmanLogic.Guess(null));
        }

        [Fact]
        public void Guess_GivenStringSameLengthAsWordButNotEqual_ReturnsFalse()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.False(hangmanLogic.Guess("sauceapple"));
        }

        [Fact]
        public void Guess_GivenStringEqualToWord_ReturnsTrue()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.True(hangmanLogic.Guess("applesauce"));
        }

        [Fact]
        public void Guess_GivenStringOfOneLetterThatExistsInWord_ReturnsTrue()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.True(hangmanLogic.Guess("a"));
        }

        [Fact]
        public void Guess_GivenStringOfOneLetterThatDoesNotExistInWord_ReturnsFalse()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.False(hangmanLogic.Guess("x"));
        }


        [Fact]
        public void Guess_CalledWhenNumRemainingGuessesIsZero_ThrowsException()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            hangmanLogic.NewGame(1);
            hangmanLogic.Guess("x");
            Assert.Throws<Exception>( () => hangmanLogic.Guess("y") );
        }

        //GetNumRemaining Guesses, NOTE: This should probably be part of GUESS tests!
        //also add check for double guessing so it remains unchanged. (What about Double guessing wrong letter?)
        [Fact]
        public void GetNumRemainingGuesses_AfterIncorrectGuess_ReturnsValueReducedByOne()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            int numGuesses = hangmanLogic.GetNumRemainingGuesses();
            hangmanLogic.Guess("applesauce");
            Assert.Equal(numGuesses - 1, hangmanLogic.GetNumRemainingGuesses());
        }

        [Fact]
        public void GetNumRemainingGuesses_AfterCorrectGuess_ReturnsSameValueAsBefore()
        {

            IHangmanLogic hangmanLogic = SetupGame();
            int numGuesses = hangmanLogic.GetNumRemainingGuesses();
            hangmanLogic.Guess("applesauce");
            Assert.Equal(numGuesses, hangmanLogic.GetNumRemainingGuesses());
        }


        [Fact]
        public void GetNumRemainingGuesses_AfterImproperGuess_ReturnsSameValueAsBefore()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            int numGuesses = hangmanLogic.GetNumRemainingGuesses();
            hangmanLogic.Guess("sauceapple");
            Assert.Equal(numGuesses, hangmanLogic.GetNumRemainingGuesses());

        }

        IHangmanLogic SetupGame()
        {
            IHangmanLogic hangmanLogic = new HangmanLogic();

            hangmanLogic.AddWord("ApPlESaUcE");
            hangmanLogic.NewGame(10);

            return hangmanLogic;
        }
    }
}
