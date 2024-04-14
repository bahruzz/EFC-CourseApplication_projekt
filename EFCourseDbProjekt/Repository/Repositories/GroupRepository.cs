using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.DTOs.Group;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GroupRepository:BaseRepository<Group>,IGroupRepository
    {
        private readonly AppDbContext _appDbContext;

        public GroupRepository()
        {
            _appDbContext = new AppDbContext();
        }

        public async Task<List<GroupWithEducationDto>> GetAllWithEducationAsync()
        {
            var groups = await _appDbContext.Groups.Include(m => m.Education).ToListAsync();
            var datas = groups.Select(m => new GroupWithEducationDto
            {
                Group = m.Name,
                Education = m.Education.Name
            });
            return datas.ToList();
        }

        public async Task<List<Group>> GetGroupByEducationIdAsync(int id)
        {
            return await _appDbContext.Groups.Where(m=>m.EducationId == id).ToListAsync();
        }

        public async Task<List<Group>> SearchByNameAsync(string str)
        {
            return await _appDbContext.Groups.Where(m => m.Name.ToLower().Trim().Contains(str.ToLower().Trim())).ToListAsync();
        }

        public async Task<List<Group>> SortWithCapacityAsync(string text)
        {
            if (text.ToLower().Trim() == "asc")
            {
                return await _appDbContext.Groups.OrderBy(m => m.Capacity).ToListAsync();
            }
            else if (text.ToLower().Trim() == "desc")
            {
                return await _appDbContext.Groups.OrderByDescending(m => m.Capacity).ToListAsync();
            }
            else
            {
                throw new Exception("Incorrect Operation");
            }
        }

        public async Task<Group> GetByNameAsync(string name)
        {
            return await _appDbContext.Groups.FirstOrDefaultAsync(m => m.Name == name);
        }
    }

}
