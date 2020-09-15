using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hangman
{
    class HangmanConsole : IHangman
    {
        private IHangmanLogic _hangmanLogic = new HangmanLogic();
        private bool _isRunning = false;
        public const int NUM_GUESSES = 10;
        public void Draw()
        {
            Clear();
            DrawIncorrectGuesses();
            DrawHangman();
            DrawSecretWord();

            if (_hangmanLogic.IsGameFinished())
                DrawGameResult();

            Write("");
        }

        public void init()
        {
            string[] words = LoadWordsFromFile("Words.txt");
            if (words != null)
            {
                foreach (var word in words)
                {
                    if (word.ToLower() != "exit" && word.ToLower() != "quit")
                        _hangmanLogic.AddWord(word);
                }
            }
            else
            {
                _hangmanLogic.AddWord("nothing");
                _hangmanLogic.AddWord("tragedy");
            }
            _hangmanLogic.NewGame(NUM_GUESSES);
            DrawIntro();
            Draw();
        }
        private string[] LoadWordsFromFile(string path)
        {
            string[] words = null;
            StreamReader streamReader = null;
            try
            {
                streamReader = File.OpenText(path);

                words = streamReader.ReadToEnd().Replace(" ", "").Split(','); //System.IO.File.ReadAllText(path).Split(','); would also work with less bloat but wanted to try the finally statement
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Critical Error: Unable to load game data. The file \"{path}\" is missing.");
                Console.ReadLine();
            }
            finally
            {
                if(streamReader != null)
                    streamReader.Close();
            }
            return words;
        }

        public bool IsRunning()
        {
            return _isRunning;
        }

        public void Update()
        {
            _isRunning = true;


            if (_hangmanLogic.IsGameFinished())
            {
                Pause("Press <Enter> to start a new game. ");
                _hangmanLogic.NewGame(NUM_GUESSES);
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
                    try
                    {
                        _hangmanLogic.Guess(input);
                    }
                    catch(Exception e) //FIX cash right excepty
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }

        }
        private void Write(string message)
        {
            Console.WriteLine(" ::    " + message);
        }
        private void Clear()
        {
            Console.Clear();
            Console.WriteLine(" ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
            Write("");
        }
        private void Pause(string message = "<Press Enter>")
        {
            Write("");
            Console.WriteLine();
            Console.Write("\t" + message);
            Console.ReadLine();
            Clear();
        }
        private void DrawIncorrectGuesses()
        {
            string incorrectGuesses = _hangmanLogic.GetGuesses();
            if (incorrectGuesses.Length > 0)
                Write("Incorrect Guesses: " + incorrectGuesses);
            else Write("");
        }
        private void DrawSecretWord()
        {
            Write("");
            Write("The Secret Word: " + _hangmanLogic.GetSecretWord());
            Write("");
        }
        private void DrawGameResult()
        {
            if (_hangmanLogic.PlayerWon())
            {
                Write("     YOU WIN !     ");
                Write("");
                Write("Poor Old Jack lives to see another day.");
            }
            else
            {
                Write("     YOU LOSE !    ");
                Write("");
                Write("Poor Old Jack is no longer with us.");
            }
        }
        private void DrawIntro()
        {
            Clear();
            Write("POOR OLD JACK PUDDINGS!");
            Pause();
            Write("Caught stealing a loaf of bread!");
            Pause();
            Write(" - Off with his head!");
            Write(" - Hang the Man!");
            Write(" - To the Gallows!");
            Write("Chanted the forming crowd merrily.");
            Pause();
            Write("It was saturday and the people were eager for entertainment.");
            Write("How about we play a little game with Poor Old Jack...");
            Write("...We are honorable men and women after all.");
            Pause();
            Write("We have decided on a Secret Word.");
            Write("You need to guess it in order to be spared!");
            Write("Either guess a letter in the word,");
            Write("Or guess the entire word.");
            Pause();
            Write($"Be careful though, if you guess incorrectly {NUM_GUESSES} times..");
            Write("");
            Write("You shall hang for your sins!");
            Pause();

        }
        private void DrawHangman()
        {

            switch (NUM_GUESSES- _hangmanLogic.GetNumRemainingGuesses())
            {
                case 0:
                    Write("                   ");
                    Write("                   ");
                    Write("                   ");
                    Write("                   ");
                    Write("                   ");
                    Write("                   ");
                    Write("     .........     ");
                    Write("   .'   ,  .  '.   ");
                    Write(" .' .  .     , .'. ");
                    Write("                   ");
                    break;
                case 1:
                    Write("                   ");
                    Write("                   ");
                    Write("                   ");
                    Write("                   ");
                    Write("                   ");
                    Write("                   ");
                    Write("     .I.....I.     ");
                    Write("   .'   ,  .  '.   ");
                    Write(" .' .  .     , .'. ");
                    Write("                   ");
                    break;
                case 2:
                    Write("                   ");
                    Write("                   ");
                    Write("                   ");
                    Write("                   ");
                    Write("                   ");
                    Write("      _______      ");
                    Write("     .I.....I.     ");
                    Write("   .'   ,  .  '.   ");
                    Write(" .' .  .     , .'. ");
                    Write("                   ");
                    break;
                case 3:
                    Write("                   ");
                    Write("                   ");
                    Write("           |       ");
                    Write("           |       ");
                    Write("           |       ");
                    Write("      _____|_      ");
                    Write("     .I.....I.     ");
                    Write("   .'   ,  .  '.   ");
                    Write(" .' .  .     , .'. ");
                    Write("                   ");
                    break;
                case 4:
                    Write("                   ");
                    Write("       _____       ");
                    Write("           |       ");
                    Write("           |       ");
                    Write("           |       ");
                    Write("      _____|_      ");
                    Write("     .I.....I.     ");
                    Write("   .'   ,  .  '.   ");
                    Write(" .' .  .     , .'. ");
                    Write("                   ");
                    break;
                case 5:
                    Write("                   ");
                    Write("       _____       ");
                    Write("       |   |       ");
                    Write("           |       ");
                    Write("           |       ");
                    Write("      _____|_      ");
                    Write("     .I.....I.     ");
                    Write("   .'   ,  .  '.   ");
                    Write(" .' .  .     , .'. ");
                    Write("                   ");
                    break;
                case 6:
                    Write("                   ");
                    Write("       _____       ");
                    Write("       |   |       ");
                    Write("       °   |       ");
                    Write("           |       ");
                    Write("      _____|_      ");
                    Write("     .I.....I.     ");
                    Write("   .'   ,  .  '.   ");
                    Write(" .' .  .     , .'. ");
                    Write("                   ");
                    break;
                case 7:
                    Write("                   ");
                    Write("       _____       ");
                    Write("       |   |       ");
                    Write("      -°   |       ");
                    Write("           |       ");
                    Write("      _____|_      ");
                    Write("     .I.....I.     ");
                    Write("   .'   ,  .  '.   ");
                    Write(" .' .  .     , .'. ");
                    Write("                   ");
                    break;
                case 8:
                    Write("                   ");
                    Write("       _____       ");
                    Write("       |   |       ");
                    Write("      -°-  |       ");
                    Write("           |       ");
                    Write("      _____|_      ");
                    Write("     .I.....I.     ");
                    Write("   .'   ,  .  '.   ");
                    Write(" .' .  .     , .'. ");
                    Write("                   ");
                    break;
                case 9:
                    Write("                   ");
                    Write("       _____       ");
                    Write("       |   |       ");
                    Write("      -°-  |       ");
                    Write("        `  |       ");
                    Write("      _____|_      ");
                    Write("     .I.....I.     ");
                    Write("   .'   ,  .  '.   ");
                    Write(" .' .  .     , .'. ");
                    Write("                   ");
                    break;
                case 10:
                    Write("                   ");
                    Write("       _____       ");
                    Write("       |   |       ");
                    Write("      -°-  |       ");
                    Write("      ´ `  |       ");
                    Write("      _____|_      ");
                    Write("     .I.....I.     ");
                    Write("   .'   ,  .  '.   ");
                    Write(" .' .  .     , .'. ");
                    Write("                   ");
                    break;
            }
        }
    }
}
