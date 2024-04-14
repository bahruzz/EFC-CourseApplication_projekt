using Domain.Models;
using Repository.DTOs.Group;
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
    public class GroupService: IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService()
        {
            _groupRepository= new GroupRepository();
        }

        public async Task CreateAsync(Group entity)
        {
            await _groupRepository.CreateAsync(entity);
        }

     

        public async Task DeleteAsync(Group entity)
        {
            await _groupRepository.DeleteAsync(entity);
        }

        public async Task<List<Group>> GetAllAsync()
        {
            return await _groupRepository.GetAllAsync();
        }

        public async Task<List<GroupWithEducationDto>> GetAllWithEducationAsync()
        {
            return await _groupRepository.GetAllWithEducationAsync();
        }

        public async Task<Group> GetByIdAsync(int id)
        {
            return await _groupRepository.GetByIdAsync(id);
        }

        public async Task<List<Group>> GetGroupByEducationIdAsync(int id)
        {
            return await _groupRepository.GetGroupByEducationIdAsync(id);
        }

        public async Task<List<Group>> SearchByNameAsync(string str)
        {
            return await _groupRepository.SearchByNameAsync(str);
        }

        public async Task<List<Group>> SortWithCapacityAsync(string text)
        {
            return await _groupRepository.SortWithCapacityAsync(text);
        }

        public async Task UpdateAsync(Group entity)
        {
            await _groupRepository.UpdateAsync(entity);
        }

    }
}
