using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Text;
using System.Diagnostics;

namespace SimpleDesignApp
{
    public partial class GraphicalDisplay : Form
    {
        float usdcurrency;
        OleDbConnection Myconnection;
        String connectionName = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=price.mdb;";

        Thread thread;

        PrivateFontCollection pfc;
        public GraphicalDisplay()
        {
            InitializeComponent();
            this.Location = new Point(Screen.FromControl(this).Bounds.Width - 400, Screen.FromControl(this).Bounds.Height - 400);
            Myconnection = Mainform.Myconnection;
            cointypeComboBox.SelectedIndex = 0;

            coinHistoryChart.Series["coinbaseSeries"].Color = Color.Blue;
            coinHistoryChart.Series["bitfinexSeries"].Color = Color.Red;
            coinHistoryChart.Series["poloniexSeries"].Color = Color.FromArgb(152, 152, 5);
            makeChart();

            pfc = new PrivateFontCollection();
            pfc.AddFontFile("fonts\\Oswald-Bold.ttf");
            pfc.AddFontFile("fonts\\Oswald-Regular.ttf");
            pfc.AddFontFile("fonts\\Roboto-Bold_0.ttf");
            pfc.AddFontFile("fonts\\Roboto-Regular_0.ttf");

            Font robotoFont_8_regular = new Font(pfc.Families[1], 8);
            Font robotoFont_9_regular = new Font(pfc.Families[1], 9);
            Font robotoFont_10_regular = new Font(pfc.Families[1], 10);
            Font robotoFont_11_bold = new Font(pfc.Families[1], 11, FontStyle.Bold);

            Font oswaldFont_10_bold = new Font(pfc.Families[0], 10, FontStyle.Bold);
            Font oswaldFont_10_regular = new Font(pfc.Families[0], 10);
            Font oswaldFont_11_regular = new Font(pfc.Families[0], 11);
            Font oswaldFont_11_bold = new Font(pfc.Families[0], 11, FontStyle.Bold);

            dateTimeLabel.Font = oswaldFont_10_regular;
            settingLabel.Font = robotoFont_10_regular;
            cointypeLabel.Font = oswaldFont_11_regular;
            cointypeComboBox.Font = robotoFont_11_bold;

            coinHistoryChart.Titles[0].Font = oswaldFont_10_bold;
            coinHistoryChart.ChartAreas[0].AxisY.LabelStyle.Font = robotoFont_9_regular;
            coinHistoryChart.ChartAreas[0].AxisX.LabelStyle.Font = robotoFont_8_regular;

            websitesLabel.Font = robotoFont_9_regular;

            thread = new Thread(new ThreadStart(displayDateTime));
            thread.Start();
        }


        public void displayDateTime()
        {
            while (true)
            {
                // Date Time display
                dateTimeLabel.Text = DateTime.Now.ToString("yyyy - MM - dd  HH:mm");
                makeChart();
                Thread.Sleep(30000);
            }
        }

