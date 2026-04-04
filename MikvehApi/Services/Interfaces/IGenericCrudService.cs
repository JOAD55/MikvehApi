using System;

namespace MikvehApi.Services.Interfaces;

public interface IGenericCrudService<TDto, TCreateDto, TUpdateDto>
{
    Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TCreateDto dto);
        Task<TDto?> UpdateAsync(int id, TUpdateDto dto);
        Task<bool> DeleteAsync(int id);
}
