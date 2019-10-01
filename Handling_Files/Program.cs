using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Handling_Files.Entities;
using System.Globalization;

namespace Handling_Files
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.Write("Number of records: ");
            int qtdReg = int.Parse(Console.ReadLine());
            List<Product> list = new List<Product>();
            try
            {
                for (int i = 1; i <= qtdReg; i++)
                {
                    Console.Write($"Registro #{i}: ");
                    string reg = Console.ReadLine();
                    string[] fields = reg.Split(',');
                    string name = fields[0];
                    double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                    int quantity = int.Parse(fields[2]);

                    list.Add(new Product(name, price, quantity));

                }


                Console.Write("Where do you want to save this file? ");
                string sourceFilePath = Console.ReadLine() + @"\source.csv";

                using (StreamWriter sw1 = File.AppendText(sourceFilePath))
                {
                    foreach (Product p in list)
                    {
                        sw1.WriteLine(p.Name + "," + p.Price + "," + p.Quantity);
                    }
                }




                string[] lines = File.ReadAllLines(sourceFilePath);

                string sourceFolderPath = Path.GetDirectoryName(sourceFilePath);
                string targetFolderPath = sourceFolderPath + @"\out";
                string targetFilePath = targetFolderPath + @"\summary.csv";

                Directory.CreateDirectory(targetFolderPath);

                using (StreamWriter sw = File.AppendText(targetFilePath))
                {
                    foreach (string line in lines)
                    {

                        string[] fields = line.Split(',');
                        string name = fields[0];
                        double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                        int quantity = int.Parse(fields[2]);

                        Product prod = new Product(name, price, quantity);

                        sw.WriteLine(prod.Name + "," + prod.Total().ToString("F2", CultureInfo.InvariantCulture));
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }

        }
    }
}
