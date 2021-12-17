using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulKitapligiADONET
{
    public partial class FormGiris : Form
    {
        public FormGiris()
        {
            InitializeComponent();
        }

        private void FormGiris_Load(object sender, EventArgs e)
        {
            UC_MyButtonFormKitaplar.myButton.Text = "Kitaplar Formu";
            UC_MyButtonFormKitaplar.myButton.Click += new EventHandler(btn_FormKitaplar);
        }

        private void btn_FormKitaplar(object sender,EventArgs e)
        {
            FormKitaplar frmKitap = new FormKitaplar();
            this.Hide();
            frmKitap.Show();
        }
    }

}
