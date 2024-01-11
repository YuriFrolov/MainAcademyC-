using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_4_1_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            var shop = new OnlineShop();
                        
            // 9) declare object of OnlineShop 
            var vasia = new Customer("Vasia");
            var petia = new Customer("Petia");
            // 10) declare several objects of Customer

            // 11) subscribe method GotNewGoods() of every Customer instance 
            // to event NewGoodsInfo of object of OnlineShop
            shop.GoodsInfoEvent += vasia.SampleEventHandler;
            shop.GoodsInfoEvent += petia.SampleEventHandler;
            // 12) invoke method NewGoods() of object of OnlineShop
            // discuss results
            shop.NewGoods("apple");
            Console.ReadLine();
        }
    }
}
