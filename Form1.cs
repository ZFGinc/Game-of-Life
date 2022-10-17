using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveGame
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private int resolution;

        private GameEngine gameEngine;

        public Form1()
        {
            InitializeComponent();
        }

        private void UIControll(bool isEnabled = true)
        {
            numericUpDown1.Enabled = isEnabled;
            numericUpDown2.Enabled = isEnabled;
            numericUpDown3.Enabled = isEnabled;
            checkBox1.Enabled = isEnabled;
            checkBox2.Enabled = isEnabled;
            comboBox1.Enabled = isEnabled;
        }

        private void Start()
        {
            if (timer1.Enabled) return;

            UIControll(false);

            resolution = (int)numericUpDown1.Value;
            bool isStruct = !checkBox1.Checked;
            int key = (!isStruct) ? (int)numericUpDown2.Value : (int)comboBox1.SelectedIndex;

            gameEngine = new GameEngine
            (
                rows: pictureBox1.Height / resolution,
                cols: pictureBox1.Width / resolution,
                isStruct: isStruct,
                key: key,
                alive: textBox1.Text,
                dead: textBox2.Text
            );

            label5.Text = gameEngine.GetNumbersData();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);

            timer1.Interval = (int)numericUpDown3.Value;
            timer1.Start();
        }      

        private void timer1_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);

            bool[,] currentArea = gameEngine.GetCurrentGeneration();

            for (int i = 0; i < currentArea.GetLength(0); i++)
            {
                for (int j = 0; j < currentArea.GetLength(1); j++)
                {
                    if (currentArea[i, j]) 
                        graphics.FillRectangle(Brushes.Green, i * resolution, j * resolution, resolution, resolution);
                }
            }

            gameEngine.NextGeneration();
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) return;

            timer1.Stop();
            UIControll(true);
        }
        

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                int x = e.Location.X / resolution;
                int y = e.Location.Y / resolution;
                //if (isValidMousePos(x, y)) Area[x, y] = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                int x = e.Location.X / resolution;
                int y = e.Location.Y / resolution;
                //if (isValidMousePos(x, y)) Area[x, y] = false;
            }
        }

        //wprivate bool isValidMousePos(int x, int y)
        //{
        //    return x >= 0 && y >= 0 && x < cols && y < rows;
        //}
        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked) checkBox2.Checked = !checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) checkBox1.Checked = !checkBox2.Checked;
        }
    }
}
