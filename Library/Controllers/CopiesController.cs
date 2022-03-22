// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc;
// using Library.Models;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using System.Threading.Tasks;
// using System.Security.Claims;

// namespace Library.Controllers
// {
//   [Authorize]
//   public class CopiesController : Controller
//   {
//     private readonly LibraryContext _db;
//     private readonly UserManager<ApplicationUser> _userManager;
//     public CopiesController(UserManager<ApplicationUser> userManager, LibraryContext db)
//     {
//       _userManager = userManager;
//       _db = db;
//     }
//     public async Task<ActionResult> Index()
//     {
//       var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//       var currentUser = await _userManager.FindByIdAsync(userId);
//       var userCopies = _db.Copies.Where(entry => entry.User.Id == currentUser.Id).ToList();
//       return View(userCopies);
//     }
    
//     public ActionResult Create()
//     {
//       return View();
//     }
    
//     [HttpPost]
//     public async Task<ActionResult> Create(Copy copy, int PatronId)
//     {
//       var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//       var currentUser = await _userManager.FindByIdAsync(userId);
//       copy.User = currentUser;
//       _db.Copies.Add(copy);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }
    
//     public ActionResult Details(int id)
//     {
//       var thisCopy = _db.Copies
//         .Include(a => a.JoinEntities)
//         .ThenInclude(join => join.Patron)
//         .FirstOrDefault(a => a.CopyId == id);
//       return View(thisCopy);
//     }
//     public ActionResult Edit(int id)
//     {
//       var thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
//       return View(thisCopy);
//     }
//     [HttpPost]
//     public ActionResult Edit(Copy copy)
//     {
//       _db.Entry(copy).State = EntityState.Modified;
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult Delete(int id)
//     {
//       var thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
//       return View(thisCopy);
      
//     }
    
//     [HttpPost, ActionName("Delete")]
//     public ActionResult DeleteConfirmed(int id)
//     {
//       var thisCopy = _db.Copies.FirstOrDefault(c => c.CopyId == id);
//       _db.Copies.Remove(thisCopy);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }
//   }
// }