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
      return view();
    }
    
    public ActionResult Create()
    {
      return view();
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

    public async ActionResult Delete(int id)
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