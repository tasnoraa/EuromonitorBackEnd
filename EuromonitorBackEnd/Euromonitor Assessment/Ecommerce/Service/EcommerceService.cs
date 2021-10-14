using EcommerceRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Ecommerce.Service
{
    public class EcommerceService: IEcommerceService
    {
        EcommerceDBContext _context;
        public EcommerceService(EcommerceDBContext context)
        {
            _context = context;
        }

        public List<Book> GetBooks() 
        {
            return _context.books.ToList();
        }
        [HttpGet("{id}")]
        public Book GetBook(int id) 
        {
            return _context.books.Where(x => x.id == id).FirstOrDefault();
        }
    }
}
