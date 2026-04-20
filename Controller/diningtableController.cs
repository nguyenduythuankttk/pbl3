using Backend.Models;
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;
namespace Backend.Services.Interface{
    [ApiController]
    [Route("api/pbl3/[controller]")]
    public class diningTableController : ControllerBase{
        private readonly IDiningTableService _diningTable;
        public diningTableController (IDiningTableService diningTable){
            _diningTable = diningTableService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(int storeID){
            try{
                var tables = await _diningTable.GetAllTablesAtStore(storeID);
                if (tables == null) {
                    return NotFound("Not found table in " + storeID);
                }
                return OK(tables);
            } catch (Exception e){
                return StatusCode(500, "Error in diningtableController.GetAll: "+ e.Message);
            }
        }
        [HttpGet("get/{tableID}")]
        public async Task<IActionResult> GetByID (int tableID){
            try {
                var table = await _diningTable.GetTableByID(tableID);
                if (table == null) return NotFound("Not found Table");
                return OK(table);
            } catch (Exception e){
                return StatusCode(500, "Error in diningtableController.GetByID: " +e.Message );
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> Add(DiningTable newTable){
            try {
                await _diningTable.AddTable(newTable);
                return OK("Create Table Successfully");
            } catch (Exception e){
                return StatusCode(500, "Error in diningtableController.Add: " +e.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(int tableID, int capacity){
            try{
                await _diningTable.UpdateTable(tableID, capacity);
                return OK("Update Table Successfully");
            } catch (Exception e){
                return StatusCode(500, "Error in diningtableController.Update" + e.Message);
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int tableID){
            try {
                await _diningTable.DeleteTable(tableID);
                return OK("Delete Table Successfully");
            } catch (Exception e){
                return StatusCode(500, "Error in diningtableController.Delete" + e.Message);
            }
        }
    }
}