using PhotoZgop.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoZgop
{
    public partial class Form1 : Form
    {

        private List<LayerEntitys> layers;

        private int layersCount = 0;

        private int compCount = 1;

        //--------------------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();
            layers = new List<LayerEntitys>();
        }

        //--------------------------------------------------------------------------------

        private void button1_Click(object sender, EventArgs e)
        {

            

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            layers.Add(new LayerEntitys(filename, pictureBox1.Height, pictureBox1.Width, Operation.Non, ChoseRgb.RGB));

            listBox1.Items.Add(filename);

            pictureBox1.Image = layers[layersCount].bitmap;

            if (layersCount > 0)
            {

                comboBox1.Enabled = true;

                comboBox2.Enabled = true;

                button2.Enabled = true;
            }

            layersCount += 1;
        }

        //--------------------------------------------------------------------------------

        private void button2_Click(object sender, EventArgs e)
        {
                Bitmap bitmap = null;

                foreach (var item in layers)
                {
                    bitmap = item.renderingNew(bitmap);
                }

                layers.Clear();

                layers.Add(new LayerEntitys(bitmap, compCount, 640, 1060));

                pictureBox1.Image = layers[0].bitmap;

                layersCount = 1;

                listBox1.Items.Clear();

                listBox1.Items.Add($"Итоговая картинка {compCount}");
        }

        //--------------------------------------------------------------------------------

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                pictureBox1.Image = layers[listBox1.SelectedIndex].bitmap;

                if (listBox1.SelectedIndex <= 0)
                {
                    comboBox1.Enabled = false;

                    comboBox2.Enabled = false;
                }
                else
                {

                    comboBox1.Enabled = true;

                    comboBox2.Enabled = true;

                    comboBox1.SelectedIndex = (int)layers[listBox1.SelectedIndex].operation;

                    comboBox2.SelectedIndex = (int)layers[listBox1.SelectedIndex].choseRgb;
                }
            }
        }

        //--------------------------------------------------------------------------------

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {
                layers[listBox1.SelectedIndex].operation = (Operation)(comboBox1.SelectedIndex);
            }
        }

        //--------------------------------------------------------------------------------

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {
                layers[listBox1.SelectedIndex].choseRgb = (ChoseRgb)(comboBox2.SelectedIndex);
            }
        }

        //--------------------------------------------------------------------------------
    }
}
