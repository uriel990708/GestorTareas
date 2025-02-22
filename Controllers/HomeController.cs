using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using GestorTareas.Data;
using GestorTareas.Models;
using Microsoft.EntityFrameworkCore; // Agregar esta referencia

namespace GestorTareas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddTask(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                var todoTask = new TodoTask
                {
                    Description = description,
                    CreatedDate = DateTime.Now,
                    IsCompleted = false
                };

                _context.TodoTasks.Add(todoTask);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Index
        {
            get
            {
                if (!User.Identity.IsAuthenticated)
                    return RedirectToAction("Login");

                return View(_context.TodoTasks.ToList());
            }
        }
    }
}