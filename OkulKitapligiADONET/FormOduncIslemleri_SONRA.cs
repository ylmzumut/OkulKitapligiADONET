using OkulKitapligiADONET_BLL;
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
    public partial class FormOduncIslemleri_SONRA : Form
    {
        public FormOduncIslemleri_SONRA()
        {
            InitializeComponent();
        }

        //Global alan
        KitapOduncIslemManager kitapOduncIslemManager = new KitapOduncIslemManager();
        private void FormOduncIslemleri_Load(object sender, EventArgs e)
        {
            //sayfa load olduğunda kitaplar listboxa gelecek
            TumKitaplariUC_MyListBoxKitaplaraGetir();
            //sayfa load olduğunda öğrenciler listboxa gelecek

        }
        private void TumKitaplariUC_MyListBoxKitaplaraGetir()
        {
            UC_MyListBoxKitaplar.myListBox.DataSource = kitapOduncIslemManager.TumKitaplariGetir();
        }
    }
}
