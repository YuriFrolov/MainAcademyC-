using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello_Cons_Dr_Methods
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Implement start position, width and height, symbol, message input

                //Create Box class instance

                //Use  Box.Draw() method

                var startPosition = new Tuple<int, int>(5, 5);
                var width = 50;
                var height = 10;
                var symbol = '*';
                var message =
                    "In a hole in the ground there lived a hobbit. Not a nasty, dirty, wet hole, filled with the ends of worms and an oozy smell, nor yet a dry, bare, sandy hole with nothing in it to sit down on or to eat: it was a hobbit-hole, and that means comfort.";
                var box = new Box();
                box.Draw(startPosition, width, height, symbol, message);

                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
            }

        }
    }
}
