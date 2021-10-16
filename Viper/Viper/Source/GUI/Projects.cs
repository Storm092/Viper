using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Viper.Properties;

namespace Viper.Source.GUI
{
    public partial class Projects : Form
    {
        public Projects()
        {
            InitializeComponent();
            siticoneRoundedTextBox1.Text = Settings.Default.projectDirectory;
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            Settings.Default.projectDirectory = siticoneRoundedTextBox1.Text;
            Settings.Default.Save();
            new UI().Show();
            Hide();
        }
    }
}
