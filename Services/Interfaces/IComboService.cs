using Backend.Models;
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;
namespace Backend.Services.Interface{
    public interface IComboService{
        
        Task <List<Combo>?> GetAllCombo();
        Task <List<Combo>?> GetAllComboIsActive();
        Task <Combo?> GetComboByID(int comboID);
        Task UpdateCombo (ComboRequest combo);
        Task AddCombo(Combo newCombo);
        
    }
}