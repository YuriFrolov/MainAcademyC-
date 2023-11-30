using System;
using System.IO;

namespace CSharp_Net_module1_3_1_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            CatchExceptionClass cec = new CatchExceptionClass();
            cec.CatchExceptionMethod();
            // 11) Make some unhandled exception and study Visual Studio debugger report – 
            // read description and find the reason of exception
            //int i = 0; i = 10 / i; //to rause unhandled exception

            Console.ReadKey();

        }
    }
}
