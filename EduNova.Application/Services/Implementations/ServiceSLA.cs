using AutoMapper;
using EduNova.Application.DTOs;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Models;
using EduNova.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Implementations
{
    public class ServiceSLA : IServiceSLA
    {

        private readonly IRepositorySLA _repository;
        private readonly IMapper _mapper;

        public ServiceSLA(IRepositorySLA repositorySLA, IMapper mapper)
        {
            _repository= repositorySLA ;
            _mapper = mapper;
        }
        

     public  async  Task<SLADTO> FindByIdAsync(int id)
        {
            var @object =  await _repository.FindByIdAsync(id);
               return _mapper.Map<SLADTO>(@object);


        }

     public async   Task<ICollection<SLADTO>> ListAsync()
        {
          var collection = await _repository.ListAsync();
            return _mapper.Map<ICollection<SLADTO>>(collection);
        }
    }
}
