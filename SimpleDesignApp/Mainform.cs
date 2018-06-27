using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.OleDb;

namespace SimpleDesignApp
{
    public partial class Mainform : Form
    {
        public static FrmMain valueForm;
        public static GraphicalDisplay graphicalDisplay;
        
        public static int currencyIndex = 0;
        public static int viewIndex = 0;
        public static int timeInterval = 30;
        public static int timezoneIndex = 0;
        public static bool alwaysOnTopValue = true;

        public static OleDbConnection Myconnection;
        String connectionName = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=price.mdb;";

        public Mainform()
        {
            InitializeComponent();
            this.Visible = false;
            this.Hide();
            try
            {
                Myconnection = new OleDbConnection(connectionName);
                Myconnection.Open();
                valueForm = new FrmMain();
                graphicalDisplay = new GraphicalDisplay();
                valueForm.Visible = true;
                graphicalDisplay.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Database error!");
                Myconnection.Close();
                this.Close();
            }

            
        }
       
    }
}
