namespace SimpleDesignApp
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.quitLabel = new System.Windows.Forms.Label();
            this.okButtonLabel = new System.Windows.Forms.Label();
            this.intervalLabel = new System.Windows.Forms.Label();
            this.viewTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.graphicalRadioButton = new System.Windows.Forms.RadioButton();
            this.numericalRadioButton = new System.Windows.Forms.RadioButton();
            this.currencyComboBox = new System.Windows.Forms.ComboBox();
            this.currencyLabel = new System.Windows.Forms.Label();
            this.viewTypeLabel = new System.Windows.Forms.Label();
            this.timezoneLabel = new System.Windows.Forms.Label();
            this.utcComboBox = new System.Windows.Forms.ComboBox();
            this.timeIntervalTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.topCheckBox = new System.Windows.Forms.CheckBox();
            this.checkUpdateLinkLabel = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel2.SuspendLayout();
            this.viewTypeGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel2.Controls.Add(this.quitLabel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.okButtonLabel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(102, 279);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(293, 49);
            this.tableLayoutPanel2.TabIndex = 23;
            // 
            // quitLabel
            // 
            this.quitLabel.AutoSize = true;
            this.quitLabel.BackColor = System.Drawing.Color.Transparent;
            this.quitLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quitLabel.Font = new System.Drawing.Font("Roboto", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(175)))), ((int)(((byte)(178)))));
            this.quitLabel.Location = new System.Drawing.Point(102, 5);
            this.quitLabel.Margin = new System.Windows.Forms.Padding(0, 5, 60, 5);
            this.quitLabel.Name = "quitLabel";
            this.quitLabel.Size = new System.Drawing.Size(131, 39);
            this.quitLabel.TabIndex = 16;
            this.quitLabel.Text = "Quit the Program";
            this.quitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.quitLabel.Click += new System.EventHandler(this.quitLabel_Click);
            this.quitLabel.MouseLeave += new System.EventHandler(this.quitLabel_MouseLeave);
            this.quitLabel.MouseHover += new System.EventHandler(this.quitLabel_MouseHover);
            // 
            // okButtonLabel
            // 
            this.okButtonLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(175)))), ((int)(((byte)(178)))));
            this.okButtonLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.okButtonLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.okButtonLabel.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButtonLabel.ForeColor = System.Drawing.Color.White;
            this.okButtonLabel.Location = new System.Drawing.Point(10, 8);
            this.okButtonLabel.Margin = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.okButtonLabel.Name = "okButtonLabel";
            this.okButtonLabel.Size = new System.Drawing.Size(82, 33);
            this.okButtonLabel.TabIndex = 15;
            this.okButtonLabel.Text = "OK";
            this.okButtonLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.okButtonLabel.Click += new System.EventHandler(this.label4_Click);
            this.okButtonLabel.MouseLeave += new System.EventHandler(this.label4_MouseLeave);
            this.okButtonLabel.MouseHover += new System.EventHandler(this.label4_MouseHover);
            // 
            // intervalLabel
            // 
            this.intervalLabel.AutoSize = true;
            this.intervalLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.intervalLabel.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.intervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(96)))), ((int)(((byte)(110)))));
            this.intervalLabel.Location = new System.Drawing.Point(15, 181);
            this.intervalLabel.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.intervalLabel.Name = "intervalLabel";
            this.intervalLabel.Size = new System.Drawing.Size(84, 55);
            this.intervalLabel.TabIndex = 19;
            this.intervalLabel.Text = "Interval : ";
            this.intervalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // viewTypeGroupBox
            // 
            this.viewTypeGroupBox.Controls.Add(this.graphicalRadioButton);
            this.viewTypeGroupBox.Controls.Add(this.numericalRadioButton);
            this.viewTypeGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewTypeGroupBox.Location = new System.Drawing.Point(104, 74);
            this.viewTypeGroupBox.Margin = new System.Windows.Forms.Padding(5, 3, 20, 3);
            this.viewTypeGroupBox.Name = "viewTypeGroupBox";
            this.viewTypeGroupBox.Size = new System.Drawing.Size(274, 49);
            this.viewTypeGroupBox.TabIndex = 8;
            this.viewTypeGroupBox.TabStop = false;
            // 
            // graphicalRadioButton
            // 
            this.graphicalRadioButton.AutoSize = true;
            this.graphicalRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.graphicalRadioButton.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphicalRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(96)))), ((int)(((byte)(110)))));
            this.graphicalRadioButton.Location = new System.Drawing.Point(143, 16);
            this.graphicalRadioButton.Name = "graphicalRadioButton";
            this.graphicalRadioButton.Size = new System.Drawing.Size(78, 20);
            this.graphicalRadioButton.TabIndex = 1;
            this.graphicalRadioButton.TabStop = true;
            this.graphicalRadioButton.Text = "Graphical";
            this.graphicalRadioButton.UseVisualStyleBackColor = false;
            // 
            // numericalRadioButton
            // 
            this.numericalRadioButton.AutoSize = true;
            this.numericalRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.numericalRadioButton.Checked = true;
            this.numericalRadioButton.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(96)))), ((int)(((byte)(110)))));
            this.numericalRadioButton.Location = new System.Drawing.Point(14, 16);
            this.numericalRadioButton.Name = "numericalRadioButton";
            this.numericalRadioButton.Size = new System.Drawing.Size(81, 20);
            this.numericalRadioButton.TabIndex = 0;
            this.numericalRadioButton.TabStop = true;
            this.numericalRadioButton.Text = "Numerical";
            this.numericalRadioButton.UseVisualStyleBackColor = false;
            // 
            // currencyComboBox
            // 
            this.currencyComboBox.BackColor = System.Drawing.Color.White;
            this.currencyComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currencyComboBox.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currencyComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(96)))), ((int)(((byte)(110)))));
            this.currencyComboBox.FormattingEnabled = true;
            this.currencyComboBox.Location = new System.Drawing.Point(104, 25);
            this.currencyComboBox.Margin = new System.Windows.Forms.Padding(5, 25, 150, 15);
            this.currencyComboBox.Name = "currencyComboBox";
            this.currencyComboBox.Size = new System.Drawing.Size(144, 29);
            this.currencyComboBox.TabIndex = 7;
            this.currencyComboBox.Text = "USD";
            this.currencyComboBox.SelectedIndexChanged += new System.EventHandler(this.currencyComboBox_SelectedIndexChanged);
            // 
            // currencyLabel
            // 
            this.currencyLabel.AutoSize = true;
            this.currencyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currencyLabel.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currencyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(96)))), ((int)(((byte)(110)))));
            this.currencyLabel.Location = new System.Drawing.Point(15, 0);
            this.currencyLabel.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.currencyLabel.Name = "currencyLabel";
            this.currencyLabel.Size = new System.Drawing.Size(84, 71);
            this.currencyLabel.TabIndex = 0;
            this.currencyLabel.Text = "Currency : ";
            this.currencyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // viewTypeLabel
            // 
            this.viewTypeLabel.AutoSize = true;
            this.viewTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewTypeLabel.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewTypeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(96)))), ((int)(((byte)(110)))));
            this.viewTypeLabel.Location = new System.Drawing.Point(15, 71);
            this.viewTypeLabel.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.viewTypeLabel.Name = "viewTypeLabel";
            this.viewTypeLabel.Size = new System.Drawing.Size(84, 55);
            this.viewTypeLabel.TabIndex = 4;
            this.viewTypeLabel.Text = "View Type : ";
            this.viewTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timezoneLabel
            // 
            this.timezoneLabel.AutoSize = true;
            this.timezoneLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timezoneLabel.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timezoneLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(96)))), ((int)(((byte)(110)))));
            this.timezoneLabel.Location = new System.Drawing.Point(15, 126);
            this.timezoneLabel.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.timezoneLabel.Name = "timezoneLabel";
            this.timezoneLabel.Size = new System.Drawing.Size(84, 55);
            this.timezoneLabel.TabIndex = 6;
            this.timezoneLabel.Text = "Time Zone : ";
            this.timezoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // utcComboBox
            // 
            this.utcComboBox.BackColor = System.Drawing.Color.White;
            this.utcComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.utcComboBox.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.utcComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(96)))), ((int)(((byte)(110)))));
            this.utcComboBox.FormattingEnabled = true;
            this.utcComboBox.Location = new System.Drawing.Point(104, 135);
            this.utcComboBox.Margin = new System.Windows.Forms.Padding(5, 9, 20, 9);
            this.utcComboBox.Name = "utcComboBox";
            this.utcComboBox.Size = new System.Drawing.Size(274, 26);
            this.utcComboBox.TabIndex = 11;
            this.utcComboBox.SelectedIndexChanged += new System.EventHandler(this.utcComboBox_SelectedIndexChanged);
            // 
            // timeIntervalTextBox
            // 
            this.timeIntervalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timeIntervalTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeIntervalTextBox.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.timeIntervalTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(96)))), ((int)(((byte)(110)))));
            this.timeIntervalTextBox.Location = new System.Drawing.Point(104, 196);
            this.timeIntervalTextBox.Margin = new System.Windows.Forms.Padding(5, 15, 20, 9);
            this.timeIntervalTextBox.Name = "timeIntervalTextBox";
            this.timeIntervalTextBox.Size = new System.Drawing.Size(274, 25);
            this.timeIntervalTextBox.TabIndex = 20;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.timeIntervalTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.utcComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.timezoneLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.viewTypeLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.currencyLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.currencyComboBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.viewTypeGroupBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.intervalLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.5171F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.02454F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.02454F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.02454F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.54297F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.86629F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(398, 348);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.topCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.checkUpdateLinkLabel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(102, 239);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(293, 34);
            this.flowLayoutPanel1.TabIndex = 24;
            // 
            // topCheckBox
            // 
            this.topCheckBox.AutoSize = true;
            this.topCheckBox.Checked = true;
            this.topCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.topCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topCheckBox.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(96)))), ((int)(((byte)(110)))));
            this.topCheckBox.Location = new System.Drawing.Point(3, 3);
            this.topCheckBox.Name = "topCheckBox";
            this.topCheckBox.Size = new System.Drawing.Size(98, 20);
            this.topCheckBox.TabIndex = 17;
            this.topCheckBox.Text = "Alway on top";
            this.topCheckBox.UseVisualStyleBackColor = true;
            // 
            // checkUpdateLinkLabel
            // 
            this.checkUpdateLinkLabel.AutoSize = true;
            this.checkUpdateLinkLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkUpdateLinkLabel.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkUpdateLinkLabel.Location = new System.Drawing.Point(124, 0);
            this.checkUpdateLinkLabel.Margin = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.checkUpdateLinkLabel.Name = "checkUpdateLinkLabel";
            this.checkUpdateLinkLabel.Size = new System.Drawing.Size(106, 26);
            this.checkUpdateLinkLabel.TabIndex = 18;
            this.checkUpdateLinkLabel.TabStop = true;
            this.checkUpdateLinkLabel.Text = "Check for updates";
            this.checkUpdateLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkUpdateLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkUpdateLinkLabel_LinkClicked);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(398, 348);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.viewTypeGroupBox.ResumeLayout(false);
            this.viewTypeGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label quitLabel;
        private System.Windows.Forms.Label okButtonLabel;
        private System.Windows.Forms.Label intervalLabel;
        private System.Windows.Forms.GroupBox viewTypeGroupBox;
        private System.Windows.Forms.RadioButton graphicalRadioButton;
        private System.Windows.Forms.RadioButton numericalRadioButton;
        private System.Windows.Forms.ComboBox currencyComboBox;
        private System.Windows.Forms.Label currencyLabel;
        private System.Windows.Forms.Label viewTypeLabel;
        private System.Windows.Forms.Label timezoneLabel;
        private System.Windows.Forms.ComboBox utcComboBox;
        private System.Windows.Forms.TextBox timeIntervalTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox topCheckBox;
        private System.Windows.Forms.LinkLabel checkUpdateLinkLabel;

    }
}