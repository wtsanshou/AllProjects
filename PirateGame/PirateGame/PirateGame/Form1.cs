using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace PirateGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        int[] test = { 0, 0, 0 };
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Up:
                    //when the person go up and meet the top, then stop
                    if (person.Top <= 0)
                    {
                        person.Top = 0;
                    }
                    //when push up the box and meet top, then stop
                    else if ((box1.Top == 0) && (person.Left == box1.Left) && (person.Top == box1.Bottom))
                    {
                        box1.Top = 0;
                    }
                    else if ((box2.Top == 0) && (person.Left == box2.Left) && (person.Top == box2.Bottom))
                    {
                        box2.Top = 0;
                    }
                    else if ((box3.Top == 0) && (person.Left == box3.Left) && (person.Top == box3.Bottom))
                    {
                        box3.Top = 0;
                    }
                        //when push up the box and meet wall1 and wall3, then stop
                    else if ((box1.Left == person.Left) && (person.Top == box1.Bottom) && (((box1.Top == wall3.Bottom)&&(box1.Left == wall3.Left)) || ((box1.Top == wall1.Bottom)&&(box1.Left == wall1.Left))))
                    {
                        person.Top = this.person.Top;
                        box1.Top = this.box1.Top;
                    }
                    else if ((box2.Left == person.Left) && (person.Top == box2.Bottom) && (((box2.Top == wall3.Bottom) && (box2.Left == wall3.Left)) || ((box2.Top == wall1.Bottom) && (box2.Left == wall1.Left))))
                    {
                        person.Top = this.person.Top;
                        box2.Top = this.box2.Top;
                    }
                    else if ((box3.Left == person.Left) && (person.Top == box3.Bottom) && (((box3.Top == wall3.Bottom) && (box3.Left == wall3.Left)) || ((box3.Top == wall1.Bottom) && (box3.Left == wall1.Left))))
                    {
                        person.Top = this.person.Top;
                        box3.Top = this.box3.Top;
                    }
                        //when push up a box and meet another box, then stop
                    else if ((box1.Left == person.Left) && (person.Top == box1.Bottom) && (((box1.Top == box2.Bottom) && (box1.Left == box2.Left)) || ((box1.Top == box3.Bottom) && (box1.Left == box3.Left))))
                    {
                        person.Top = this.person.Top;
                        box1.Top = this.box1.Top;
                    }
                    else if ((box2.Left == person.Left) && (person.Top == box2.Bottom) && (((box2.Top == box1.Bottom) && (box2.Left == box1.Left)) || ((box2.Top == box3.Bottom) && (box2.Left == box3.Left))))
                    {
                        person.Top = this.person.Top;
                        box2.Top = this.box2.Top;
                    }
                    else if ((box3.Left == person.Left) && (person.Top == box3.Bottom) && (((box3.Top == box2.Bottom) && (box3.Left == box2.Left)) || ((box3.Top == box1.Bottom) && (box1.Left == box3.Left))))
                    {
                        person.Top = this.person.Top;
                        box3.Top = this.box3.Top;
                    }

                        //when the person go up and meet wall1, wall2, wall3, then stop
                    else if ((wall3.Left == person.Left) && (person.Top == wall3.Top + 50))
                    {
                        person.Top = wall3.Top + 50;
                    }
                    else if ((wall1.Left == person.Left) && (person.Top == wall1.Top + 50))
                    {
                        person.Top = wall1.Top + 50;
                    }
                    else if ((wall2.Left == person.Left) && (person.Top == wall2.Top + 50))
                    {
                        person.Top = wall2.Top + 50;
                    }

                    else
                    {
                        //move the boxes
                        if ((person.Left == box1.Left) && (person.Top == box1.Bottom))
                        {
                            box1.Top -= 50;
                        }
                        else if ((person.Left == box2.Left) && (person.Top == box2.Bottom))
                        {
                            box2.Top -= 50;
                        }
                        else if ((person.Left == box3.Left) && (person.Top == box3.Bottom))
                        {
                            box3.Top -= 50;
                        }
                        person.Top -= 50;
                    }

                    break;
                case Keys.Down:
                    //when the person go down and meet the bottom, then stop
                    if (person.Bottom >= panelMap.Height)
                    {
                        person.Top = panelMap.Height - 50;
                    }
                        //when then person push down the boxes and meet the bottom, then stop
                    else if ((box1.Bottom == panelMap.Height) && (person.Left == box1.Left) && (person.Bottom == box1.Top))
                    {
                        box1.Top = panelMap.Height - 50;
                    }
                    else if ((box2.Bottom == panelMap.Height) && (person.Left == box2.Left) && (person.Bottom == box2.Top))
                    {
                        box2.Top = panelMap.Height - 50;
                    }
                    else if ((box3.Bottom == panelMap.Height) && (person.Left == box3.Left) && (person.Bottom == box3.Top))
                    {
                        box3.Top = panelMap.Height - 50;
                    }
                    //when push down the box and meet wall2 and wall4, then stop
                    else if ((box1.Left == person.Left) && (person.Bottom == box1.Top) && (((box1.Left == wall2.Left) && (box1.Bottom == wall2.Top)) || ((box1.Left == wall4.Left) && (box1.Bottom == wall4.Top))))
                    {
                        person.Top = this.person.Top;
                        box1.Top = this.box1.Top;
                    }
                    else if ((box2.Left == person.Left) && (person.Bottom == box2.Top) && (((box2.Left == wall2.Left) && (box2.Bottom == wall2.Top)) || ((box2.Left == wall4.Left) && (box2.Bottom == wall4.Top))))
                    {
                        person.Top = this.person.Top;
                        box2.Top = this.box2.Top;
                    }
                    else if ((box3.Left == person.Left) && (person.Bottom == box3.Top) && (((box3.Left == wall2.Left) && (box3.Bottom == wall2.Top)) || ((box3.Left == wall4.Left) && (box3.Bottom == wall4.Top))))
                    {
                        person.Top = this.person.Top;
                        box3.Top = this.box3.Top;
                    }
                    //when push down the box and meet another box, then stop
                    else if ((box1.Left == person.Left) && (person.Bottom == box1.Top) && (((box1.Left == box2.Left) && (box1.Bottom == box2.Top)) || ((box1.Left == box3.Left) && (box1.Bottom == box3.Top))))
                    {
                        person.Top = this.person.Top;
                        box1.Top = this.box1.Top;
                    }
                    else if ((box2.Left == person.Left) && (person.Bottom == box2.Top) && (((box1.Left == box2.Left) && (box2.Bottom == box1.Top)) || ((box2.Left == box3.Left) && (box2.Bottom == box3.Top))))
                    {
                        person.Top = this.person.Top;
                        box2.Top = this.box2.Top;
                    }
                    else if ((box3.Left == person.Left) && (person.Bottom == box3.Top) && (((box3.Left == box2.Left) && (box3.Bottom == box2.Top)) || ((box1.Left == box3.Left) && (box3.Bottom == box1.Top))))
                    {
                        person.Top = this.person.Top;
                        box3.Top = this.box3.Top;
                    }

                        //when then person go down and meet wall1,wall2 and wall4, then stop
                    else if((person.Right == panelMap.Width)&&(person.Bottom == wall4.Top))
                    {
                        person.Top = wall4.Top - 50;   
                    }
                    else if ((wall1.Right == person.Right) && (person.Top == wall1.Top - 50))
                    {
                        person.Top = wall1.Top - 50;
                    }
                    else if ((wall2.Right == person.Right) && (person.Top == wall2.Top - 50))
                    {
                        person.Top = wall2.Top - 50;
                    }
                   
                    else
                    {
                        //move the boxes
                        if ((person.Left == box1.Left) && (person.Bottom == box1.Top))
                        {
                            box1.Top += 50;
                        }
                        else if ((person.Left == box2.Left) && (person.Bottom == box2.Top))
                        {
                            box2.Top += 50;
                        }
                        else if ((person.Left == box3.Left) && (person.Bottom == box3.Top))
                        {
                            box3.Top += 50;
                        }
                        person.Top += 50;
                    }
                    break;
                case Keys.Left:
                    
                    if (person.Left <= 0)
                    {
                        person.Left = 0;
                    }
                    else if ((box1.Left == 0) && (person.Top == box1.Top) && (person.Left == box1.Right))
                    {
                        box1.Left = 0;
                    }
                    else if ((box2.Left == 0) && (person.Top == box2.Top) && (person.Left == box2.Right))
                    {
                        box2.Left = 0;
                    }
                    else if ((box3.Left == 0) && (person.Top == box3.Top) && (person.Left == box3.Right))
                    {
                        box3.Left = 0;
                    }
                        //when push left boxes and meet wall3, then stop
                    else if ((box1.Top == wall3.Top) && (person.Top == wall3.Top) && (wall3.Right == box1.Left) && (box1.Right == person.Left))
                    {
                        person.Left = this.person.Left;
                        box1.Left = this.box1.Left;
                    }
                    else if ((box2.Top == wall3.Top) && (person.Top == wall3.Top) && (wall3.Right == box2.Left) && (box2.Right == person.Left))
                    {
                        person.Left = this.person.Left;
                        box2.Left = this.box2.Left;
                    }
                    else if ((box3.Top == wall3.Top) && (person.Top == wall3.Top) && (wall3.Right == box3.Left) && (box3.Right == person.Left))
                    {
                        person.Left = this.person.Left;
                        box3.Left = this.box3.Left;
                    }

                        //when push left the box and meet another box, then stop
                    else if ((box1.Top == person.Top) && (person.Left == box1.Right) && (((box1.Top == box2.Top) && (box1.Left == box2.Right)) || ((box1.Top == box3.Top) && (box1.Left == box3.Right))))
                    {
                        person.Left = this.person.Left;
                        box1.Left = this.box1.Left;
                    }
                    else if ((box2.Top == person.Top) && (person.Left == box2.Right) && (((box1.Top == box2.Top) && (box2.Left == box1.Right)) || ((box2.Top == box3.Top) && (box2.Left == box3.Right))))
                    {
                        person.Left = this.person.Left;
                        box2.Left = this.box2.Left;
                    }
                    else if ((box3.Top == person.Top) && (person.Left == box3.Right) && (((box3.Top == box2.Top) && (box3.Left == box2.Right)) || ((box1.Top == box3.Top) && (box3.Left == box1.Right))))
                    {
                        person.Left = this.person.Left;
                        box3.Left = this.box3.Left;
                    }

                    else if ((person.Left <= wall3.Left + 50) && (person.Top == wall3.Top))
                    {
                        person.Left = wall3.Left + 50;
                    }
                    else if ((wall1.Top == person.Top) && (person.Left == wall1.Right))
                    {
                        person.Left = wall1.Right;
                    }
                    else if ((wall2.Top == person.Top) && (person.Left == wall2.Right))
                    {
                        person.Left = wall2.Right;
                    }

                    else
                    {
                        if ((person.Top == box1.Top) && (person.Left == box1.Right))
                        {
                            box1.Left -= 50;
                        }
                        else if ((person.Top == box2.Top) && (person.Left == box2.Right))
                        {
                            box2.Left -= 50;
                        }
                        if ((person.Top == box3.Top) && (person.Left == box3.Right))
                        {
                            box3.Left -= 50;
                        }
                        person.Left -= 50;

                    }
                    break;
                case Keys.Right:
                    if (person.Left == panelMap.Width - 50)
                    {
                        person.Left = panelMap.Width - 50;
                    }
                    else if ((box1.Left == panelMap.Width - 50) && (person.Top == box1.Top) && (person.Right == box1.Left))
                    {
                        box1.Left = panelMap.Width - 50;
                    }
                    else if ((box2.Left == panelMap.Width - 50) && (person.Top == box2.Top) && (person.Right == box2.Left))
                    {
                        box2.Left = panelMap.Width - 50;
                    }
                    else if ((box3.Left == panelMap.Width - 50) && (person.Top == box3.Top) && (person.Right == box3.Left))
                    {
                        box3.Left = panelMap.Width - 50;
                    }
                        //when push right boxes and meet wall1, wall2 and wall5, then stop
                    else if ((box1.Top == person.Top) && (person.Right == box1.Left) && (((box1.Top == wall1.Top) && (box1.Right == wall1.Left)) || ((box1.Top == wall2.Top) && (box1.Right == wall2.Left)) || ((box1.Top == wall5.Top) && (box1.Right == wall5.Left))))
                    {
                        person.Left = this.person.Left;
                        box1.Left = this.box1.Left;
                    }
                    else if ((box2.Top == person.Top) && (person.Right == box2.Left) && (((box2.Top == wall1.Top) && (box2.Right == wall1.Left)) || ((box2.Top == wall2.Top) && (box2.Right == wall2.Left)) || ((box2.Top == wall5.Top) && (box2.Right == wall5.Left))))
                    {
                        person.Left = this.person.Left;
                        box2.Left = this.box2.Left;
                    }
                    else if ((box3.Top == person.Top) && (person.Right == box3.Left) && (((box3.Top == wall1.Top) && (box3.Right == wall1.Left)) || ((box3.Top == wall2.Top) && (box3.Right == wall2.Left)) || ((box3.Top == wall5.Top) && (box3.Right == wall5.Left))))
                    {
                        person.Left = this.person.Left;
                        box3.Left = this.box3.Left;
                    }
                    //when push left the box and meet another box, then stop
                    else if ((box1.Top == person.Top) && (person.Right == box1.Left) && (((box1.Top == box2.Top) && (box1.Right == box2.Left)) || ((box1.Top == box3.Top) && (box1.Right == box3.Left))))
                    {
                        person.Left = this.person.Left;
                        box1.Left = this.box1.Left;
                    }
                    else if ((box2.Top == person.Top) && (person.Right == box2.Left) && (((box1.Top == box2.Top) && (box2.Right == box1.Left)) || ((box2.Top == box3.Top) && (box2.Right == box3.Left))))
                    {
                        person.Left = this.person.Left;
                        box2.Left = this.box2.Left;
                    }
                    else if ((box3.Top == person.Top) && (person.Right == box3.Left) && (((box3.Top == box2.Top) && (box3.Right == box2.Left)) || ((box1.Top == box3.Top) && (box3.Right == box1.Left))))
                    {
                        person.Left = this.person.Left;
                        box3.Left = this.box3.Left;
                    }
                    //
                    else if (((person.Top == wall4.Top) || (person.Top == wall5.Top)) && (person.Right == wall5.Left))
                    {
                        person.Left = wall5.Left - 50;
                    }
                    else if ((wall1.Top == person.Top) && (person.Right == wall1.Left))
                    {
                        person.Left = wall1.Left - 50;
                    }
                    else if ((wall2.Top == person.Top) && (person.Right == wall2.Left))
                    {
                        person.Left = wall2.Left - 50;
                    }

                    else
                    {
                        if ((person.Top == box1.Top) && (person.Right == box1.Left))
                        {
                            box1.Left += 50;
                        }
                        else if ((person.Top == box2.Top) && (person.Right == box2.Left))
                        {
                            box2.Left += 50;
                        }
                        else if ((person.Top == box3.Top) && (person.Right == box3.Left))
                        {
                            box3.Left += 50;
                        }
                        person.Left += 50;
                    }
                    break;
            }
            
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            
            //Point[] island = {island1.Location, island2.Location, island3.Location };
            //Point[] box = { box1.Location, box2.Location, box3.Location };
            int[] islandX = { island1.Location.X, island2.Location.X, island3.Location.X };
            int[] islandY = { island1.Location.Y, island2.Location.Y, island3.Location.Y };
            int[] boxX = { box1.Location.X, box2.Location.X, box3.Location.X };
            int[] boxY = { box1.Location.Y, box2.Location.Y, box3.Location.Y };
            int n = islandX.Length;
            //Point key;
            int key = 0;
            int i = 0;
            for (i = 0; i < n; i++)
            {
                if (islandX[0].Equals(boxX[i]) && islandY[0].Equals(boxY[i]))
                {
                    key = islandX[0];
                    islandX[0] = islandX[n - 1];
                    islandX[n - 1] = key;

                    key = islandY[0];
                    islandY[0] = islandY[n - 1];
                    islandY[n - 1] = key;

                    key = boxX[i];
                    boxX[i] = boxX[n - 1];
                    boxX[n - 1] = key;

                    key = boxY[i];
                    boxY[i] = boxY[n - 1];
                    boxY[n - 1] = key;

                    i = 0;
                    n = n - 1;
                }
            }
            
            label1.Text = Convert.ToString(n);

            
            if (n == 1)
            {
                DialogResult win = MessageBox.Show("Congratulations! you win! Do you want play again?","",
                    MessageBoxButtons.YesNo);
                if (win == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
            
            
          

        }
    }
}
