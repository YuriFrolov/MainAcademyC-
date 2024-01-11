using System;

namespace Hello_Exception_stud
{
    class Bird
    {
        private bool _birdFleeAway;
        private int _flySpeed;
        private int _normalSpeed;
        private string _nick;

        public Bird(string nick, int flySpeed)
        {
            _birdFleeAway = false;
            _normalSpeed = 0;
            _nick = nick;
            _flySpeed = flySpeed;
        }

        public void FlyAway(int incrmnt)
        {
            if (_birdFleeAway)
                Console.WriteLine("{0} is already flying", _nick);
            else
            {
                _normalSpeed += incrmnt;
                if (_normalSpeed >= _flySpeed)
                {
                    throw new BirdFlewAwayException(string.Format("{0} flew with incredible speed!", _nick),
                        "Oh! Startle.", DateTime.Now);

                }
                else
                {
                    Console.WriteLine("Current normal speed of {0} is {1}. Fly speed is {2}", _nick, _normalSpeed, _flySpeed);
                }
            }
        }
    }

    //Create the Bird class with the fields, properties, constructors and the method
    //The public void FlyAway( int incrmnt ) method generates custom exception 


    //Create fields and properties

    //Create constructors

    //Implement Method public void FlyAway( int incrmnt ) which check Bird state by reading field  BirdFlewAway
    // check BirdFlewAway
    // if true 

    // write the message to console
    // else

    // increment the Bird speed by method argument
    // check the condition (NormalSpeed >= FlySpeed[3]) 
    // If it's true 

    // BirdFlewAway = true and we generate custom exception    BirdFlewAwayException(string.Format("{0} flew with incredible speed!", Nick), "Oh! Startle.", DateTime.Now)
    // with HelpLink = "http://en.wikipedia.org/wiki/Tufted_titmouse" else  console.writeline about Bird speed 

}
