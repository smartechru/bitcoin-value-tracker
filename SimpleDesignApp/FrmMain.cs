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
using System.Net;
using System.Data.OleDb;
using System.Drawing.Text;
using System.Diagnostics;

namespace SimpleDesignApp
{
    public partial class FrmMain : Form
    {
        // Bitcoin api urls
        String bitfinex_btcusd = "https://api.bitfinex.com/v1/pubticker/btcusd";
        String bitfinex_ethusd = "https://api.bitfinex.com/v1/pubticker/ethusd";
        String bitfinex_bchusd = "https://api.bitfinex.com/v1/pubticker/bchusd";
        String bitfinex_etcusd = "https://api.bitfinex.com/v1/pubticker/etcusd";
        String bitfinex_ltcusd = "https://api.bitfinex.com/v1/pubticker/ltcusd";
        String bitfinex_start_split = "last_price\":\"";
        String bitfinex_stop_split = "\",";

        String poloniex_btcusd = "https://poloniex.com/public?command=returnOrderBook&currencyPair=USDT_BTC&depth=1";
        String poloniex_ethusd = "https://poloniex.com/public?command=returnOrderBook&currencyPair=USDT_ETH&depth=1";
        String poloniex_bchusd = "https://poloniex.com/public?command=returnOrderBook&currencyPair=USDT_BCH&depth=1";
        String poloniex_etcusd = "https://poloniex.com/public?command=returnOrderBook&currencyPair=USDT_ETC&depth=1";
        String poloniex_ltcusd = "https://poloniex.com/public?command=returnOrderBook&currencyPair=USDT_LTC&depth=1";
        String poloniex_start_split = "asks\":[[\"";
        String poloniex_stop_split = "\",";

        String coinbase_btcusd = "https://api.coinbase.com/v2/prices/BTC-USD/buy";
        String coinbase_ethusd = "https://api.coinbase.com/v2/prices/ETH-USD/buy";
        String coinbase_ltcusd = "https://api.coinbase.com/v2/prices/LTC-USD/buy";
        String coinbase_start_split = "amount\":\"";
        String coinbase_stop_split = "\"}";


        // Thread for realtime tracking
        Thread thread;
        Thread killForm;

        // float values for real time storing
        float bitfinex_btc, bitfinex_eth, bitfinex_bch, bitfinex_etc, bitfinex_ltc;
        float poloniex_btc, poloniex_eth, poloniex_bch, poloniex_etc, poloniex_ltc;
        float coinbase_btc, coinbase_eth, coinbase_ltc;

        // float ex-values for real time storing
        float ex_bitfinex_btc, ex_bitfinex_eth, ex_bitfinex_bch, ex_bitfinex_etc, ex_bitfinex_ltc;
        float ex_poloniex_btc, ex_poloniex_eth, ex_poloniex_bch, ex_poloniex_etc, ex_poloniex_ltc;
        float ex_coinbase_btc, ex_coinbase_eth, ex_coinbase_ltc;

        // float values for difference of prices
        float bitfinex_btc_diff = 0, bitfinex_eth_diff = 0, bitfinex_bch_diff = 0, bitfinex_etc_diff = 0, bitfinex_ltc_diff = 0;
        float poloniex_btc_diff = 0, poloniex_eth_diff = 0, poloniex_bch_diff = 0, poloniex_etc_diff = 0, poloniex_ltc_diff = 0;
        float coinbase_btc_diff = 0, coinbase_eth_diff = 0, coinbase_ltc_diff = 0;

        public static float usdcurrency = 1;
        public static string currency_title = "USD";

        String website = "Coinbase";

        // Database variables
        String connectionName = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=price.mdb;";
        OleDbConnection Myconnection;

        public static String currentTime;

        public static int state = 1;
        
        PrivateFontCollection pfc;

