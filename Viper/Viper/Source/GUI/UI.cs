using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Viper.Properties;
using Viper.Source.Classes;

namespace Viper
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
            if (Directory.Exists(Settings.Default.projectDirectory + "\\src\\.engine\\"))
            {

            }
            else
            {
                Directory.CreateDirectory(Settings.Default.projectDirectory + "\\src\\.engine\\");
                File.Create(Settings.Default.projectDirectory + "\\src\\.engine\\source.cs");
            }
            ProjHandler handler = new ProjHandler();
            handler.Handle(treeView1);
            textBox1.Text = File.ReadAllText(Settings.Default.projectDirectory + "\\src\\.engine\\source.cs");
        }

        private void siticoneCustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null)
            {
                // do nothing
            }
            else if (e.Node.Tag.GetType() == typeof(DirectoryInfo))
            {
                // Opens the folder
                e.Node.Nodes.Clear();
                foreach (var dirs in Directory.GetDirectories(((DirectoryInfo)e.Node.Tag).FullName))
                {
                    DirectoryInfo DirInfo = new DirectoryInfo(dirs);
                    var nodes = e.Node.Nodes.Add(DirInfo.Name);
                    nodes.Tag = DirInfo;
                }
                foreach (var dirs in Directory.GetFiles(((DirectoryInfo)e.Node.Tag).FullName))
                {
                    FileInfo DirInfo = new FileInfo(dirs);
                    var nodes = e.Node.Nodes.Add(DirInfo.Name);
                    nodes.Tag = DirInfo;
                }
            }
            else
            {
                textBox1.Text = File.ReadAllText(Settings.Default.projectDirectory + "\\src\\.engine\\source.cs");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var writtenCode = textBox1.Text;
            string keywords = @"\b(public|private|partial|static|namespace|class|using|void|foreach|in|if|bool)\b";
            MatchCollection keyWordsCollection = Regex.Matches(writtenCode, keywords);
            string types = @"\b(Console)\b";
            MatchCollection typesCollection = Regex.Matches(writtenCode, types);
            string comments = @"\/\/.+?$|\/\*.+?\*\/";
            MatchCollection commentsCollection = Regex.Matches(writtenCode, comments, RegexOptions.Multiline);
            string strings = "\".+?\"";
            MatchCollection stringCollection = Regex.Matches(writtenCode, strings);

            int originalIndex = textBox1.SelectionStart;
            int originalLength = textBox1.SelectionLength;
            Color originalColor = Color.White;

            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = textBox1.Text.Length;
            textBox1.SelectionColor = originalColor;

            foreach (Match match in keyWordsCollection)
            {
                textBox1.SelectionStart = match.Index;
                textBox1.SelectionLength = match.Length;
                textBox1.SelectionColor = Color.Magenta;
            }
            foreach (Match match in typesCollection)
            {
                textBox1.SelectionStart = match.Index;
                textBox1.SelectionLength = match.Length;
                textBox1.SelectionColor = Color.DarkCyan;
            }
            foreach (Match match in commentsCollection)
            {
                textBox1.SelectionStart = match.Index;
                textBox1.SelectionLength = match.Length;
                textBox1.SelectionColor = Color.DarkGreen;
            }
            foreach (Match match in stringCollection)
            {
                textBox1.SelectionStart = match.Index;
                textBox1.SelectionLength = match.Length;
                textBox1.SelectionColor = Color.Aqua;
            }
            textBox1.SelectionStart = originalIndex;
            textBox1.SelectionLength = originalLength;
            textBox1.SelectionColor = originalColor;
            textBox1.Focus();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            var writtenCode = textBox1.Text;
            string keywords = @"\b(public|private|partial|static|namespace|class|using|void|foreach|in|if|bool)\b";
            MatchCollection keyWordsCollection = Regex.Matches(writtenCode, keywords);
            string types = @"\b(Console)\b";
            MatchCollection typesCollection = Regex.Matches(writtenCode, types);
            string comments = @"\/\/.+?$|\/\*.+?\*\/";
            MatchCollection commentsCollection = Regex.Matches(writtenCode, comments, RegexOptions.Multiline);
            string strings = "\".+?\"";
            MatchCollection stringCollection = Regex.Matches(writtenCode, strings);

            int originalIndex = textBox1.SelectionStart;
            int originalLength = textBox1.SelectionLength;
            Color originalColor = Color.White;

            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = textBox1.Text.Length;
            textBox1.SelectionColor = originalColor;

            foreach (Match match in keyWordsCollection)
            {
                textBox1.SelectionStart = match.Index;
                textBox1.SelectionLength = match.Length;
                textBox1.SelectionColor = Color.Magenta;
            }
            foreach (Match match in typesCollection)
            {
                textBox1.SelectionStart = match.Index;
                textBox1.SelectionLength = match.Length;
                textBox1.SelectionColor = Color.DarkCyan;
            }
            foreach (Match match in commentsCollection)
            {
                textBox1.SelectionStart = match.Index;
                textBox1.SelectionLength = match.Length;
                textBox1.SelectionColor = Color.DarkGreen;
            }
            foreach (Match match in stringCollection)
            {
                textBox1.SelectionStart = match.Index;
                textBox1.SelectionLength = match.Length;
                textBox1.SelectionColor = Color.Aqua;
            }
            textBox1.SelectionStart = originalIndex;
            textBox1.SelectionLength = originalLength;
            textBox1.SelectionColor = originalColor;
            textBox1.Focus();
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Write To File First, before compile.
            File.WriteAllText(Settings.Default.projectDirectory + "\\src\\.engine\\source.cs", textBox1.Text);
            // Compile
            string exe = "Viper_compiled.exe";
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters pars = new CompilerParameters();
            pars.GenerateExecutable = true;
            pars.OutputAssembly = exe;
            pars.GenerateInMemory = false;
            pars.TreatWarningsAsErrors = false;

            CompilerResults result = provider.CompileAssemblyFromFile(pars, Settings.Default.projectDirectory + "\\src\\.engine\\source.cs");

            if (result.Errors.Count > 0)
            {
                foreach (CompilerError err in result.Errors)
                {
                    MessageBox.Show(err.ErrorText.ToString());
                }
            }

        }
    }
}
