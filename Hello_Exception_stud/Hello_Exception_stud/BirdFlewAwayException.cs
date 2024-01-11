using System;
using System.Runtime.Serialization;

namespace Hello_Exception_stud
{
    [Serializable]
    public class BirdFlewAwayException : ApplicationException
    {
        public DateTime When { get; set; }
        public string Why { get; set; }
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public BirdFlewAwayException(string message, string why, DateTime when) : base(message)
        {
            When = when;
            Why = why;
            HelpLink = "http://en.wikipedia.org/wiki/Tufted_titmouse";
        }

        protected BirdFlewAwayException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
    //Create the BirdFlewAwayException class, derived from ApplicationException  with two properties  

    //When
    //Why

    //Create constructors

}