        public FrmMain()
        {
            InitializeComponent();
            this.Location = new Point(Screen.FromControl(this).Bounds.Width - 700, Screen.FromControl(this).Bounds.Height - 700);
            currentTime = DateTime.Now.ToString("yyyy - MM - dd  HH:mm");

            Myconnection = Mainform.Myconnection;

            initSettingValues();
            makeScreen();


            thread = new Thread(new ThreadStart(scrapDataRealtime));
            killForm = new Thread(new ThreadStart(killThisForm));

            thread.Start();
            killForm.Start();

            pfc = new PrivateFontCollection();
            pfc.AddFontFile("fonts\\Oswald-Bold.ttf");
            pfc.AddFontFile("fonts\\Oswald-Regular.ttf");
            pfc.AddFontFile("fonts\\Roboto-Bold_0.ttf");
            pfc.AddFontFile("fonts\\Roboto-Regular_0.ttf");
            Font oswaldFont_11_regular = new Font(pfc.Families[0], 11, FontStyle.Regular);
            Font oswaldFont_11_bold = new Font(pfc.Families[0], 11, FontStyle.Bold);
            Font robotoFont_11_regular = new Font(pfc.Families[1], 11, FontStyle.Regular);
            Font oswaldFont_10_regular = new Font(pfc.Families[0], 10, FontStyle.Regular);
            Font robotoFont_10_regular = new Font(pfc.Families[1], 10, FontStyle.Regular);

            dateTimeLabel.Font = oswaldFont_10_regular;
            settingLabel.Font = robotoFont_10_regular;
            coinbaseLabel.Font = oswaldFont_11_regular;
            poloniexLabel.Font = oswaldFont_11_regular;
            bitfinexLabel.Font = oswaldFont_11_regular;

            btcLabel.Font = oswaldFont_11_bold;
            ethLabel.Font = oswaldFont_11_bold;
            ltcLabel.Font = oswaldFont_11_bold;
            etcLabel.Font = oswaldFont_11_bold;
            bchLabel.Font = oswaldFont_11_bold;

            btcValueLabel.Font = robotoFont_11_regular;
            ethValueLabel.Font = robotoFont_11_regular;
            ltcValueLabel.Font = robotoFont_11_regular;
            etcValueLabel.Font = robotoFont_11_regular;
            bchValueLabel.Font = robotoFont_11_regular;

            btcDiffLabel.Font = oswaldFont_11_bold;
            ethDiffLabel.Font = oswaldFont_11_bold;
            ltcDiffLabel.Font = oswaldFont_11_bold;
            etcDiffLabel.Font = oswaldFont_11_bold;
            bchDiffLabel.Font = oswaldFont_11_bold;
        }

        public void killThisForm()
        {
            while (state == 1)
            {
                Thread.Sleep(1000);
            }
            this.Close();
        }

        public void initSettingValues()
        {
            DataSet myDataSet = new DataSet();
            try
            {
                OleDbCommand realtimeAccessCommand = new OleDbCommand("SELECT TOP 1 " + usdcurrency.ToString() + " * Bitfinex As Bitfinex, " + usdcurrency.ToString() + " * Poloniex As Poloniex, " + usdcurrency.ToString() + " * Coinbase As Coinbase from realtime_price where type = \'BTC\' ORDER BY currentTime DESC, currentDate DESC", Myconnection);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(realtimeAccessCommand);
                myDataAdapter.Fill(myDataSet, "btc");
                DataTable initTable = myDataSet.Tables["btc"];

                DataRow datarow = initTable.Rows[0];
                bitfinex_btc = float.Parse(datarow["Bitfinex"].ToString());
                poloniex_btc = float.Parse(datarow["Poloniex"].ToString());
                coinbase_btc = float.Parse(datarow["coinbase"].ToString());

                realtimeAccessCommand.CommandText = "SELECT TOP 1 " + usdcurrency.ToString() + " * Bitfinex As Bitfinex, " + usdcurrency.ToString() + " * Poloniex As Poloniex, " + usdcurrency.ToString() + " * Coinbase As Coinbase from realtime_price where type = \'ETH\' ORDER BY currentTime DESC, currentDate DESC";
                myDataAdapter.Fill(myDataSet, "eth");
                initTable = myDataSet.Tables["eth"];
                datarow = initTable.Rows[0];
                bitfinex_eth = float.Parse(datarow["Bitfinex"].ToString());
                poloniex_eth = float.Parse(datarow["Poloniex"].ToString());
                coinbase_eth = float.Parse(datarow["coinbase"].ToString());


                realtimeAccessCommand.CommandText = "SELECT TOP 1 " + usdcurrency.ToString() + " * Bitfinex As Bitfinex, " + usdcurrency.ToString() + " * Poloniex As Poloniex, " + usdcurrency.ToString() + " * Coinbase As Coinbase from realtime_price where type = \'LTC\' ORDER BY currentTime DESC, currentDate DESC";
                myDataAdapter.Fill(myDataSet, "ltc");
                initTable = myDataSet.Tables["ltc"];
                datarow = initTable.Rows[0];
                bitfinex_ltc = float.Parse(datarow["Bitfinex"].ToString());
                poloniex_ltc = float.Parse(datarow["Poloniex"].ToString());
                coinbase_ltc = float.Parse(datarow["coinbase"].ToString());


                realtimeAccessCommand.CommandText = "SELECT TOP 1 " + usdcurrency.ToString() + " * Bitfinex As Bitfinex, " + usdcurrency.ToString() + " * Poloniex As Poloniex, " + usdcurrency.ToString() + " * Coinbase As Coinbase from realtime_price where type = \'BCH\' ORDER BY currentTime DESC, currentDate DESC";
                myDataAdapter.Fill(myDataSet, "bch");
                initTable = myDataSet.Tables["bch"];
                datarow = initTable.Rows[0];
                bitfinex_bch = float.Parse(datarow["Bitfinex"].ToString());
                poloniex_bch = float.Parse(datarow["Poloniex"].ToString());


                realtimeAccessCommand.CommandText = "SELECT TOP 1 " + usdcurrency.ToString() + " * Bitfinex As Bitfinex, " + usdcurrency.ToString() + " * Poloniex As Poloniex, " + usdcurrency.ToString() + " * Coinbase As Coinbase from realtime_price where type = \'ETC\' ORDER BY currentTime DESC, currentDate DESC";
                myDataAdapter.Fill(myDataSet, "etc");
                initTable = myDataSet.Tables["etc"];
                datarow = initTable.Rows[0];
                bitfinex_etc = float.Parse(datarow["Bitfinex"].ToString());
                poloniex_etc = float.Parse(datarow["Poloniex"].ToString());

            }
            catch (Exception ex)
            { }

            // initialize the ex-values
            ex_bitfinex_btc = bitfinex_btc; ex_bitfinex_eth = bitfinex_eth; ex_bitfinex_bch = bitfinex_bch;
            ex_bitfinex_etc = bitfinex_etc; ex_bitfinex_ltc = bitfinex_ltc;

            ex_poloniex_btc = poloniex_btc; ex_poloniex_eth = poloniex_eth; ex_poloniex_bch = poloniex_bch;
            ex_poloniex_etc = poloniex_etc; ex_poloniex_ltc = poloniex_ltc;

            ex_coinbase_btc = coinbase_btc; ex_coinbase_eth = coinbase_eth; ex_coinbase_ltc = coinbase_ltc;

        }

