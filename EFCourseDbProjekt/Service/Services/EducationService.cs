using Domain.Models;
using Repository.DTOs.Education;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EducationService:IEducationService
    {
        private readonly IEducationRepository _educationRepository;

        public EducationService()
        {
            _educationRepository= new EducationRepository();
        }

        public async Task CreateAsync(Education entity)
        {
            await _educationRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(Education entity)
        {
            await _educationRepository.DeleteAsync(entity);
        }

        public async Task<List<Education>> GetAllAsync()
        {
            return await _educationRepository.GetAllAsync();
        }

        public async Task<Education> GetByIdAsync(int id)
        {
            return await _educationRepository.GetByIdAsync(id);
        }

        public async Task<List<EducationWithGroupsDto>> GetEducationWithGroupsAsync()
        {
            return await _educationRepository.GetAllWithGroupAsync();
        }

        public async Task<List<Education>> SearchByNameAsync(string str)
        {
            return await _educationRepository.SearchByNameAsync(str);
        }

        public async Task<List<Education>> SortWithCreatedDateAsync(string text)
        {
            return await _educationRepository.SortWithCreatedDateAsync(text);
        }

        public async Task UpdateAsync(Education entity)
        {
            await _educationRepository.UpdateAsync(entity);
        }
    }
}
