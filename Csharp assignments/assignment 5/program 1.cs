using System;
//1.Create a class called Books with BookName and AuthorName as members. Instantiate the class through constructor and also write a method Display() to display the details. 

//Create an Indexer of Books Object to store 5 books in a class called BookShelf.Using the indexer method assign values to the books and display the same.

//Hint(use Aggregation/composition)


namespace ConsoleApp16
{
    class Books
    {
        public string _BookName { get; set; }
        public string _AuthorName { get; set; }

        public Books(string bookName, string authorName)
        {
            _BookName = bookName;
            _AuthorName = authorName;
        }

        public void DisplayBookDetails()
        {
            Console.WriteLine("\nAdded Book Details:");
            Console.WriteLine("Book Name:" + _BookName);
            Console.WriteLine("Author Name:" + _AuthorName);
        }

    }
    class BookShelf
    {
        Books[] books = new Books[5];

        public Books[] Books
        {
            get { return books; }
        }


        public Books this[int index]
        {
            get
            {
                if ((index >= 0) && (index < books.Length))
                {
                    return books[index];
                }
                return null;
            }
            set
            {
                if ((index >= 0) && (index < books.Length))
                {
                    books[index] = value;
                }
            }
        }

        public void DisplayBookInShelf()
        {
            Console.WriteLine("\nResult\n");
            for (int i = 0; i < books.Length; i++)
            {
                if (books[i] != null)
                {
                    Console.WriteLine($"Book Name is {books[i]._BookName} and Author Name is {books[i]._AuthorName}");
                }
            }
        }
    }


    class Question1
    {
        static void Main()
        {
            BookShelf objBookShelf = new BookShelf();

            for (int i = 0; i < objBookShelf.Books.Length; i++)
            {
                Console.WriteLine($"\nEnter the data {i + 1}(Note:Seperate both book name and author name by comma)");
                string data = Console.ReadLine();
                string[] bookData = data.Split(',');
                try
                {
                    if (bookData.Length != 2)
                    {
                        throw new ArgumentNullException("Please enter both the book name and author name separated by a comma.");
                    }
                    objBookShelf[i] = new Books(bookData[0], bookData[1]);
                    objBookShelf[i].DisplayBookDetails();
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("Enter both data.It is mandatory");
                }

            }

            objBookShelf.DisplayBookInShelf();

            Console.WriteLine("\n\n***Press Enter to exit***");
            Console.ReadKey();

        }
    }

}
