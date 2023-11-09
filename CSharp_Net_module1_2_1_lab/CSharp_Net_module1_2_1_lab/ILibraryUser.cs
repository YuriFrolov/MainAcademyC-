using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_2_1_lab
{
    internal interface ILibraryUser
    {
        void AddBook(string bookName);
        void RemoveBook(string bookName);
        string BookInfo(int index);
        int BooksCount();
    }
}
