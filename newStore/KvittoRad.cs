using System;
namespace newStore
{
    public class KvittoRad
    {
        public string ProduktNamn { get; set; }
        public int ProduktId { get; set; }
        public int Antal { get; set; }
        public int Pris { get; set; }
        public int Summa { get; set; }
        //public int Total { get; set; }        

        public KvittoRad(string productname, int quantity, int price, int prodcutId)
        {
            ProduktNamn = productname;
            ProduktId = prodcutId;
            Antal = quantity;
            Pris = price;
            Summa = price * quantity;            
        }

        public void printRow()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{ProduktNamn} {Antal} * {Pris}kr = {Summa}kr ");
            Console.ResetColor();
        }

        // ANVÄND DENNA MED RABATT FUNKTIONEN
        //public void updateSum()
        //{
        //    Summa = Pris * Antal;
        //}
    }
}
