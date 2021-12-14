
namespace OkulKitapligiADONET
{
    partial class FormKitaplar
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridViewKitaplar = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.comboBoxKitapGuncelle = new System.Windows.Forms.ComboBox();
            this.groupBoxKitapGuncelle = new System.Windows.Forms.GroupBox();
            this.UC_MyButton_Guncelle = new OkulKitapligiADONET.UC_MyButton();
            this.numericUpDown_Guncelle_Stok = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Guncelle_SayfaSayisi = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbBox_Guncelle_Tur = new System.Windows.Forms.ComboBox();
            this.cmbBox_Guncelle_Yazar = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_GuncelleKitapAdi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKitaplar)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBoxKitapGuncelle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Guncelle_Stok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Guncelle_SayfaSayisi)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 510);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridViewKitaplar);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 477);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "LİSTELE";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewKitaplar
            // 
            this.dataGridViewKitaplar.AllowUserToAddRows = false;
            this.dataGridViewKitaplar.AllowUserToDeleteRows = false;
            this.dataGridViewKitaplar.AllowUserToOrderColumns = true;
            this.dataGridViewKitaplar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKitaplar.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewKitaplar.Name = "dataGridViewKitaplar";
            this.dataGridViewKitaplar.ReadOnly = true;
            this.dataGridViewKitaplar.RowHeadersWidth = 51;
            this.dataGridViewKitaplar.RowTemplate.Height = 29;
            this.dataGridViewKitaplar.Size = new System.Drawing.Size(778, 403);
            this.dataGridViewKitaplar.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 477);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "EKLE";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.comboBoxKitapGuncelle);
            this.tabPage3.Controls.Add(this.groupBoxKitapGuncelle);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.ForeColor = System.Drawing.Color.PaleTurquoise;
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 477);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "GÜNCELLE";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // comboBoxKitapGuncelle
            // 
            this.comboBoxKitapGuncelle.BackColor = System.Drawing.Color.LightCyan;
            this.comboBoxKitapGuncelle.FormattingEnabled = true;
            this.comboBoxKitapGuncelle.Location = new System.Drawing.Point(164, 72);
            this.comboBoxKitapGuncelle.Name = "comboBoxKitapGuncelle";
            this.comboBoxKitapGuncelle.Size = new System.Drawing.Size(441, 28);
            this.comboBoxKitapGuncelle.TabIndex = 2;
            this.comboBoxKitapGuncelle.SelectedIndexChanged += new System.EventHandler(this.comboBoxKitapGuncelle_SelectedIndexChanged);
            // 
            // groupBoxKitapGuncelle
            // 
            this.groupBoxKitapGuncelle.Controls.Add(this.UC_MyButton_Guncelle);
            this.groupBoxKitapGuncelle.Controls.Add(this.numericUpDown_Guncelle_Stok);
            this.groupBoxKitapGuncelle.Controls.Add(this.numericUpDown_Guncelle_SayfaSayisi);
            this.groupBoxKitapGuncelle.Controls.Add(this.label6);
            this.groupBoxKitapGuncelle.Controls.Add(this.label5);
            this.groupBoxKitapGuncelle.Controls.Add(this.cmbBox_Guncelle_Tur);
            this.groupBoxKitapGuncelle.Controls.Add(this.cmbBox_Guncelle_Yazar);
            this.groupBoxKitapGuncelle.Controls.Add(this.label4);
            this.groupBoxKitapGuncelle.Controls.Add(this.label3);
            this.groupBoxKitapGuncelle.Controls.Add(this.txt_GuncelleKitapAdi);
            this.groupBoxKitapGuncelle.Controls.Add(this.label2);
            this.groupBoxKitapGuncelle.Location = new System.Drawing.Point(45, 127);
            this.groupBoxKitapGuncelle.Name = "groupBoxKitapGuncelle";
            this.groupBoxKitapGuncelle.Size = new System.Drawing.Size(695, 316);
            this.groupBoxKitapGuncelle.TabIndex = 1;
            this.groupBoxKitapGuncelle.TabStop = false;
            // 
            // UC_MyButton_Guncelle
            // 
            this.UC_MyButton_Guncelle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UC_MyButton_Guncelle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.UC_MyButton_Guncelle.Location = new System.Drawing.Point(58, 249);
            this.UC_MyButton_Guncelle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UC_MyButton_Guncelle.Name = "UC_MyButton_Guncelle";
            this.UC_MyButton_Guncelle.Size = new System.Drawing.Size(564, 61);
            this.UC_MyButton_Guncelle.TabIndex = 13;
            // 
            // numericUpDown_Guncelle_Stok
            // 
            this.numericUpDown_Guncelle_Stok.Location = new System.Drawing.Point(443, 208);
            this.numericUpDown_Guncelle_Stok.Name = "numericUpDown_Guncelle_Stok";
            this.numericUpDown_Guncelle_Stok.Size = new System.Drawing.Size(180, 27);
            this.numericUpDown_Guncelle_Stok.TabIndex = 11;
            // 
            // numericUpDown_Guncelle_SayfaSayisi
            // 
            this.numericUpDown_Guncelle_SayfaSayisi.Location = new System.Drawing.Point(171, 208);
            this.numericUpDown_Guncelle_SayfaSayisi.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown_Guncelle_SayfaSayisi.Name = "numericUpDown_Guncelle_SayfaSayisi";
            this.numericUpDown_Guncelle_SayfaSayisi.Size = new System.Drawing.Size(179, 27);
            this.numericUpDown_Guncelle_SayfaSayisi.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label6.Location = new System.Drawing.Point(378, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "Stok:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label5.Location = new System.Drawing.Point(46, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 25);
            this.label5.TabIndex = 8;
            this.label5.Text = "Sayfa Sayısı:";
            // 
            // cmbBox_Guncelle_Tur
            // 
            this.cmbBox_Guncelle_Tur.FormattingEnabled = true;
            this.cmbBox_Guncelle_Tur.Location = new System.Drawing.Point(131, 150);
            this.cmbBox_Guncelle_Tur.Name = "cmbBox_Guncelle_Tur";
            this.cmbBox_Guncelle_Tur.Size = new System.Drawing.Size(491, 28);
            this.cmbBox_Guncelle_Tur.TabIndex = 7;
            // 
            // cmbBox_Guncelle_Yazar
            // 
            this.cmbBox_Guncelle_Yazar.FormattingEnabled = true;
            this.cmbBox_Guncelle_Yazar.Location = new System.Drawing.Point(131, 86);
            this.cmbBox_Guncelle_Yazar.Name = "cmbBox_Guncelle_Yazar";
            this.cmbBox_Guncelle_Yazar.Size = new System.Drawing.Size(491, 28);
            this.cmbBox_Guncelle_Yazar.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label4.Location = new System.Drawing.Point(45, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Yazar:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label3.Location = new System.Drawing.Point(58, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tür:";
            // 
            // txt_GuncelleKitapAdi
            // 
            this.txt_GuncelleKitapAdi.Location = new System.Drawing.Point(131, 24);
            this.txt_GuncelleKitapAdi.Name = "txt_GuncelleKitapAdi";
            this.txt_GuncelleKitapAdi.Size = new System.Drawing.Size(491, 27);
            this.txt_GuncelleKitapAdi.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(17, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Kitap Adı:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Purple;
            this.label1.Location = new System.Drawing.Point(201, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(383, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Güncellemek istediğiniz kitabı seçiniz...";
            // 
            // FormKitaplar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 510);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormKitaplar";
            this.Text = "Kitap İşlemleri";
            this.Load += new System.EventHandler(this.FormKitaplar_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKitaplar)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBoxKitapGuncelle.ResumeLayout(false);
            this.groupBoxKitapGuncelle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Guncelle_Stok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Guncelle_SayfaSayisi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridViewKitaplar;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox comboBoxKitapGuncelle;
        private System.Windows.Forms.GroupBox groupBoxKitapGuncelle;
        private System.Windows.Forms.NumericUpDown numericUpDown_Guncelle_Stok;
        private System.Windows.Forms.NumericUpDown numericUpDown_Guncelle_SayfaSayisi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbBox_Guncelle_Tur;
        private System.Windows.Forms.ComboBox cmbBox_Guncelle_Yazar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_GuncelleKitapAdi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private UC_MyButton UC_MyButton_Guncelle;
    }
}