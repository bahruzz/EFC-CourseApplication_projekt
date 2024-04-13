using Domain.Models;
using Repository.DTOs.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
   public interface IEducationRepository:IBaseRepository<Education>
    {
        Task<List<Education>> SearchByNameAsync(string str);
       
        Task<List<Education>> SortWithCreatedDateAsync(string text);

        Task<List<EducationWithGroupsDto>> GetAllWithGroupAsync();
    }
}
