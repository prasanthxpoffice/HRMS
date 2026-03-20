using HRMS.Models;

namespace HRMS.Services
{
    public interface ICrudService<T> where T : class
    {
        Task<List<T>> GetAllAsync(object? parameters = null);
        Task<T?> GetByIdAsync(object id);
        Task<ApiResponse<object>> SaveAsync(T item);
        Task<ApiResponse<object>> DeleteAsync(object id);
    }
}
