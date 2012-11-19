using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace serveruserinterface
{
    public partial class main : Form
    {


        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }

    public class sui : EventSlice.Interfaces.Module
    {
        private serveruserinterface.main me = new serveruserinterface.main();
        public override string getModuleName()
        { return "Database"; }


        
    }
}