        public void makeChart()
        {
            usdcurrency = FrmMain.usdcurrency;
            try
            {
                // Open connection

                coinHistoryChart.Titles[0].Text = cointypeComboBox.SelectedItem.ToString() + " (" + FrmMain.currency_title + ")";
                DataSet myDataSet = new DataSet();
                OleDbCommand realtimeAccessCommand = new OleDbCommand("SELECT TOP 49 CurrentTime, " + usdcurrency.ToString() + " * Bitfinex As Bitfinex, " + usdcurrency.ToString() + " * Poloniex As Poloniex, " + usdcurrency.ToString() + " * Coinbase As Coinbase from realtime_price where type = \'" + cointypeComboBox.SelectedItem.ToString() + "\' ORDER BY currentTime DESC, currentDate DESC", Myconnection);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(realtimeAccessCommand);
                myDataAdapter.Fill(myDataSet, "realtime_price");
                DataTable realtimePrice = myDataSet.Tables["realtime_price"];

                coinHistoryChart.Series["bitfinexSeries"].Points.Clear();
                coinHistoryChart.Series["poloniexSeries"].Points.Clear();
                coinHistoryChart.Series["coinbaseSeries"].Points.Clear();
                coinHistoryChart.Titles[0].Text = cointypeComboBox.SelectedItem.ToString() + " (" + FrmMain.currency_title + ")";
                foreach (DataRow datarow in realtimePrice.Rows)
                {
                    coinHistoryChart.Series["bitfinexSeries"].Points.InsertXY(0, datarow["CurrentTime"].ToString(), float.Parse(float.Parse(datarow["Bitfinex"].ToString()).ToString("0.00")));
                    coinHistoryChart.Series["poloniexSeries"].Points.InsertXY(0, datarow["CurrentTime"].ToString(), float.Parse(float.Parse(datarow["Poloniex"].ToString()).ToString("0.00")));
                    coinHistoryChart.Series["coinbaseSeries"].Points.InsertXY(0, datarow["CurrentTime"].ToString(), float.Parse(float.Parse(datarow["Coinbase"].ToString()).ToString("0.00")));
                }
                // set the max axis value
                double maxBitfinex = coinHistoryChart.Series["bitfinexSeries"].Points.FindMaxByValue().YValues[0];
                double maxPoloniex = coinHistoryChart.Series["poloniexSeries"].Points.FindMaxByValue().YValues[0];
                double maxCoinbase = coinHistoryChart.Series["coinbaseSeries"].Points.FindMaxByValue().YValues[0];
                coinHistoryChart.ChartAreas["ChartArea1"].AxisY.Maximum = Math.Max(maxBitfinex, Math.Max(maxPoloniex, maxCoinbase)) + Math.Max(maxBitfinex, Math.Max(maxPoloniex, maxCoinbase)) * 0.05;

                // set the min axis value
                double minBitfinex = coinHistoryChart.Series["bitfinexSeries"].Points.FindMinByValue().YValues[0];
                double minPoloniex = coinHistoryChart.Series["poloniexSeries"].Points.FindMinByValue().YValues[0];
                double minCoinbase = coinHistoryChart.Series["coinbaseSeries"].Points.FindMinByValue().YValues[0];
                coinHistoryChart.ChartAreas["ChartArea1"].AxisY.Minimum = Math.Min(minBitfinex, Math.Min(minPoloniex, minCoinbase)) - Math.Min(minBitfinex, Math.Min(minPoloniex, minCoinbase)) * 0.05;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void cointypeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cointypeComboBox.SelectedItem.ToString() != null)
                makeChart();
        }

        private void GraphicalDisplay_Activated(object sender, EventArgs e)
        {
            makeChart();
            // Date Time display
            dateTimeLabel.Text = DateTime.Now.ToString("yyyy - MM - dd  HH:mm");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            SettingForm setform = new SettingForm();
            setform.Location = this.Location;
            setform.ShowDialog(this);
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            settingLabel.BackColor = Color.FromArgb(135, 146, 210);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            settingLabel.BackColor = Color.FromArgb(85, 96, 110);
        }

        ToolTip tooltip = new ToolTip();
        private void coinHistoryChart_GetToolTipText(object sender, System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs e)
        {
            // Check selected chart element and set tooltip text for it
            tooltip.RemoveAll();
            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.DataPoint:
                    var dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                    tooltip.Show(dataPoint.YValues[0].ToString(), this.coinHistoryChart, e.X + 20, e.Y);
                    break;
            }
        }

        private bool mouseDown;
        private Point lastLocation;
        private void dateTimeLabel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void dateTimeLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void dateTimeLabel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void GraphicalDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process[] process = Process.GetProcessesByName("SimpleDesignApp");
            Mainform.Myconnection.Close();
            for (int i = 0; i < process.Length; i++)
                process[i].Kill();
        }
    }
}
