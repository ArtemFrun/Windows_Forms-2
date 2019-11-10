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

namespace Exsplorer_2._0
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Text = "Путь";
            richTextBox1.AllowDrop = true;
            richTextBox1.DragEnter += RichTextBox1_DragEnter;
            richTextBox1.DragDrop += RichTextBox1_DragDrop;
        }

        //Получаем перетаскиваем файл в RichTextBox
        private void RichTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            richTextBox1.Clear();
            try
            {
                string name = (string)e.Data.GetData(DataFormats.StringFormat);
                //string[] name = (string[])e.Data.GetData(DataFormats.FileDrop); // если файл не с программы
                //foreach (string file in name)
                //{
                //if(file != "")
                using (StreamReader reader = new StreamReader(name, Encoding.Default))
                        {
                            richTextBox1.Text = reader.ReadToEnd();
                        }
               // }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Refresh();
            }
        }


        //При наводе курсора на форму RichTextBox
        private void RichTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }


        //Создаем новый файл
        private void ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();

            sf.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            sf.FilterIndex = 1;
            sf.Title = "New File";
            sf.FileName = "new_file";
            sf.DefaultExt = "txt";
            sf.ValidateNames = true;

            if(sf.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Clear();
                Text = sf.FileName;
                using (StreamWriter sw = new StreamWriter(sf.FileName, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(richTextBox1.Text);
                    MessageBox.Show("Создано", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //Открыть файл
        private void ToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                Text = open.FileName;
                using (StreamReader sr = new StreamReader(open.FileName, System.Text.Encoding.Default))
                {
                    richTextBox1.Text = sr.ReadToEnd();
                }
            }
        }

        //Сохранить файл
        private void ToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(Text, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(richTextBox1.Text);
                MessageBox.Show("Сохранено", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Сохранить как
        private void ToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            sf.FilterIndex = 1;
            sf.Title = "Save as";
            sf.DefaultExt = "txt";
            sf.ValidateNames = true;

            if (sf.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sf.FileName, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(richTextBox1.Text);
                    MessageBox.Show("Сохранено", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //Отмена действия
        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
            richTextBox1.ClearUndo();
        }

        //Вырезать
        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        //Копировать
        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        //Вставить
        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        //Выделить все
        private void ToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        //Выбор шрифта
        private void ToolStripMenuItem14_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();

            if(fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fd.Font;
            }
        }

        //Цвет Шрифта
        private void ToolStripMenuItem15_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = cd.Color;
            }
        }

        //Цвет фона
        private void ToolStripMenuItem16_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.BackColor = cd.Color;
            }
        }
    }

}
