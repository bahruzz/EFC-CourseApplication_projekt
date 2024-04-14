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

        public Task<List<Group>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupWithEducationDto>> GetAllWithEducationAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Group> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Group>> GetGroupByEducationIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Group>> SearchByNameAsync(string str)
        {
            throw new NotImplementedException();
        }

        public Task<List<Group>> SortWithCapacityAsync(string text)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Group entity)
        {
            throw new NotImplementedException();
        }

    }
}
