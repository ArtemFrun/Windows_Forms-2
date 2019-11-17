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
using System.Xml.Serialization;

namespace MDI_приложение
{
    public partial class Form1 : Form
    {
        List<Bus_flight> bus_s = new List<Bus_flight>();
        public Form1()
        {
            InitializeComponent();
            Deserialize_Bus();
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

        //Вырезать текст с активного окна
        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = this.ActiveMdiChild.Controls[0] as RichTextBox;
            rtb.Cut();
        }

        //Вставить текст в активное окно
        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = this.ActiveMdiChild.Controls[0] as RichTextBox;
            rtb.Paste();
        }

        //Открытия окна для поиска рейса по пункту прибытия
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Form fr = new Form();
            fr.MdiParent = this;

            RichTextBox rtb = new RichTextBox();
            Button button = new Button();

            fr.Text = "Прибытия в...";
            fr.ClientSize = new Size(300, 110);
            fr.MaximumSize = fr.MinimumSize = fr.ClientSize;
            button.Text = "Поиск";
            button.MouseClick += Search_Arival_To_Button_MouseClick;
            button.Size = new Size(50, 20);
            button.Top = fr.ClientSize.Height - (fr.ClientSize.Height / 3);
            button.Left = fr.ClientSize.Width - (fr.ClientSize.Width / 2) - 25;
            rtb.Size = new Size(fr.ClientSize.Width, fr.ClientSize.Height/2);

