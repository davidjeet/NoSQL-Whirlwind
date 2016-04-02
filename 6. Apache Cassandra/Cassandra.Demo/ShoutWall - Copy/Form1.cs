using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitterClient.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            TwitterClient.Instance.DropSchema();


            ////TwitterClient.Instance.InitDatabase();
            ////var results = TwitterClient.Instance.QuerySchema();

            ////textBox1.Clear();
            //foreach (var row in results)
            //{
            //    string s = String.Format("{0, -30}\t{1, -20}\t{2, -20}\t",
            //        row.GetValue<String>("title"), row.GetValue<String>("album"),
            //        row.GetValue<String>("artist"));
            //    textBox1.Text += s + Environment.NewLine;
            //}
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            TwitterClient.Instance.InitDatabase();
            textBox1.Text = "Data loaded successfully.";
        }
    }
}
