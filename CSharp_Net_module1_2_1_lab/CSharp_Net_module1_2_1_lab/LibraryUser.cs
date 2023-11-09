using System;
using System.Linq;

namespace CSharp_Net_module1_2_1_lab
{

    // 1) declare interface ILibraryUser
    // declare method's signature for methods of class LibraryUser



    // 2) declare class LibraryUser, it implements ILibraryUser


    // 3) declare properties: FirstName (read only), LastName (read only), 
    // Id (read only), Phone (get and set), BookLimit (read only)

    // 4) declare field (bookList) as a string array


    // 5) declare indexer BookList for array bookList

    // 6) declare constructors: default and parameter

    // 7) declare methods: 

    //AddBook() – add new book to array bookList

    //RemoveBook() – remove book from array bookList

    //BookInfo() – returns book info by index

    //BooksCout() – returns current count of books

    class LibraryUser : ILibraryUser
    {
        private static int _lastId;
        private string[] _bookList;
        private int _booksCount;

        public string this[int index] => BookInfo(index);
        public string FirstName { get; }

        public string LastName { get; }

        public int Id { get; }

        public string Phone { get; set; }

        public int BookLimit { get; }

        
        public LibraryUser(string firstName, string lastName, string phone, int bookLimit)
        {
            Id = _lastId++;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            BookLimit = bookLimit;
            _bookList = new string[BookLimit];
        }

        public LibraryUser() : this("Ivan", "Ivanov", "+000000000000", 10)
        {

        }


        public void AddBook(string bookName)
        {

            if (_booksCount >= BookLimit)
                return;
            for (var i = 0; i < _booksCount; i++)
            {
                if (bookName == _bookList[i])
                    return;
            }
            _bookList[_booksCount] = bookName;
            _booksCount++;
        }

        public void RemoveBook(string bookName)
        {
            for (var i = 0; i < _booksCount; i++)
            {
                if (bookName == _bookList[i])
                {
                    _booksCount--;
                    if (i < _booksCount)
                        Array.Copy(_bookList, i + 1, _bookList, i, _booksCount - i);
                    _bookList[_booksCount] = "";
                    break;
                }
            }
            
        }

        public int BooksCount()
        {
            return _booksCount;
        }

        public string BookInfo(int index)
        {
            if (index < 0 || index >= _booksCount)
                return "";
            return _bookList[index];
        }

    }


}
