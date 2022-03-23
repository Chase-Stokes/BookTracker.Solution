using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers
{
  public class HomeController : Controller
  {
    public readonly LibraryContext _db;
    public HomeController(LibraryContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Search(string Search)
    {
      var authors = _db.Authors.Where(author => (author.Name.Contains(Search) || (author.Name == Search))).ToList();
      ViewBag.Authors = authors;
      return View();
    }

    public ActionResult Heh()
    {
      return View();
    }
  }
}