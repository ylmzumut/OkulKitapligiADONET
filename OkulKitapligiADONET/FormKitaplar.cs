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
        string SQLBaglantiCumlesi = @"Server=.;Database=OkulKutuphanesi;Trusted_Connection=True;";
        SqlConnection baglanti = new SqlConnection();

        private void FormKitaplar_Load(object sender, EventArgs e)
        {
            dataGridViewKitaplar.MultiSelect = false;
            dataGridViewKitaplar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            TumKitaplariViewileGrideGetir();

            UC_MyButton_Guncelle.myButton.Text = "GÜNCELLE";

            UC_MyButton_Guncelle.myButton.Click += new EventHandler(btn_KitapGuncelle);

            GuncelleSayfasidakiGroupBoxIciniTemizle();
            //Düzenle'deki combo dolsun
            TumKitaplariComboBoxaGetir();
            //Yazarları combo'ya getir
            TumYazarlariComboBoxaGetir();
            //Türleri comba'ya getir
            TumTurleriComboBoxaGetir();

            tabControl1.Click += new EventHandler(tabControl1_Click);

            //Ekle

            UC_MyButton_Ekle.myButton.Text = "KİTAP EKLE";
            UC_MyButton_Ekle.myButton.Click += new EventHandler(btn_KitapEkle);

            //Sil
            UC_MyButton_Sil.myButton.Text = "KİTAP SİL";
            UC_MyButton_Sil.myButton.Click += new EventHandler(btn_KitapSil);
        }

        //SİL
        private void btn_KitapSil(object sender, EventArgs e)
        {
            try
            {
                //silme işlemi
                DialogResult cevap = MessageBox.Show("Seçtiğiniz kitabı silmek yerine listeden kaldırmak (pasifleştirmek) ister misiniz?", "UYARI", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    //pasifleştirmek
                    SqlCommand komut = new SqlCommand($"update Kitaplar set SilindiMi=1 where KitapID={cmbBox_Sil_Kitap.SelectedValue.ToString()}", baglanti);
                    BaglantiyiAc();
                    int sonuc = komut.ExecuteNonQuery();
                    if (sonuc > 0)
                    {
                        MessageBox.Show("Kitap pasifleştirme işlemiyle listeden kaldırıldı...");
                    }
                    else
                    {
                        MessageBox.Show("HATA: Beklenmedik hata oluştu...");
                    }
                    BaglantiyiKapat();
                }
                else if (cevap == DialogResult.No)
                {
                    //gerçekten silmek
                    //Ama silmeden önce ödünç kitap işlemlerinin olduğu tabloda verisi
                    SqlCommand komut = new SqlCommand($"select * from Kitaplar k inner join Islemler i on i.KitapId = k.KitapId where k.KitapId={cmbBox_Sil_Kitap.SelectedValue.ToString()}", baglanti);
                    BaglantiyiAc();
                    SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                    //içinde birden çok database'i barındırabilen nesnedir.
                    DataSet dataSet = new DataSet();
                    adaptor.Fill(dataSet);
                    if ((int)dataSet.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("Bu kitaba ait ödünç almma işlemleri yapılmış. Silmek için ödünç işlemler sayfasına gidip oradan bu kitabın bilgilerini silmelisiniz!!!");
                    }

                    BaglantiyiKapat();
                }

                else
                {
                    // silecek
                    SqlCommand komut = new SqlCommand($"delete from Kitaplar where KitapId=@kitapid", baglanti);
                    komut.Parameters.Clear();

                    komut.Parameters.AddWithValue("@kitapid", cmbBox_Sil_Kitap.SelectedValue.ToString());

                    BaglantiyiAc();
                    int sonuc = komut.ExecuteNonQuery();
                    if (sonuc > 0)
                    {
                        MessageBox.Show("Silindi");
                        SilmeSayfasiTumKontrolleriTemizle();
                    }
                    else
                    {
                        MessageBox.Show("HATA:Silinemedi!");
                    }
                    BaglantiyiKapat();
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: Silme işleminde hata oluştu! " + ex.Message);
            }
        }
        private void SilmeSayfasiTumKontrolleriTemizle()
        {
            cmbBox_Sil_Kitap.SelectedIndex = -1;
            cmbBox_Sil_Kitap.Text = "Kitap seçiniz...";
            richTextBoxKitap.Clear();

        }

        //EKLE
        private void btn_KitapEkle(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtKitapEkle.Text.Trim()))
                {
                    MessageBox.Show("Kitap adı girmeniz gerekiyor!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EklemeSayfasiTumKontrolleriTemizle();
                }
                else if (numericUpDown_Ekle_SayfaSayisi.Value <= 0)
                {
                    MessageBox.Show("Kitabın sayfa sayısı sıfırdan büyük olmalıdır!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EklemeSayfasiTumKontrolleriTemizle();
                }
                else if (numericUpDown_Ekle_Stok.Value <= 0)
                {
                    MessageBox.Show("Kitabın stok adedi sıfırdan büyük olmalıdır!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EklemeSayfasiTumKontrolleriTemizle();
                }
                else if (cmbBox_Ekle_Tur.SelectedIndex < 0)
                {
                    MessageBox.Show("Kitabın türünü seçmelisiniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EklemeSayfasiTumKontrolleriTemizle();
                }
                else if (cmbBox_Ekle_Yazar.SelectedIndex < 0)
                {
                    MessageBox.Show("Kitabın yazarını seçmelisiniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EklemeSayfasiTumKontrolleriTemizle();
                }
                else
                {
                    using (baglanti)
                    {
                        //ekleme işlemini prosedürle yapacağız
                        SqlCommand komut = new SqlCommand($"SP_KitapEkle", baglanti);
                        komut.CommandType = CommandType.StoredProcedure;
                        //parametreleri ekle

                        ////1.yol
                        //SqlParameter kitapAdParameter = new SqlParameter();
                        //kitapAdParameter.Value = "@kitapAdi";

                        //2.yol
                        komut.Parameters.AddWithValue("@kayitTarihi", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        komut.Parameters.AddWithValue("@kitapAdi", txtKitapEkle.Text.Trim());
                        komut.Parameters.AddWithValue("@sayfaSayisi", numericUpDown_Ekle_SayfaSayisi.Value);
                        komut.Parameters.AddWithValue("@turID", (int)cmbBox_Ekle_Tur.SelectedValue);
                        komut.Parameters.AddWithValue("@yazarID", (int)cmbBox_Ekle_Yazar.SelectedValue);
                        komut.Parameters.AddWithValue("@stok", numericUpDown_Ekle_Stok.Value);
                        BaglantiyiAc();
                        int sonEklenenKitapID = Convert.ToInt32(komut.ExecuteScalar());

                        //ExecuteScalar , genellikle sorgunuz tek bir değer verdiğinde kullanılır. Daha fazla döndürürse, sonuç ilk satırın ilk sütunu olur. Bir örnek SELECT @@IDENTITY AS 'Identity' olabilir. Birden çok satır / sütuna sahip herhangi bir sonuç döndürmeye ihtiyacınız olursa ExecuteReader kullanmalısınız. (örneğin, SELECT col1, col2 from sometable )

                        if (sonEklenenKitapID > 0)
                        {
                            MessageBox.Show($"Yeni bir kitap eklendi. \n kitapID: {sonEklenenKitapID}");
                            EklemeSayfasiTumKontrolleriTemizle();
                        }
                        else
                        {
                            MessageBox.Show("HATA: Kitap eklenemedi!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: Ekleme işleminde hata oluştu! " + ex.Message);
            }
        }
        private void EklemeSayfasiTumKontrolleriTemizle()
        {
            txtKitapEkle.Clear();
            cmbBox_Ekle_Tur.SelectedIndex = -1;
            cmbBox_Ekle_Tur.Text = "Tür seçiniz...";
            cmbBox_Ekle_Yazar.SelectedIndex = -1;
            cmbBox_Ekle_Yazar.Text = "Yazar seçiniz...";
            numericUpDown_Ekle_SayfaSayisi.Value = 0;
            numericUpDown_Ekle_Stok.Value = 0;
        }
        //ORTAK
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

                    cmbBox_Ekle_Yazar.DisplayMember = "YazarAdSoyad";
                    cmbBox_Ekle_Yazar.ValueMember = "YazarID";
                    cmbBox_Ekle_Yazar.DataSource = sanalTablo;
                    cmbBox_Ekle_Yazar.SelectedIndex = -1;
                    cmbBox_Ekle_Yazar.Text = "Yazar seçiniz...";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata oluştu! " + ex.Message);
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

                    cmbBox_Ekle_Tur.DisplayMember = "TurAdi";
                    cmbBox_Ekle_Tur.ValueMember = "TurID";
                    cmbBox_Ekle_Tur.DataSource = sanalTablo;
                    cmbBox_Ekle_Tur.SelectedIndex = -1;
                    cmbBox_Ekle_Tur.Text = "Tür seçiniz...";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata oluştu! " + ex.Message);
            }
        }
        private void tabControl1_Click(object sender, EventArgs e)
        {
            TumKitaplariViewileGrideGetir();
            TumKitaplariComboBoxaGetir();
            GuncelleSayfasiPasiflestirmeTemizlikIslemiYap();
            EklemeSayfasiTumKontrolleriTemizle();
            SilmeSayfasiTumKontrolleriTemizle();
        }
        //GÜNCELLE
        private void GuncelleSayfasiPasiflestirmeTemizlikIslemiYap()
        {
            comboBoxKitapGuncelle.SelectedIndex = -1;
            GuncelleSayfasidakiGroupBoxIciniTemizle();
            groupBoxKitapGuncelle.Enabled = false;
        }
        private void TumKitaplariComboBoxaGetir() //güncelle-sil
        {
            try
            {
                SqlCommand komut = new SqlCommand("select * from Kitaplar where SilindiMi=0 order by KitapAdi", baglanti);
                BaglantiyiAc();
                SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                DataTable sanalTablo = new DataTable();
                adaptor.Fill(sanalTablo);
                comboBoxKitapGuncelle.DisplayMember = "KitapAdi";
                comboBoxKitapGuncelle.ValueMember = "KitapID";
                comboBoxKitapGuncelle.DataSource = sanalTablo;
                comboBoxKitapGuncelle.SelectedIndex = -1;

                cmbBox_Sil_Kitap.DisplayMember = "KitapAdi";
                cmbBox_Sil_Kitap.ValueMember = "KitapID";
                cmbBox_Sil_Kitap.DataSource = sanalTablo;
                cmbBox_Sil_Kitap.SelectedIndex = -1;
                cmbBox_Sil_Kitap.Text = "Kitap seçiniz...";
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata oluştu!" + ex.Message);
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
                        cmbBox_Guncelle_Yazar.SelectedValue = (int)okuyucu["YazarID"];
                    }
                    BaglantiyiKapat();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata oluştu" + ex.Message);
            }
        }
        private void btn_KitapGuncelle(object sender, EventArgs e)
        {
            //eğer yazar seçilmemişse uyarsın
            //eğer kitap adı yazılmamışsa uyarsın
            //stok ve sayfa sayısı > 0 olmalı

            if (string.IsNullOrEmpty(txt_GuncelleKitapAdi.Text.Trim()))
            {
                MessageBox.Show("Lütfen kitap adı giriniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GuncelleSayfasiPasiflestirmeTemizlikIslemiYap();
            }
            else if (numericUpDown_Guncelle_SayfaSayisi.Value <= 0)
            {
                MessageBox.Show("Sayfa sayısı sıfırdan büyük olmalıdır!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GuncelleSayfasiPasiflestirmeTemizlikIslemiYap();
            }
            else if (numericUpDown_Guncelle_Stok.Value <= 0)
            {
                MessageBox.Show("Stok adedi sıfırdan büyük olmalıdır!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GuncelleSayfasiPasiflestirmeTemizlikIslemiYap();
            }
            else if (cmbBox_Guncelle_Yazar.SelectedIndex < 0)
            {
                MessageBox.Show("Kitabın yazarını seçmelisiniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GuncelleSayfasiPasiflestirmeTemizlikIslemiYap();
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
                    DataRowView data = comboBoxKitapGuncelle.SelectedItem as DataRowView;
                    string eskiKitapAdi = data.Row.ItemArray[2].ToString();
                    MessageBox.Show($"{eskiKitapAdi} isimli ait bilgiler güncellendi!");
                    GuncelleSayfasidakiGroupBoxIciniTemizle();
                    comboBoxKitapGuncelle.SelectedIndex = -1;
                    TumKitaplariComboBoxaGetir();
                    TumKitaplariViewileGrideGetir();
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
                komut.CommandText = "select * from View_KitapYazarTur where SilindiMi=0";
                BaglantiyiAc();
                SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                DataTable sanalTablo = new DataTable();
                adaptor.Fill(sanalTablo);
                dataGridViewKitaplar.DataSource = sanalTablo;
                dataGridViewKitaplar.Columns["SilindiMi"].Visible = false;
                BaglantiyiKapat();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Kitaplar listenirken beklenmedik bir hata oluştu!" + ex.Message);
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
        private void GuncelleSayfasiAktiflestirmeTemizlikIslemiYap()
        {
            groupBoxKitapGuncelle.Enabled = true;
            GuncelleSayfasidakiGroupBoxIciniTemizle();
        }
        private void comboBoxKitapGuncelle_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combodan neyi seçtiğimizi anlamamız gerekli
            if (comboBoxKitapGuncelle.SelectedIndex >= 0)
            {
                GuncelleSayfasiAktiflestirmeTemizlikIslemiYap();
                int secilenYazarID = (int)comboBoxKitapGuncelle.SelectedValue;
                CombodanSecilenKitabinTumBilgileriniDoldur(secilenYazarID);
            }
            else
            {
                GuncelleSayfasiPasiflestirmeTemizlikIslemiYap();
            }
        }
        private void GuncelleSayfasidakiGroupBoxIciniTemizle()
        {
            txt_GuncelleKitapAdi.Clear();
            numericUpDown_Guncelle_SayfaSayisi.Value = 0;
            numericUpDown_Guncelle_Stok.Value = 0;
            cmbBox_Guncelle_Yazar.SelectedIndex = -1;
            cmbBox_Guncelle_Tur.SelectedIndex = -1;
        }

        private void cmbBox_Sil_Kitap_SelectedIndexChanged(object sender, EventArgs e)
        {
            //seçili olan kitaba ait olan bilgileri richtext'a yazacağız
            //dataset ile yapalım

            try
            {
                if (cmbBox_Sil_Kitap.SelectedIndex>=0)
                {
                    using (baglanti)
                    {
                        SqlCommand komut = new SqlCommand($"select * from Kitaplar k left join Turler t on k.TurID=t.TurID left join Yazarlar y on k.YazarID=y.YazarID where KitapID={cmbBox_Sil_Kitap.SelectedValue}", baglanti);
                        SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                        BaglantiyiAc();
                        DataSet dataSet = new DataSet();
                        adaptor.Fill(dataSet, "Kitaplar");

                        string kitapBilgileri = dataSet.Tables["Kitaplar"].Rows[0]["KitapAdi"].ToString()
                            + "\n" + 
                            dataSet.Tables["Kitaplar"].Rows[0]["TurAdi"].ToString()
                            + "\n" +
                            dataSet.Tables["Kitaplar"].Rows[0]["YazarAdSoyad"].ToString();

                        komut = new SqlCommand($"select * from View_KitabinIslemAdedi where KitapID={cmbBox_Sil_Kitap.SelectedValue}", baglanti);

                        object islemAdetBilgisi = komut.ExecuteScalar();
                        islemAdetBilgisi = islemAdetBilgisi == null ? 0 : islemAdetBilgisi;

                        kitapBilgileri += $" \nBu kitabın {islemAdetBilgisi.ToString()} adet işlemi vardır";
                        richTextBoxKitap.Text = kitapBilgileri;

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("HATA: Beklenmedik hata oluştu. "+ex.Message);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
