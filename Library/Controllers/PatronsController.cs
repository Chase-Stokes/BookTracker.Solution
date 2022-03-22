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
    // public async Task<ActionResult> Index()
    // {
    //     var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //     var currentUser = await _userManager.FindByIdAsync(userId);
    //     var userItems = _db.Patrons.Where(entry => entry.User.Id == currentUser.Id).ToList();
    //     return View(userItems);
    // }

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
        ViewBag.Copies = _db.Copies
        .Include(c => c.Book)
        .ToList();
        Patron thisPatron = _db.Patrons
        .Include(patron => patron.JoinEntitiesOne)
        // .ThenInclude(join => join.Copy)
        .ThenInclude(join => join.Book)
        .FirstOrDefault(patron => patron.PatronId == id);
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

    [HttpPost]
    public ActionResult CheckOut(int CopyId, int PatronId)
    {
      Copy target = _db.Copies.FirstOrDefault(copy => copy.CopyId == CopyId);
      target.PatronId = PatronId;
      target.Checkout = true;
      _db.Entry(target).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = PatronId});
    }

    [HttpPost]
    public ActionResult CheckIn(int CopyId)
    {
      Copy target = _db.Copies.FirstOrDefault(copy => copy.CopyId == CopyId);
      int patronId = target.PatronId;
      target.Checkout = false;
      _db.Entry(target).State = EntityState.Modified;
      _db.SaveChanges(); 
      return RedirectToAction("Details", new {id = patronId});
    }

    // public ActionResult AddCopy(int id)
    // {
    //     var thisPatron = _db.Patrons.FirstOrDefault(patron => patron.PatronId == id);
    //     ViewBag.CopyId = new SelectList(_db.Copies, "CopyId", "Name");
    //     return View(thisPatron);
    // }

    // [HttpPost]
    // public ActionResult AddCopy(Patron patron, int CopyId)
    // {
    //     if (CopyId != 0)
    //     {
    //       _db.Checkout.Add(new Checkout() { CopyId = CopyId, PatronId = patron.PatronId });
    //       _db.SaveChanges();
    //     }
    //     return RedirectToAction("Index");
    // }

    public ActionResult Delete(int id)
    {
        var thisPatron = _db.Patrons.FirstOrDefault(p => p.PatronId == id);
        return View(thisPatron);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        var thisPatron = _db.Patrons.FirstOrDefault(p => p.PatronId == id);
        _db.Patrons.Remove(thisPatron);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    // [HttpPost]
    // public ActionResult DeleteCopy(int joinId)
    // {
    //     var joinEntry = _db.Checkout.FirstOrDefault(entry => entry.CheckoutId == joinId);
    //     _db.Checkout.Remove(joinEntry);
    //     _db.SaveChanges();
    //     return RedirectToAction("Index");
    // }
  }
}