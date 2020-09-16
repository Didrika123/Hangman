using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xunit;
/*
 * My test result for the project Hangman
  
PS C:\...\repos\Hangman\Hangman.Tests> dotnet test /p:CollectCoverage=true
Test run for C:\...\repos\Hangman\Hangman.Tests\bin\Debug\netcoreapp3.1\Hangman.Tests.dll(.NETCoreApp,Version=v3.1)
Microsoft (R) Test Execution Command Line Tool Version 16.7.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

A total of 1 test files matched the specified pattern.

Test Run Successful.
Total tests: 34
     Passed: 34
 Total time: 1.8586 Seconds

Calculating coverage result...
  Generating report 'C:\...\repos\Hangman\Hangman.Tests\coverage.json'

+---------+--------+--------+--------+
| Module  | Line   | Branch | Method |
+---------+--------+--------+--------+
| Hangman | 60.53% | 74.48% | 90.62% |
+---------+--------+--------+--------+

+---------+--------+--------+--------+
|         | Line   | Branch | Method |
|         | Line   | Branch | Method |
+---------+--------+--------+--------+
| Total   | 60.53% | 74.48% | 90.62% |
+---------+--------+--------+--------+
| Average | 60.53% | 74.48% | 90.62% |
+---------+--------+--------+--------+

PS C:\...\repos\Hangman\Hangman.Tests>
  
 *
 */
namespace Hangman.Tests
{
    public class HangmanLogicTest
    {

        public class HangmanConsoleMethods
        {
            [Fact]
            public void Update_GivenExitUserInput_MakesItStopRunning()
            {
                //Set up mock objects
                var output = new StringWriter();
                Console.SetOut(output);

                var input = new StringReader("");
                Console.SetIn(input);

                //Initialize the program
                HangmanConsole hangmanConsole = new HangmanConsole();
                hangmanConsole.Init();
                hangmanConsole.Draw();

                //Update normally
                input = new StringReader("someguess");
                Console.SetIn(input);
                hangmanConsole.Update();
                Assert.True(hangmanConsole.IsRunning()); //Its running

                //Update again with "Exit" input 
                input = new StringReader("exit");
                Console.SetIn(input);
                hangmanConsole.Update();
                Assert.False(hangmanConsole.IsRunning()); //It stopped running
            }
        }


