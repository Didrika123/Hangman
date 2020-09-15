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
            Assert.Throws<FormatException>(() => hangmanLogic.AddWord("app!e"));
        }

        [Fact]
        public void AddWord_GivenStringWithImproperLength_ThrowsException()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.Throws<FormatException>(() => hangmanLogic.AddWord("uncharacteristically" + "x"));
            Assert.Throws<FormatException>(() => hangmanLogic.AddWord("x"));
        }

        [Fact]
        public void AddWord_GivenNullOrEmptyString_ThrowsException()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.Throws<ArgumentNullException>(() => hangmanLogic.AddWord(null));
            Assert.Throws<ArgumentNullException>(() => hangmanLogic.AddWord(""));
        }

        //NEW GAME
        [Fact]
        public void NewGame_GivenIntGivenOutsideBounds_ThrowsException()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.Throws<ArgumentOutOfRangeException>(() => hangmanLogic.NewGame(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => hangmanLogic.NewGame(27));
        }
        [Fact]
        public void NewGame_CalledBeforeAddingWordsToTheGame_ThrowsException()
        {
            IHangmanLogic hangmanLogic = new HangmanLogic();
            Assert.Throws<Exception>(() => hangmanLogic.NewGame(10));
        }

        //GUESS
        [Fact]
        public void Guess_GivenStringWithImproperLength_ThrowsException()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.Throws<FormatException>(() => hangmanLogic.Guess("applesauce" + "x"));
            Assert.Throws<FormatException>(() => hangmanLogic.Guess("ap"));
        }

        [Fact]
        public void Guess_GivenStringContainingCharactersOutsideAlphabet_ThrowsException()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.Throws<FormatException>(() => hangmanLogic.Guess("!"));
        }

        [Fact]
        public void Guess_GivenNullOrEmptyString_ThrowsException()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            Assert.Throws<ArgumentNullException>(() => hangmanLogic.Guess(null));
            Assert.Throws<ArgumentNullException>(() => hangmanLogic.Guess(""));
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

        //GetNumRemaining
        [Fact]
        public void GetNumRemainingGuesses_AfterIncorrectGuess_ReturnsValueReducedByOne()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            int numGuesses = hangmanLogic.GetNumRemainingGuesses();
            hangmanLogic.Guess("sauceapple");
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
        public void GetNumRemainingGuesses_AfterGuessingSomethingAlreadyGuessed_ReturnsSameValueAsBefore()
        {

            IHangmanLogic hangmanLogic = SetupGame();
            hangmanLogic.Guess("a"); //correct guess
            hangmanLogic.Guess("x"); //incorrect guess
            int numGuesses = hangmanLogic.GetNumRemainingGuesses();
            hangmanLogic.Guess("a");
            hangmanLogic.Guess("x");
            Assert.Equal(numGuesses, hangmanLogic.GetNumRemainingGuesses());
        }

        [Fact]
        public void GetNumRemainingGuesses_AfterImproperGuess_ReturnsSameValueAsBefore()
        {
            IHangmanLogic hangmanLogic = SetupGame();
            int numGuesses = hangmanLogic.GetNumRemainingGuesses();
            try
            {
                hangmanLogic.Guess("asdas"); //improper guess
            }
            catch (Exception) { }
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
