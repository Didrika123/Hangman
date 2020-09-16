namespace HangmanGDI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelIncorrectGuesses = new System.Windows.Forms.Label();
            this.textboxGuess = new System.Windows.Forms.TextBox();
            this.labelCorrectGuesses = new System.Windows.Forms.Label();
            this.btnGuess = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(30, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(237, 210);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // labelIncorrectGuesses
            // 
            this.labelIncorrectGuesses.Location = new System.Drawing.Point(30, 347);
            this.labelIncorrectGuesses.Name = "labelIncorrectGuesses";
            this.labelIncorrectGuesses.Size = new System.Drawing.Size(227, 60);
            this.labelIncorrectGuesses.TabIndex = 1;
            // 
            // textboxGuess
            // 
            this.textboxGuess.Location = new System.Drawing.Point(64, 296);
            this.textboxGuess.Name = "textboxGuess";
            this.textboxGuess.Size = new System.Drawing.Size(100, 23);
            this.textboxGuess.TabIndex = 2;
            this.textboxGuess.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textboxGuess_KeyDown);
            // 
            // labelCorrectGuesses
            // 
            this.labelCorrectGuesses.AutoSize = true;
            this.labelCorrectGuesses.Location = new System.Drawing.Point(110, 250);
            this.labelCorrectGuesses.Name = "labelCorrectGuesses";
            this.labelCorrectGuesses.Size = new System.Drawing.Size(46, 15);
            this.labelCorrectGuesses.TabIndex = 4;
            this.labelCorrectGuesses.Text = "Correct";
            // 
            // btnGuess
            // 
            this.btnGuess.Location = new System.Drawing.Point(171, 295);
            this.btnGuess.Name = "btnGuess";
            this.btnGuess.Size = new System.Drawing.Size(75, 23);
            this.btnGuess.TabIndex = 5;
            this.btnGuess.Text = "Guess";
            this.btnGuess.UseVisualStyleBackColor = true;
            this.btnGuess.Click += new System.EventHandler(this.btnGuess_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(31, 329);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Incorrect Guesses";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 416);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGuess);
            this.Controls.Add(this.labelCorrectGuesses);
            this.Controls.Add(this.textboxGuess);
            this.Controls.Add(this.labelIncorrectGuesses);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Tag = "";
            this.Text = "Hangman";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelIncorrectGuesses;
        private System.Windows.Forms.TextBox textboxGuess;
        private System.Windows.Forms.Label labelCorrectGuesses;
        private System.Windows.Forms.Button btnGuess;
        private System.Windows.Forms.Label label1;
    }
}

