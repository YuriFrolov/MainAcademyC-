using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hello_Cons_Dr_Methods
{
    class Box
    {
        //1.  Implement public  auto-implement properties for start position (point position)
        //auto-implement properties for width and height of the box
        //and auto-implement properties for a symbol of a given set of valid characters (*, + ,.) to be used for the border 
        //and a message inside the box
        public Tuple<int, int> StartPosition { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public char Symbol { get; set; }
        public string Message { get; set; }

        //2.  Implement public Draw() method
        //to define start position, width and height, symbol, message  according to properties
        //Use Math.Min() and Math.Max() methods
        //Use draw() to draw the box with message

        public void Draw(Tuple<int, int> startPosition, int width, int height, char symbol, string message)
        {
            StartPosition = startPosition;
            Width = Math.Min(Console.WindowWidth - startPosition.Item1, Math.Max(3, width));
            Height = Math.Min(Console.WindowHeight - startPosition.Item2, Math.Max(3, height));
            Symbol = symbol;
            Message = message;
            draw();
        }

        //3.  Implement private method draw() with parameters 
        //for start position, width and height, symbol, message
        //Change the message in the method to return the Box square
        //Use Console.SetCursorPosition() method
        //Trim the message if necessary

        private string FramedLine()
        {
            string result = "";
            for (var i = 0; i < Width; i++)
                result += Symbol;
            return result;
        }

        private string FramedLine(string line)
        {
            string result = line;
            var spacesLength = Width - 2 - line.Length;
            var leftSpacesLength = spacesLength / 2;
            var rightSpacesLength = spacesLength - leftSpacesLength;
            for (var i = 0; i < rightSpacesLength; i++)
                result += " ";
            for (var i = 0; i < leftSpacesLength; i++)
                result = " " + result;
            result = Symbol + result + Symbol;
            return result;
        }

        private List<string> PrepareMessageLines()
        {
            string message = Message.TrimStart().TrimEnd();
            var maxMessageWidth = Width - 2;
            var maxMessageHeight = Height - 2;
            List<string> lines = new List<string>();
            string breakSymbols = " ,.:;!?!'\"/+-\\*={}[]";
            while (message.Length > maxMessageWidth)
            {
                var breakIndex = -1;
                for (var i = 0; i < maxMessageWidth; i++)
                {
                    if (breakSymbols.IndexOf(message[i]) >= 0)
                        breakIndex = i;
                }
                if (breakIndex < 0)
                    breakIndex = maxMessageWidth - 1;
                lines.Add(FramedLine(message.Remove(breakIndex)));
                message = message.Remove(0, breakIndex);
            }
            if (message.Length > 0)
                lines.Add(FramedLine(message));
            while (lines.Count > maxMessageHeight)
            {
                lines.RemoveAt(lines.Count - 1);
            }

            var emptyLine = FramedLine("");
            while (lines.Count < maxMessageHeight)
            {
                lines.Insert(0, emptyLine);
                if (lines.Count >= maxMessageHeight)
                    break;
                lines.Add(emptyLine);
            }
            var filledLine = FramedLine();
            lines.Insert(0, filledLine);
            lines.Add(filledLine);
            return lines;
        }
        private void draw()
        {
            if (Height <= 3 ||
                Width <= 3 ||
                StartPosition.Item1 < 0 ||
                StartPosition.Item1 + Width > Console.WindowWidth ||
                StartPosition.Item2 + Height > Console.WindowHeight ||
                StartPosition.Item2 < 0 ||
                Symbol < 10)
                return;
            var backupBackgroundColor = Console.BackgroundColor;
            var backupForegroundColor = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            var messages = PrepareMessageLines();
            for (var i = 0; i < messages.Count; i++)
            {
                Console.SetCursorPosition(StartPosition.Item1, StartPosition.Item2 + i);
                Console.Write(messages[i]);
            }
            Console.BackgroundColor = backupBackgroundColor;
            Console.ForegroundColor = backupForegroundColor;
            Console.WriteLine();
        }

    }
}
