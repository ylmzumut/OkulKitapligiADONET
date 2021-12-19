using OkulKitapligiADONET_BLL.ViewModels;
using OkulKitapligiADONET_DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulKitapligiADONET_BLL
{
    public class KitapOduncIslemManager
    {
        MyPocketDAL myPocketDAL = new MyPocketDAL(".", "OkulKutuphanesi", "", "");

        public DataTable TumKitaplariGetir()
        {
            try
            {
                DataTable data = new DataTable();
                data = myPocketDAL.GetTheData("Kitaplar", "*", "SilindiMi=0");
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable TumOgrencileriGetir()
        {
            try
            {
                DataTable data = new DataTable();
                data = myPocketDAL.GetTheData("Ogrenciler", "OgrID,CONCAT(OgrAd,' ',OgrSoyad) as OgrenciAdSoyad, Cinsiyet, Sinif, DogumTarihi, SilindiMi", "SilindiMi=0");
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GrideVerileriGetir()
        {
            try
            {
                DataTable data = new DataTable();
                data = myPocketDAL.GetTheData("select i.IslemID, i.KitapID, CONCAT(o.OgrAd,' ',o.OgrSoyad) as OgrenciAdSoyad, k.KitapAdi, i.OduncAldigiTarih, i.OduncBitisTarihi from Islemler i inner join Kitaplar k on k.KitapID = i.KitapID inner join Ogrenciler o on o.OgrID = i.OgrID");
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<IslemViewModel> GrideVerileriViewModelleGetir()
        {
            List<IslemViewModel> data = new List<IslemViewModel>();
            try
            {
                DataTable theData = new DataTable();
                theData = myPocketDAL.GetTheData("select i.IslemID, k.KitapID, o.OgrID, CONCAT(o.OgrAd, ' ', o.OgrSoyad) as OgrenciAdSoyad, k.KitapAdi, i.OduncAldigiTarih, i.OduncBitisTarihi, i.TeslimEdildiMi from  Islemler i inner join Kitaplar k on k.KitapID = i.KitapID inner join Ogrenciler o on o.OgrID = i.OgrID");

                //veriler dataTable ile geldi. Ama ben o verileri tek tek döngü ile dönerken içindeki verileri viewmodelime aktaracağım.
                for (int i = 0; i < theData.Rows.Count; i++)
                {
                    DataRow satir = theData.Rows[i];
                    IslemViewModel veri = new IslemViewModel()
                    {
                        IslemID = (int)theData.Rows[i].ItemArray[0],
                        KitapID = (int)theData.Rows[i].ItemArray[1],
                        OgrID = (int)theData.Rows[i].ItemArray[2],
                        OgrenciAdSoyad = theData.Rows[i].ItemArray[3].ToString(),
                        KitapAdi = theData.Rows[i].ItemArray[4].ToString(),
                        OduncAldigiTarih=Convert.ToDateTime(theData.Rows[i].ItemArray[5]),
                        OduncBitisTarihi = Convert.ToDateTime(theData.Rows[i].ItemArray[6]),
                        TeslimEdildiMi = (bool)theData.Rows[i].ItemArray[7]
                    };
                    //IslemViewModel tipindeki veri isimli nesne IslemViewModel tipine sahip listeye eklenecek
                    data.Add(veri); // ekledik
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }
        public int KitabınStogunuGetir(int kitapID)
        {
            try
            {
                int stokAdeti = 0;
                object data = myPocketDAL.GetTheDataByExecuteScalar($"select Stok from Kitaplar where KitapID={kitapID}");
                if (data != null)
                {
                    stokAdeti = Convert.ToInt32(data);
                }
                return stokAdeti;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool OduncKitapKaydiniYap(string tableName, Hashtable htVeri)
        {
            bool sonuc = false;
            try
            {
                //stok adet
                object stokAdeti = myPocketDAL.GetTheDataByExecuteScalar("select Stok from Kitaplar where KitapID=" + htVeri["KitapID"].ToString());
                if (stokAdeti != null)
                {
                    //stoğu azaltacağız

                    stokAdeti = (int)stokAdeti - 1;
                    string updateCumlesi = "update Kitaplar set Stok=" + stokAdeti + " where KitapID=" + htVeri["KitapID"];

                    //ödünç
                    string insertCumlesi = myPocketDAL.CreateInsertQueryAsString(tableName, htVeri);

                    sonuc = myPocketDAL.ExecuteTheQueriesWithTransaction(insertCumlesi, updateCumlesi);
                }
                else
                {
                    throw new Exception("HATA: Stok adet bilgisi alınamadığı için hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



            return sonuc;
        }
        public bool OduncKitapTeslimEt(string tableName, int islemID, int kitapID)
        {
            bool sonuc = false;

            try
            {
                //stok
                object stokAdet = myPocketDAL.GetTheDataByExecuteScalar("select Stok from Kitaplar where KitapID=" + kitapID);
                if (stokAdet != null)
                {
                    stokAdet = (int)stokAdet + 1;
                    string[] komutcumleleri = new string[2];
                    komutcumleleri[0] = "update Kitaplar Set Stok=" + stokAdet + " where KitapID=" + kitapID;
                    //CreateUpdateAsString isimli metodunu kullanmak amacıyla hastable oluşturacağız.
                    //Eğer o metodu kullanmak istemezseniz yani Hashtable oluşturmaya erindiniz diyelim o zaman 2.komut cümlesi aşağıdaki gibi oluşturabilirsiniz.
                    //komutcumleleri[1] = "update Islemler Set TeslimEdildiMi=1, OduncBitisTarih='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where IslemID=" + islemID;

                    Hashtable htVeri = new Hashtable();
                    string bitisTarih = "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    htVeri.Add("TeslimEdildiMi", "1");
                    htVeri.Add("OduncBitisTarih", bitisTarih);
                    string kosulum = "IslemID=" + islemID;
                    komutcumleleri[1] = myPocketDAL.CreateUpdateQueryAsString("Islemler", htVeri, kosulum);
                    sonuc = myPocketDAL.ExecuteTheQueriesWithTransaction(komutcumleleri);
                }
                else
                {
                    throw new Exception("HATA: Stok adeti çekilemediği için hata oluştu");
                }
                return sonuc;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
