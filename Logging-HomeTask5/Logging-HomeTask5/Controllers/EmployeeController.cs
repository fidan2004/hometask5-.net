using Logging_HomeTask5.Data.Context;
using Logging_HomeTask5.Data.Entities;
using Logging_HomeTask5.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Logging_HomeTask5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly AppDbContext _appDbContext;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(AppDbContext appDbContext, ILogger<EmployeeController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           _logger.LogInformation("Request accepted at {date}", DateTime.Now);
            var result =  _appDbContext.Employees;
            _logger.LogWarning("Request successfully completed at {Date}, result: {result}", DateTime.Now, JsonSerializer.Serialize(result));
            return Ok(result);

        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody]  EmployeeDto employeedto)
        {
            var watch = new System.Diagnostics.Stopwatch();
           
            watch.Start();
            if (ModelState.IsValid != true)
            {
                return BadRequest(ModelState);
            }

            Employee employee = new()
           { 
                Id=employeedto.Id,
                Name=employeedto.Name,
                BirthDate=employeedto.BirthDate,
                Salary=employeedto.Salary,
                Surname =employeedto.Surname,
                Position=employeedto.Position,
                IsManager = employeedto.IsManager
            
            };
            var emp =  _appDbContext.Employees.Add(employee);
            await _appDbContext.SaveChangesAsync();
             watch.Stop();
            _logger.LogInformation($"Execution Time: {watch.ElapsedMilliseconds} ms");
            return  Ok(emp);


        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            var existingEmployee = _appDbContext.Employees.FirstOrDefault(employee => employee.Id == id);
            existingEmployee.Name = employee.Name;
            existingEmployee.Surname = employee.Surname;
            await _appDbContext.SaveChangesAsync();
            return Ok(existingEmployee);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Request accepted to delete the student with id: {id}", id);
                var existingEmployee = _appDbContext.Employees.FirstOrDefault(employee => employee.Id == id);
                _logger.LogDebug("Student is fetched from database successfully", id);
                _appDbContext.Employees.Remove(existingEmployee);
                await _appDbContext.SaveChangesAsync();
                _logger.LogDebug("Student is deleted from db and transaction committed {id}", id);

                _logger.LogInformation("Request is successfully completed to delete the student: {id}", id);


                return Ok(existingEmployee);
            }
            catch(Exception exc)
            {
                _logger.LogError(exc, "Error occurred when deleting the student with id {id}", id);
                throw;

            }
        }
    }
}
