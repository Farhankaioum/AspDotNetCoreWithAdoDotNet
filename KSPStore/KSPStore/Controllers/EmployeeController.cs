using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSPStore.DataProvider;
using KSPStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace KSPStore.Controllers
{
    public class EmployeeController : Controller
    {
        private DababaseConfigurationProvider _db = new DababaseConfigurationProvider();
        private readonly IMemoryCache _cache;

        public EmployeeController(IMemoryCache cache)
        {
            _cache = cache;

            DababaseConfigurationProvider.cache = _cache;
        }

        
        // Get all employees
        public IActionResult Index()
        {
            var employees = _db.GetAll().ToList();
            return View(employees);
        }

        // For testing purpose IMemoryCache
        public IActionResult Index1()
        {
            IEnumerable<Employee> model;
            if (_cache.Get("GetAllEmp") == null)
            {
                model = _db.GetAll();
                _cache.Set("GetAllEmp", model);
                ViewBag.Msg = "Data Loaded from Database";
            }
            else
            {
                model = (IEnumerable<Employee>)_cache.Get("GetAllEmp");
                ViewBag.Msg = "Data Loaded from IMemoryCache";
            }
            return View(model);
        }

        // For testing purpose SqlCommandBuilder
        public IActionResult GetEmpSearchAndUpdate(int? id)
        {
            var model = _db.LoadEmpById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult GetEmpSearchAndUpdateAutoUpdated(Employee model)
        {
            if (model == null)
            {
                return View(model);
            }
            _db.UpdateEmpTesting(model);
            return RedirectToAction(nameof(GetEmpSearchAndUpdate), new { id = model.Id});
        }




        //Get employee via id
        public IActionResult Detail(int id)
        {
            var model = _db.GetByIdTesting(id);
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