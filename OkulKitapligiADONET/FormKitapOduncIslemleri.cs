﻿using OkulKitapligiADONET_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using OkulKitapligiADONET_BLL.ViewModels;

namespace OkulKitapligiADONET
{
    public partial class FormKitapOduncIslemleri : Form
    {
        public FormKitapOduncIslemleri()
        {
            InitializeComponent();
        }
        //global alan
        KitapOduncIslemManager kitapOduncIslemManager = new KitapOduncIslemManager();

        private void FormKitapOduncIslemleri_Load(object sender, EventArgs e)
        {

            TumOgrencileriComboyaGetir();
            OgrenciGroupBoxTemizle();
            KitapGroupBoxPasifYap();
            OduncTarihGroupBoxPasifYap();
            //datetimepicker ayarlarını burada yapalım
            dateTimePickerBaslangic.Format = DateTimePickerFormat.Custom;
            dateTimePickerBaslangic.CustomFormat = "dd.MM.yyyy";
            dateTimePickerBaslangic.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dateTimePickerBitis.Format = DateTimePickerFormat.Custom;
            dateTimePickerBitis.CustomFormat = "dd.MM.yyyy";
            dateTimePickerBitis.MinDate = dateTimePickerBaslangic.Value.AddDays(1);
            dateTimePickerBitis.MaxDate = dateTimePickerBaslangic.Value.AddMonths(3);

            //data gridi dolduralım
            GridViewiAyarlaveDoldur();

            //
            UC_MyButtonOduncAl.myButton.Text = "ÖDÜNÇ AL";
            UC_MyButtonOduncAl.myButton.Click += new EventHandler(btn_OduncKitapAl);
        }

        private void btn_OduncKitapAl(object sender, EventArgs e)
        {
            try
            {
                bool kontrol = false;
                //tarihleri kontrol et
                if (dateTimePickerBitis.Value < dateTimePickerBaslangic.Value)
                {
                    MessageBox.Show("HATA: Tarih bilgilerinde yanlış giriş yaptınız!");
                }
                else
                {
                    if (comboBoxOgrenci.SelectedIndex > -1)
                    {
                        if (comboBoxKitap.SelectedIndex > -1)
                        {
                            //kitap stoğu var mı?
                            kontrol = kitapOduncIslemManager.KitabınStogunuGetir((int)comboBoxKitap.SelectedValue) == 0 ? false : true;

                        }
                        else
                        {
                            MessageBox.Show("HATA: Kitap seçmeden işlem yapılamaz!");
                            //temizlik
                        }
                    }
                    else
                    {
                        MessageBox.Show("HATA: Öğrenci seçmeden işlem yapılamaz!");
                        //temizlik
                    }
                }

                if (kontrol)
                {
                    string baslangicTarihi = "'" + dateTimePickerBaslangic.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                    string bitisTarihi = "'" + dateTimePickerBitis.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                    Hashtable htVeri = new Hashtable();
                    htVeri.Add("OgrID",(int)comboBoxOgrenci.SelectedValue);
                    htVeri.Add("KitapID",(int)comboBoxKitap.SelectedValue);
                    htVeri.Add("OduncAldigiTarih",baslangicTarihi);
                    htVeri.Add("OduncBitisTarih",bitisTarihi);
                    if (kitapOduncIslemManager.OduncKitapKaydiniYap("Islemler",htVeri))
                    {
                        MessageBox.Show("Ödünç alma işleminiz başarıyl kayıt edilmiştir.");
                        //temizlik
                        GridViewiAyarlaveDoldur();
                        OgrenciGroupBoxTemizle();
                        KitapGroupBoxPasifYap();
                        OduncTarihGroupBoxPasifYap();
                    }
                    else
                    {
                        MessageBox.Show("HATA: Kayıt eklenirken beklenmedik bir hata oluştu!");
                    }
                }
                else
                {
                    MessageBox.Show("HATA: Kitap stokta olmadığı için ödünç alamazsınız...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Beklenmedik hata oluştu!" + ex.Message);
            }

        }

        private void GridViewiAyarlaveDoldur()
        {
            dataGridViewOduncKitaplar.MultiSelect = false;
            dataGridViewOduncKitaplar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //
            //dataGridViewOduncKitaplar.DataSource = kitapOduncIslemManager.GrideVerileriGetir();
            //dataGridViewOduncKitaplar.Columns["IslemID"].Visible = false;
            //dataGridViewOduncKitaplar.Columns["KitapID"].Visible = false;

            //datagridview'in dataSource'una veriler viewmodel ile geldi.
            List<IslemViewModel> list = kitapOduncIslemManager.GrideVerileriViewModelleGetir();
            dataGridViewOduncKitaplar.DataSource = list;
            dataGridViewOduncKitaplar.Columns["IslemID"].Visible = false;
            dataGridViewOduncKitaplar.Columns["KitapID"].Visible = false;
            dataGridViewOduncKitaplar.Columns["OgrID"].Visible = false;
            dataGridViewOduncKitaplar.Columns["TeslimEdildiMi"].Visible = false;
            dataGridViewOduncKitaplar.Columns["TeslimEdildiMiString"].HeaderText = "Teslim Durumu";
            dataGridViewOduncKitaplar.Columns["OgrenciAdSoyad"].HeaderText = "Öğrenci Ad-Soyad";
            dataGridViewOduncKitaplar.Columns["OduncAldigiTarih"].HeaderText = "Ödünç Baş. Tarih";
            dataGridViewOduncKitaplar.Columns["OduncBitisTarih"].HeaderText = "Ödünç Bitiş Tarih";


            for (int i=0;i<dataGridViewOduncKitaplar.Columns.Count;i++)
            {
                dataGridViewOduncKitaplar.Columns[i].Width = 160;
            }
            dataGridViewOduncKitaplar.ContextMenuStrip = contextMenuStrip1;
        }

        private void TumKitaplariCombayaGetir()
        {
            comboBoxKitap.DisplayMember = "KitapAdi";
            comboBoxKitap.ValueMember = "KitapID";
            comboBoxKitap.DataSource = kitapOduncIslemManager.TumKitaplariGetir();
        }

        private void TumOgrencileriComboyaGetir()
        {
            comboBoxOgrenci.DisplayMember = "OgrenciAdSoyad";
            comboBoxOgrenci.ValueMember = "OgrID";
            comboBoxOgrenci.DataSource = kitapOduncIslemManager.TumOgrencileriGetir();
        }
        private void OgrenciGroupBoxTemizle()
        {
            comboBoxOgrenci.SelectedIndex = -1;
        }
        private void KitapGroupBoxPasifYap()
        {
            comboBoxKitap.SelectedIndex = -1;
            groupBoxKitap.Enabled = false;
        }
        private void KitapGroupBoxAktifYap()
        {
            groupBoxKitap.Enabled = true;
        }
        private void OduncTarihGroupBoxPasifYap()
        {
            dateTimePickerBaslangic.Value = DateTime.Now;
            dateTimePickerBitis.Value = DateTime.Now.AddDays(1);
            groupBoxOduncTarihler.Enabled = false;
        }
        private void OduncTarihGroupBoxAktifYap()
        {
            dateTimePickerBaslangic.Value = DateTime.Now;
            dateTimePickerBitis.Value = DateTime.Now.AddDays(1);
            groupBoxOduncTarihler.Enabled = true;
        }


        private void comboBoxOgrenci_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxOgrenci.SelectedIndex > -1)
            {
                //aktif
                KitapGroupBoxAktifYap();
                TumKitaplariCombayaGetir();
                comboBoxKitap.SelectedIndex = -1;
            }
        }

