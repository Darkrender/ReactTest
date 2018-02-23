﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactTest.Models;

namespace ReactTest.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;

            if (_context.Employees.Count() == 0)
            {
                _context.Employees.Add(new Employee
                {
                    HireDate = new DateTime(),
                    IsSalaried = true,
                    Name = "Jimmie Gisclair",
                    PayFrequency = 2,
                    Wage = 45000
                });
                _context.SaveChanges();
            }
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _context.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(string id)
        {
            var model = _context.Employees.FirstOrDefaultAsync(employee => employee.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(Employee employee)
        {
            var model = _context.Employees.Update(employee);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var model = _context.Employees.FirstOrDefault(employee => employee.Id == id);

            if (model != null)
            {
                var result = _context.Employees.Remove(model);

                if (result != null)
                {
                    return Ok();
                }
            }

            return NotFound();
        }
    }
}
