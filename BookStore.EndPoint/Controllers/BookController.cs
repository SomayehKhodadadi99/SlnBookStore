using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

using System;

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
            List<Book> lstbooks = _db.Books.Include(p => p.Publisher).ToList();
            return View(lstbooks);
        }
        [HttpGet]
        public IActionResult Upsert(long? Id)
        {
            Bookvw book = new Bookvw();


            book.PublisherList = _db.Publishers.Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() }).ToList();


            if (Id == 0 || Id == null)
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

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Book book)
        {

            //--------------------------------------
            if (!ModelState.IsValid)
            {
                if (book?.Id == 0)
                {
                    //create

                    Book bookProxy = new Book();
                    var result = await _db.Books.AddAsync(book);


                }
                else
                {
                    Book mybooks = _db.Books.Include(p => p.Publisher).First(x => x.Id == book.Id);


                    if (mybooks is not null)
                    {
                        mybooks.Title = book.Title;
                        //mybooks.Authors = book.Authors;
                        mybooks.PublisherId = book.PublisherId;
                        mybooks.ISBN = book.ISBN;
                        mybooks.Price = book.Price;

                        _db.Books.Update(mybooks);
                    }

                }



                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Details(int? id)
        {
        
            Book book = _db.Books.FirstOrDefault(d => d.Id == id);

            if (book == null)
            {
                return NotFound();
            }
          
            var viewModel = new BookDetailvw
            {
                BookId = book.Id,
                mybook = book,
                BookDetail = new BookDetail()
            };

            viewModel.BookDetail = _db.BookDetails.FirstOrDefault(x => x.Book.Id == id);
         
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookDetailvw obj)
        {
                if (obj.BookDetail.Id == 0) 
                {
                //create 

                var bookSelect=_db.Books.Where(p => p.Id == obj.BookId).FirstOrDefault();

                if (bookSelect != null)
                {
                obj.BookDetail.Book = bookSelect;
                    
              
                   _db.BookDetails.Add(obj.BookDetail);
                }
           

                }
                else
                {
                //edit

                Book bookFind = _db.Books.FirstOrDefault(d => d.Id == obj.BookId);


                obj.BookDetail.Book = bookFind;
                _db.BookDetails.Update(obj.BookDetail);

                }
                _db.SaveChanges();

            return RedirectToAction("Index", new { id = obj.BookId });
            // }

        }

        [HttpGet]
        public IActionResult ManageAuthors(long Id)
        {

            AuthorBookVM authorBook = new AuthorBookVM();

            authorBook.BookAuthorList=_db.AuthorBooks.Include(p=>p.Author).Include(p=>p.Book).Where(x=>x.BookId==Id).ToList();

          
            authorBook.AuthorBook = new AuthorBook { BookId = Id };

            authorBook.Book = _db.Books.FirstOrDefault(p => p.Id == Id);


           List<long> AssignAuthor = authorBook.BookAuthorList.Select(p => p.AuthorId).ToList();

            var tempList =_db.Authors.Where(p => !AssignAuthor.Contains(p.Id)).ToList();

            authorBook.AuthorList = tempList.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString()
            });

            return View(authorBook);
      
        }


        [HttpPost]
        
        public async Task<IActionResult> ManageAuthors(AuthorBook authorBook)
        {

            //--------------------------------------
            if (authorBook.AuthorId!=0 && authorBook.BookId!=0)
            {
             

                    AuthorBook bookProxy = new AuthorBook();
                    var result = await _db.AuthorBooks.AddAsync(authorBook);



                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(ManageAuthors),new {@id=authorBook.BookId});
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult RemoveAuthors(long AuthorId,AuthorBookVM authorBook)
        {
            var bookId=authorBook.Book.Id;
            AuthorBook au= _db.AuthorBooks.FirstOrDefault(p => p.AuthorId == AuthorId && p.BookId == bookId);

            _db.AuthorBooks.Remove(au);

            _db.SaveChanges();
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });
        }




    }
}
