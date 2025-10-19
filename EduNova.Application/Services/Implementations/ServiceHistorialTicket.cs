using AutoMapper;
using EduNova.Application.DTOs;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Repository.Implementations;
using EduNova.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Implementations
{
    public class ServiceHistorialTicket : IServiceHistorialTicket
    {

        private readonly IRepositoryHistorialTicket _repository;
        private readonly IMapper _mapper;
        private readonly eduNovaContext _context;
        public ServiceHistorialTicket(IRepositoryHistorialTicket repositoryHistorialTicket, IMapper mapper, eduNovaContext eduNovaContext)
        {
            _repository = repositoryHistorialTicket;
            _mapper = mapper;
            _context = eduNovaContext;
        }
        public Task<HistorialTicketDTO> CreateHistorialTicket(HistorialTicketDTO historialTicketDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteHistorialTicket(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<HistorialTicketDTO>> GetAllHistorialTickets()
        {
           var historialTickets = await _repository.GetAllAsync();
            return _mapper.Map<ICollection<HistorialTicketDTO>>(historialTickets);
        }

        public Task<HistorialTicketDTO> GetHistorialTicketById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HistorialTicketDTO> UpdateHistorialTicket(int id, HistorialTicketDTO historialTicketDto)
        {
            throw new NotImplementedException();
        }
    }
}
