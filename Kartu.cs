using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace starkartu
{
    public class Kartu
    {
        public int isi;
        public string tipe;
        public string warna;
        public Kartu(int isi,string tipe)
        {
            this.isi = isi;
            this.tipe = tipe;
            if (tipe.Equals("Cengkih") || tipe.Equals("Waru"))
            {
                warna = "Hitam";
            }
            else
            {
                warna = "Merah";
            }
        }
    }
}
