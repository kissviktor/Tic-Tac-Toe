using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            GenerateButtons();
        }

        public static int size = 3;

        Button[,] buttons = new Button[size, size];

        private void GenerateButtons()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new Size(450 / size, 450 / size);
                    buttons[i, j].Location = new Point(i * (450 / size), j * (450 / size));
                    buttons[i, j].FlatStyle = FlatStyle.Flat;
                    buttons[i, j].Font = new System.Drawing.Font(DefaultFont.FontFamily, 80, FontStyle.Bold);

                    //Define button click Event
                    buttons[i, j].Click += new EventHandler(button_Click);

                    //Add buttons/tiles to the panel
                    panel1.Controls.Add(buttons[i, j]);
                }
            }
        }

        void button_Click(object sender, EventArgs e)
        {
            //Load the clicked button into a variable
            Button button = sender as Button;

            //Don't do anything if the selected tile is already marked
            if (button.Text != "")
            {
                return;
            }

            //Mark the tile with the current player's icon
            button.Text = PlayerButton.Text;

            TogglePlayer();

        }

        private void MakeAIMove() // Az AI sajnos autizmussal küzd, megértését köszönjük.
        {
            bool unique = false;
            Random rnd = new Random();
            while (!unique)
            {
                int a = rnd.Next(0, 3);
                int b = rnd.Next(0, 3);

                if (buttons[a, b].Text == "")
                {
                    buttons[a, b].PerformClick();
                    unique = true;
                }

            }
        }

        private void TogglePlayer()
        {
            CheckIfGameEnds();

            if (PlayerButton.Text == "X")
            {
                PlayerButton.Text = "O";
                MakeAIMove();
            }
            else
            {
                PlayerButton.Text = "X";
            }

        }

        private void CheckIfGameEnds()
        {
            List<Button> winnerButtons = new List<Button>();

            //Vertically
            for (int i = 0; i < size; i++)
            {
                winnerButtons = new List<Button>();
                for (int j = 0; j < size; j++)
                {
                    if (buttons[i, j].Text != PlayerButton.Text)
                    {
                        break;
                    }

                    winnerButtons.Add(buttons[i, j]);

                    if (j == (size - 1))
                    {
                        ShowWinner(winnerButtons);
                        //return;
                        ResetGame();
                    }
                }
            }

            //Horizontally
            for (int i = 0; i < size; i++)
            {
                winnerButtons = new List<Button>();
                for (int j = 0; j < size; j++)
                {
                    if (buttons[j, i].Text != PlayerButton.Text)
                    {
                        break;
                    }

                    winnerButtons.Add(buttons[j, i]);

                    if (j == (size - 1))
                    {
                        ShowWinner(winnerButtons);
                        //return;
                        ResetGame();
                    }
                }
            }

            //Diagonally (top L => botton R)
            for (int i = 0, j = 0; i < size; i++, j++)
            {
                if (buttons[i, j].Text != PlayerButton.Text)
                {
                    break;
                }

                winnerButtons.Add(buttons[i, j]);

                if (j == (size - 1))
                {
                    ShowWinner(winnerButtons);
                    //return;
                    ResetGame();
                }
            }

            //Diagonally (bottom L => top R)
            for (int i = 2, j = 0; j < size; i--, j++)
            {
                if (buttons[i, j].Text != PlayerButton.Text)
                {
                    break;
                }

                winnerButtons.Add(buttons[i, j]);

                if (j == (size - 1))
                {
                    ShowWinner(winnerButtons);
                    //return;
                    ResetGame();

                }
            }

            //Check if all the tiles are marked
            foreach (var button in buttons)
            {
                if (button.Text == "")
                {
                    return;
                }
            }

            MessageBox.Show("Game Draw");
            ResetGame();
        }

        private void ShowWinner(List<Button> winnerButtons)
        {
            //Color all the winner's tiles
            foreach (var button in winnerButtons)
            {
                button.BackColor = Color.Red;
            }
            MessageBox.Show("Player " + PlayerButton.Text + " Wins!");

        }

        private void ResetGame()
        {
            PlayerButton.Text = "X";

            foreach (var button in buttons)
            {
                button.BackColor = Color.White;
                button.Text = "";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            size = 3;
            ResetGame();
        }

    }
}
