using HRMS.Models;
using System.Reflection;

namespace HRMS.Services
{
    public class CrudService<T> : ICrudService<T> where T : class
    {
        private readonly IDatabaseService _db;
        private readonly string? _getProc;
        private readonly string? _saveProc;
        private readonly string? _deleteProc;

        public CrudService(IDatabaseService db)
        {
            _db = db;
            var attr = typeof(T).GetCustomAttribute<StoredProcedureAttribute>();
            _getProc = attr?.Get;
            _saveProc = attr?.Save;
            _deleteProc = attr?.Delete;
        }

        public async Task<List<T>> GetAllAsync(object? parameters = null)
        {
            if (string.IsNullOrEmpty(_getProc)) return new();
            var response = await _db.ExecuteQueryAsync<List<T>>(_getProc, parameters);
            return response?.Data ?? new();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            if (string.IsNullOrEmpty(_getProc)) return null;
            var response = await _db.ExecuteQueryAsync<List<T>>(_getProc, new { Id = id });
            return response?.Data?.FirstOrDefault();
        }

        public async Task<ApiResponse<object>> SaveAsync(T item)
        {
            if (string.IsNullOrEmpty(_saveProc)) 
                return new ApiResponse<object> { Success = false, Message = "Save procedure not defined." };
            
            return await _db.ExecuteQueryAsync<object>(_saveProc, item);
        }

        public async Task<ApiResponse<object>> DeleteAsync(object id)
        {
            if (string.IsNullOrEmpty(_deleteProc)) 
                return new ApiResponse<object> { Success = false, Message = "Delete procedure not defined." };

            return await _db.ExecuteQueryAsync<object>(_deleteProc, new { Id = id });
        }
    }
}
