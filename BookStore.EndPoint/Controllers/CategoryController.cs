using Application.Interfaces.Contexts;
using Application.Services.Categories.Commands;
using Application.Services.Categories.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.EndPoint.Controllers
{
    public class CategoryController : Controller
    {

        public IGetCategoryService Category { get; set; }
        public IRegisterCategoryService RegCat { get; set; }

        public IDataBaseContext _context;


        public CategoryController(IGetCategoryService _category,IRegisterCategoryService _reqCat,IDataBaseContext Context)
        {
            Category = _category;
            RegCat = _reqCat;
            _context = Context;
        }



        public IActionResult Index()
        {
            List<Category> lstCategory = _context.Categories.ToList();

            return View(lstCategory);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Category category = new();
            if (id == null || id == 0)
            {
                //create
                return View(category);
            }


            category = _context.Categories.First(c => c.Id == id);
            //edit
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Category cat)
        {
            if (ModelState.IsValid)
            {
                if (cat.Id == 0)
                {
                    cat.InsertTime = DateTime.Now;
                    cat.IsRemoved = false;
                    await _context.Categories.AddAsync(cat);
                }
                else
                {
                    _context.Categories.Update(cat);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cat);
        }


        public async Task<IActionResult> Delete(int id)
        {
            Category cat = _context.Categories.First(c => c.Id == id);
            if (cat != null)
            {
                _context.Categories.Remove(cat);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateMultiPle2()
        {
            List<Category> lstCat = new List<Category>();
            for (int i = 0; i < 2; i++)
            {

                lstCat.Add(new Category { CategoryName = Guid.NewGuid().ToString(), InsertTime = DateTime.Now,IsRemoved=false });

            }
            _context.Categories.AddRange(lstCat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateMultiPle5()
        {
            List<Category> lstCat = new List<Category>();
            for (int i = 0; i < 5; i++)
            {

                lstCat.Add(new Category { CategoryName = Guid.NewGuid().ToString(), InsertTime = DateTime.Now, IsRemoved = false });

            }
            _context.Categories.AddRange(lstCat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveMultiple2()
        {

            List<Category> lstCat = _context.Categories.OrderByDescending(p => p.Id).Take(2).ToList();

            _context.Categories.RemoveRange(lstCat);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveMultiple5()
        {

            List<Category> lstCat = _context.Categories.OrderByDescending(p => p.Id).Take(5).ToList();

            _context.Categories.RemoveRange(lstCat);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }

}

