
namespace OkulKitapligiADONET
{
    partial class FormGiris
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
            this.UC_MyButtonFormKitaplar = new OkulKitapligiADONET.UC_MyButton();
            this.uC_MyButton2 = new OkulKitapligiADONET.UC_MyButton();
            this.SuspendLayout();
            // 
            // UC_MyButtonFormKitaplar
            // 
            this.UC_MyButtonFormKitaplar.Location = new System.Drawing.Point(79, 47);
            this.UC_MyButtonFormKitaplar.Name = "UC_MyButtonFormKitaplar";
            this.UC_MyButtonFormKitaplar.Size = new System.Drawing.Size(162, 61);
            this.UC_MyButtonFormKitaplar.TabIndex = 0;
            // 
            // uC_MyButton2
            // 
            this.uC_MyButton2.Location = new System.Drawing.Point(79, 153);
            this.uC_MyButton2.Name = "uC_MyButton2";
            this.uC_MyButton2.Size = new System.Drawing.Size(162, 61);
            this.uC_MyButton2.TabIndex = 1;
            // 
            // FormGiris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 450);
            this.Controls.Add(this.uC_MyButton2);
            this.Controls.Add(this.UC_MyButtonFormKitaplar);
            this.Name = "FormGiris";
            this.Text = "HOŞGELDİNİZ...";
            this.Load += new System.EventHandler(this.FormGiris_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UC_MyButton UC_MyButtonFormKitaplar;
        private UC_MyButton uC_MyButton2;
    }
}