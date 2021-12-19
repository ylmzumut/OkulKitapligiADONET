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
    public partial class FormYazarlar : Form
    {
        public FormYazarlar()
        {
            InitializeComponent();
        }

        //Global Alan

        //SQLCONNECTION Nesnesi: SQL veritabanına bağlantı kurmak için kullanacağımız classtır.
        //System.Data.Client namespace'i içerisinde yer alır.


        SqlConnection baglanti = new SqlConnection();
        string SQLBaglantiCumlesi = @"Server=.;Database=OkulKutuphanesi;Trusted_Connection=True;";

        private void FormYazarlar_Load(object sender, EventArgs e)
        {
            dataGridViewYazarlar.MultiSelect = false; //Çoklu satır seçimi engellendi
            dataGridViewYazarlar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //fare ile datagrid üzerinde bir hücreye tıkladığında bulunduğu satırı tamamen seçecek

            //dataGridView'e sağ tıklanınca gelecek olan contextmenustrip ataması yapıldı
            dataGridViewYazarlar.ContextMenuStrip = contextMenuStrip1;

            //Grid'in içine bilgileri getireceğiz
            TumYazarlariGetir();


        }

        private void TumYazarlariGetir()
        {

            try
            {

                //SQLCOMMAND nesnesi: Sorgularımızı ve proserdürlerimize ait komutları alan nesnedir.
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                string sorgu = "Select * from Yazarlar where SilindiMi=0 order by YazarID desc";
                komut.CommandText = sorgu;
                BaglantiyiAc();

                //DataSQLADAPTER nesnesi, sorgu çalışınca oluşan dataların aktarılması işlemini yapar. Adaptore hangi komut işleneceğini ctor'da verebiliriz ya da daha sonra verebiliriz.
                //1.Yöntem
                SqlDataAdapter adaptor = new SqlDataAdapter(komut); // ctor'da komutu verdik
                //2.Yöntem
                //SqlDataAdapter adaptor = new SqlDataAdapter();
                //adaptor.SelectCommand = komut;  //Komutu daha sonradan da verilebilir.

                //Adaptorun içindeki verileri sanalTabloya alıyoruz.
                DataTable sanalTablo = new DataTable();
                adaptor.Fill(sanalTablo);

                dataGridViewYazarlar.DataSource = sanalTablo;
                dataGridViewYazarlar.Columns["SilindiMi"].Visible = false;
                dataGridViewYazarlar.Columns["YazarAdSoyad"].Width = 220;
                dataGridViewYazarlar.Columns["KayitTarihi"].Width = 150;

                BaglantiyiKapat();

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Beklenmedik bir hata oluştu! HATA: {ex.Message}", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            switch (btnEkle.Text)
            {
                case "EKLE":
                    try
                    {
                        if (string.IsNullOrEmpty(txtYazar.Text))
                        {
                            MessageBox.Show("Lütfen yazar bilgisini giriniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            //ekleme yapacağız.
                            string insertCumlesi = $"insert into Yazarlar (KayitTarihi, YazarAdSoyad, SilindiMi) values ('{TarihiDuzenle(DateTime.Now)}','{txtYazar.Text.Trim()}',0) ";

                            SqlCommand insertkomut = new SqlCommand(insertCumlesi, baglanti);
                            //baglantı açılacak metot çağıralım
                            BaglantiyiAc();
                            int sonucum = insertkomut.ExecuteNonQuery();
                            if (sonucum > 0) //effected rows var
                            {
                                MessageBox.Show("Yeni yazar sisteme eklendi");
                                TumYazarlariGetir();
                            }
                            else
                            {
                                MessageBox.Show("Bir hata oluştu. Yeni yazar eklenemedi!");
                            }


                            //baglantı kapanacak metot çağıralım
                            BaglantiyiKapat();


                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Ekleme işleminde beklemek hata oldu!" + ex.Message);
                    }
                    Temizle();
                    break;
                case "GÜNCELLE":
                    try
                    {
                        if (!string.IsNullOrEmpty(txtYazar.Text))
                        {
                            using (baglanti)
                            {
                                DataGridViewRow satir = dataGridViewYazarlar.SelectedRows[0];
                                int YazarID = Convert.ToInt32(satir.Cells["YazarID"].Value);
                                //1.yol
                                string updateSorguCumlesi = $"Update Yazarlar Set YazarAdSoyad='{txtYazar.Text.Trim()}' where YazarID={YazarID}";

                                SqlCommand updateCommand = new SqlCommand(updateSorguCumlesi, baglanti);
                                BaglantiyiAc();

                                int sonuc = updateCommand.ExecuteNonQuery();
                                if (sonuc > 0)
                                {
                                    MessageBox.Show($"Yazar güncellendi");
                                    TumYazarlariGetir();
                                }
                                else
                                {
                                    MessageBox.Show("Yazar güncellenmedi!");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Yazar adı olmadan güncelleme yapamazsın!");
                        }


                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Güncelleme işleminde beklenmedik bir hata oluştu!");
                    }
                    Temizle();
                    break;
                default:
                    break;
            }
        }

        private void Temizle()
        {
            btnEkle.Text = "EKLE";
            txtYazar.Clear();
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

        private string TarihiDuzenle(DateTime tarih)
        {
            string tarihString = string.Empty;
            if (tarih != null)
            {
                tarihString = tarih.Year + "-" + tarih.Month + "-" + tarih.Day + " " + tarih.Hour + ":" + tarih.Minute + ":" + tarih.Second;
            }
            return tarihString;
        }

        private void guncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Seçili olan satırdaki istediğiniz hücreden(kolondan) değer aldık.
            if (dataGridViewYazarlar.SelectedRows.Count > 0)
            {
                DataGridViewRow satir = dataGridViewYazarlar.SelectedRows[0];
                string yazarAdSoyad = Convert.ToString(satir.Cells["YazarAdSoyad"].Value);
                btnEkle.Text = "GÜNCELLE";
                txtYazar.Text = yazarAdSoyad;

                //kısa olsun isterseniz
                //txtYazar.Text = COnvert.ToString(satir.Cells["YazarAdSoyad"].Value);
            }
            else
            {
                MessageBox.Show("");
            }

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow secilensatir = dataGridViewYazarlar.SelectedRows[0];
            int yazarID = (int)secilensatir.Cells["YazarID"].Value;
            string yazar = Convert.ToString(secilensatir.Cells["YazarAdSoyad"].Value);

            //Yazarın kitapları varsa Kitaplar tablosunda YazarID foregin key vardır.
            //Bu durumda silme işlemi yapılmamalıdır.
            //Varsa silmesine izin vermeyeceğiz.
            //Yoksa silmek ister misin diye son bir kez sorup evet derse sileceğiz.

            SqlCommand komut = new SqlCommand($"select * from Kitaplar where YazarID={yazarID}", baglanti);

            komut.Connection = baglanti;
            SqlDataAdapter adaptor = new SqlDataAdapter(komut); //adaptöre işleyeceği komutu adaptorun constructor'ında verdik.

            DataTable sanalTablo = new DataTable();
            BaglantiyiAc();
            adaptor.Fill(sanalTablo);
            if (sanalTablo.Rows.Count > 0)
            {
                MessageBox.Show($"{yazar} adlı yazarın Kitaplar tablosunda {sanalTablo.Rows.Count.ToString()} adet kitabı bulunmaktadır. Bu yazarı silmek için öncelikle sistemdeki ona tanımlı kitapları silmeni gerekmetedir. Litfen Kitap İşlemleri sayfasına gidiniz!");
            }
            else
            {
                //kitabı yok. Foreign key patlaması olmaz. Silebiliriz.



                DialogResult cevap = MessageBox.Show($"{yazar} adlı yazarı silmek istediğinize emin misiniz?", "ONAY", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (cevap==DialogResult.Yes)
                {
                    //silecek
                    //komut.CommandText = $"Delete from Yazarlar where YazarID={yazarID}";

                    //@yzrid diyerek bir parametre oluşturmuş olduk.
                    komut.CommandText = $"Delete from Yazarlar where YazarID=@yzrid";
                    komut.Parameters.Clear();
                    //AddWithValue metodu @yzrid yerine yazarID değerine sqlcommand nesnesinin commendText'inde bulunan sorgu cümlesine entegre eder.
                    komut.Parameters.AddWithValue("@yzrid",yazarID);

                    BaglantiyiAc();
                    int sonuc = komut.ExecuteNonQuery();
                    if (sonuc>0)
                    {
                        MessageBox.Show("Silindi");
                        TumYazarlariGetir();
                    }
                    else
                    {
                        MessageBox.Show("HATA:Silinemedi!");
                    }
                }
            }


        }

        private void silPasifeCekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Kullanıcı silindi zannedecek ama biz aslında psif yapıcağız
            try
            {
                using (baglanti)
                {
                    DataGridViewRow satir = dataGridViewYazarlar.SelectedRows[0];
                    int YazarID = Convert.ToInt32(satir.Cells["YazarID"].Value);
                    //1.yol
                    //string updateSorguCumlesi = $"Update Yazarlar Set SilindiMi=1 where YazarID={YazarID}";
                    //2.yol
                    string updateSorguCumlesi = $"Update Yazarlar Set SilindiMi=1 where YazarID=@yzrid";

                    SqlCommand updateCommand = new SqlCommand(updateSorguCumlesi, baglanti);
                    updateCommand.Parameters.Clear();
                    updateCommand.Parameters.AddWithValue("@yzrid",YazarID);

                    BaglantiyiAc();

                    int sonuc = updateCommand.ExecuteNonQuery();
                    if (sonuc > 0)
                    {
                        MessageBox.Show($"Yazar silindi");
                        TumYazarlariGetir();
                    }
                    else
                    {
                        MessageBox.Show("DİKKAT: Yazar SİLİNMAEDİ!");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Pasife çek silme işeleminde hata: "+ex.Message);
            }
        }

        private void silBaskaBirYontemToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Bu yöntem yukarıdakiler gibi kullanışlı değildir.
            try
            {

                DataGridViewRow secilenSatir = dataGridViewYazarlar.SelectedRows[0];
                int yazarId = (int)secilenSatir.Cells["YazarId"].Value;
                string yazar = Convert.ToString(secilenSatir.Cells["YazarAdSoyad"].Value);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                DialogResult cevap = MessageBox.Show($"{yazar} adlı yazarı silmek istediğinize emin misiniz?", "ONAY", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    komut.CommandText = $"Delete from Yazarlar where YazarId=@yzrid";
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@yzrid", yazarId);
                    BaglantiyiAc();
                    int sonuc = komut.ExecuteNonQuery();
                    if (sonuc > 0)
                    {
                        MessageBox.Show("Silindi");
                        TumYazarlariGetir();
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

                MessageBox.Show("HATA: " + ex.Message);
            }
        }
    }
}
