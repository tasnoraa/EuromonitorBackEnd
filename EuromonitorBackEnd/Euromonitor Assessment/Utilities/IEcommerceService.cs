using EcommerceRepository;
using System.Collections.Generic;


namespace Utilities
{
    public interface IEcommerceService
    {
        List<Book> GetBooks();
        Book GetBook(int id);
    }
}
