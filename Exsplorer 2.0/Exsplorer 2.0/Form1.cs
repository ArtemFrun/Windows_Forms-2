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
    public partial class Form1 : Form
    {
        string path_file = "";
        public Form1()
        {
            InitializeComponent();
            DriveInfo[] dr = DriveInfo.GetDrives();
            foreach (DriveInfo drive in dr)
            {
                TreeNode node = new TreeNode(drive.Name);
                treeView1.Nodes.Add(node);
                Directory_name(node, node.FullPath);
            }
            Image_ADD();
            listView1.View = View.LargeIcon;
            listView1.LargeImageList = imageList1;
            listView1.AllowDrop = true;
            treeView1.HideSelection = true;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string newPath = e.Node.FullPath.Replace("\\\\", "\\");
            treeView1.Text = $"Путь: {newPath}";

            if (e.Node.GetNodeCount(true) == 0)
            {
                Directory_name(e.Node, e.Node.FullPath);
            }

        }

        private void Directory_name (TreeNode tree, string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(tree.FullPath);

                foreach (string item in dirs)
                {
                    string[] tmp = item.Split('\\');

                    TreeNode ntr = new TreeNode(tmp[tmp.Length - 1]);

                    tree.Nodes.Add(ntr);
                }
                
            }
            catch (Exception)
            {
            }
        }

        private void TreeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string newPath = treeView1.SelectedNode.FullPath.Replace("\\\\", "\\");
                path_file = newPath;
                ListView_Fill(newPath);
                Direct_and_Filee_Info(newPath);
            }
        }

        private void ListView_Fill(string path)
        {
            try
            {
                listView1.Items.Clear();
                string[] dirs = Directory.GetDirectories(path);

                foreach (string dr in dirs)
                {
                    string[] name = dr.Split('\\');

                    listView1.Items.Add(name[name.Length - 1], 0);
                }

                string[] files = Directory.GetFiles(path);

                foreach (string file in files)
                {
                    string[] name = file.Split('\\');

                    listView1.Items.Add(name[name.Length - 1], 1);
                }
            }
            catch (Exception)
            {
            }
        }

        private void Direct_and_Filee_Info (string path)
        {
            listView2.Items.Clear();
            DirectoryInfo drinfo = new DirectoryInfo(path);
            if (drinfo.Exists != false)
            {
                listView2.Items.Add("Полное имя " + drinfo.FullName);
                listView2.Items.Add("Дата создания " + drinfo.CreationTime.ToString());
            }
            else
            {
                FileInfo finfo = new FileInfo(path);
                if (finfo.Exists == true)
                {
                    listView2.Items.Add("Полное имя " + finfo.FullName);
                    listView2.Items.Add("Дата создания " + finfo.CreationTime.ToString());
                    listView2.Items.Add("Размер " +finfo.Length.ToString());
                }
            }
        }

        private void Image_ADD()
        {
            Image img = Image.FromFile("folder.ico");
            imageList1.Images.Add(img);
            img = Image.FromFile("file.ico");
            imageList1.Images.Add(img);
            imageList1.Images.SetKeyName(0, "folder.ico");
            imageList1.Images.SetKeyName(1, "file.ico");

            listView1.FullRowSelect = true;
            listView1.GridLines = false;
            listView1.SmallImageList = imageList1;
            listView1.Columns.Add("Name", listView1.Width / 3);
            listView1.Columns.Add("Path", listView1.Width / 3);
            listView1.Columns.Add("Size, Gb", listView1.Width / 3);
            
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
            imageList1.ImageSize = new Size(25, 25);
            Image_ADD();
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
            imageList1.ImageSize = new Size(50, 50);
            Image_ADD();
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
            imageList1.ImageSize = new Size(16, 16);
            Image_ADD();
        }

        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            listView1.View = View.List;
            imageList1.ImageSize = new Size(16, 16);
            Image_ADD();
        }

        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            imageList1.ImageSize = new Size(16, 16);
            Image_ADD();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        //Перетаскивания файла с формы listView
        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {

            try
            {
                string fullname = e.Item.ToString();
                string[] names = fullname.Split('{', '}', ':');
                string name = names[names.Length - 2];
                string path = path_file + "\\" + name;
                DoDragDrop(path, DragDropEffects.Copy | DragDropEffects.Move);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Refresh();
            }
            
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                if (listView1.SelectedIndices.Count >0)
                {
                    for (int i = 0; i < listView1.SelectedItems.Count; i++)
                    {
                        if (listView1.SelectedItems[i].Selected == true)
                        {
                            string name = listView1.SelectedItems[i].Text;
                            string path = path_file + "\\" + name;
                            Direct_and_Filee_Info(path);
                        }
                    }
                }
            }
        }
    }
}