            fr.Controls.Add(button);
            fr.Controls.Add(rtb);
            fr.Show();
        }

        //Поиск рейса по пункту прибытия
        private void Search_Arival_To_Button_MouseClick(object sender, MouseEventArgs e)
        {
            RichTextBox rtb = this.ActiveMdiChild.Controls[1] as RichTextBox;
            List<Bus_flight> sa = new List<Bus_flight>();
            foreach (var bus in bus_s)
            {
                if (bus.destination.IndexOf(rtb.Text) != -1)
                {
                    sa.Add(bus);
                }
            }

            Form fr = new Form();
            fr.MdiParent = this;
            RichTextBox rtb_result = new RichTextBox();
            rtb_result.Dock = DockStyle.Fill;
            if (sa.Count > 0)
            {
                foreach (var res in sa)
                {
                    rtb_result.Text += $"Автобус №{res.nomber}, Тип {res.type}, Пункт назначени {res.destination}, Дата и время отправки {res.departure_date_and_time}, Дата и время прибытия {res.arrivale_date_and_time}\n\n";
                }
            }
            else
                rtb_result.Text = "Такого автобуса нет!";

            fr.Controls.Add(rtb_result);
            fr.Show();
        }


        //Создание рейса автобуса и сериализация в XML
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Form fr = new Form();
            fr.MdiParent = this;

            RichTextBox number = new RichTextBox();
            RichTextBox type = new RichTextBox();
            RichTextBox destination = new RichTextBox();
            RichTextBox departure_date = new RichTextBox();
            RichTextBox departure_time = new RichTextBox();
            RichTextBox arrivale_date = new RichTextBox();
            RichTextBox arrivale_time = new RichTextBox();
            Button button = new Button();
            Button button1 = new Button();

            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            Label label4 = new Label();
            Label label5 = new Label();
            Label label6 = new Label();
            Label label7 = new Label();

            label1.Text = "Номер автобуса";
            label2.Text = "Тип автобума";
            label3.Text = "Пункт назначения";
            label4.Text = "Дата отправки\n (день/мес/год)";
            label5.Text = "Время отправки\n (час:мин)";
            label6.Text = "Дата прибытия\n (день/мес/год)";
            label7.Text = "Время прибытия\n (час:мин)";

            label1.Size = label2.Size = label3.Size = label4.Size = label5.Size = label6.Size = label7.Size = new Size(fr.ClientSize.Width / 2, 30);
            fr.Text = "Создание рейса";
            fr.ClientSize = new Size(300, 400);
            fr.MaximumSize = fr.MinimumSize = fr.ClientSize;
            button.Text = "Сохранить";
            button1.Text = "Отменить";
            button.MouseClick += Save_Bus_flight;
            button1.MouseClick += Cansel_Bus_flight;
            button.Size = new Size(70, 20);
            button1.Size = new Size(70, 20);
            button.Top = fr.ClientSize.Height - (fr.ClientSize.Height / 10);
            button.Left = fr.ClientSize.Width - (fr.ClientSize.Width / 2) - 100;
            button1.Top = fr.ClientSize.Height - (fr.ClientSize.Height / 10);
            button1.Left = fr.ClientSize.Width - (fr.ClientSize.Width / 2) + 25;
            destination.Size = type.Size = number.Size  = departure_date.Size = departure_time.Size = arrivale_date.Size = arrivale_time.Size = new Size(fr.ClientSize.Width /2 , 20);
            label1.Top = number.Top = 10;
            label2.Top = type.Top = number.Top + 40;
            label3.Top = destination.Top = type.Top + 40;
            label4.Top = departure_date.Top = destination.Top + 40;
            label5.Top = departure_time.Top = departure_date.Top + 40;
            label6.Top = arrivale_date.Top = departure_time.Top + 40;
            label7.Top = arrivale_time.Top = arrivale_date.Top + 40;
            number.Left = type.Left = destination.Left = departure_date.Left = departure_time.Left = arrivale_date.Left = arrivale_time.Left = fr.ClientSize.Width / 2;
            label1.Left = label2.Left = label3.Left = label4.Left = label5.Left = label6.Left = label7.Left = 5;



            fr.Controls.Add(button);
            fr.Controls.Add(button1);
            fr.Controls.Add(number);
            fr.Controls.Add(type);
            fr.Controls.Add(destination);
            fr.Controls.Add(departure_date);
            fr.Controls.Add(departure_time);
            fr.Controls.Add(arrivale_date);
            fr.Controls.Add(arrivale_time);
            fr.Controls.Add(label1);
            fr.Controls.Add(label2);
            fr.Controls.Add(label3);
            fr.Controls.Add(label4);
            fr.Controls.Add(label5);
            fr.Controls.Add(label6);
            fr.Controls.Add(label7);
            fr.Show();
        }

        //Сохранить данные про рейс и провести сериализацию
        private void Save_Bus_flight(object sender, MouseEventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                Form fr = ActiveMdiChild;
                RichTextBox[] bus_info = new RichTextBox[7];
                int i = 0;
                foreach (RichTextBox rtb in fr.Controls.OfType<RichTextBox>())
                {
                    bus_info[i] = rtb;
                    i++;
                }
                if (bus_info[0].Text != "" && bus_info[1].Text != "" && bus_info[2].Text != "" && bus_info[3].Text != "" &&
                    bus_info[4].Text != "" && bus_info[5].Text != "" && bus_info[6].Text != "")
                {
                    try
                    {
                        Bus_flight bus = new Bus_flight();
                        bus.nomber = bus_info[0].Text;
                        bus.type = bus_info[1].Text;
                        bus.destination = bus_info[2].Text;
                        string[] departure_date = bus_info[3].Text.Split('\\', '/', '.');
                        string[] departure_time = bus_info[4].Text.Split(':', '.');
                        bus.departure_date_and_time = new DateTime(int.Parse(departure_date[2]), int.Parse(departure_date[1]), int.Parse(departure_date[0]), int.Parse(departure_time[0]), int.Parse(departure_time[1]), 0);
                        string[] arrivale_date = bus_info[5].Text.Split('\\', '/', '.');
                        string[] arrivale_time = bus_info[6].Text.Split(':', '.');
                        bus.arrivale_date_and_time = new DateTime(int.Parse(arrivale_date[2]), int.Parse(arrivale_date[1]), int.Parse(arrivale_date[0]), int.Parse(arrivale_time[0]), int.Parse(arrivale_time[1]), 0);

                        bus_s.Add(bus);

                        var formatter = new XmlSerializer(typeof(List<Bus_flight>));

                        using (FileStream fs = new FileStream("Bus_flight.xml", FileMode.OpenOrCreate))
                        {
                            formatter.Serialize(fs, bus_s);
                        }

                        MessageBox.Show("Рейс сохранен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Не верно введено дату или время", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Закрыть окно создания рейса
        private void Cansel_Bus_flight(object sender, MouseEventArgs e)
        {
            if (ActiveMdiChild != null)
                    ActiveMdiChild.Close();
        }

        //Десериализация
        private void Deserialize_Bus()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Bus_flight>));

            using (FileStream fs = new FileStream("Bus_flight.xml", FileMode.OpenOrCreate))
            {
                bus_s = (List<Bus_flight>)formatter.Deserialize(fs);
            }
        }


        //Открытия списка рейсов
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Show_Bus_flight();
        }

        //Открытия списка рейсов
        private void Show_Bus_flight()
        {
            Form fr = new Form();
            fr.MdiParent = this;

            RichTextBox rtb = new RichTextBox();
            rtb.Dock = DockStyle.Fill;
            
            foreach(var res in bus_s)
                rtb.Text += $"Автобус №{res.nomber}, Тип {res.type}, Пункт назначени {res.destination}, Дата и время отправки {res.departure_date_and_time}, Дата и время прибытия {res.arrivale_date_and_time}\n\n";


            fr.Controls.Add(rtb);
            fr.Show();
        }
    }
}
