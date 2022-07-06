namespace Betting_app
{
    partial class FIR
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.MatchList = new System.Windows.Forms.ComboBox();
            this.Playing1 = new System.Windows.Forms.Label();
            this.Draw = new System.Windows.Forms.Label();
            this.Playing2 = new System.Windows.Forms.Label();
            this.Lock = new System.Windows.Forms.Button();
            this.Wynik = new System.Windows.Forms.Label();
            this.score1 = new System.Windows.Forms.NumericUpDown();
            this.score2 = new System.Windows.Forms.NumericUpDown();
            this.LockScore = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.score1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.score2)).BeginInit();
            this.SuspendLayout();
            // 
            // MatchList
            // 
            this.MatchList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MatchList.FormattingEnabled = true;
            this.MatchList.Location = new System.Drawing.Point(96, 12);
            this.MatchList.MaxDropDownItems = 21;
            this.MatchList.Name = "MatchList";
            this.MatchList.Size = new System.Drawing.Size(121, 21);
            this.MatchList.TabIndex = 3;
            this.MatchList.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Playing1
            // 
            this.Playing1.AutoSize = true;
            this.Playing1.Location = new System.Drawing.Point(96, 52);
            this.Playing1.Name = "Playing1";
            this.Playing1.Size = new System.Drawing.Size(0, 13);
            this.Playing1.TabIndex = 10;
            // 
            // Draw
            // 
            this.Draw.AutoSize = true;
            this.Draw.Location = new System.Drawing.Point(139, 52);
            this.Draw.Name = "Draw";
            this.Draw.Size = new System.Drawing.Size(32, 13);
            this.Draw.TabIndex = 11;
            this.Draw.Text = "Draw";
            // 
            // Playing2
            // 
            this.Playing2.AutoSize = true;
            this.Playing2.Location = new System.Drawing.Point(182, 52);
            this.Playing2.Name = "Playing2";
            this.Playing2.Size = new System.Drawing.Size(0, 13);
            this.Playing2.TabIndex = 26;
            // 
            // Lock
            // 
            this.Lock.Location = new System.Drawing.Point(99, 280);
            this.Lock.Name = "Lock";
            this.Lock.Size = new System.Drawing.Size(118, 23);
            this.Lock.TabIndex = 34;
            this.Lock.Text = "LOCK BET";
            this.Lock.UseVisualStyleBackColor = true;
            this.Lock.Click += new System.EventHandler(this.Lock_Click);
            // 
            // Wynik
            // 
            this.Wynik.AutoSize = true;
            this.Wynik.Location = new System.Drawing.Point(139, 337);
            this.Wynik.Name = "Wynik";
            this.Wynik.Size = new System.Drawing.Size(35, 13);
            this.Wynik.TabIndex = 35;
            this.Wynik.Text = "Score";
            // 
            // score1
            // 
            this.score1.Location = new System.Drawing.Point(98, 364);
            this.score1.Name = "score1";
            this.score1.Size = new System.Drawing.Size(36, 20);
            this.score1.TabIndex = 36;
            // 
            // score2
            // 
            this.score2.Location = new System.Drawing.Point(180, 364);
            this.score2.Name = "score2";
            this.score2.Size = new System.Drawing.Size(36, 20);
            this.score2.TabIndex = 37;
            // 
            // LockScore
            // 
            this.LockScore.Location = new System.Drawing.Point(99, 403);
            this.LockScore.Name = "LockScore";
            this.LockScore.Size = new System.Drawing.Size(118, 23);
            this.LockScore.TabIndex = 38;
            this.LockScore.Text = "LOCK SCORE";
            this.LockScore.UseVisualStyleBackColor = true;
            this.LockScore.Click += new System.EventHandler(this.button1_Click);
            // 
            // FIR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(787, 451);
            this.Controls.Add(this.LockScore);
            this.Controls.Add(this.score2);
            this.Controls.Add(this.score1);
            this.Controls.Add(this.Wynik);
            this.Controls.Add(this.Lock);
            this.Controls.Add(this.Playing2);
            this.Controls.Add(this.Draw);
            this.Controls.Add(this.Playing1);
            this.Controls.Add(this.MatchList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FIR";
            this.ShowIcon = false;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FIR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.score1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.score2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox MatchList;
        private System.Windows.Forms.Label Playing1;
        private System.Windows.Forms.Label Draw;
        private System.Windows.Forms.Label Playing2;
        private System.Windows.Forms.Button Lock;
        private System.Windows.Forms.Label Wynik;
        private System.Windows.Forms.NumericUpDown score1;
        private System.Windows.Forms.NumericUpDown score2;
        private System.Windows.Forms.Button LockScore;
    }
}

