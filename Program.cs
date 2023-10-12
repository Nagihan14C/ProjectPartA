using System;

namespace Kvitto
{
    internal class Program
    {
        const decimal Vat = 0.25M;
        public struct Article
        {
            public string Name;
            public decimal Price;
            public static int NrProducts;
            public static decimal CombinedPrice;

            public Article(string name, decimal price)
            {
                Name = name;
                Price = price;
                NrProducts++;
                CombinedPrice += price;
            }
        }

        static void Main(string[] args)
        {
            int TotalProducts = GetNumberOfProducts();
            Article[] products = new Article[TotalProducts];

            GetProductNamesAndPrices(TotalProducts, products);
            Console.Clear();
            WritePrices(products);
        }

        static void WritePrices(Article[] products)
        {
            Console.WriteLine($"Number of articles: {Article.NrProducts}");
            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine($"{i,-5}{products[i].Name,-20} {products[i].Price,-1} kr");
            }
            Console.WriteLine($"\nTotal purchase: {Article.CombinedPrice} kr");

            decimal totalWithoutVat = 0;

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name,-20} {product.Price,-1:F2} kr");
                totalWithoutVat += product.Price;
            }
            decimal VatTotal = totalWithoutVat * Vat;
            decimal totalWithVat = totalWithoutVat + Vat;

            Console.WriteLine($"Includes VAT ({Vat * 100}%): {totalWithVat:F2} kr");
        }

        private static void GetProductNamesAndPrices(int TotalProducts, Article[] products)
        {
            for (int i = 0; i < TotalProducts; i++)
            {
                while (true)
                {
                    Console.WriteLine($"Please enter the name and price for article {i} in the format name; price (example: Bananas; 6,48):");
                    string input = Console.ReadLine();
                    string[] splittedArray = input.Split(";");

                    if (splittedArray.Length == 2 && decimal.TryParse(splittedArray[1], out decimal price))
                    {
                        products[i] = new Article(splittedArray[0].Trim(), price);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, Please try again!");
                    }
                }
            }
        }

        public static int GetNumberOfProducts()
        {
            int TotalProducts = default;
            while (true)
            {
                Console.WriteLine("How many articles do you want (between 1-10)?:");
                string input = Console.ReadLine();
                if (int.TryParse(input, out TotalProducts) && TotalProducts > 0 && TotalProducts <= 10)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong input, please try again");
                }
            }
            return TotalProducts;
        }
    }
}