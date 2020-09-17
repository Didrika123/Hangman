using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace HangmanGDI
{
    public partial class Form1 : Form 
    {
        private const int NUM_GUESSES = 10;
        private Hangman.Hangman _hangman = new Hangman.Hangman();
        readonly private Bitmap[] _hangmanBmp = new Bitmap[11];
        private bool redrawOnStartup = false;
        private Graphics pictureBoxGraphics;
        public Form1()
        {
            InitializeComponent();
            _hangman.UseWordsFromTextFile("Words.txt");
            _hangman.NewGame(NUM_GUESSES);
            labelCorrectGuesses.Text = _hangman.GetSecretWordProgress();
            pictureBoxGraphics = pictureBox1.CreateGraphics();
            for (int i = 0; i < 11; i++)
                _hangmanBmp[i] = new Bitmap(@"images\" + i + ".bmp");

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            DrawString();
            DrawHangman();
            if (!redrawOnStartup) //if i dont have this the picture wont be drawn until i move the form around or resize it or something
            {
                Refresh();
                redrawOnStartup = true;
            }
        }
        public void DrawString()
        {
            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            string drawString = _hangman.GetNumRemainingGuesses().ToString();// "Sample Text";
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            float x = 150.0F;
            float y = 50.0F;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            formGraphics.Dispose();
        }

        private void btnGuess_Click(object sender, EventArgs e)
        {
            PerformGuess();
        }
        private void textboxGuess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformGuess();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void DrawHangman()
        {
            int numCases = 10; //If you have fewer or more guesses than drawable cases then we try to calculate a proper case (Ex 5 start guesses would yield: case 0 -> case 2 -> case 4 -> case 6-> case 8 -> case 10)
            int calculatedStage = numCases - (int)(numCases * (double)_hangman.GetNumRemainingGuesses() / _hangman.GetStartingNumberOfGuesses());
            pictureBoxGraphics.DrawImage(_hangmanBmp[calculatedStage], 1, 1, pictureBox1.Width, pictureBox1.Height);

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            foreach (Bitmap bmp in _hangmanBmp)
                bmp.Dispose();
            base.OnClosing(e);
        }
        private void PerformGuess()
        {
            LetUserGuess(textboxGuess.Text);
            labelCorrectGuesses.Text = _hangman.GetSecretWordProgress();
            labelIncorrectGuesses.Text = _hangman.GetIncorrectGuesses();

            if (_hangman.IsGameFinished())
            {
                if (_hangman.PlayerWon())
                {
                    DisplayMessage("You Win!\nPress OK to start a new game.", "Congratulations");
                }
                else
                {
                    DisplayMessage("You Lose!\nPress OK to start a new game.", "Game Over");
                }
                _hangman.NewGame(NUM_GUESSES);
                labelCorrectGuesses.Text = _hangman.GetSecretWordProgress();
                labelIncorrectGuesses.Text = _hangman.GetIncorrectGuesses();
            }
            textboxGuess.Clear();
            textboxGuess.Focus();
            InvokePaint(this, null);
        }
        private void LetUserGuess(string input)
        {
            try
            {
                _hangman.Guess(input);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException)
            {
                DisplayMessage(ex.Message, "Invalid Guess");
            }
            catch (Hangman.GameIsFinishedException)
            {
                _hangman.NewGame(NUM_GUESSES);
            }
        }
        private void DisplayMessage(string msg, string title)
        {
            int x = this.Location.X + this.Width / 4;
            int y = this.Location.Y + this.Height / 4;
            FindAndMoveMsgBox(x, y, true, title);
            MessageBox.Show(msg, title);
        }

        //In order to do a seemingly simple thing as moving a messagebox you need all this complex coding haha
        // Courtesy of Thomas Daniels : https://www.codeproject.com/tips/472294/position-a-windows-forms-messagebox-in-csharp
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(IntPtr classname, string title); // extern method: FindWindow

        [DllImport("user32.dll")]
        static extern void MoveWindow(IntPtr hwnd, int X, int Y,
            int nWidth, int nHeight, bool rePaint); // extern method: MoveWindow

        [DllImport("user32.dll")]
        static extern bool GetWindowRect
            (IntPtr hwnd, out Rectangle rect); // extern method: GetWindowRect
        void FindAndMoveMsgBox(int x, int y, bool repaint, string title)
        {
            Thread thr = new Thread(() => // create a new thread
            {
                IntPtr msgBox = IntPtr.Zero;
                // while there's no MessageBox, FindWindow returns IntPtr.Zero
                while ((msgBox = FindWindow(IntPtr.Zero, title)) == IntPtr.Zero) ;
                // after the while loop, msgBox is the handle of your MessageBox
                Rectangle r = new Rectangle();
                GetWindowRect(msgBox, out r); // Gets the rectangle of the message box
                MoveWindow(msgBox /* handle of the message box */, x, y,
                   r.Width - r.X /* width of originally message box */,
                   r.Height - r.Y /* height of originally message box */,
                   repaint /* if true, the message box repaints */);
            });
            thr.Start(); // starts the thread
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
