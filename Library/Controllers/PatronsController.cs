using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System;

namespace Library.Controllers
{
  [Authorize]
  public class PatronsController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public PatronsController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Patrons.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.CopyId = new SelectList(_db.Copies, "CopyId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Patron patron, int CopyId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      patron.User = currentUser;
      _db.Patrons.Add(patron);
      _db.SaveChanges();
      if (CopyId != 0)
      {
        _db.Checkout.Add(new Checkout() { CopyId = CopyId, PatronId = patron.PatronId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    { 
      ViewBag.Test = _db.Copies.ToList().Count;
      Patron thisPatron = _db.Patrons
        .Include(patron => patron.JoinEntitiesOne)
        .ThenInclude(join => join.Book)
        .FirstOrDefault(patron => patron.PatronId == id);
      ViewBag.Books = _db.Books
        .Include(b => b.JoinEntitiesOne)
        .FirstOrDefault(c => c.BookId == c.BookId);
      return View(thisPatron);
    }

    public ActionResult Edit(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(patron => patron.PatronId == id);
      ViewBag.CopyId = new SelectList(_db.Copies, "CopyId", "Name");
      return View(thisPatron);
    }

    [HttpPost]
    public ActionResult Edit(Patron patron, int CopyId)
    {
      if (CopyId != 0)
      {
        _db.Checkout.Add(new Checkout() { CopyId = CopyId, PatronId = patron.PatronId });
      }
      _db.Entry(patron).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
      public ActionResult CheckOut(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(patron => patron.PatronId == id);
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
      return View(thisPatron);
    }

    [HttpPost]
    public ActionResult CheckOut(Book book, int PatronId)
    {
      if (book.BookId != 0)
      {
        _db.Copies.Add(new Copy() { BookId = book.BookId, PatronId = PatronId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    // [HttpPost]
    // public ActionResult CheckIn(int CopyId)
    // {
    //   Copy target = _db.Copies.FirstOrDefault(copy => copy.CopyId == CopyId);
    //   _db.Entry(target).State = EntityState.Modified;
    //   _db.SaveChanges(); 
    //   return RedirectToAction("Details", new {id = patronId});
    // }

    public ActionResult Delete(int CopyId)
    {
      var thisCopy = _db.Copies.FirstOrDefault(c => c.CopyId == CopyId);
      //ViewBag.test = _db.Books.FirstOrDefault(b => b.BookId == thisCopy.BookId);
      ViewBag.test = CopyId;
      Console.WriteLine("delete get post");
      return View(thisCopy);
    }

    // [HttpPost, ActionName("Delete")]
    // public ActionResult DeleteConfirmed(int id)
    // {
    //   var thisPatron = _db.Patrons.FirstOrDefault(p => p.PatronId == id);
    //   _db.Patrons.Remove(thisPatron);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }

    [HttpPost]
    public ActionResult Delete(int CopyId, string test)
    {
      var joinEntry = _db.Copies.FirstOrDefault(entry => entry.CopyId == CopyId);
      Console.WriteLine("ddddelete post");
      _db.Copies.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}