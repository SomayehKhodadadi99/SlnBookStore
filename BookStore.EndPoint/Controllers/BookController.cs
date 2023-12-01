using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace BookStore.EndPoint.Controllers
{
    public class BookController : Controller
    {

        private readonly DataBaseContext _db;

        public BookController(DataBaseContext context)
        {
                _db = context;
        }


        public IActionResult Index()
        {
            List<Book> lstbooks=_db.Books.ToList();
            return View(lstbooks);
        }
        [HttpGet]
        public  IActionResult Upsert(long? Id)
        {
            if (Id==0 || Id==null)
            {
                //باید یه دراپ بسازم
                //insert

                Bookvw book = new Bookvw();

                book.Book=new Book();

                book.PublisherList = _db.Publishers.Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() }).ToList();
                return View(book);
            }
           
                var bookId=_db.Books.First(p => p.Id == Id);
                if (bookId == null)
                {
                    return NotFound();    
                }
                return View(bookId);
            

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Book book)
        {
             if (!ModelState.IsValid) 
            {
                if (book.Id==0) 
                {
                    //create

                    Book bookProxy = new Book();
                  await  _db.Books.AddAsync(book);
                    
                  
                }
                else
                {
                    Book mybooks = _db.Books.First(x => x.Id == book.Id);
                    //update
                    _db.Books.Update(mybooks);

                }



               await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
             else
            {
                return NotFound();
            }
        }
    }
}
