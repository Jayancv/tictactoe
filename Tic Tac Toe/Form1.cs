﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySQLClass;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        bool turn = true;
        bool computer = true;
        int turnCount = 0;
        String p, p1, p1;
        int WonP, WonP1, WonP2, DefP, DefP1, DefP2;
        
        //check
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By jayan", "Abot tic tac toe");    
            }

        private void btnClick(object sender, EventArgs e)
        
        {
            //if (!computer) { 

            Button b = (Button)sender;
            if (turn == true)
                b.Text = "X";
            else
                b.Text = "O";
            turn = !turn;
            b.Enabled = false;
            turnCount++;
            checkWinner();
           // }
            if ((!turn) && (computer) && (turnCount<9))
            {
                computer_make_move();
            }
        }


        private void checkWinner()
        {
            bool theWin = false;
            if (A1.Text == A2.Text && A2.Text == A3.Text && (!A1.Enabled))
                theWin = true;
            if (B1.Text == B2.Text && B2.Text == B3.Text && (!B1.Enabled))
                theWin = true;
            if (C1.Text == C2.Text && C2.Text == C3.Text && (!C1.Enabled))
                theWin = true;
            if (A1.Text == B1.Text && B1.Text == C1.Text && (!A1.Enabled))
                theWin = true;
            if (A2.Text == B2.Text && B2.Text == C2.Text && (!A2.Enabled))
                theWin = true;
            if (A3.Text == B3.Text && B3.Text == C3.Text && (!A3.Enabled))
                theWin = true;
            if (A1.Text == B2.Text && B2.Text == C3.Text && (!A1.Enabled))
                theWin = true;
            if (A3.Text == B2.Text && B2.Text == C1.Text && (!A3.Enabled))
                theWin = true;

            if(theWin){
                disable();
                String winner;
                if(turn)
                    winner="O";
                else
                    winner="X";
                if (computer && (winner == "O"))
                    MessageBox.Show( "Computer won the game !","Winner");
                else
                    MessageBox.Show(winner +" won the game !", "Winner");
            }
            else
            {
               if( turnCount==9)
                   MessageBox.Show("game Over Draw!", "Loose");

            }
        }
        private void disable()
        {
            try
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
            }
            catch { }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

            turn = true;
            turnCount = 0;
            foreach (Control c in Controls)
            {
                try { 
                Button b = (Button)c;
                b.Text = "";
                b.Enabled = true;
                }
                catch { }
            }
        }

        private void mouse_Enter(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                if (turn)
                    b.Text = "X";
                else
                    b.Text = "O";
            }
        }

        private void mouse_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                if (turn)
                    b.Text = "";
                else
                    b.Text = "";
            }
        }

        private void single(object sender, EventArgs e)
        {
            computer = true;
            panel2.Visible = true;
            turn = true;
        }

        private void multi(object sender, EventArgs e)
        {
            computer = false;
            panel1.Visible=true;

        }




        private void computer_make_move()
        {
            //priority 1:  get tick tac toe
            //priority 2:  block x tic tac toe
            //priority 3:  go for corner space
            //priority 4:  pick open space
 
            Button move = null;
 
            move = look_for_win_or_block("O"); 
            if (move == null)
            {
                move = look_for_win_or_block("X");
                if (move == null)
                {
                    move = look_for_corner();
                    if (move == null)
                    {
                        move = look_for_open_space();
                    }
                }
            }

            move.PerformClick();
        }
 
        private Button look_for_open_space()
        {
            Console.WriteLine("Looking for open space");
            Button b = null;
            foreach (Control c in Controls)
            {
                b = c as Button;
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }//end if
            }//end if
           
           return null;
        }
 
        private Button look_for_corner()
        {
            Console.WriteLine("Looking for corner");
            if (A1.Text == "O")
            {
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }
 
            if (A3.Text == "O")
            {
                if (A1.Text == "")
                    return A1;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }
 
            if (C3.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C1.Text == "")
                    return C1;
            }
 
            if (C1.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
            }
           
            if (A1.Text == "")
                return A1;
            if (A3.Text == "")
                return A3;
            if (C1.Text == "")
                return C1;
            if (C3.Text == "")
                return C3;
 
            return null;
        }
 
        private Button look_for_win_or_block(string mark)
        {
            Console.WriteLine("Looking for win or block:  " + mark);
            //HORIZONTAL TESTS
            if ((A1.Text == mark) && (A2.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A2.Text == mark) && (A3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (A3.Text == mark) && (A2.Text == ""))
                return A2;
 
            if ((B1.Text == mark) && (B2.Text == mark) && (B3.Text == ""))
                return B3;
            if ((B2.Text == mark) && (B3.Text == mark) && (B1.Text == ""))
                return B1;
            if ((B1.Text == mark) && (B3.Text == mark) && (B2.Text == ""))
                return B2;
 
            if ((C1.Text == mark) && (C2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((C2.Text == mark) && (C3.Text == mark) && (C1.Text == ""))
                return C1;
            if ((C1.Text == mark) && (C3.Text == mark) && (C2.Text == ""))
                return C2;
 
            //VERTICAL TESTS
            if ((A1.Text == mark) && (B1.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B1.Text == mark) && (C1.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C1.Text == mark) && (B1.Text == ""))
                return B1;
            Console.WriteLine("skip " );

            if ((A2.Text == mark) && (B2.Text == mark) && (C2.Text == ""))
                return C2;
            if ((B2.Text == mark) && (C2.Text == mark) && (A2.Text == ""))
                return A2;
            if ((A2.Text == mark) && (C2.Text == mark) && (B2.Text == ""))
                return B2;
 
            if ((A3.Text == mark) && (B3.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B3.Text == mark) && (C3.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C3.Text == mark) && (B3.Text == ""))
                return B3;
 
            //DIAGONAL TESTS
            if ((A1.Text == mark) && (B2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B2.Text == mark) && (C3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C3.Text == mark) && (B2.Text == ""))
                return B2;
 
            if ((A3.Text == mark) && (B2.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B2.Text == mark) && (C1.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C1.Text == mark) && (B2.Text == ""))
                return B2;
 
            return null;
        }

        private void check()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void repateGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGameToolStripMenuItem_Click(sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void submit_Click(object sender, EventArgs e)
        {
            if (player1.Text.Equals("") || player2.Text.Equals(""))
            {
                panel1.Visible = true;
            }
            else {
                panel1.Visible = false;
            }
        }

        private void submitP1_Click(object sender, EventArgs e)
        {
            if (singlePlayer.Text.Equals("")) {
                panel2.Visible = true;
            }
            else { panel2.Visible = false ; }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void btnCancelP2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void scoreToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
    


}