        public void scrapData()
        {
            try
            {
                // bitfinex
                bitfinex_btc = float.Parse(getValueFromJSON(new WebClient().DownloadString(bitfinex_btcusd), bitfinex_start_split, bitfinex_stop_split));
                bitfinex_eth = float.Parse(getValueFromJSON(new WebClient().DownloadString(bitfinex_ethusd), bitfinex_start_split, bitfinex_stop_split));
                bitfinex_bch = float.Parse(getValueFromJSON(new WebClient().DownloadString(bitfinex_bchusd), bitfinex_start_split, bitfinex_stop_split));
                bitfinex_etc = float.Parse(getValueFromJSON(new WebClient().DownloadString(bitfinex_etcusd), bitfinex_start_split, bitfinex_stop_split));
                bitfinex_ltc = float.Parse(getValueFromJSON(new WebClient().DownloadString(bitfinex_ltcusd), bitfinex_start_split, bitfinex_stop_split));

                // poloniex
                poloniex_btc = float.Parse(getValueFromJSON(new WebClient().DownloadString(poloniex_btcusd), poloniex_start_split, poloniex_stop_split));
                poloniex_eth = float.Parse(getValueFromJSON(new WebClient().DownloadString(poloniex_ethusd), poloniex_start_split, poloniex_stop_split));
                poloniex_bch = float.Parse(getValueFromJSON(new WebClient().DownloadString(poloniex_bchusd), poloniex_start_split, poloniex_stop_split));
                poloniex_etc = float.Parse(getValueFromJSON(new WebClient().DownloadString(poloniex_etcusd), poloniex_start_split, poloniex_stop_split));
                poloniex_ltc = float.Parse(getValueFromJSON(new WebClient().DownloadString(poloniex_ltcusd), poloniex_start_split, poloniex_stop_split));

                // Coin
                coinbase_btc = float.Parse(getValueFromJSON(new WebClient().DownloadString(coinbase_btcusd), coinbase_start_split, coinbase_stop_split));
                coinbase_eth = float.Parse(getValueFromJSON(new WebClient().DownloadString(coinbase_ethusd), coinbase_start_split, coinbase_stop_split));
                coinbase_ltc = float.Parse(getValueFromJSON(new WebClient().DownloadString(coinbase_ltcusd), coinbase_start_split, coinbase_stop_split));
            }
            catch (Exception ex)
            {

            }
        }

