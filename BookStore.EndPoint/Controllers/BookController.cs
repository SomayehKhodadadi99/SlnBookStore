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
            List<Book> lstbooks=_db.Books.Include(p=>p.Publisher).ToList();
            return View(lstbooks);
        }
        [HttpGet]
        public  IActionResult Upsert(long? Id)
        {
            Bookvw book = new Bookvw();
             

            book.PublisherList = _db.Publishers.Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() }).ToList();

         
            if (Id==0 || Id==null)
            {
                //باید یه دراپ بسازم
                //insert

              

                book.Book = new Book();

                return View(book);
            }

            var bookResult = _db.Books.Where(x => x.Id == Id).SingleOrDefault();
            if (bookResult != null)
            {
                book.Book = bookResult;
                return View(book);
            }
            return NotFound();



            //book.Book=_db.Books.Include(p=>p.Publisher).First(p => p.Id == Id);


            //if (book == null)
            //{
            //    return NotFound();    
            //}
            //return View(book);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Book book)
        {

            //--------------------------------------
             if (!ModelState.IsValid) 
            {
                if (book?.Id==0) 
                {
                    //create

                    Book bookProxy = new Book();
                    var result = await  _db.Books.AddAsync(book);
                    
                  
                }
                else
                {
                    Book mybooks = _db.Books.Include(p => p.Publisher).First(x => x.Id == book.Id);


                    if (mybooks is not null)
                    {
                        mybooks.Title = book.Title;
                        mybooks.Authors = book.Authors;
                        mybooks.PublisherId = book.PublisherId;
                        mybooks.ISBN = book.ISBN;
                        mybooks.Price = book.Price;

                        _db.Books.Update(mybooks);
                    }



                    //update
                  

                }



               await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
             else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult Details(long? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            BookDetail obj = new BookDetail();

            obj.Book = _db.Books.FirstOrDefault(u => u.Id == Id);
            
            //edit
            if ( obj.Id!=0)
            {

                obj = _db.BookDetails.Include(p => p.Book).FirstOrDefault(p => p.Book.Id == Id);
            }
         

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Details(BookDetail bd)
        {

        
            if (bd.Id==0)
            {
                bd.Id = 0;
                //insert
                bd.InsertTime = DateTime.Now;
                bd.IsRemoved = false;
                await _db.BookDetails.AddAsync(bd);
            }
            else
            {
                //update
                 _db.BookDetails.Update(bd);
            }
            _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
