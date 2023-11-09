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
            _bookList = new string[0];
        }

        public LibraryUser() : this("Ivan", "Ivanov", "+000000000000", 10)
        {

        }


        public void AddBook(string bookName)
        {
            if (_bookList.Length >= BookLimit)
                return;
            foreach (var book in _bookList)
            {
                if (bookName == book)
                    return;
            }
            var list = _bookList.ToList();
            list.Add(bookName);
            _bookList = list.ToArray();
        }

        public void RemoveBook(string bookName)
        {
            var list = _bookList.ToList();
            if (list.Remove(bookName))
                _bookList = list.ToArray();
        }

        public int BooksCount()
        {
            return _bookList.Length;
        }

        public string BookInfo(int index)
        {
            if (index < 0 || index >= _bookList.Length)
                return "";
            return _bookList[index];
        }

    }


}
