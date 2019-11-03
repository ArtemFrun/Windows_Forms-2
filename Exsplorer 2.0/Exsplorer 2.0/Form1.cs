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
    }
}
