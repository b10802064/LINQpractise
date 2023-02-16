using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linqpractise01
{
    internal class Program
    {
        static void Main(string[] args)
        {


            List<Product> list = productslist();


            foreach (Product product in list)
            {
                Console.WriteLine(product.Name + product.No);
            }
            var reader = new StreamReader(File.OpenRead(@"product.csv"));
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            List<int> listC = new List<int>();
            List<decimal> listD = new List<decimal>();
            List<string> listE = new List<string>();
            bool isFirst = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    listA.Add(values[0]);
                    listB.Add(values[1]);
                    listC.Add(int.Parse(values[2]));
                    listD.Add(decimal.Parse(values[3]));
                    listE.Add(values[4]);
                }
            }



            for (int i = 0; i < listA.Count; i++)
            {
                list.Add(new Product() { No = listA[i], Name = listB[i], quantity = listC[i], price = listD[i], clsaaes = listE[i] });
            }



            //foreach (Product product in list)
            //{
            //    Console.WriteLine(product.Name +"\t"+ product.No+ "\t" + product.quantity + "\t" + product.price + "\t" + product.clsaaes);
            //}全部商品

            var sumprice = list.Sum(x => x.price);
            Console.WriteLine($"總價格為:{sumprice}");
            Console.WriteLine("-----------------------------------");
            var avgprice = list.Average(x => x.price);
            Console.WriteLine($"平均價格為:{avgprice}");
            Console.WriteLine("-----------------------------------");
            var sumquantity = list.Sum(x => x.quantity);
            Console.WriteLine($"總數量為{sumquantity}");
            Console.WriteLine("-----------------------------------");
            var avgquantity = list.Average(x => x.quantity);
            Console.WriteLine($"平均數量為:{avgquantity}");
            Console.WriteLine("-----------------------------------");
            var maxprice = list.Max(x => x.price);
            Console.WriteLine($"最貴商品為:{maxprice}");
            Console.WriteLine("-----------------------------------");
            var minprice = list.Min(x => x.price);
            Console.WriteLine($"最便宜商品為:{minprice}");
            Console.WriteLine("-----------------------------------");
            var cccsumprice = list.Where(x => x.clsaaes == "3C").Sum(x => x.price);
            Console.WriteLine($"3C商品總價為:{cccsumprice}");
            Console.WriteLine("-----------------------------------");
            var dietprice = list.Where(x => x.clsaaes == "飲料" || x.clsaaes == "食品").Sum(x => x.price);
            Console.WriteLine($"飲料食品商品總價為:{dietprice}");
            Console.WriteLine("-----------------------------------");
            var min100quantitydiet = list.Where(x => (x.clsaaes == "飲料" || x.clsaaes == "食品") && (x.quantity > 100));
            Console.WriteLine($"以下為飲料食品商品數量大於100的商品:");
            foreach (var item in min100quantitydiet)
            {
                Console.WriteLine($"{item.No}\t{item.Name}\t{item.quantity}\t{item.price}\t{item.clsaaes}");
            }
            Console.WriteLine("-----------------------------------");
            var min1000 =
                from o in list
                where o.quantity > 1000
                group o by o.clsaaes into gp
                select gp;
            Console.WriteLine("以下為各類別商品數量大於1000的商品:");
            foreach (var item in min1000)
            {
                Console.WriteLine($"類別 : {item.Key}");
                foreach (var item2 in item)
                {
                    Console.WriteLine($"{item2.No}\t{item2.Name}\t{item2.quantity}\t{item2.price}");
                }
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("以下為各類別商品數量大於1000的商品的平均價格:");
            var min1000avg =
                from o in list
                where o.quantity > 1000
                group o by o.clsaaes into gp
                select gp;
            foreach (var item in min1000)
            {
                Console.WriteLine($"類別 : {item.Key}");
                Console.WriteLine($"{item.Average((x) => x.price)}");
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("商品價格由高到低排序");
            list.Sort((x, y) => -x.price.CompareTo(y.price));
            foreach (Product product in list)
            {
                Console.WriteLine(product.Name + "\t" + product.No + "\t" + product.quantity + "\t" + product.price + "\t" + product.clsaaes);
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("商品價格由低到高排序");
            list.Sort((x, y) => x.price.CompareTo(y.price));
            foreach (Product product in list)
            {
                Console.WriteLine(product.Name + "\t" + product.No + "\t" + product.quantity + "\t" + product.price + "\t" + product.clsaaes);
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("以下為各商品類別底下，最貴的商品:");
            var maxpriceclass =
                from o in list
                group o by o.clsaaes into gp
                select gp;
            foreach (var item in maxpriceclass)
            {
                Console.WriteLine($"類別 : {item.Key}");
                Console.WriteLine($"{item.Max((x) => x.price)}");
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("以下為各商品類別底下，最便宜的商品:");
            var minpriceclass =
                from o in list
                group o by o.clsaaes into gp
                select gp;
            foreach (var item in maxpriceclass)
            {
                Console.WriteLine($"類別 : {item.Key}");
                Console.WriteLine($"{item.Min((x) => x.price)}");
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("以下為價格小於等於 10000 的商品");
            var min10000 = list.Where((x) => x.price >= 10000);
            foreach (var item in min10000)
            {
                Console.WriteLine($"{item.No}\t{item.Name}\t{item.quantity}\t{item.price}\t{item.clsaaes}");
            }
            Console.WriteLine("-----------------------------------");
            bool pagetf = true;
            var page1 = list.Take(4);
            var page2 = list.Skip(4).Take(4);
            var page3 = list.Skip(8).Take(4);
            var page4 = list.Skip(12).Take(4);
            var page5 = list.Skip(16).Take(4);

            while (pagetf)
            {
                Console.Write("總共5頁，請輸入頁碼:");
                string page = Console.ReadLine();
                int pageint = Convert.ToInt32(page);
                switch (pageint)
                {
                    case 1:
                        foreach (var item in page1)
                        {
                            Console.WriteLine($"{item.No}\t{item.Name}\t{item.quantity}\t{item.price}\t{item.clsaaes}");
                        }
                        break;
                    case 2:
                        foreach (var item in page2)
                        {
                            Console.WriteLine($"{item.No}\t{item.Name}\t{item.quantity}\t{item.price}\t{item.clsaaes}");
                        }
                        break;
                    case 3:
                        foreach (var item in page3)
                        {
                            Console.WriteLine($"{item.No}\t{item.Name}\t{item.quantity}\t{item.price}\t{item.clsaaes}");
                        }
                        break;
                    case 4:
                        foreach (var item in page4)
                        {
                            Console.WriteLine($"{item.No}\t{item.Name}\t{item.quantity}\t{item.price}\t{item.clsaaes}");
                        }
                        break;
                    case 5:
                        foreach (var item in page5)
                        {
                            Console.WriteLine($"{item.No}\t{item.Name}\t{item.quantity}\t{item.price}\t{item.clsaaes}");
                        }
                        break;
                    default:
                        Console.WriteLine("無此頁碼");
                        break;
                }
                Console.Write("要繼續看嗎(n/y)");
                string yesno = Console.ReadLine();
                if (yesno == "n")
                {
                    pagetf = false;
                }
            }


            Console.ReadLine();

        }



        static List<Product> productslist()
        {
            return new List<Product>();
        }
    }
}
