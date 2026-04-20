using Backend.Models;
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;
namespace Backend.Services.Interface{
    [ApiController]
    [Route("api/pbl3/[controller]")]
    public class ingredientController : ControllerBase {
        private readonly IIngredientService _ingredientSerive;
        public ingredientController (IIngredientService ingredientService){
            _ingredientSerive = ingredientService;
        }
        [Http("get-all")]
        public async Task<IActionResult> GetAll(){
            var Ingredients = await _ingredientSerive.GetAllIngredient();
            if (Ingredients == null) return NotFound ("Not Found");
        }
    }
}