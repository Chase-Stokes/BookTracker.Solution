using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.Controllers
{
  public class AuthorsController : Controller
  {
    private readonly LibraryContext _db;
    public AuthorsController(LibraryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Author> model = _db.Authors.ToList();
      return View(model);
    }
    
    public ActionResult Create()
    {
      return View();
    }
    
    [HttpPost]
    public ActionResult Create(Author author)
    {
      _db.Authors.Add(author);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult Details(int id)
    {
      var thisAuthor = _db.Authors
        .Include(a => a.JoinEntities)
        .ThenInclude(join => join.Book)
        .FirstOrDefault(a => a.AuthorId == id);
      var rList = _db.Authors.ToList();
      rList.Reverse();
      ViewBag.Previous = rList.FirstOrDefault(a => a.AuthorId < id);
      ViewBag.Next = _db.Authors.FirstOrDefault(a => a.AuthorId > id);
      return View(thisAuthor);
    }
    public ActionResult Edit(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }
    [HttpPost]
    public ActionResult Edit(Author author)
    {
      _db.Entry(author).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddBook(int id)
    {
      List<Book> books = _db.Books.ToList();
      List<Book> BooksList = _db.Books.ToList();
      var a = _db.AuthorBook.Where(book => book.AuthorId == id);
      foreach(AuthorBook w in a)
      {
        foreach(Book book in books)
        {
          if (book.BookId == w.BookId)
          {
            BooksList.Remove(book);
          }
        }
      }
      ViewBag.num = BooksList.Count;
      ViewBag.BookId = new SelectList(BooksList, "BookId", "Title");
      var thisAuthor = _db.Authors.FirstOrDefault(a => a.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost]
    public ActionResult AddBook(Author author, int BookId)
    {
      if (BookId != 0 && !_db.AuthorBook.Any(f => f.AuthorId == author.AuthorId && f.BookId == BookId))
      {
        _db.AuthorBook.Add(new AuthorBook() { AuthorId = author.AuthorId, BookId = BookId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
      
    }
    
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(c => c.AuthorId == id);
      _db.Authors.Remove(thisAuthor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}