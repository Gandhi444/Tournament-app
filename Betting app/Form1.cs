using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Betting_app
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
        }
        public string Name;
        public int Bilans;
        public int Cash;
        public string[] boxes;
        public int wins;
        public int draws;
        public int loses;
    }

    public partial class FIR : Form
    {
        string[] Matches;
        bool[,,] bet_state;
        bool[] bet_locks;
        int Player_nr;
        int[] scores;
        Player[] players;
        public void Create_new_box(string name, int x, int y)
        {
            CheckBox b = new CheckBox();
            b.Name = name;
            b.AutoSize = true;
            b.Location = new System.Drawing.Point(106 + 43 * x, 74 + 26 * y);
            b.Size = new System.Drawing.Size(15, 14);
            b.UseVisualStyleBackColor = true;
            b.CheckedChanged += new System.EventHandler(this.Box_click_handler);
            Controls.Add(b);
        }
        public void load_matches_and_players()
        {
            Matches = System.IO.File.ReadAllLines("Matches.txt");
            string[] Players_names = System.IO.File.ReadAllLines("Players.txt");
            Player_nr = Players_names.Length;
            for (int i = 0; i < Matches.Length; i++)
            {
                string[] subs = Matches[i].Split(' ');
                Matches[i] = String.Concat((i + 1).ToString(), ". ", subs[0], " vs ", subs[1]);
            }
            bet_state = new bool[Matches.Length, Player_nr, 3];
            bet_locks = new bool[Matches.Length];
            scores = new int[Matches.Length];
            players = new Player[Player_nr];
            for (int i = 0; i < Player_nr; i++)
            {
                players[i] = new Player(Players_names[i]);
                Label player_label = new Label();
                player_label.AutoSize = true;
                player_label.Location = new System.Drawing.Point(12, 75 + 25 * (i));
                player_label.Name = Players_names[i];
                player_label.Text = Players_names[i];
                Controls.Add(player_label);
                string[] buf_names = new string[3];
                for (int j = 0; j < 3; j++)
                {
                    string name = string.Concat("P", (i + 1).ToString(), "B", j.ToString());
                    buf_names[j] = name;
                    Create_new_box(name, j, i);
                }
                players[i].boxes = buf_names;
            }
        }
        public void update_boxes(object sender)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked)
            {
                for (int i = 0; i < Player_nr; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (players[i].boxes[j] == chk.Name)
                        {
                            CheckBox buf1 = (CheckBox)Controls.Find(players[i].boxes[(j + 1) % 3], false)[0];
                            CheckBox buf2 = (CheckBox)Controls.Find(players[i].boxes[(j + 2) % 3], false)[0];
                            buf1.Checked = false; buf2.Checked = false;
                            bet_state[MatchList.SelectedIndex, i, j] = chk.Checked;
                            bet_state[MatchList.SelectedIndex, i, (j + 1) % 3] = buf1.Checked;
                            bet_state[MatchList.SelectedIndex, i, (j + 2) % 3] = buf2.Checked;
                            return;
                        }
                    }
                }
            }
        }
        public void load_bet_state()
        {
            int idx = MatchList.SelectedIndex;
            bool lock_state = bet_locks[MatchList.SelectedIndex];
            for (int i = 0; i < Player_nr; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    CheckBox buf = (CheckBox)Controls.Find(players[i].boxes[j], false)[0];
                    buf.Checked = bet_state[idx, i, j];
                    buf.Enabled = !lock_state;
                }
            }
        }
        public FIR()
        {
            InitializeComponent();
        }
        private void FIR_Load(object sender, EventArgs e)
        {
            load_matches_and_players();
            MatchList.Items.AddRange(Matches);
            MatchList.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string current = MatchList.Items[MatchList.SelectedIndex].ToString();
            Playing1.Text = current.Split(' ')[1];
            Playing2.Text = current.Split(' ')[3];
            load_bet_state();
        }

        private void Box_click_handler(object sender, EventArgs e)
        {
            update_boxes(sender);
        }

        private void Lock_Click(object sender, EventArgs e)
        {
            bet_locks[MatchList.SelectedIndex] = true;
            for (int i = 0; i < Player_nr; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    CheckBox buf = (CheckBox)Controls.Find(players[i].boxes[j], false)[0];
                    buf.Enabled = false;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bet_locks[MatchList.SelectedIndex])
            {
                int score = (int)score1.Value - (int)score2.Value;
                if (score < 0)
                {
                    scores[MatchList.SelectedIndex] = -1;
                }
                if (score == 0)
                {
                    scores[MatchList.SelectedIndex] = 2;
                }
                if (score > 0)
                {
                    scores[MatchList.SelectedIndex] = 1;
                }
                for (int i = 0; i < Player_nr; i++)
                {
                    if (players[i].Name == Playing1.Text)
                    {
                        players[i].Bilans += score;
                    }
                    if (scores[MatchList.SelectedIndex] == 1)
                    {
                        players[i].wins++;
                    }
                    if (scores[MatchList.SelectedIndex] == 2)
                    {
                        players[i].draws++;
                    }
                    if (scores[MatchList.SelectedIndex] == -1)
                    {
                        players[i].loses++;
                    }
                }
                score = -1 * score;
                for (int i = 0; i < Player_nr; i++)
                {
                    if (players[i].Name == Playing2.Text)
                    {
                        players[i].Bilans += score;
                    }
                    if (scores[MatchList.SelectedIndex] == 1)
                    {
                        players[i].wins++;
                    }
                    if (scores[MatchList.SelectedIndex] == 2)
                    {
                        players[i].draws++;
                    }
                    if (scores[MatchList.SelectedIndex] == -1)
                    {
                        players[i].loses++;
                    }
                }
                score1.Enabled = false;
                score2.Enabled = false;
            }
        }
    }
}
