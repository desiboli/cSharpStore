using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace newStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var produkter = ReadProductFromFile();

            while (true)
            {
                Console.WriteLine("KASSA");
                Console.WriteLine("1. Ny kund");
                Console.WriteLine("0. Avsluta");

                var select = Console.ReadLine();
                if (select == "0")
                {
                    break;
                }
                else if (select == "1")
                {
                    nyKund(produkter);
                }
                else
                {
                    Console.WriteLine("Fel inmatning, försök igen!");
                }
            }
        }

        private static List<Produkt> ReadProductFromFile()
        {
            var produktLista = new List<Produkt>();

            using (var sr = File.OpenText(@"../../../Produkter.txt"))
            {
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null) break;

                    //Bananer,300,12,typ
                    string[] split = line.Split(',');
                    var p = new Produkt();
                    p.ProduktNamn = split[0];
                    p.ProduktId = Convert.ToInt32(split[1]);
                    p.Pris = Convert.ToInt32(split[2]);
                    p.PrisTyp = (PriceType)Enum.Parse(typeof(PriceType), split[3]);
                    produktLista.Add(p);
                }
            }

            return produktLista;
        }

        public static void nyKund(List<Produkt> produkter)
        {
            var kvitto = new Kvitto();

            while (true)
            {
                int productID = -1;
                Produkt produkt;

                Console.WriteLine("kommandon:");                
                Console.WriteLine("<produktid>  <antal>");
                Console.WriteLine("PAY");

                string input = Console.ReadLine();

                var inputType = checkInputType(input);

                if (inputType == "ERROR")
                {
                    Console.WriteLine("Fel kommando");
                }
                else if (inputType == "PAY")
                {
                    if (kvitto.KvittoRader.Count() != 0)
                    {
                        kvitto.saveReceipt();
                    }
                    else
                    {
                        Console.WriteLine("Inget kvitto har sparats. Välkommen Åter! :-)");
                    }
                    break;
                }
                else if (inputType == "RETURN")
                {
                    if (kvitto.KvittoRader.Count() != 0)
                    {
                        productID = checkReturnInput(input);

                        if (productID == -1)
                        {
                            Console.WriteLine("Fel kommando");
                        }
                        else
                        {
                            productID = checkReturnInput(input);
                            produkt = produkter.Find(p => p.ProduktId == productID);
                            if (produkt != null)
                            {
                                //kvitto.decreaseQuantity(productID);
                                kvitto.printReceipt();
                            }
                            else
                            {
                                Console.WriteLine("Produkten finns ej!");
                            }
                        }
                    }
                }
                else
                {
                    // CONTINUE
                    var validatedInput = checkContinueInput(input);

                    if (validatedInput.Item1 == -1)
                    {
                        Console.WriteLine("Fel kommando");
                    }
                    else
                    {
                        produkt = produkter.Find(p => p.ProduktId == validatedInput.Item1);
                        if (produkt != null)
                        {
                            var receiptRow = new KvittoRad(produkt.ProduktNamn, validatedInput.Item2, produkt.Pris, produkt.ProduktId);
                            kvitto.addRecepitRow(receiptRow);
                            kvitto.printReceipt();
                        }
                        else
                        {
                            Console.WriteLine("Produkten finns ej!");
                        }
                    }
                }
            }
        }

        public static string checkInputType(string input)
        {
            string inputType = "";
            var inputList = input.Split(' ');
            if ((inputList.Count() == 1) && (input.ToUpper() == ("PAY")))
            {
                inputType = "PAY";
            }
            else if ((inputList.Count() == 2) && (inputList[0].ToUpper() == "RETURN"))
            {
                inputType = "RETURN";
            }
            else if (inputList.Count() == 2)
            {
                inputType = "CONTINUE";
            }
            else
            {
                inputType = "ERROR";
            }
            return inputType;
        }

        public static int checkReturnInput(string input)
        {
            int productID;
            var inputList = input.Split(' ');
            bool result = int.TryParse(inputList[1], out productID);
            if (result == false)
            {
                productID = -1;
            }
            return productID;
        }

        public static Tuple<int, int> checkContinueInput(string input)
        {
            var inputList = input.Split(' ');
            int productID;
            int quantity;

            bool resultProductID = int.TryParse(inputList[0], out productID);
            bool resultQuantity = int.TryParse(inputList[1], out quantity);

            if ((resultProductID == true) && (resultQuantity == true))
            {
                return Tuple.Create(productID, quantity);
            }
            else
            {
                return Tuple.Create(-1, -1); // error in return command
            }
        }
    }
}
