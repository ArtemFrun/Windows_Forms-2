using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MDI_приложение
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //Открыть новое окно без данных
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form fr = new Form();
            fr.MdiParent = this;

            RichTextBox rtb = new RichTextBox();
            rtb.Dock = DockStyle.Fill;

            fr.Controls.Add(rtb);
            fr.Show();

        }


        //Открыть файл
        private void ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Form fr = new Form();
                fr.MdiParent = this;

                RichTextBox rtb = new RichTextBox();
                rtb.Dock = DockStyle.Fill;
                using (StreamReader sr = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.Default))
                {
                    rtb.Text = sr.ReadToEnd();
                }
                fr.Controls.Add(rtb);
                fr.Show();
            }
        }


        //Сохранить файл как
        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
                saveFileDialog1.FileName = "";
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.ValidateNames = true;
                saveFileDialog1.FilterIndex = 1;
            }
        }


        //Копировать текст с активного окна
        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = this.ActiveMdiChild.Controls[0] as RichTextBox;
            rtb.Copy();
        }

        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = this.ActiveMdiChild.Controls[0] as RichTextBox;
            rtb.Cut();
        }

        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = this.ActiveMdiChild.Controls[0] as RichTextBox;
            rtb.Paste();
        }
    }
}
