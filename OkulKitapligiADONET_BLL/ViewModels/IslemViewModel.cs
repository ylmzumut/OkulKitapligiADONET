using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulKitapligiADONET_BLL.ViewModels
{
    public class IslemViewModel
    {
        public string OgrenciAdSoyad { get; set; }
        public string KitapAdi { get; set; }
        public int IslemID { get; set; }
        public int KitapID { get; set; }
        public int OgrID { get; set; }
        public DateTime OduncAldigiTarih { get; set; }
        public DateTime OduncBitisTarih { get; set; }
        public bool TeslimEdildiMi { get; set; }

        private string _teslimEdildiMiString = "";
        public string TeslimEdildiMiString
        {
            get
            {
                if (this.TeslimEdildiMi)
                {
                    _teslimEdildiMiString = "EVET";
                }
                else
                {
                    _teslimEdildiMiString = "HAYIR";
                }
                return _teslimEdildiMiString;
            }
        }

    }
}
