using EduNova.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Interfaces
{
    public interface IServiceSLA
    {
   Task<ICollection<SLADTO>> ListAsync();
        Task<SLADTO> FindByIdAsync(int id);
    }
}
