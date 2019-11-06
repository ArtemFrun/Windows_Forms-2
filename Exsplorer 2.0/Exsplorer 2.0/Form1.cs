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
                ListView_Fill(newPath);
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
    }
}