        //NEW GAME
        public class NewGameMethod
        {
            [Fact]
            public void NewGame_GivenIntGivenOutsideBounds_ThrowsException()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.Throws<ArgumentOutOfRangeException>(() => hangmanLogic.NewGame(0));
                Assert.Throws<ArgumentOutOfRangeException>(() => hangmanLogic.NewGame(27, "anything"));
            }
            [Fact]
            public void NewGame_GivenStringContainingNonAlphabeticalChars_ThrowsException()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.Throws<FormatException>(() => hangmanLogic.NewGame(1, "aa?aad"));
            }
            [Fact]
            public void NewGame_GivenStringImproperLength_ThrowsException()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.Throws<FormatException>(() => hangmanLogic.NewGame(1, "a"));
                Assert.Throws<FormatException>(() => hangmanLogic.NewGame(1, "uncharacteristically" + "x"));
            }
            [Fact]
            public void NewGame_GivenNullOrEmptyString_ThrowsException()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.Throws<ArgumentNullException>(() => hangmanLogic.NewGame(1, null));
                Assert.Throws<ArgumentNullException>(() => hangmanLogic.NewGame(1, ""));
            }
        }

        public class GuessMethod
        {

            [Fact]
            public void Guess_GivenStringWithImproperLength_ThrowsException()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.Throws<FormatException>(() => hangmanLogic.Guess("applesauce" + "x"));
                Assert.Throws<FormatException>(() => hangmanLogic.Guess("ap"));
            }

            [Fact]
            public void Guess_GivenStringContainingCharactersOutsideAlphabet_ThrowsException()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.Throws<FormatException>(() => hangmanLogic.Guess("!"));
            }

            [Fact]
            public void Guess_GivenNullOrEmptyString_ThrowsException()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.Throws<ArgumentNullException>(() => hangmanLogic.Guess(null));
                Assert.Throws<ArgumentNullException>(() => hangmanLogic.Guess(""));
            }

            [Fact]
            public void Guess_GivenStringSameLengthAsWordButNotEqual_ReturnsFalse()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.False(hangmanLogic.Guess("sauceapple"));
            }

            [Fact]
            public void Guess_GivenStringEqualToWord_ReturnsTrue()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.True(hangmanLogic.Guess("applesauce"));
            }

            [Fact]
            public void Guess_GivenStringOfOneLetterThatExistsInWord_ReturnsTrue()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.True(hangmanLogic.Guess("a"));
            }

            [Fact]
            public void Guess_GivenStringOfOneLetterThatDoesNotExistInWord_ReturnsFalse()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.False(hangmanLogic.Guess("x"));
            }


            [Fact]
            public void Guess_CalledWhenNumRemainingGuessesIsZero_ThrowsException()
            {
                IHangman hangmanLogic = SetupGame();
                hangmanLogic.NewGame(1);
                hangmanLogic.Guess("x");
                Assert.Throws<GameIsFinishedException>(() => hangmanLogic.Guess("y"));
            }
        }

        public class GuessCharMethod
        {
            [Fact]
            public void Guess_GivenCharNotAlphabeticalLetter_ThrowsException()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.Throws<FormatException>(() => hangmanLogic.Guess((char)('a' - 1)));
                Assert.Throws<FormatException>(() => hangmanLogic.Guess((char)('z' + 1)));
                Assert.Throws<FormatException>(() => hangmanLogic.Guess((char)('A' - 1)));
                Assert.Throws<FormatException>(() => hangmanLogic.Guess((char)('Z' + 1)));
            }
            [Fact]
            public void Guess_GivenProperCharButIncorrect_ReturnsFalse()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.False(hangmanLogic.Guess('q'));
            }
            [Fact]
            public void Guess_GivenCorrectChar_ReturnsTrue()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.True(hangmanLogic.Guess('p'));
            }
        }

        public class IsGameFinishedMethod
        {
            [Fact]
            public void IsGameFinished_PlayerLost_ReturnsTrue()
            {
                IHangman hangmanLogic = SetupGame();
                hangmanLogic.NewGame(1, "hello");
                hangmanLogic.Guess("x");
                Assert.True(hangmanLogic.IsGameFinished());
            }
            [Fact]
            public void IsGameFinished_PlayerWon_ReturnsTrue()
            {
                IHangman hangmanLogic = SetupGame();
                hangmanLogic.NewGame(5, "hello");
                hangmanLogic.Guess("hello");
                Assert.True(hangmanLogic.IsGameFinished());
            }
            [Fact]
            public void IsGameFinished_StillHaveGuessesButNobodyWonYet_ReturnsFalse()
            {
                IHangman hangmanLogic = SetupGame();
                Assert.False(hangmanLogic.IsGameFinished());
            }
        }

        public class PlayerWonMethod
        {
            [Fact]
            public void PlayerWon_AfterCorrectWordGuess_ReturnsTrue()
            {
                IHangman hangmanLogic = SetupGame();
                hangmanLogic.Guess("applesauce");
                Assert.True(hangmanLogic.PlayerWon());
            }

            [Fact]
            public void PlayerWon_PlayerWonOnLastGuess_ReturnsTrue()
            {
                IHangman hangmanLogic = SetupGame();
                hangmanLogic.NewGame(1, "hello");
                hangmanLogic.Guess("hello");
                Assert.True(hangmanLogic.PlayerWon());

            }

            [Fact]
            public void PlayerWon_AfterWordGuessAndNewGame_ReturnsFalse()
            {
                IHangman hangmanLogic = SetupGame();
                hangmanLogic.Guess("applesauce");
                hangmanLogic.NewGame(1);
                Assert.False(hangmanLogic.PlayerWon());
            }
        }

        public class GetNumRemainingGuessesMethod
        {
            [Fact]
            public void GetNumRemainingGuesses_AfterIncorrectGuess_ReturnsValueReducedByOne()
            {
                IHangman hangmanLogic = SetupGame();
                int numGuesses = hangmanLogic.GetNumRemainingGuesses();
                hangmanLogic.Guess("sauceapple");
                Assert.Equal(numGuesses - 1, hangmanLogic.GetNumRemainingGuesses());
            }

            [Fact]
            public void GetNumRemainingGuesses_AfterCorrectGuess_ReturnsSameValueAsBefore()
            {

                IHangman hangmanLogic = SetupGame();
                int numGuesses = hangmanLogic.GetNumRemainingGuesses();
                hangmanLogic.Guess("applesauce");
                Assert.Equal(numGuesses, hangmanLogic.GetNumRemainingGuesses());
            }

            [Fact]
            public void GetNumRemainingGuesses_AfterGuessingSomethingAlreadyGuessed_ReturnsSameValueAsBefore()
            {

                IHangman hangmanLogic = SetupGame();
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
                IHangman hangmanLogic = SetupGame();
                int numGuesses = hangmanLogic.GetNumRemainingGuesses();
                try
                {
                    hangmanLogic.Guess("asdas"); //improper guess
                }
                catch (Exception) { }
                Assert.Equal(numGuesses, hangmanLogic.GetNumRemainingGuesses());

            }
        }
        public class GetStartingNumberOfGuessesMethod
        {
            [Fact]
            public void GetStartingNumberOfGuesses_NewGameWithXGuesses_ReturnsX()
            {
                IHangman hangmanLogic = SetupGame();
                int numStartGuesses = 5;
                hangmanLogic.NewGame(numStartGuesses);
                Assert.Equal(numStartGuesses, hangmanLogic.GetStartingNumberOfGuesses());

            }
        }
        public class GetSecretWordProgressMethod
        {
            [Fact]
            public void GetSecretWordProgress_AfterNewGame_ReturnsString()
            {
                IHangman hangmanLogic = SetupGame();
                string secretWord = "someword";
                string expected = "";
                for (int i = 0; i < secretWord.Length; i++)
                    expected += "_ ";
                hangmanLogic.NewGame(1, secretWord);
                Assert.Equal(expected.Length, hangmanLogic.GetSecretWordProgress().Length);
                Assert.Equal(expected, hangmanLogic.GetSecretWordProgress());

            }
            [Fact]
            public void GetSecretWordProgress_AfterPlayerWon_ReturnsSecretWord()
            {
                IHangman hangmanLogic = SetupGame();
                string secretWord = "someword";
                string expected = "S O M E W O R D ";
                hangmanLogic.NewGame(1, secretWord);
                hangmanLogic.Guess(secretWord);
                Assert.Equal(expected, hangmanLogic.GetSecretWordProgress());
            }

            [Fact]
            public void GetSecretWordProgress_AfterCorrectCharGuess_ReturnsStringWithThoseLettersUnblanked()
            {
                IHangman hangmanLogic = SetupGame();
                string secretWord = "someword";
                string expected = "_ O _ _ _ O _ _ ";
                hangmanLogic.NewGame(1, secretWord);
                hangmanLogic.Guess('O');
                Assert.Equal(expected, hangmanLogic.GetSecretWordProgress());
            }


            [Fact]
            public void GetSecretWordProgress_AfterCorrectCharGuessThenNewGame_ReturnsStringWithOnlyUnderlines()
            {
                IHangman hangmanLogic = SetupGame();
                string secretWord = "someword";
                string expected = "_ _ _ _ _ _ _ _ ";
                hangmanLogic.NewGame(1, secretWord);
                hangmanLogic.Guess('O');
                hangmanLogic.NewGame(1, secretWord);
                Assert.Equal(expected, hangmanLogic.GetSecretWordProgress());
            }


        }

        public class GetIncorrectGuessesMethod
        {
            [Fact]
            public void GetIncorrectGuesses_AfterIncorrectWordGuess_ReturnsStringSameWord()
            {
                IHangman hangmanLogic = SetupGame();
                hangmanLogic.Guess("sauceapple");
                Assert.Equal("SAUCEAPPLE", hangmanLogic.GetIncorrectGuesses());
            }
            [Fact]
            public void GetIncorrectGuesses_AfterIncorrectCharGuess_ReturnsStringSameChar()
            {
                IHangman hangmanLogic = SetupGame();
                hangmanLogic.Guess("x");
                Assert.Equal("X", hangmanLogic.GetIncorrectGuesses());
            }

        }
        public class UseWordsFromTextFileMethod
        {
            [Fact]
            public void UseWordsFromTextFile_AfterUnexistingFile_ThrowsException()
            {
                IHangman hangmanLogic = SetupGame();

                Assert.Throws<FileNotFoundException>( () => hangmanLogic.UseWordsFromTextFile("no-file.txt") );
            }

        }

        public static IHangman SetupGame()
        {
            IHangman hangmanLogic = new Hangman();

            hangmanLogic.NewGame(10, "ApPlESaUcE");

            return hangmanLogic;
        }
    }
}
