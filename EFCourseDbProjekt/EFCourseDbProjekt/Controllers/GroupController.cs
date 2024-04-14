using Domain.Models;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
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
                Console.WriteLine("Group id-"  +  item.Id +" " +  "Group name-"  +  item.Name +" " +"Capacity-" + item.Capacity +" "+ "Education-" + education.Name + " CreatedDate-" + item.CreatedDate);
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

        public async Task GetByIdAsync()

        {
            try
            {
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
                        Console.WriteLine("Group name-" + response.Name +" " + "Capacity-" + response.Capacity + " " + " CreatedDate-" + response.CreatedDate);
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

        public async Task UpdateAsync()
        {
            var datas = await _groupService.GetAllAsync();
            foreach (var item in datas)
            {
                Console.WriteLine("Group Id-" + item.Id + "Group Name-" + item.Name + " Education-" + item.Education + " Capacity-" + item.Capacity + " CreatedDate-" + item.CreatedDate);
            }
            bool update = true;
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
                try
                {
                    var data = _groupService.GetByIdAsync(id);
                    
                    if (data.Result is null)
                    {
                        throw new NotFoundException(ResponseMessages.DataNotFound);
                        
                    }
                Group: ConsoleColor.Cyan.WriteConsole("Add Group name ");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        var response = await _groupService.SearchByNameAsync(newName);
                        if (response.Count == 0)
                        {
                            if (data.Result.Name.ToLower().Trim() != newName.ToLower().Trim())
                            {
                                data.Result.Name = newName;
                                update = false;
                            }
                        }
                        else
                        {
                            ConsoleColor.Red.WriteConsole("This group is exist");
                            goto Group;
                        }

                    }
                Education: ConsoleColor.Cyan.WriteConsole("Add Education");
                    string newEducation = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newEducation))
                    {
                        var edu = await _educationService.SearchByNameAsync(newEducation);
                        if (edu.Count != 0)
                        {
                            if (data.Result.Education.Name.ToLower().Trim() != newEducation.ToLower().Trim())
                            {
                                data.Result.Education.Name = newEducation;
                                update = false;
                            }
                        }

                        else
                        {
                            ConsoleColor.Red.WriteConsole("This Education is not Exist");
                            goto Education;
                        }

                    }
                    ConsoleColor.Cyan.WriteConsole("Add capacity");
                    string capacityStr = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(capacityStr))
                    {
                        int capacity;
                        bool isCorrectCapacityFormat = int.TryParse(capacityStr, out capacity);
                        if (isCorrectCapacityFormat)
                        {

                            if (data.Result.Capacity != capacity)
                            {
                                data.Result.Capacity = capacity;
                                update = false;
                            }

                        }
                    }

               
                    else
                    {
                      
                        _groupService.UpdateAsync(data.Result);
                        ConsoleColor.Green.WriteConsole("Data update succes");

                       
                    }

                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                   
                    goto Id;
                }

            }
            else
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                goto Id;
            }
        }

        public async Task SearchByNameAsync()
        {
        Str: ConsoleColor.Cyan.WriteConsole("Enter str");
            string str = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(str))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Str;
            }
            var data = await _groupService.SearchByNameAsync(str);
            if (data.Count == 0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                goto Str;
            }
            foreach (var item in data)
            {
                var education = await _educationService.GetByIdAsync(item.EducationId);
                Console.WriteLine("Group-" + item.Name + " Education-" + education.Name + " Capacity-" + item.Capacity + " CreatedDate-" + item.CreatedDate);
            }
        }
        public async Task GetAllGroupWithEducationIdAsync()
        {
            var edu = await _educationService.GetAllAsync();
            foreach (var item in edu)
            {
                Console.WriteLine("Id-" + item.Id + " Name-" + item.Name);
            }
        Id: ConsoleColor.Cyan.WriteConsole("Add Id");
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
                var response = await _groupService.GetGroupByEducationIdAsync(id);
                foreach (var item in response)
                {
                    string result = $"Name-{item.Name} CreatedDate+{item.CreatedDate}";
                    Console.WriteLine(result);
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                goto Id;
            }

        }

        public async Task SortWithCapacityAsync()
        {
            try
            {
            Choose: ConsoleColor.Cyan.WriteConsole("Choose Sort Type\n Asc or Desc");
                string text = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(text))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty");
                    goto Choose;
                }
                var datas = await _groupService.SortWithCapacityAsync(text);
                foreach (var data in datas)
                {
                    Console.WriteLine("Name-" + data.Name + " CreateDate-" + data.CreatedDate);
                }
            }
            catch (Exception ex)
            {

                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task FilterByEducationNameAsync()
        {
            var datas = await _educationService.GetAllAsync();
            foreach (var data in datas)
            {
                Console.WriteLine("Id-" + data.Id + " Education-" + data.Name);
            }
        Name: ConsoleColor.Cyan.WriteConsole("Add Education Name");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Name;
            }
            var response = await _educationService.GetByNameAsync(name);

            if (response is null)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                goto Name;
            }


            var groups = await _groupService.GetGroupByEducationIdAsync(response.Id);
            if (groups != null)
            {
                foreach (var item in groups)
                {
                    Console.WriteLine("Group-" + item.Name + " Capacity-" + item.Capacity + " CreatedDate-" + item.CreatedDate);

                }
            }

            else
            {
                ConsoleColor.Red.WriteConsole("Incorrect Education Name");
                goto Name;
            }
        }



    }
}
