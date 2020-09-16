using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hangman
{
    public class HangmanConsole
    {
        private const int NUM_GUESSES = 10;
        readonly private IHangman _hangman = new Hangman();
        private bool _isRunning = true;


        public bool IsRunning()
        {
            return _isRunning;
        }

        public void Draw()
        {
            Clear();
            DrawIncorrectGuesses();
            DrawHangman();
            DrawSecretWordProgress();

            if (_hangman.IsGameFinished())
                DrawGameResult();

            GameTextToConsole("");
        }

        public void Init()
        {
            LoadGameData();
            _hangman.NewGame(NUM_GUESSES);
            DrawIntro();
            Draw();
        }

        public void Update()
        {

            if (_hangman.IsGameFinished())
            {
                WaitForUserResponse("Press <Enter> to start a new game. ");
                _hangman.NewGame(NUM_GUESSES);
            }
            else
            {
                Console.Write("\n\tYour Guess: ");
                string input = Console.ReadLine();
                if (input.ToLower() == "exit" || input.ToLower() == "quit")
                {
                    _isRunning = false;
                }
                else
                {
                    LetUserGuess(input);
                }
            }

        }
        private void LoadGameData()
        {
            try
            {
                _hangman.UseWordsFromTextFile("Words.txt");
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is BadGameDataException)
            {
                Console.WriteLine("File Error: " + ex.Message);
                Console.WriteLine("You can still play the game but the intended secret words are missing.");
                Console.ReadLine();
            }
        }
        private void LetUserGuess(string input)
        {
            try
            {
                _hangman.Guess(input);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException )
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\tInvalid Guess: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
            }
            catch (GameIsFinishedException ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Starting new game...");
                _hangman.NewGame(NUM_GUESSES);
            }
        }
        private void GameTextToConsole(string message)
        {
            Console.WriteLine(" ::    " + message);
        }
        private void Clear()
        {
            Console.Clear();
            Console.WriteLine(" ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
            GameTextToConsole("");
        }
        private void WaitForUserResponse(string message = "<Press Enter>")
        {
            GameTextToConsole("");
            Console.WriteLine();
            Console.Write("\t" + message);
            Console.ReadLine();
            Clear();
        }
        private void DrawIncorrectGuesses()
        {
            string incorrectGuesses = _hangman.GetIncorrectGuesses();
            if (incorrectGuesses.Length > 0)
                GameTextToConsole("Incorrect Guesses: " + incorrectGuesses);
            else GameTextToConsole("");
        }
        private void DrawSecretWordProgress()
        {
            GameTextToConsole("");
            GameTextToConsole("The Secret Word: " + _hangman.GetSecretWordProgress());
            GameTextToConsole("");
        }
        private void DrawGameResult()
        {
            if (_hangman.PlayerWon())
            {
                GameTextToConsole("     YOU WIN !     ");
                GameTextToConsole("");
                GameTextToConsole("Poor Old Jack lives to see another day.");
            }
            else
            {
                GameTextToConsole("     YOU LOSE !    ");
                GameTextToConsole("");
                GameTextToConsole("Poor Old Jack is no longer with us.");
            }
        }
        private void DrawIntro()
        {
            Clear();
            GameTextToConsole("POOR OLD JACK PUDDINGS!");
            WaitForUserResponse();

            GameTextToConsole("Caught stealing a loaf of bread!");
            WaitForUserResponse();

            GameTextToConsole(" - Off with his head!");
            GameTextToConsole(" - Hang the Man!");
            GameTextToConsole(" - To the Gallows!");
            GameTextToConsole("Chanted the forming crowd merrily.");
            WaitForUserResponse();

            GameTextToConsole("It was saturday and the people were eager for entertainment.");
            GameTextToConsole("How about we play a little game with Poor Old Jack...");
            GameTextToConsole("...We are honorable men and women after all.");
            WaitForUserResponse();

            GameTextToConsole("We have decided on a Secret Word.");
            GameTextToConsole("You need to guess it in order to be spared!");
            GameTextToConsole("Either guess a letter in the word,");
            GameTextToConsole("Or guess the entire word.");
            WaitForUserResponse();

            GameTextToConsole($"Be careful though, if you guess incorrectly {NUM_GUESSES} times..");
            GameTextToConsole("");
            GameTextToConsole("You shall hang for your sins!");
            WaitForUserResponse();

        }
        private void DrawHangman()
        {
            int numCases = 10; //If you have fewer or more guesses than drawable cases then we try to calculate a proper case (Ex 5 start guesses would yield: case 0 -> case 2 -> case 4 -> case 6-> case 8 -> case 10)
            int calculatedStage = numCases - (int) (numCases * (double)_hangman.GetNumRemainingGuesses() / _hangman.GetStartingNumberOfGuesses());

            switch (calculatedStage)
            {
                case 0:
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("     .........     ");
                    GameTextToConsole("   .'   ,  .  '.   ");
                    GameTextToConsole(" .' .  .     , .'. ");
                    GameTextToConsole("                   ");
                    break;
                case 1:
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("     .I.....I.     ");
                    GameTextToConsole("   .'   ,  .  '.   ");
                    GameTextToConsole(" .' .  .     , .'. ");
                    GameTextToConsole("                   ");
                    break;
                case 2:
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("      _______      ");
                    GameTextToConsole("     .I.....I.     ");
                    GameTextToConsole("   .'   ,  .  '.   ");
                    GameTextToConsole(" .' .  .     , .'. ");
                    GameTextToConsole("                   ");
                    break;
                case 3:
                    GameTextToConsole("                   ");
                    GameTextToConsole("                   ");
                    GameTextToConsole("           |       ");
                    GameTextToConsole("           |       ");
                    GameTextToConsole("           |       ");
                    GameTextToConsole("      _____|_      ");
                    GameTextToConsole("     .I.....I.     ");
                    GameTextToConsole("   .'   ,  .  '.   ");
                    GameTextToConsole(" .' .  .     , .'. ");
                    GameTextToConsole("                   ");
                    break;
                case 4:
                    GameTextToConsole("                   ");
                    GameTextToConsole("       _____       ");
                    GameTextToConsole("           |       ");
                    GameTextToConsole("           |       ");
                    GameTextToConsole("           |       ");
                    GameTextToConsole("      _____|_      ");
                    GameTextToConsole("     .I.....I.     ");
                    GameTextToConsole("   .'   ,  .  '.   ");
                    GameTextToConsole(" .' .  .     , .'. ");
                    GameTextToConsole("                   ");
                    break;
                case 5:
                    GameTextToConsole("                   ");
                    GameTextToConsole("       _____       ");
                    GameTextToConsole("       |   |       ");
                    GameTextToConsole("           |       ");
                    GameTextToConsole("           |       ");
                    GameTextToConsole("      _____|_      ");
                    GameTextToConsole("     .I.....I.     ");
                    GameTextToConsole("   .'   ,  .  '.   ");
                    GameTextToConsole(" .' .  .     , .'. ");
                    GameTextToConsole("                   ");
                    break;
                case 6:
                    GameTextToConsole("                   ");
                    GameTextToConsole("       _____       ");
                    GameTextToConsole("       |   |       ");
                    GameTextToConsole("       °   |       ");
                    GameTextToConsole("           |       ");
                    GameTextToConsole("      _____|_      ");
                    GameTextToConsole("     .I.....I.     ");
                    GameTextToConsole("   .'   ,  .  '.   ");
                    GameTextToConsole(" .' .  .     , .'. ");
                    GameTextToConsole("                   ");
                    break;
                case 7:
                    GameTextToConsole("                   ");
                    GameTextToConsole("       _____       ");
                    GameTextToConsole("       |   |       ");
                    GameTextToConsole("      -°   |       ");
                    GameTextToConsole("           |       ");
                    GameTextToConsole("      _____|_      ");
                    GameTextToConsole("     .I.....I.     ");
                    GameTextToConsole("   .'   ,  .  '.   ");
                    GameTextToConsole(" .' .  .     , .'. ");
                    GameTextToConsole("                   ");
                    break;
                case 8:
                    GameTextToConsole("                   ");
                    GameTextToConsole("       _____       ");
                    GameTextToConsole("       |   |       ");
                    GameTextToConsole("      -°-  |       ");
                    GameTextToConsole("           |       ");
                    GameTextToConsole("      _____|_      ");
                    GameTextToConsole("     .I.....I.     ");
                    GameTextToConsole("   .'   ,  .  '.   ");
                    GameTextToConsole(" .' .  .     , .'. ");
                    GameTextToConsole("                   ");
                    break;
                case 9:
                    GameTextToConsole("                   ");
                    GameTextToConsole("       _____       ");
                    GameTextToConsole("       |   |       ");
                    GameTextToConsole("      -°-  |       ");
                    GameTextToConsole("        `  |       ");
                    GameTextToConsole("      _____|_      ");
                    GameTextToConsole("     .I.....I.     ");
                    GameTextToConsole("   .'   ,  .  '.   ");
                    GameTextToConsole(" .' .  .     , .'. ");
                    GameTextToConsole("                   ");
                    break;
                case 10:
                    GameTextToConsole("                   ");
                    GameTextToConsole("       _____       ");
                    GameTextToConsole("       |   |       ");
                    GameTextToConsole("      -°-  |       ");
                    GameTextToConsole("      ´ `  |       ");
                    GameTextToConsole("      _____|_      ");
                    GameTextToConsole("     .I.....I.     ");
                    GameTextToConsole("   .'   ,  .  '.   ");
                    GameTextToConsole(" .' .  .     , .'. ");
                    GameTextToConsole("                   ");
                    break;
            }
        }
    }
}
