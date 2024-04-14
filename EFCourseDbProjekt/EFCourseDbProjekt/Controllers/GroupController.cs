using Domain.Models;
using Service.Helpers.Constants;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCourseDbProjekt.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;
        private readonly IEducationService _educationService;

        public GroupController()
        {
            _groupService=new GroupService();
            _educationService=new EducationService();
        }

        public async Task CreateAsync()
        {
            try
            {
                var edu = await _educationService.GetAllAsync();
                if (edu.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("First creat education");
                    return;
                }
            GroupName: ConsoleColor.Cyan.WriteConsole("Add Group Name");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty");
                    goto GroupName;
                }
                var gru = await _groupService.GetAllAsync();
                foreach (var item in gru)
                {
                    if (item.Name.ToLower() == name.ToLower().Trim())
                    {
                        ConsoleColor.Red.WriteConsole("Group name can't be same");
                        goto GroupName;
                    }
                }


                var response = await _educationService.GetAllAsync();
                foreach (var item in response)
                {

                    ConsoleColor.Cyan.WriteConsole("Id-" + item.Id + " Name-" + item.Name);
                }
            EducationName: Console.WriteLine("Choose Education ID ");
                string idStr = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(idStr))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty");
                    goto EducationName;
                }
                int id;
                bool isCorrectIdFormat = int.TryParse(idStr, out id);

                if (isCorrectIdFormat)
                {

                    Education education = await _educationService.GetByIdAsync(id);
                    if (education == null)
                    {
                        ConsoleColor.Red.WriteConsole("Education not found");
                        return;
                    }


                Capacity: ConsoleColor.Cyan.WriteConsole("Add Capacity");
                    string capacityStr = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(capacityStr))
                    {
                        ConsoleColor.Red.WriteConsole("Input can't be empty");
                        goto Capacity;
                    }
                    int capacity;
                    bool isCorrectCapacityFormat = int.TryParse(capacityStr, out capacity);
                    if (isCorrectCapacityFormat)
                    {
                        DateTime time = DateTime.Now;


                        await _groupService.CreateAsync(new Domain.Models.Group { Name = name.Trim().ToLower(), EducationId = education.Id, Capacity = capacity, CreatedDate = time });
                        ConsoleColor.Green.WriteConsole("Data succesfuly added");
                    }
                    else
                    {
                        ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                        goto Capacity;
                    }
                }
            }
            catch (Exception ex)
            {

                ConsoleColor.Red.WriteConsole(ex.Message);
               
            }
            
        }

        public async Task GetAllAsync()
        {
            var response = await _groupService.GetAllAsync();

            foreach (var item in response)
            {
                var education = await _educationService.GetByIdAsync(item.EducationId);
                Console.WriteLine("Group-" + item.Name + " Capacity-" + item.Capacity + " Education-" + education.Name + " CreatedDate-" + item.CreatedDate);
            }

        }

        public async Task DeleteAsync()
        {
            try
            {
                var data = await _groupService.GetAllAsync();
                foreach (var item in data)
                {
                    Console.WriteLine("Id-" + item.Id + " Name-" + item.Name + " CreatedDate-" + item.CreatedDate);
                }

            Id: ConsoleColor.Cyan.WriteConsole("Add id:");
                string idStr = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(idStr))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty");
                    goto Id;
                }
                int id;
                bool isCorrectIdFormat = int.TryParse(idStr, out id);
                if (isCorrectIdFormat)
                {
                    var response = await _groupService.GetByIdAsync(id);
                    if (response is null)
                    {
                        ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    }
                    else
                    {
                        _groupService.DeleteAsync(response);
                        ConsoleColor.Green.WriteConsole("Data succesfully deleted");
                    }

                }
                else
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                    goto Id;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }


        }


    }
}
