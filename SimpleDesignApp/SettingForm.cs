using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Reflection;

namespace SimpleDesignApp
{
    public partial class SettingForm : Form
    {

        // currency api
        String currency = "http://api.fixer.io/latest?base=USD";

        PrivateFontCollection pfc;

        String updateSiteURL = "http://www.mysite.com/version.txt";

        public SettingForm()
        {
            InitializeComponent();

            pfc = new PrivateFontCollection();
            pfc.AddFontFile("fonts\\Oswald-Bold.ttf");
            pfc.AddFontFile("fonts\\Oswald-Regular.ttf");
            pfc.AddFontFile("fonts\\Roboto-Bold_0.ttf");
            pfc.AddFontFile("fonts\\Roboto-Regular_0.ttf");

            Font robotoFont_9_regular = new Font(pfc.Families[1], 9);
            Font robotoFont_10_regular = new Font(pfc.Families[1], 10);
            Font robotoFont_10_bold = new Font(pfc.Families[1], 10, FontStyle.Bold);
            Font robotoFont_10_bold_underline = new Font(pfc.Families[1], 10, FontStyle.Bold | FontStyle.Underline);
            Font robotoFont_11_bold = new Font(pfc.Families[1], 11, FontStyle.Bold);

            Font oswaldFont_11_regular = new Font(pfc.Families[0], 11, FontStyle.Regular);

            currencyLabel.Font = oswaldFont_11_regular;
            viewTypeLabel.Font = oswaldFont_11_regular;
            timezoneLabel.Font = oswaldFont_11_regular;
            intervalLabel.Font = oswaldFont_11_regular;

            currencyComboBox.Font = robotoFont_11_bold;
            numericalRadioButton.Font = robotoFont_9_regular;
            graphicalRadioButton.Font = robotoFont_9_regular;
            utcComboBox.Font = robotoFont_10_regular;
            timeIntervalTextBox.Font = robotoFont_10_regular;
            topCheckBox.Font = robotoFont_9_regular;
            checkUpdateLinkLabel.Font = robotoFont_9_regular;

            okButtonLabel.Font = robotoFont_10_bold;
            quitLabel.Font = robotoFont_10_bold_underline;

            makeCurrencyList();

            ReadOnlyCollection<TimeZoneInfo> tz = TimeZoneInfo.GetSystemTimeZones();
            //MessageBox.Show(tz.First<TimeZoneInfo>().ToString());
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
                utcComboBox.Items.Add(z.Id);

            initSettings();
        }
        public void makeCurrencyList()
        {
            var json = new WebClient().DownloadString(currency);
            String data = getValueFromJSON(json, "\":{", "}");
            String[] separator = { "\"", "\":" };
            String[] list_currency = data.Split(separator, StringSplitOptions.None);
            data = "";
            currencyComboBox.Items.Add("USD");
            for (int i = 1; i < list_currency.Length; i += 2)
                currencyComboBox.Items.Add(list_currency[i]);
            currencyComboBox.SelectedIndex = 0;
        }// the function which extracts the value from json

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
                MessageBox.Show(this, ex.ToString());
            }

            return value;
        }

        public void initSettings()
        {
            currencyComboBox.SelectedIndex = Mainform.currencyIndex;
            if (Mainform.viewIndex == 0)
                numericalRadioButton.Checked = true;
            else
                graphicalRadioButton.Checked = true;
            utcComboBox.SelectedIndex = Mainform.timezoneIndex;
            timeIntervalTextBox.Text = Mainform.timeInterval.ToString();
            topCheckBox.Checked = Mainform.alwaysOnTopValue;
        }

        private void currencyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currencyComboBox.SelectedIndex == 0)
                FrmMain.usdcurrency = 1;
            else
            {
                var json = new WebClient().DownloadString(currency);
                String data = getValueFromJSON(json, "\":{", "}");
                String value = getValueFromJSON(data, currencyComboBox.SelectedItem.ToString() + "\":", ",");
                FrmMain.usdcurrency = float.Parse(value);
                FrmMain.currency_title = currencyComboBox.SelectedItem.ToString();
            }
        }

        private void utcComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById(utcComboBox.SelectedItem.ToString());
            //TimeZoneInfo.
            FrmMain.currentTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, utcComboBox.SelectedItem.ToString()).ToString("yyyy-MM-dd HH:mm");
            
            Mainform.timezoneIndex = utcComboBox.SelectedIndex;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (saveSettings())
                this.Close();
            else
                return;

            if (numericalRadioButton.Checked)
            {
                if (Mainform.graphicalDisplay.Visible == true)
                    Mainform.valueForm.Location = Mainform.graphicalDisplay.Location;
                Mainform.valueForm.Visible = true;
                Mainform.graphicalDisplay.Visible = false;
                Mainform.valueForm.Activate();
            }
            else
            {
                if (Mainform.valueForm.Visible == true)
                    Mainform.graphicalDisplay.Location = Mainform.valueForm.Location;
                Mainform.valueForm.Visible = false;
                Mainform.graphicalDisplay.Visible = true;
                Mainform.graphicalDisplay.Activate();
            }
            if (topCheckBox.Checked)
            {
                Mainform.valueForm.TopMost = true;
                Mainform.graphicalDisplay.TopMost = true;
            }
            else
            {
                Mainform.valueForm.TopMost = false;
                Mainform.graphicalDisplay.TopMost = false;
            }
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            okButtonLabel.BackColor = Color.FromArgb(85, 96, 110);
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            okButtonLabel.BackColor = Color.FromArgb(36, 175, 178);
        }
        
        public bool saveSettings()
        {            
            String str = timeIntervalTextBox.Text;
            int refreshTime = 30;
            if (int.TryParse(str, out refreshTime))
                Mainform.timeInterval = refreshTime;
            else
            {
                MessageBox.Show(this, "Plese select the validated time interval values");
                timeIntervalTextBox.Text = "30";
                return false;
            }

            Mainform.currencyIndex =  currencyComboBox.SelectedIndex;
            if (numericalRadioButton.Checked)
            {
                Mainform.viewIndex = 0;
            }
            else
            {
                Mainform.viewIndex = 1;
            }
            Mainform.timezoneIndex = utcComboBox.SelectedIndex;
            Mainform.alwaysOnTopValue = topCheckBox.Checked;

            return true;
        }
        
        private void quitLabel_Click(object sender, EventArgs e)
        {
            Process[] process = Process.GetProcessesByName("SimpleDesignApp");
            Mainform.Myconnection.Close();
            for (int i = 0; i < process.Length; i++)
                process[i].Kill();

        }

        private void quitLabel_MouseHover(object sender, EventArgs e)
        {
            quitLabel.BackColor = Color.FromArgb(85, 96, 110);
        }

        private void quitLabel_MouseLeave(object sender, EventArgs e)
        {
            quitLabel.BackColor = Color.Transparent;
        }

        private void checkUpdateLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WebRequest wr = WebRequest.Create(new Uri(updateSiteURL));
            WebResponse ws = wr.GetResponse();
            StreamReader sr = new StreamReader(ws.GetResponseStream());

            string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string newversion = sr.ReadToEnd();
            if (currentVersion.Contains(newversion))
            {
                MessageBox.Show(this, "This is the latest version!");
            }
            else
            {
                DialogResult result = MessageBox.Show(this, "Do you want to install updated version?", "Update", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Mainform.Myconnection.Close();
                    Process.Start("UpdateVersionApp.exe");
                }
            }
        }

    }
}
