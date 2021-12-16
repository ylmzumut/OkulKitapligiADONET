
namespace OkulKitapligiADONET
{
    partial class FormKitapOduncIslemleri
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
            this.comboBoxOgrenci = new System.Windows.Forms.ComboBox();
            this.comboBoxKitap = new System.Windows.Forms.ComboBox();
            this.groupBoxOgrenci = new System.Windows.Forms.GroupBox();
            this.groupBoxKitap = new System.Windows.Forms.GroupBox();
            this.groupBoxOduncTarihler = new System.Windows.Forms.GroupBox();
            this.dateTimePickerBitis = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerBaslangic = new System.Windows.Forms.DateTimePicker();
            this.UC_MyButtonOduncAl = new OkulKitapligiADONET.UC_MyButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewOduncKitaplar = new System.Windows.Forms.DataGridView();
            this.groupBoxOgrenci.SuspendLayout();
            this.groupBoxKitap.SuspendLayout();
            this.groupBoxOduncTarihler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOduncKitaplar)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxOgrenci
            // 
            this.comboBoxOgrenci.FormattingEnabled = true;
            this.comboBoxOgrenci.Location = new System.Drawing.Point(6, 47);
            this.comboBoxOgrenci.Name = "comboBoxOgrenci";
            this.comboBoxOgrenci.Size = new System.Drawing.Size(356, 28);
            this.comboBoxOgrenci.TabIndex = 0;
            this.comboBoxOgrenci.Text = "Öğrenci seçiniz...";
            this.comboBoxOgrenci.SelectedIndexChanged += new System.EventHandler(this.comboBoxOgrenci_SelectedIndexChanged);
            // 
            // comboBoxKitap
            // 
            this.comboBoxKitap.FormattingEnabled = true;
            this.comboBoxKitap.Location = new System.Drawing.Point(6, 44);
            this.comboBoxKitap.Name = "comboBoxKitap";
            this.comboBoxKitap.Size = new System.Drawing.Size(356, 28);
            this.comboBoxKitap.TabIndex = 1;
            this.comboBoxKitap.Text = "Kitap seçiniz...";
            this.comboBoxKitap.SelectedIndexChanged += new System.EventHandler(this.comboBoxKitap_SelectedIndexChanged);
            // 
            // groupBoxOgrenci
            // 
            this.groupBoxOgrenci.Controls.Add(this.comboBoxOgrenci);
            this.groupBoxOgrenci.Location = new System.Drawing.Point(30, 44);
            this.groupBoxOgrenci.Name = "groupBoxOgrenci";
            this.groupBoxOgrenci.Size = new System.Drawing.Size(368, 107);
            this.groupBoxOgrenci.TabIndex = 2;
            this.groupBoxOgrenci.TabStop = false;
            this.groupBoxOgrenci.Text = "Öğrenci";
            // 
            // groupBoxKitap
            // 
            this.groupBoxKitap.Controls.Add(this.comboBoxKitap);
            this.groupBoxKitap.Location = new System.Drawing.Point(30, 180);
            this.groupBoxKitap.Name = "groupBoxKitap";
            this.groupBoxKitap.Size = new System.Drawing.Size(368, 107);
            this.groupBoxKitap.TabIndex = 3;
            this.groupBoxKitap.TabStop = false;
            this.groupBoxKitap.Text = "Kitap";
            // 
            // groupBoxOduncTarihler
            // 
            this.groupBoxOduncTarihler.Controls.Add(this.dateTimePickerBitis);
            this.groupBoxOduncTarihler.Controls.Add(this.dateTimePickerBaslangic);
            this.groupBoxOduncTarihler.Controls.Add(this.UC_MyButtonOduncAl);
            this.groupBoxOduncTarihler.Controls.Add(this.label2);
            this.groupBoxOduncTarihler.Controls.Add(this.label1);
            this.groupBoxOduncTarihler.Location = new System.Drawing.Point(414, 44);
            this.groupBoxOduncTarihler.Name = "groupBoxOduncTarihler";
            this.groupBoxOduncTarihler.Size = new System.Drawing.Size(356, 243);
            this.groupBoxOduncTarihler.TabIndex = 4;
            this.groupBoxOduncTarihler.TabStop = false;
            this.groupBoxOduncTarihler.Text = "Tarih seçiniz...";
            // 
            // dateTimePickerBitis
            // 
            this.dateTimePickerBitis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerBitis.Location = new System.Drawing.Point(37, 148);
            this.dateTimePickerBitis.Name = "dateTimePickerBitis";
            this.dateTimePickerBitis.Size = new System.Drawing.Size(275, 27);
            this.dateTimePickerBitis.TabIndex = 4;
            // 
            // dateTimePickerBaslangic
            // 
            this.dateTimePickerBaslangic.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerBaslangic.Location = new System.Drawing.Point(37, 70);
            this.dateTimePickerBaslangic.Name = "dateTimePickerBaslangic";
            this.dateTimePickerBaslangic.Size = new System.Drawing.Size(275, 27);
            this.dateTimePickerBaslangic.TabIndex = 3;
            this.dateTimePickerBaslangic.ValueChanged += new System.EventHandler(this.dateTimePickerBaslangic_ValueChanged);
            // 
            // UC_MyButtonOduncAl
            // 
            this.UC_MyButtonOduncAl.Location = new System.Drawing.Point(19, 193);
            this.UC_MyButtonOduncAl.Name = "UC_MyButtonOduncAl";
            this.UC_MyButtonOduncAl.Size = new System.Drawing.Size(316, 44);
            this.UC_MyButtonOduncAl.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bitiş Tarihi:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Başlangıç Tarihi:";
            // 
            // dataGridViewOduncKitaplar
            // 
            this.dataGridViewOduncKitaplar.AllowUserToAddRows = false;
            this.dataGridViewOduncKitaplar.AllowUserToDeleteRows = false;
            this.dataGridViewOduncKitaplar.AllowUserToOrderColumns = true;
            this.dataGridViewOduncKitaplar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOduncKitaplar.Location = new System.Drawing.Point(30, 312);
            this.dataGridViewOduncKitaplar.Name = "dataGridViewOduncKitaplar";
            this.dataGridViewOduncKitaplar.ReadOnly = true;
            this.dataGridViewOduncKitaplar.RowHeadersWidth = 51;
            this.dataGridViewOduncKitaplar.RowTemplate.Height = 29;
            this.dataGridViewOduncKitaplar.Size = new System.Drawing.Size(740, 254);
            this.dataGridViewOduncKitaplar.TabIndex = 5;
            // 
            // FormKitapOduncIslemleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(790, 584);
            this.Controls.Add(this.dataGridViewOduncKitaplar);
            this.Controls.Add(this.groupBoxOduncTarihler);
            this.Controls.Add(this.groupBoxKitap);
            this.Controls.Add(this.groupBoxOgrenci);
            this.Name = "FormKitapOduncIslemleri";
            this.Text = "Kitap Ödunç Alım Formu";
            this.Load += new System.EventHandler(this.FormKitapOduncIslemleri_Load);
            this.groupBoxOgrenci.ResumeLayout(false);
            this.groupBoxKitap.ResumeLayout(false);
            this.groupBoxOduncTarihler.ResumeLayout(false);
            this.groupBoxOduncTarihler.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOduncKitaplar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimePickerBaslangic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewOduncKitaplar;
        public System.Windows.Forms.ComboBox comboBoxOgrenci;
        public System.Windows.Forms.ComboBox comboBoxKitap;
        public System.Windows.Forms.DateTimePicker dateTimePickerBitis;
        public UC_MyButton UC_MyButtonOduncAl;
        public System.Windows.Forms.GroupBox groupBoxOgrenci;
        public System.Windows.Forms.GroupBox groupBoxKitap;
        public System.Windows.Forms.GroupBox groupBoxOduncTarihler;
    }
}