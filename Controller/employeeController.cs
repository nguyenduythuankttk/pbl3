using Backend.Models;
using Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller {
    [ApiController]
    [Route("api/pbl3/[controller]")]
    public class employeeController : ControllerBase {
        private readonly IEmployeeService _employeeService;

        public employeeController(IEmployeeService employeeService) {
            _employeeService = employeeService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll() {
            try {
                var employees = await _employeeService.GetAllEmployee();
                if (employees == null || employees.Count == 0) return NotFound("Không có nhân viên nào");
                return Ok(employees);
            } catch (Exception e) {
                return StatusCode(500, "Error in employeeController.GetAll: " + e.Message);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetByID(Guid id) {
            try {
                var employee = await _employeeService.GetEmployeeByID(id);
                if (employee == null) return NotFound("Không tìm thấy nhân viên");
                return Ok(employee);
            } catch (Exception e) {
                return StatusCode(500, "Error in employeeController.GetByID: " + e.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Add([FromBody] Employee newEmployee) {
            try {
                await _employeeService.AddEmployee(newEmployee);
                return Ok("Thêm nhân viên thành công");
            } catch (Exception e) {
                return StatusCode(500, "Error in employeeController.Add: " + e.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> SoftDelete(Guid id) {
            try {
                await _employeeService.SoftDeleteEmployee(id);
                return Ok("Xóa nhân viên thành công");
            } catch (Exception e) {
                return StatusCode(500, "Error in employeeController.SoftDelete: " + e.Message);
            }
        }
    }
}
