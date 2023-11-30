using Hello_Class_stud.Hello_Class_stud;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Hello_Class_stud
{
    //Implement class Morse_matrix derived from String_matrix, which realize IMorse_crypt
    class Morse_matrix : String_matrix, IMorse_crypt
    {
        public const int Size_2 = Alphabet.Size;
        private int offset_key = 0;

        //Implement Morse_matrix constructor with the int parameter for offset
        //Use fd(Alphabet.Dictionary_arr) and sd() methods

        public Morse_matrix(int offsetKey = 0) : this(Alphabet.Dictionary_arr, offsetKey) { }

        //Implement Morse_matrix constructor with the string [,] Dict_arr and int parameter for offset
        //Use fd(Dict_arr) and sd() methods

        public Morse_matrix(string[,] dictionaryArr, int offsetKey = 0)
        {
            offset_key = offsetKey;
            fd(dictionaryArr);
            sd();
        }

        private Morse_matrix(Morse_matrix morseMatrix, int offsetKey)
        {
            offset_key = offsetKey;
            for (int ii = 0; ii < Size1; ii++)
                for (int jj = 0; jj < Size_2; jj++)
                    this[ii, jj] = morseMatrix[ii, jj];
            sd();
        }


        private void fd(string[,] Dict_arr)
        {
            for (int ii = 0; ii < Size1; ii++)
                for (int jj = 0; jj < Size_2; jj++)
                    this[ii, jj] = Dict_arr[ii, jj];
        }


        private  void sd()
        {
            int off = Size_2 - offset_key;
            for (int jj = 0; jj < off; jj++)
                this[1, jj] = this[1, jj + offset_key];
            for (int jj = off; jj < Size_2; jj++)
                this[1, jj] = this[1, jj - off];
        }

        //Implement Morse_matrix operator +

        public static Morse_matrix operator +(Morse_matrix a, Morse_matrix b)
        {            
            return new Morse_matrix(a, a.offset_key + b.offset_key);
        }


        //Realize crypt() with string parameter
        //Use indexers

        public string crypt(string line)
        {
            var symbols = line.ToLowerInvariant().ToCharArray();
            var result = "";
            foreach (var symbol in symbols)
            {
                for (var j = 0; j < Size2; j++)
                {
                    if (symbol == this[0, j][0])
                    {
                        result += this[1, j];
                        break;
                    }
                }
            }
            return result;
        }

        //Realize decrypt() with string array parameter
        //Use indexers

        public string decrypt(string[] lines)
        {
            var result = "";
            foreach (var line in lines)
            {
                for (var j = 0; j < Size2; j++)
                {
                    if (line == this[1, j])
                    {
                        result += this[0, j];
                        break;
                    }
                }
            }
            return result;
        }


        //Implement Res_beep() method with string parameter to beep the string
        public void Res_beep(string line)
        {
            var arr = line.ToLower().ToCharArray();
            foreach (char morzeSymbol in arr)
            {
                Console.Write(morzeSymbol);
                if (morzeSymbol == '.')
                    Console.Beep(1000, 250);
                else if (morzeSymbol == '-')
                    Console.Beep(1000, 750);
                Thread.Sleep(50);
            }
            Console.WriteLine();
        }


    }
}

