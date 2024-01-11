using System;

namespace Hello_Exception_stud
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Observation titmouse flight");

            Bird myBird = new Bird("Titmouse", 20);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Throw code array overflow exception");
                Console.WriteLine("2. Throw my exception");
                Console.WriteLine("3. Incrementing of bird speed");
                Console.WriteLine("4. Quit");
                var key = Console.ReadKey();
                Console.Write("\b");
                try
                {
                    switch (key.KeyChar)
                    {
                        case '1':
                            throw new OverflowException("overflow exception");
                        case '2':
                            throw new System.Exception("Oh! My system exception...");
                        case '3':
                            myBird.FlyAway(3);
                            break;
                        case '4':
                            return;

                    }
                }
                catch (BirdFlewAwayException ex)
                {
                    Console.WriteLine("Catching bird flew exception: " + ex);
                    Console.WriteLine("When : {0}, Why : {1}", ex.When, ex.Why);
                    Console.WriteLine("HelpLink: {0}", ex.HelpLink);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Catching other exceptions: " + ex);
                    Console.WriteLine("HelpLink: {0}", ex.HelpLink);
                }
                finally
                {
                    if (key.KeyChar != '4')
                    {
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                }
            }

            //1. Create the skeleton code with the  basic exception handling for the menu in the main method 
            //try - catch
            // 1. begin

            //2. Create code for the nested special exception handling in the main method
            //try - catch - catch - finally
            // 2. begin

            //3. Create the menu for three options in the inner try block  
            //In the second option throw the System.Exception
            // 3. begin

            //4. in case 1 code array overflow exception 
            //in case 2 code throw (new System.Exception("Oh! My system exception..."));
            //in case 3  code the sequentially incrementing of Bird speed until to the exception 

            // 3. end

            // 2. end

            // 1. end

        }

    }
}
