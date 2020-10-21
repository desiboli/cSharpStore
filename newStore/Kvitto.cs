using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace newStore
{
    public class Kvitto
    {
        private double Total { get; set; }
        private DateTime KvittoDatum { get; set; }        
        private double TotalProduktAntal { get; set; }
        public List<KvittoRad> KvittoRader { get; set; }
        //private double Rabatt { get; set; } // ANVÄND DENNA MED RABATT FUNKTIONEN

        public Kvitto()
        {
            Total = 0;
            KvittoDatum = DateTime.Now;
            TotalProduktAntal = 0;
            KvittoRader = new List<KvittoRad>();
            //Rabatt = 0; // ANVÄND DENNA MED RABATT FUNKTIONEN
        }

        public void addRecepitRow(KvittoRad recepitRow)
        {
            int index = receiptRowExists(recepitRow.ProduktId);
            if (index == -1)
            {
                KvittoRader.Add(recepitRow);
            }
            else
            {
                KvittoRader[index].Antal += recepitRow.Antal;
                //KvittoRader[index].updateSum(); // ANVÄND DENNA MED RABATT FUNKTIONEN
            }

            TotalProduktAntal = KvittoRader.Sum(item => item.Summa);
        }

        // ANVÄND DENNA MED RABATT FUNKTIONEN
        //public void decreaseQuantity(int productID)
        //{
        //    int index = receiptRowExists(productID);
        //    if (index != -1)
        //    {
        //        KvittoRader[index].Antal -= 1;
        //        KvittoRader[index].updateSum();
        //        TotalProduktAntal = KvittoRader.Sum(item => item.Summa);
        //    }
        //}

        public void printReceipt()
        {
            printDate();
            printRecepitRows();
            //printDiscount();
            printTotal();
        }

        public void saveReceipt()
        {
            string todaysDate = DateTime.Today.ToString("yyyyMMdd");
            var outFile = $"../../../receipts/RECEIPT_{todaysDate}.txt";
            using (StreamWriter sw = new StreamWriter(outFile, true))
            {
                sw.WriteLine("--------Ny Kvittorad--------");
                sw.WriteLine(KvittoDatum.ToString());
                foreach (var receiptRow in KvittoRader)
                {
                    sw.WriteLine(receiptRow.ProduktNamn + " " + receiptRow.Antal + " * " + receiptRow.Pris + " = " + receiptRow.Summa);
                }
                sw.WriteLine("Total: " + Total);
                sw.WriteLine("----------------------------");
            }
        }

        public void printRecepitRows()
        {
            foreach (var kvittoRad in KvittoRader)
            {
                kvittoRad.printRow();
            }
        }

        private int receiptRowExists(int productID)
        {
            return KvittoRader.FindIndex(item => item.ProduktId == productID);
        }

        // ANVÄND DENNA MED RABATT FUNKTIONEN
        //private void cheackDiscount()
        //{
        //    if (TotalProduktAntal >= 1001 && TotalProduktAntal <= 2000)
        //    {
        //        Rabatt = Math.Round((TotalProduktAntal * 0.02), 2);
        //        Total = TotalProduktAntal - Rabatt;
        //    }
        //    else if (TotalProduktAntal > 2000)
        //    {
        //        Rabatt = Math.Round((TotalProduktAntal * 0.03), 2);
        //        Total = TotalProduktAntal - Rabatt;
        //    }
        //}

        // ANVÄND DENNA MED RABATT FUNKTIONEN
        //private void printDiscount()
        //{
        //    cheackDiscount();
        //    if (Rabatt > 0)
        //    {
        //        Console.BackgroundColor = ConsoleColor.Red;
        //        Console.ForegroundColor = ConsoleColor.White;
        //        Console.WriteLine($"Items Total:  {TotalProduktAntal} kr");
        //        Console.WriteLine($"Rabbat:  -{Rabatt} kr");
        //        Console.ResetColor();
        //    }
        //    else
        //    {
        //        Total = TotalProduktAntal;
        //    }
        //}

        private void printDate()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"KVITTO   {KvittoDatum}");
            Console.ResetColor();
        }

        private void printTotal()
        {
            Total = TotalProduktAntal;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Total:  {Total} kr");
            Console.ResetColor();
        }
    }
}
