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
    
    public partial class FIR : Form
    {
        string[] Matches;
        bool[,,] bet_state;
        bool[] bet_locks;
        bool[] score_lock;
        int nr_players;
        int[] scores1;
        int[] scores2;
        Player[] players;
        private System.Data.DataSet dataSet;
        public void load_matches_and_players()
        {
            Matches = System.IO.File.ReadAllLines("Matches.txt");
            string[] Players_names = System.IO.File.ReadAllLines("Players.txt");
            nr_players = Players_names.Length;
            dataGridView1.ColumnCount = 10;
            dataGridView1.Columns[0].Name = "Imię";
            dataGridView1.Columns[1].Name = "RM";
            dataGridView1.Columns[2].Name = "Z";
            dataGridView1.Columns[3].Name = "R";
            dataGridView1.Columns[4].Name = "P";
            dataGridView1.Columns[5].Name = "BZ ";
            dataGridView1.Columns[6].Name = "BS";
            dataGridView1.Columns[7].Name = "+/-";
            dataGridView1.Columns[8].Name = "PKT";
            dataGridView1.Columns[9].Name = "$";
            for (int i = 0; i < Matches.Length; i++)
            {
                string[] subs = Matches[i].Split(' ');
                Matches[i] = String.Concat((i + 1).ToString(), ". ", subs[0], " vs ", subs[1]);
            }
            bet_state = new bool[Matches.Length, nr_players, 3];
            bet_locks = new bool[Matches.Length];
            score_lock=new bool[Matches.Length];
            players = new Player[nr_players];
            scores1 = new int[Matches.Length];
            scores2 = new int[Matches.Length];
            for (int i = 0; i < nr_players; i++)
            {
                players[i] = new Player(Players_names[i]);
                players[i].boxes = new CheckBox[3];
                Label player_label = new Label();
                player_label.AutoSize = true;
                player_label.Location = new System.Drawing.Point(12, 75 + 25 * (i));
                player_label.Name = Players_names[i];
                player_label.Text = Players_names[i];
                Controls.Add(player_label);
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = Players_names[i];
                string[] buf_names = new string[3];
                for (int j = 0; j < 3; j++)
                {
                    string name = string.Concat("P", (i + 1).ToString(), "B", j.ToString());
                    buf_names[j] = name;
                    players[i].boxes[j]= new CheckBox();
                    players[i].boxes[j].Name = name;
                    players[i].boxes[j].AutoSize = true;
                    players[i].boxes[j].Location = new System.Drawing.Point(106 + 43 * j, 74 + 26 * i);
                    players[i].boxes[j].Size = new System.Drawing.Size(15, 14);
                    players[i].boxes[j].UseVisualStyleBackColor = true;
                    players[i].boxes[j].CheckedChanged += new System.EventHandler(this.Box_click_handler);
                    Controls.Add(players[i].boxes[j]);
                }
            }
            Update_table();
            
        }
        public void update_boxes(object sender)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked)
            {
                for (int i = 0; i < nr_players; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (players[i].boxes[j] == chk)
                        {
                            players[i].boxes[(j + 1) % 3].Checked = false;
                            players[i].boxes[(j + 2) % 3].Checked = false;
                            bet_state[MatchList.SelectedIndex, i, j] = chk.Checked;
                            bet_state[MatchList.SelectedIndex, i, (j + 1) % 3] = false;
                            bet_state[MatchList.SelectedIndex, i, (j + 2) % 3] = false;

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
            for (int i = 0; i < nr_players; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    players[i].boxes[j].Checked = bet_state[idx, i, j];
                    players[i].boxes[j].Enabled = !lock_state;
                }
            }
        }
        public void CalculateBet(int Outcome)
        {
            float pot = 0;
            float nr_winners=0;
            foreach(Player p in players)
            {
                if (p.boxes[Outcome].Checked)
                {
                    nr_winners++;
                }
            }
            if (nr_winners > 0)
            {
                foreach (Player p in players)
                {
                    if (!p.boxes[Outcome].Checked)
                    {
                        foreach (CheckBox box in p.boxes)
                        {
                            if (box.Checked)
                            {
                                pot++;
                                p.Cash--;
                                break;
                            }
                        }
                    }
                }
                foreach (Player p in players)
                {
                    if (p.boxes[Outcome].Checked)
                    {
                        p.Cash += pot / nr_winners;
                    }
                }
            }

        }
        public void Update_table()
        {
            for(int i=0;i<nr_players;i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = players[i].wins + players[i].loses + players[i].draws;
                dataGridView1.Rows[i].Cells[2].Value = players[i].wins;
                dataGridView1.Rows[i].Cells[3].Value = players[i].draws;
                dataGridView1.Rows[i].Cells[4].Value = players[i].loses;
                dataGridView1.Rows[i].Cells[5].Value = players[i].goals_got;
                dataGridView1.Rows[i].Cells[6].Value = players[i].goals_lost;
                dataGridView1.Rows[i].Cells[7].Value = players[i].Bilans;
                dataGridView1.Rows[i].Cells[8].Value = players[i].points;
                dataGridView1.Rows[i].Cells[9].Value = players[i].Cash;
            }
            dataGridView1.AutoResizeColumns();
            var totalHeight = dataGridView1.ColumnHeadersHeight;
            var totalWidth = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                totalHeight += row.Height;
            }
            foreach (DataGridViewColumn row in dataGridView1.Columns)
            {
                totalWidth += row.Width;
            }
            dataGridView1.ClientSize = new Size(totalWidth, totalHeight);
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
            for (int i = 0; i < nr_players; i++)
            {
                if (players[i].Name == Playing1.Text)
                {
                    foreach (CheckBox chk in players[i].boxes)
                    {
                        chk.Enabled = false;
                    }
                }
                else if (players[i].Name == Playing2.Text)
                {
                    foreach (CheckBox chk in players[i].boxes)
                    {
                        chk.Enabled = false;
                    }
                }
            }
            score1.Value=scores1[MatchList.SelectedIndex];
            score2.Value = scores2[MatchList.SelectedIndex];
            score1.Enabled = !score_lock[MatchList.SelectedIndex];
            score2.Enabled = !score_lock[MatchList.SelectedIndex];
        }

        private void Box_click_handler(object sender, EventArgs e)
        {
            update_boxes(sender);
        }

        private void Lock_Click(object sender, EventArgs e)
        {
            bet_locks[MatchList.SelectedIndex] = true;
            for (int i = 0; i < nr_players; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    players[i].boxes[j].Enabled = false;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bet_locks[MatchList.SelectedIndex]&& !score_lock[MatchList.SelectedIndex])
            {
                score_lock[MatchList.SelectedIndex] = true;
                int score = (int)score1.Value - (int)score2.Value;
                int idx1=0, idx2=0;
                for (int i= 0;i < nr_players;i++)
                {
                    if (players[i].Name ==Playing1.Text)
                    {
                        idx1 = i;
                    }
                    if (players[i].Name == Playing2.Text)
                    {
                        idx2 = i;
                    }
                }
                players[idx1].goals_got += (int)score1.Value;
                players[idx2].goals_got += (int)score2.Value;
                players[idx1].goals_lost += (int)score2.Value;
                players[idx2].goals_lost += (int)score1.Value;
                if (score > 0)
                {
                    
                    players[idx1].wins++;
                    players[idx1].points+=3;
                    players[idx2].loses++;
                    CalculateBet(0);
                }
                else if(score < 0)
                {
                    players[idx1].loses++;
                    players[idx2].wins++;
                    players[idx2].points += 3;
                    CalculateBet(2);
                }
                else
                {
                    players[idx1].draws++;
                    players[idx1].points += 1;
                    players[idx2].draws++;
                    players[idx2].points += 1;
                    CalculateBet(1);
                }
                players[idx1].Bilans = players[idx1].goals_got - players[idx1].goals_lost;
                players[idx2].Bilans = players[idx2].goals_got - players[idx2].goals_lost;
                score1.Enabled = false;
                score2.Enabled = false;
                Update_table();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void score1_ValueChanged(object sender, EventArgs e)
        {
            scores1[MatchList.SelectedIndex] = (int)score1.Value;
        }

        private void score2_ValueChanged(object sender, EventArgs e)
        {
            scores2[MatchList.SelectedIndex] = (int)score2.Value;
        }
    }
    public class Player
    {
        public Player(string name)
        {
            Name = name;
        }
        public string Name;
        public int points;
        public int Bilans;
        public int goals_got;
        public int goals_lost;
        public float Cash;
        public CheckBox[] boxes;
        public int wins;
        public int draws;
        public int loses;
    }
}
