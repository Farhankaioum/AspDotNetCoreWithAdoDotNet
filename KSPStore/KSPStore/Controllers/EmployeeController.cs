using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSPStore.DataProvider;
using KSPStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace KSPStore.Controllers
{
    public class EmployeeController : Controller
    {
        private DababaseConfigurationProvider _db = new DababaseConfigurationProvider();

        // Get all employees
        public IActionResult Index()
        {
            var employees = _db.GetAll().ToList();
            return View(employees);
        }

        //Get employee via id
        public IActionResult Detail(int id)
        {
            var model = _db.GetById(id);
            return View(model);
        }

        //Create employee
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name,UserName,Address,Email,Password")]Employee  model)
        {
            if (ModelState.IsValid)
            {
                _db.Insert(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        //Create employee
        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = _db.GetById(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Update([Bind("Id,Name,UserName,Address,Email,Password")]Employee model)
        {
            if (ModelState.IsValid)
            {
                _db.Update(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // for delete 
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _db.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // testing purpose: Get value from two different table
        [HttpGet]
        public IActionResult GetValueFromDiffTable()
        {
            var model = _db.GetAllValueFromDifferentTable();
            return View(model);
        }
    }
}