        private void comboBoxKitap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxKitap.SelectedIndex > -1)
            {
                OduncTarihGroupBoxAktifYap();
            }
            else
            {
                OduncTarihGroupBoxPasifYap();
            }
        }

        private void dateTimePickerBaslangic_ValueChanged(object sender, EventArgs e)
        {
            //burada seçilecek tarih bitişi etkileyecek
            dateTimePickerBitis.MinDate = dateTimePickerBaslangic.Value.AddDays(1);
            dateTimePickerBitis.Value = dateTimePickerBaslangic.Value.AddDays(1);
            dateTimePickerBitis.MaxDate = dateTimePickerBaslangic.Value.AddMonths(3);

        }

        private void kitabiTeslimEtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //veri tabanına aşağıdaki sorgu ile yeni alan eklendi
            //alter table Islemler
            //add TeslimEdildiMi bit not null default 0
            try
            {
                //datagridview'de seçilen satır alınacak
                //o satırdaki islemID ve kitapID alınacak
                DataGridViewRow secilenSatir = dataGridViewOduncKitaplar.SelectedRows[0];
                //islemID
                int islemID = (int)secilenSatir.Cells["IslemID"].Value;
                //kitapID
                int kitapID = (int)secilenSatir.Cells["KitapID"].Value;
                bool teslimSonuc = false;
                teslimSonuc = kitapOduncIslemManager.OduncKitapTeslimEt("Islemler", islemID, kitapID);
                if (teslimSonuc)
                {
                    MessageBox.Show("Teşekkürler... Kitap teslim alındı...");
                    //temizlik
                    GridViewiAyarlaveDoldur();
                    OgrenciGroupBoxTemizle();
                    KitapGroupBoxPasifYap();
                    OduncTarihGroupBoxPasifYap();
                }
                else
                {
                    MessageBox.Show("HATA: Kitap teslim edilemedi!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: Bir hata oluştu! "+ex.Message+" "+ex.ToString());
            }
        }
    }
}
