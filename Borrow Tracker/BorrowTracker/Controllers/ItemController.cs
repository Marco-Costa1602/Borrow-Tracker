using GroceryStore.Data;
using GroceryStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStore.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ItemController(ApplicationDbContext db)
        {
            _db = db;
        }


        //  GET INDEX
        public IActionResult Index()
        {
            IEnumerable<Item> objList = _db.Items; // Assign Database items to a list of <Items> that will later be used
            return View(objList);                  // in a foreach statement at the View part.
        }

        // GET CREATE
        public IActionResult Create()
        {

            return View(); // Returns the view to the create page.
        }

        // POST CREATE
        [HttpPost] // Set the method to be POST, so the user can send data.
        public IActionResult Create(Item obj) // Receive parameters from the View
        {
            _db.Items.Add(obj); // Add the "obj" parameter as an entry in the database
            _db.SaveChanges(); // Save the changes made in the db
            return RedirectToAction("Index"); // Returns to the list (index) page.
        }


        // GET DELETE
        public IActionResult Delete(int? id) // Get the row item id
        {
            if (id == null) return NotFound(); // Checks if Id exists

            var obj = _db.Items.Find(id); // Assign a Db item with the same Id as the parameter to an Obj
            if (obj == null) return NotFound(); // Check if obj is not null

            return View(obj); // Return the "obj" to the View page.
        }

        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Items.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Items.Remove(obj); // Deletes the obj from the db
            _db.SaveChanges();
            return RedirectToAction("Index"); // Redirect the user to the List(index)
        }


    }
}
