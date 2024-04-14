using Domain.Models;
using Service.Helpers.Constants;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EFCourseDbProjekt.Controllers
{
    public class EducationController
    {
        private readonly IEducationService _educationService;
        private readonly GroupService _groupService;
        public EducationController()
        {
            _educationService = new EducationService();
            _groupService = new GroupService();
        }

        public async Task CreateAsync()
        {
        Education: ConsoleColor.Cyan.WriteConsole("Add Education Name");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");

                goto Education;

            }
            if (!Regex.IsMatch(name, @"^[\p{L}\p{M}' \.\-]+$"))
            {
                ConsoleColor.Red.WriteConsole("Format is wrong");
                goto Education;
            }
            var result=await _educationService.GetAllAsync();
            if(result.Any(m=>m.Name.ToLower() == name.ToLower()))
            {
                ConsoleColor.Red.WriteConsole("Education is already exist");

                goto Education;
            }
           Color: ConsoleColor.Cyan.WriteConsole("Add Color");
            string color = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(color))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");

                goto Color;

            }
            if (!Regex.IsMatch(color, @"^[\p{L}\p{M}' \.\-]+$"))
            {
                ConsoleColor.Red.WriteConsole("Color format is wrong");
                goto Color;
            }
            var data = await _educationService.GetAllAsync();
            if (data.Any(m => m.Color.ToLower() == color.ToLower()))
            {
                ConsoleColor.Red.WriteConsole("This color already exist");

                goto Color;
            }
            else
            {
                try
                {
                    DateTime time = DateTime.Now;
                    await _educationService.CreateAsync(new Education { Name = name.Trim().ToLower(), Color = color.Trim().ToLower(), CreatedDate = time });
                    ConsoleColor.Green.WriteConsole("Data succesfuly added");
                }
                catch (Exception ex)
                {

                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto Color;
                }
            }
        }

        public async Task GetAllAsync()
        {
            var response = await _educationService.GetAllAsync();
            if (response.Count == 0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                return;
            }
            else
            {


                foreach (var item in response)
                {
                    string data = $"Education id : {item.Id},  Education name : {item.Name}     ,     Education color : {item.Color}    ,     CreatedDate:{item.CreatedDate}";
                    Console.WriteLine(data);
                }
            }
        }

        public async Task DeleteAsync()
        {
            try
            {
                var data = await _educationService.GetAllAsync();
                foreach (var item in data)
                {
                    Console.WriteLine("Id-" + item.Id + " Name-" + item.Name + " CreatedDate-" + item.CreatedDate);
                }

            Id: Console.WriteLine("Add id:");
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
                    var response = await _educationService.GetByIdAsync(id);
                    if (response is null)
                    {
                        ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    }
                    else
                    {
                        _educationService.DeleteAsync(response);
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
            Id: Console.WriteLine("Add id:");
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
                    var response = await _educationService.GetByIdAsync(id);
                    if (response is null)
                    {
                        ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    }
                    else
                    {
                        Console.WriteLine("Education-" + response.Name + " Color-" + response.Color + " CreatedDate-" + response.CreatedDate);
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
