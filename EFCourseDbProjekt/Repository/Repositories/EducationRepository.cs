using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.DTOs.Education;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EducationRepository:BaseRepository<Education>,IEducationRepository
    {
        private readonly AppDbContext _appDbContext;

        public EducationRepository()
        {
            _appDbContext = new AppDbContext();
        }

        public async Task<List<EducationWithGroupsDto>> GetAllWithGroupAsync()
        {
            var education = await _appDbContext.Educations.Include(m => m.Groups).ToListAsync();
            var datas = education.Select(m => new EducationWithGroupsDto
            {
                Education = m.Name,
                Groups = m.Groups.Select(m => m.Name).ToList(),
            });
            return datas.ToList();
        }

        public async Task<List<Education>> SearchByNameAsync(string str)
        {
           return await _appDbContext.Educations.Where(m => m.Name.ToLower().Trim().Contains(str.ToLower().Trim())).ToListAsync();
             
        }

        public async Task<List<Education>> SortWithCreatedDateAsync(string text)
        {
            if (text.ToLower().Trim() == "asc")
            {
                return await _appDbContext.Educations.OrderBy(m => m.CreatedDate).ToListAsync();
            }
            else if (text.ToLower().Trim() == "desc")
            {
                return await _appDbContext.Educations.OrderByDescending(m => m.CreatedDate).ToListAsync();
            }
            else
            {
                throw new Exception("Incorrect Operation");
            }
        }

        public async Task<Education> GetByNameAsync(string name)
        {
            return await _appDbContext.Educations.FirstOrDefaultAsync(m => m.Name == name);
        }
    }
}
