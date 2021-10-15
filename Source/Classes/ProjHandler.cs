using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Viper.Properties;

namespace Viper.Source.Classes
{
    class ProjHandler
    {
        public void Handle(TreeView treeView1)
        {
            foreach (var dirs in Directory.GetDirectories(Settings.Default.projectDirectory))
            {
                DirectoryInfo DirInfo = new DirectoryInfo(dirs);
                var nodes = treeView1.Nodes.Add(DirInfo.Name, DirInfo.Name, 0);
                nodes.Tag = DirInfo;
            }
            foreach (var Files in Directory.GetFiles(Settings.Default.projectDirectory))
            {
                FileInfo fileInfo = new FileInfo(Files);
                var nodes = treeView1.Nodes.Add(fileInfo.Name, fileInfo.Name, 1);
                nodes.Tag = fileInfo;
            }
        }
    }
}