        public void calculateDiff()
        {
            // bitfinex differences
            bitfinex_btc_diff = bitfinex_btc - ex_bitfinex_btc;
            bitfinex_eth_diff = bitfinex_eth - ex_bitfinex_eth;
            bitfinex_bch_diff = bitfinex_bch - ex_bitfinex_bch;
            bitfinex_etc_diff = bitfinex_etc - ex_bitfinex_etc;
            bitfinex_ltc_diff = bitfinex_ltc - ex_bitfinex_ltc;

            // Poloniex Differences
            poloniex_btc_diff = poloniex_btc - ex_poloniex_btc;
            poloniex_eth_diff = poloniex_eth - ex_poloniex_eth;
            poloniex_bch_diff = poloniex_bch - ex_poloniex_bch;
            poloniex_etc_diff = poloniex_etc - ex_poloniex_etc;
            poloniex_ltc_diff = poloniex_ltc - ex_poloniex_ltc;

            // Coinbase Differences
            coinbase_btc_diff = coinbase_btc - ex_coinbase_btc;
            coinbase_eth_diff = coinbase_eth - ex_coinbase_eth;
            coinbase_ltc_diff = coinbase_ltc - ex_coinbase_ltc;

            // initialize the ex-values
            ex_bitfinex_btc = bitfinex_btc; ex_bitfinex_eth = bitfinex_eth; ex_bitfinex_bch = bitfinex_bch;
            ex_bitfinex_etc = bitfinex_etc; ex_bitfinex_ltc = bitfinex_ltc;

            ex_poloniex_btc = poloniex_btc; ex_poloniex_eth = poloniex_eth; ex_poloniex_bch = poloniex_bch;
            ex_poloniex_etc = poloniex_etc; ex_poloniex_ltc = poloniex_ltc;

            ex_coinbase_btc = coinbase_btc; ex_coinbase_eth = coinbase_eth; ex_coinbase_ltc = coinbase_ltc;
        }

        public void scrapDataRealtime()
        {
            while (true)
            {
                scrapData();
                calculateDiff();
                // Date Time display
                currentTime = DateTime.Now.ToString("yyyy - MM - dd  HH:mm");
                dateTimeLabel.Text = currentTime;

                makeScreen();
                updateDatabase();
                Thread.Sleep(Mainform.timeInterval * 1000);
            }
        }

        // the function which extracts the value from json
        public String getValueFromJSON(String JSON, String start_split, String end_split)
        {
            String value = "";
            String[] first_separator = { start_split };
            String[] second_separator = { end_split };
            try
            {
                value = JSON.Split(first_separator, StringSplitOptions.None)[1].Split(second_separator, StringSplitOptions.None)[0];
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.ToString());
            }

            return value;
        }

