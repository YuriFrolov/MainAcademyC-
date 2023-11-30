using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello_fact_advstud
{
    class Program
    {
        static void Main(string[] args)
        {
            //Define parameters to calculate the factorial of
            //Call fact() method to calculate
            Console.WriteLine("Enter positive integer n");
            if (int.TryParse(Console.ReadLine() ?? "", out int n) && n >= 0)
                Console.WriteLine(n + "! = " + fact(n));
            Console.ReadKey();
        }

        //Create fact() method  with parameter to calculate factorial
        //Use ternary operator
        static ulong fact(int n)
        {
            return n == 0 ? 1 : fact(n - 1) * (ulong)n;
        }
    }



}
