using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
namespace newStore
{
    public class Produkt
    {
        public int ProduktId { get; set; }
        public string ProduktNamn { get; set; }
        public int Pris { get; set; }        
        public PriceType PrisTyp { get; set; }

        public Produkt()
        {
        }

        public Produkt(int id)
        {
            ProduktId = id;
        }
    }

    public enum PriceType
    {
        kg,
        st
    }
}