        public void makeScreen()
        {
            // Showing the difference
            if (website == "Coinbase")
            {
                // Coinbase
                btcDiffLabel.Text = (usdcurrency * coinbase_btc_diff).ToString("0.00");
                if (coinbase_btc_diff >= 0)
                    btcDiffLabel.ForeColor = Color.Green;
                else
                    btcDiffLabel.ForeColor = Color.Red;
                ethDiffLabel.Text = (usdcurrency * coinbase_eth_diff).ToString("0.00");
                if (coinbase_eth_diff >= 0)
                    ethDiffLabel.ForeColor = Color.Green;
                else
                    ethDiffLabel.ForeColor = Color.Red;
                ltcDiffLabel.Text = (usdcurrency * coinbase_ltc_diff).ToString("0.00");
                if (coinbase_ltc_diff >= 0)
                    ltcDiffLabel.ForeColor = Color.Green;
                else
                    ltcDiffLabel.ForeColor = Color.Red;

                bchDiffLabel.Text = "";
                etcDiffLabel.Text = "";

                btcValueLabel.Text = (usdcurrency * coinbase_btc).ToString("0.000");
                ethValueLabel.Text = (usdcurrency * coinbase_eth).ToString("0.000");
                ltcValueLabel.Text = (usdcurrency * coinbase_ltc).ToString("0.000");
                bchValueLabel.Text = "";
                etcValueLabel.Text = "";
            }
            else if (website == "Poloniex")
            {
                // Poloniex
                btcDiffLabel.Text = (usdcurrency * poloniex_btc_diff).ToString("0.00");
                if (poloniex_btc_diff >= 0)
                    btcDiffLabel.ForeColor = Color.Green;
                else
                    btcDiffLabel.ForeColor = Color.Red;
                ethDiffLabel.Text = (usdcurrency * poloniex_eth_diff).ToString("0.00");
                if (poloniex_eth_diff >= 0)
                    ethDiffLabel.ForeColor = Color.Green;
                else
                    ethDiffLabel.ForeColor = Color.Red;
                ltcDiffLabel.Text = (usdcurrency * poloniex_ltc_diff).ToString("0.00");
                if (poloniex_ltc_diff >= 0)
                    ltcDiffLabel.ForeColor = Color.Green;
                else
                    ltcDiffLabel.ForeColor = Color.Red;
                bchDiffLabel.Text = (usdcurrency * poloniex_bch_diff).ToString("0.00");
                if (poloniex_bch_diff >= 0)
                    bchDiffLabel.ForeColor = Color.Green;
                else
                    bchDiffLabel.ForeColor = Color.Red;
                etcDiffLabel.Text = (usdcurrency * poloniex_etc_diff).ToString("0.00");
                if (poloniex_etc_diff >= 0)
                    etcDiffLabel.ForeColor = Color.Green;
                else
                    etcDiffLabel.ForeColor = Color.Red;

                // Poloniex
                btcValueLabel.Text = (usdcurrency * poloniex_btc).ToString("0.000");
                ethValueLabel.Text = (usdcurrency * poloniex_eth).ToString("0.000");
                ltcValueLabel.Text = (usdcurrency * poloniex_ltc).ToString("0.000");
                bchValueLabel.Text = (usdcurrency * poloniex_bch).ToString("0.000");
                etcValueLabel.Text = (usdcurrency * poloniex_etc).ToString("0.000");
            }
            else 
            {
                // Bitfinex
                btcDiffLabel.Text = (usdcurrency * bitfinex_btc_diff).ToString("0.00");
                if (bitfinex_btc_diff >= 0)
                    btcDiffLabel.ForeColor = Color.Green;
                else
                    btcDiffLabel.ForeColor = Color.Red;
                ethDiffLabel.Text = (usdcurrency * bitfinex_eth_diff).ToString("0.00");
                if (bitfinex_eth_diff >= 0)
                    ethDiffLabel.ForeColor = Color.Green;
                else
                    ethDiffLabel.ForeColor = Color.Red;
                ltcDiffLabel.Text = (usdcurrency * bitfinex_ltc_diff).ToString("0.00");
                if (bitfinex_ltc_diff >= 0)
                    ltcDiffLabel.ForeColor = Color.Green;
                else
                    ltcDiffLabel.ForeColor = Color.Red;
                bchDiffLabel.Text = (usdcurrency * bitfinex_bch_diff).ToString("0.00");
                if (bitfinex_bch_diff >= 0)
                    bchDiffLabel.ForeColor = Color.Green;
                else
                    bchDiffLabel.ForeColor = Color.Red;
                etcDiffLabel.Text = (usdcurrency * bitfinex_etc_diff).ToString("0.00");
                if (bitfinex_etc_diff >= 0)
                    etcDiffLabel.ForeColor = Color.Green;
                else
                    etcDiffLabel.ForeColor = Color.Red;

                // Bitfinex
                btcValueLabel.Text = (usdcurrency * bitfinex_btc).ToString("0.000");
                ethValueLabel.Text = (usdcurrency * bitfinex_eth).ToString("0.000");
                ltcValueLabel.Text = (usdcurrency * bitfinex_ltc).ToString("0.000");
                bchValueLabel.Text = (usdcurrency * bitfinex_bch).ToString("0.000");
                etcValueLabel.Text = (usdcurrency * bitfinex_etc).ToString("0.000");
            }
                // Showing the currency

            btcLabel.Text = "BTC (" + currency_title + ")";
            ethLabel.Text = "ETH (" + currency_title + ")";
            ltcLabel.Text = "LTC (" + currency_title + ")";
            if (website == "Coinbase")
            {
                bchLabel.Text = "";
                etcLabel.Text = "";
            }
            else
            {
                bchLabel.Text = "BCH (" + currency_title + ")";
                etcLabel.Text = "ETC (" + currency_title + ")";
            }
        }

