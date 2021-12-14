using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulKitapligiADONET
{
    public partial class FormKitaplar : Form
    {
        public FormKitaplar()
        {
            InitializeComponent();
        }
        //Global alan
        string SQLBaglantiCumlesi = @"Server=DESKTOP-TUMHS1A;Database=OKULKITAPLIGI;Trusted_Connection=True;";
        SqlConnection baglanti = new SqlConnection();


        private void FormKitaplar_Load(object sender, EventArgs e)
        {
            dataGridViewKitaplar.MultiSelect = false;
            dataGridViewKitaplar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            TumKitaplariViewileGrideGetir();

            UC_MyButton_Guncelle.myButton.Text = "GÜNCELLE";

            UC_MyButton_Guncelle.myButton.Click += new EventHandler(btn_KitapGuncelle);


            GuncelleSayfasindakileriTemizle();
            //Düzenle'deki combo dolsun
            TumKitaplariComboBoxaGetir();

            //Yazarları combo'ya getir
            TumYazarlariComboBoxaGetir();
            //Türleri comba'ya getir
            TumTurleriComboBoxaGetir();

            tabControl1.Click += new EventHandler(tabControl1_Click);
        }
        private void tabControl1_Click(object sender, EventArgs e)
        {
            TumKitaplariViewileGrideGetir();
            TumKitaplariComboBoxaGetir();
        }
        private void TumYazarlariComboBoxaGetir()
        {
            try
            {
                using (baglanti)
                {
                    SqlCommand komut = new SqlCommand("select * from Yazarlar order by YazarAdSoyad", baglanti);
                    BaglantiyiAc();
                    SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                    DataTable sanalTablo = new DataTable();
                    adaptor.Fill(sanalTablo);
                    cmbBox_Guncelle_Yazar.DisplayMember = "YazarAdSoyad";
                    cmbBox_Guncelle_Yazar.ValueMember = "YazarID";
                    cmbBox_Guncelle_Yazar.DataSource = sanalTablo;
                    cmbBox_Guncelle_Yazar.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata oluştu! "+ex.Message);
            }
        }

        private void TumTurleriComboBoxaGetir()
        {
            try
            {
                using (baglanti)
                {
                    SqlCommand komut = new SqlCommand("select * from Turler order by TurAdi", baglanti);
                    BaglantiyiAc();
                    SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                    DataTable sanalTablo = new DataTable();
                    adaptor.Fill(sanalTablo);
                    cmbBox_Guncelle_Tur.DisplayMember = "TurAdi";
                    cmbBox_Guncelle_Tur.ValueMember = "TurID";
                    cmbBox_Guncelle_Tur.DataSource = sanalTablo;
                    cmbBox_Guncelle_Tur.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata oluştu! " + ex.Message);
            }
        }
        private void TumKitaplariComboBoxaGetir()
        {
            try
            {
                SqlCommand komut = new SqlCommand("select * from Kitaplar order by KitapAdi", baglanti);
                BaglantiyiAc();
                SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                DataTable sanalTablo = new DataTable();
                adaptor.Fill(sanalTablo);    
                comboBoxKitapGuncelle.DisplayMember = "KitapAdi";
                comboBoxKitapGuncelle.ValueMember = "KitapID";
                comboBoxKitapGuncelle.DataSource = sanalTablo;
                comboBoxKitapGuncelle.SelectedIndex = -1;
                BaglantiyiKapat();
                

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata oluştu!"+ex.Message);
            }
        }
        private void CombodanSecilenKitabinTumBilgileriniDoldur(int kitapID)
        {
            try
            {
                using (baglanti)
                {
                    SqlCommand komut = new SqlCommand($"select * from Kitaplar k left join Turler t on k.TurID=t.TurID inner join Yazarlar y on y.yazarID=k.yazarID where k.kitapID={kitapID}", baglanti);
                    BaglantiyiAc();
                    SqlDataReader okuyucu = komut.ExecuteReader();
                    while (okuyucu.Read())
                    {
                        txt_GuncelleKitapAdi.Text = okuyucu["KitapAdi"].ToString();
                        numericUpDown_Guncelle_SayfaSayisi.Value = (int)okuyucu["SayfaSayisi"];
                        numericUpDown_Guncelle_Stok.Value = (int)okuyucu["Stok"];
                        if (string.IsNullOrEmpty(okuyucu["TurID"].ToString()))
                        {
                            //kitabın türü yok
                            cmbBox_Guncelle_Tur.SelectedIndex = -1;
                            cmbBox_Guncelle_Tur.Text = "Tür Seçiniz...";
                        }
                        else
                        {
                            cmbBox_Guncelle_Tur.SelectedValue = (int)okuyucu["TurID"];
                        }
                        cmbBox_Guncelle_Yazar.SelectedValue=(int)okuyucu["YazarID"];
                    }
                    BaglantiyiKapat();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata oluştu"+ex.Message);
            }
        }
        private void btn_KitapGuncelle(object sender, EventArgs e)
        {
            //eğer yazar seçilmemişse uyarsın
            //eğer kitap adı yazılmamışsa uyarsın
            //stok ve sayfa sayısı > 0 olmalı

            if (numericUpDown_Guncelle_SayfaSayisi.Value<=0 || numericUpDown_Guncelle_Stok.Value<=0||cmbBox_Guncelle_Yazar.SelectedIndex<0|| string.IsNullOrEmpty(txt_GuncelleKitapAdi.Text.Trim()))
            {
                MessageBox.Show("Eksik bilgi girdiniz... Bilgileri giriniz..");
            }
            else
            {
                object turidDurumu = (int)cmbBox_Guncelle_Tur.SelectedValue < 0 ? null : cmbBox_Guncelle_Tur.SelectedValue;
                //güncelleyeceğiz.
                string updateSQLQuery = $"update Kitaplar set KitapAdi='{txt_GuncelleKitapAdi.Text.Trim()}' ,TurId={turidDurumu},YazarID={cmbBox_Guncelle_Yazar.SelectedValue},Stok={numericUpDown_Guncelle_Stok.Value},SayfaSayisi={numericUpDown_Guncelle_SayfaSayisi.Value} where KitapID={comboBoxKitapGuncelle.SelectedValue}";
                SqlCommand komut = new SqlCommand(updateSQLQuery, baglanti);
                BaglantiyiAc();
                int sonuc = komut.ExecuteNonQuery();
                if (sonuc > 0)
                {
                    MessageBox.Show($"{comboBoxKitapGuncelle.SelectedItem}'a ait bilgiler  güncellendi!");
                    GuncelleSayfasindakileriTemizle();
                    comboBoxKitapGuncelle.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show($"HATA: {comboBoxKitapGuncelle.SelectedItem}'a ait bilgiler güncellenmedi !");
                }


                //viewmodel oluşturarak türü boş seçebileceğiz.
            }

            
        }
        private void TumKitaplariViewileGrideGetir()
        {
            try
            {
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                komut.CommandText = "select * from View_KitapYazarTur";
                BaglantiyiAc();
                SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                DataTable sanalTablo = new DataTable();
                adaptor.Fill(sanalTablo);
                dataGridViewKitaplar.DataSource = sanalTablo;
                //dataGridViewKitaplar.RowHeadersWidth = 150;
                BaglantiyiKapat();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Kitaplar listenirken beklenmedik bir hata oluştu!"+ex.Message);
            }
        }
        private void BaglantiyiKapat()
        {
            try
            {
                if (baglanti.State != ConnectionState.Closed)
                {
                    baglanti.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Bağlantıyı kapatırken bir hata oldu! " + ex.Message);
            }
        }
        private void BaglantiyiAc()
        {
            try
            {
                // bağlantı açık değilse açalım
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.ConnectionString = SQLBaglantiCumlesi;
                    baglanti.Open();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Bağlantı açılırken bir hata oluştu!" + ex.Message);
            }
        }

    

        
        private void comboBoxKitapGuncelle_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combodan neyi seçtiğimizi anlamamız gerekli
            if (comboBoxKitapGuncelle.DataSource != null && comboBoxKitapGuncelle.SelectedIndex >= 0)
            {
                GuncelleSayfasindakileriTemizle();
                int secilenYazarID = (int)comboBoxKitapGuncelle.SelectedValue;
                CombodanSecilenKitabinTumBilgileriniDoldur(secilenYazarID);
            }
            else
            {
                GuncelleSayfasindakileriTemizle();
            }
            


        }

        private void GuncelleSayfasindakileriTemizle()
        {
            txt_GuncelleKitapAdi.Clear();
            numericUpDown_Guncelle_SayfaSayisi.Value = 0;
            numericUpDown_Guncelle_Stok.Value = 0;
            cmbBox_Guncelle_Yazar.SelectedIndex = -1;
            cmbBox_Guncelle_Tur.SelectedIndex = -1;
        }

        
    }
}
