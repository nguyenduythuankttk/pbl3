using Backend.Data;
using Backend.Models;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Backend.Models.DTOs.Request;
using Backend.Models.DTOs.Reponse;
namespace Backend.Services.Implementations{
    public class DiningTableService : IDiningTableService{
        private readonly AppDbContext _dbcontext;
        public DiningTableService (AppDbContext dbContext){
            _dbcontext = dbContext;
        }
        public async Task<TableListResponse?> GetAllTablesAtStore(int storeID) {
            var tables = await _dbcontext.DiningTable
                    .Include(t => t.Store)
                    .Include(t => t.Reservation)
                    .Where(t => t.StoreID == storeID)
                    .ToListAsync();

            if (!tables.Any()) return null;

            return new TableListResponse {
                StoreID = storeID,
                TotalTables = tables.Count,
                AvailableTables  = tables.Count(t => t.Status == TableStatus.Available),
                Tables = tables.Select(t => new DiningTableResponse {
                    TableID = t.TableID,
                    TableNumber = t.TableNumber,
                    Capacity = t.Capacity,
                    Status = t.Status.ToString(),
                    StorePhone = t.Store.Phone,
                    StoreEmail = t.Store.Email,
                    PendingReservations = t.Reservation.Count(r => r.Status == ReservationStatus.Pending)
                }).ToList()
            };
        }

        public async Task<DiningTableResponse?> GetTableByID(int tableID) {
            var table = await _dbcontext.DiningTable
                    .Include(t => t.Store)
                    .Include(t => t.Reservation)
                    .FirstOrDefaultAsync(t => t.TableID == tableID);

            if (table == null) return null;

            return new DiningTableResponse {
                TableID = table.TableID,
                TableNumber = table.TableNumber,
                Capacity = table.Capacity,
                Status = table.Status.ToString(),
                StorePhone = table.Store.Phone,
                StoreEmail = table.Store.Email,
                PendingReservations = table.Reservation.Count(r => r.Status == ReservationStatus.Pending)
            };
        }
        public async Task AddTable (DiningTable table){
            try {
                _dbcontext.DiningTable.Add(table);
                await _dbcontext.SaveChangesAsync();
            }catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
        public async Task UpdateTable (int tableID, int capacity){
            try{
                var table = await _dbcontext.DiningTable.FirstOrDefaultAsync(t => t.TableID == tableID);
                if (table!=null){
                    table.Capacity = capacity;
                    _dbcontext.DiningTable.Update(table);
                    await _dbcontext.SaveChangesAsync();
                }
            }catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
        public async Task SoftDeleteTable (int tableID){
            var table = await _dbcontext.DiningTable
                .FirstOrDefaultAsync(t => t.TableID == tableID &&
                                    t.DeletedAt == null);
            
            if(table == null){
                throw new Exception("Table not found");
            }

            try{
                table.DeletedAt = DateTime.Now;
                await _dbcontext.SaveChangesAsync();
            }catch(Exception ex){
                Console.WriteLine($"Soft delete table error {ex.Message}");
                throw new Exception($"An error occurred while soft deleting table: {ex.Message}");
            }
        }
    }
}