        public void updateDatabase()
        {
            // Update the database
            OleDbCommand realtimeAccessCommand;
            try
            {
                // deleted unused records.
                realtimeAccessCommand = new OleDbCommand("DELETE FROM realtime_price WHERE id NOT IN (SELECT TOP 15 ID FROM realtime_price ORDER BY currentDate DESC, CurrentTime DESC)", Myconnection);
                realtimeAccessCommand.ExecuteNonQuery();

                // btc values;
                realtimeAccessCommand = new OleDbCommand("INSERT INTO realtime_price (CurrentTime, currentDate, Bitfinex, Poloniex, Coinbase, type) VALUES (?, ?, ?, ?, ?, ?)", Myconnection);
                realtimeAccessCommand.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm"));//, Myconnection
                realtimeAccessCommand.Parameters.AddWithValue("@currentDate", DateTime.Now.ToString("yyyy-M-dd"));
                realtimeAccessCommand.Parameters.AddWithValue("@Bitfinex", bitfinex_btc.ToString());
                realtimeAccessCommand.Parameters.AddWithValue("@Poloniex", poloniex_btc.ToString());
                realtimeAccessCommand.Parameters.AddWithValue("@Coinbase", coinbase_btc.ToString());
                realtimeAccessCommand.Parameters.AddWithValue("@type", "BTC");
                realtimeAccessCommand.ExecuteNonQuery();
                // eth values
                realtimeAccessCommand.Dispose();
                realtimeAccessCommand = new OleDbCommand("INSERT INTO realtime_price (CurrentTime, currentDate, Bitfinex, Poloniex, Coinbase, type) VALUES (?, ?, ?, ?, ?, ?)", Myconnection);
                realtimeAccessCommand.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm"));//, Myconnection
                realtimeAccessCommand.Parameters.AddWithValue("@currentDate", DateTime.Now.ToString("yyyy-M-dd"));
                realtimeAccessCommand.Parameters.AddWithValue("@Bitfinex", bitfinex_eth.ToString());
                realtimeAccessCommand.Parameters.AddWithValue("@Poloniex", poloniex_eth.ToString());
                realtimeAccessCommand.Parameters.AddWithValue("@Coinbase", coinbase_eth.ToString());
                realtimeAccessCommand.Parameters.AddWithValue("@type", "ETH");
                realtimeAccessCommand.ExecuteNonQuery();
                // ltc values
                realtimeAccessCommand.Dispose();
                realtimeAccessCommand = new OleDbCommand("INSERT INTO realtime_price (CurrentTime, currentDate, Bitfinex, Poloniex, Coinbase, type) VALUES (?, ?, ?, ?, ?, ?)", Myconnection);
                realtimeAccessCommand.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm"));//, Myconnection
                realtimeAccessCommand.Parameters.AddWithValue("@currentDate", DateTime.Now.ToString("yyyy-M-dd"));
                realtimeAccessCommand.Parameters.AddWithValue("@Bitfinex", bitfinex_ltc.ToString());
                realtimeAccessCommand.Parameters.AddWithValue("@Poloniex", poloniex_ltc.ToString());
                realtimeAccessCommand.Parameters.AddWithValue("@Coinbase", coinbase_ltc.ToString());
                realtimeAccessCommand.Parameters.AddWithValue("@type", "LTC");
                realtimeAccessCommand.ExecuteNonQuery();
                // bch values
                realtimeAccessCommand.Dispose();
                realtimeAccessCommand = new OleDbCommand("INSERT INTO realtime_price (CurrentTime, currentDate, Bitfinex, Poloniex, Coinbase, type) VALUES (?, ?, ?, ?, ?, ?)", Myconnection);
                realtimeAccessCommand.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm"));//, Myconnection
                realtimeAccessCommand.Parameters.AddWithValue("@currentDate", DateTime.Now.ToString("yyyy-M-dd"));
                realtimeAccessCommand.Parameters.AddWithValue("@Bitfinex", bitfinex_bch.ToString());
                realtimeAccessCommand.Parameters.AddWithValue("@Poloniex", poloniex_bch.ToString());
                realtimeAccessCommand.Parameters.AddWithValue("@Coinbase", "0");
                realtimeAccessCommand.Parameters.AddWithValue("@type", "BCH");
                realtimeAccessCommand.ExecuteNonQuery();
                // etc values
                realtimeAccessCommand.Dispose();
                realtimeAccessCommand = new OleDbCommand("INSERT INTO realtime_price (CurrentTime, currentDate, Bitfinex, Poloniex, Coinbase, type) VALUES (?, ?, ?, ?, ?, ?)", Myconnection);
                realtimeAccessCommand.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm"));//, Myconnection
                realtimeAccessCommand.Parameters.AddWithValue("@currentDate", DateTime.Now.ToString("yyyy-M-dd"));
                realtimeAccessCommand.Parameters.AddWithValue("@Bitfinex", bitfinex_etc.ToString());
                realtimeAccessCommand.Parameters.AddWithValue("@Poloniex", poloniex_etc.ToString());
                realtimeAccessCommand.Parameters.AddWithValue("@Coinbase", "0");
                realtimeAccessCommand.Parameters.AddWithValue("@type", "ETC");
                realtimeAccessCommand.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
            killForm.Abort();

            Process[] process = Process.GetProcessesByName("SimpleDesignApp");
            Mainform.Myconnection.Close();
            for (int i = 0; i < process.Length; i++)
                process[i].Kill();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            SettingForm setform = new SettingForm();
            setform.Location = this.Location;
            setform.ShowDialog(this);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            makeScreen();
            // Date Time display
            dateTimeLabel.Text = FrmMain.currentTime;
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            settingLabel.BackColor = Color.FromArgb(135, 146, 210);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            settingLabel.BackColor = Color.FromArgb(85, 96, 110);
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
        

        private void coinbaseLabel_Click_1(object sender, EventArgs e)
        {
            coinbaseLabel.BackColor = Color.FromArgb(85, 96, 110);
            poloniexLabel.BackColor = Color.FromArgb(36, 175, 178);
            bitfinexLabel.BackColor = Color.FromArgb(36, 175, 178);

            var margin = coinbaseLabel.Margin;
            margin.Top = 0;
            coinbaseLabel.Margin = margin;

            margin = poloniexLabel.Margin;
            margin.Top = 2;
            poloniexLabel.Margin = margin;

            margin = bitfinexLabel.Margin;
            margin.Top = 2;
            bitfinexLabel.Margin = margin;

            website = "Coinbase";
            makeScreen();
        }

        private void poloniexLabel_Click_1(object sender, EventArgs e)
        {
            coinbaseLabel.BackColor = Color.FromArgb(36, 175, 178);
            poloniexLabel.BackColor = Color.FromArgb(85, 96, 110);
            bitfinexLabel.BackColor = Color.FromArgb(36, 175, 178);

            var margin = coinbaseLabel.Margin;
            margin.Top =2;
            coinbaseLabel.Margin = margin;

            margin = poloniexLabel.Margin;
            margin.Top = 0;
            poloniexLabel.Margin = margin;

            margin = bitfinexLabel.Margin;
            margin.Top = 2;
            bitfinexLabel.Margin = margin;

            website = "Poloniex";
            makeScreen();
        }

        private void bitfinexLabel_Click_1(object sender, EventArgs e)
        {
            coinbaseLabel.BackColor = Color.FromArgb(36, 175, 178);
            poloniexLabel.BackColor = Color.FromArgb(36, 175, 178);
            bitfinexLabel.BackColor = Color.FromArgb(85, 96, 110);

            var margin = coinbaseLabel.Margin;
            margin.Top = 2;
            coinbaseLabel.Margin = margin;

            margin = poloniexLabel.Margin;
            margin.Top = 2;
            poloniexLabel.Margin = margin;

            margin = bitfinexLabel.Margin;
            margin.Top = 0;
            bitfinexLabel.Margin = margin;

            website = "Bitfinex";
            makeScreen();
        }

        private void changeLabelBackground(String selectedLabel)
        {
            switch (selectedLabel)
            {
                case "btc":
                    btcLabel.BackColor = Color.FromArgb(244, 244, 244);
                    btcValueLabel.BackColor = Color.FromArgb(244, 244, 244);
                    btcDiffLabel.BackColor = Color.FromArgb(244, 244, 244);

                    ethLabel.BackColor = Color.Transparent; ethValueLabel.BackColor = Color.Transparent; ethDiffLabel.BackColor = Color.Transparent;
                    ltcLabel.BackColor = Color.Transparent; ltcValueLabel.BackColor = Color.Transparent; ltcDiffLabel.BackColor = Color.Transparent;
                    etcLabel.BackColor = Color.Transparent; etcValueLabel.BackColor = Color.Transparent; etcDiffLabel.BackColor = Color.Transparent;
                    bchLabel.BackColor = Color.Transparent; bchValueLabel.BackColor = Color.Transparent; bchDiffLabel.BackColor = Color.Transparent;
                    break;
                case "eth":
                    ethLabel.BackColor = Color.FromArgb(244, 244, 244);
                    ethValueLabel.BackColor = Color.FromArgb(244, 244, 244);
                    ethDiffLabel.BackColor = Color.FromArgb(244, 244, 244);

                    btcLabel.BackColor = Color.Transparent; btcValueLabel.BackColor = Color.Transparent; btcDiffLabel.BackColor = Color.Transparent;
                    ltcLabel.BackColor = Color.Transparent; ltcValueLabel.BackColor = Color.Transparent; ltcDiffLabel.BackColor = Color.Transparent;
                    etcLabel.BackColor = Color.Transparent; etcValueLabel.BackColor = Color.Transparent; etcDiffLabel.BackColor = Color.Transparent;
                    bchLabel.BackColor = Color.Transparent; bchValueLabel.BackColor = Color.Transparent; bchDiffLabel.BackColor = Color.Transparent;
                    break;
                case "ltc":
                    ltcLabel.BackColor = Color.FromArgb(244, 244, 244);
                    ltcValueLabel.BackColor = Color.FromArgb(244, 244, 244);
                    ltcDiffLabel.BackColor = Color.FromArgb(244, 244, 244);

                    btcLabel.BackColor = Color.Transparent; btcValueLabel.BackColor = Color.Transparent; btcDiffLabel.BackColor = Color.Transparent;
                    ethLabel.BackColor = Color.Transparent; ethValueLabel.BackColor = Color.Transparent; ethDiffLabel.BackColor = Color.Transparent;
                    etcLabel.BackColor = Color.Transparent; etcValueLabel.BackColor = Color.Transparent; etcDiffLabel.BackColor = Color.Transparent;
                    bchLabel.BackColor = Color.Transparent; bchValueLabel.BackColor = Color.Transparent; bchDiffLabel.BackColor = Color.Transparent;
                    break;
                case "etc":
                    if (website == "Coinbase")
                        break;
                    etcLabel.BackColor = Color.FromArgb(244, 244, 244);
                    etcValueLabel.BackColor = Color.FromArgb(244, 244, 244);
                    etcDiffLabel.BackColor = Color.FromArgb(244, 244, 244);

                    btcLabel.BackColor = Color.Transparent; btcValueLabel.BackColor = Color.Transparent; btcDiffLabel.BackColor = Color.Transparent;
                    ethLabel.BackColor = Color.Transparent; ethValueLabel.BackColor = Color.Transparent; ethDiffLabel.BackColor = Color.Transparent;
                    ltcLabel.BackColor = Color.Transparent; ltcValueLabel.BackColor = Color.Transparent; ltcDiffLabel.BackColor = Color.Transparent;
                    bchLabel.BackColor = Color.Transparent; bchValueLabel.BackColor = Color.Transparent; bchDiffLabel.BackColor = Color.Transparent;
                    break;
                case "bch":
                    if (website == "Coinbase")
                        break;
                    bchLabel.BackColor = Color.FromArgb(244, 244, 244);
                    bchValueLabel.BackColor = Color.FromArgb(244, 244, 244);
                    bchDiffLabel.BackColor = Color.FromArgb(244, 244, 244);

                    btcLabel.BackColor = Color.Transparent; btcValueLabel.BackColor = Color.Transparent; btcDiffLabel.BackColor = Color.Transparent;
                    ethLabel.BackColor = Color.Transparent; ethValueLabel.BackColor = Color.Transparent; ethDiffLabel.BackColor = Color.Transparent;
                    ltcLabel.BackColor = Color.Transparent; ltcValueLabel.BackColor = Color.Transparent; ltcDiffLabel.BackColor = Color.Transparent;
                    etcLabel.BackColor = Color.Transparent; etcValueLabel.BackColor = Color.Transparent; etcDiffLabel.BackColor = Color.Transparent;
                    break;
            }
        }

        private void btcLabel_Click(object sender, EventArgs e)
        {
            changeLabelBackground("btc");
        }

        private void ethLabel_Click(object sender, EventArgs e)
        {
            changeLabelBackground("eth");
        }

        private void ltcLabel_Click(object sender, EventArgs e)
        {
            changeLabelBackground("ltc");
        }

        private void bchLabel_Click(object sender, EventArgs e)
        {
            changeLabelBackground("bch");
        }

        private void etcLabel_Click(object sender, EventArgs e)
        {
            changeLabelBackground("etc");
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.tableLayoutPanel3.ClientRectangle, Color.FromArgb(211, 211, 211), ButtonBorderStyle.Solid);
        }

        private void btcValueLabel_Click(object sender, EventArgs e)
        {
            changeLabelBackground("btc");
        }

        private void ethValueLabel_Click(object sender, EventArgs e)
        {
            changeLabelBackground("eth");
        }

        private void ltcValueLabel_Click(object sender, EventArgs e)
        {
            changeLabelBackground("ltc");
        }

        private void bchValueLabel_Click(object sender, EventArgs e)
        {
            changeLabelBackground("bch");
        }

        private void etcValueLabel_Click(object sender, EventArgs e)
        {
            changeLabelBackground("etc");
        }

    }
}
