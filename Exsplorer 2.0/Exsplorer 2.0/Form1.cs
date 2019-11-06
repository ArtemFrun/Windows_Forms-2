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
            listView1.View = View.LargeIcon;
            listView1.LargeImageList = imageList1;
            listView1.SmallImageList = imageList1;
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

                    listView1.Items.Add(name[name.Length - 1]);
                }

                string[] files = Directory.GetFiles(path);

                foreach (string file in files)
                {
                    string[] name = file.Split('\\');

                    listView1.Items.Add(name[name.Length - 1]);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
