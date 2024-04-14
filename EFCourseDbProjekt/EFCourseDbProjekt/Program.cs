
using EFCourseDbProjekt.Controllers;
using Service.Helpers.Extensions;
using Service.Helpers.Enums;

EducationController educationController = new EducationController();

while (true)
{
    GetMenues();
Operation: string operationStr = Console.ReadLine();
    int operation;
    bool isCorrectOperationFormat = int.TryParse(operationStr, out operation);
    if (isCorrectOperationFormat)
    {
        switch (operation)
        {
            case (int)OperationType.EducationCreate:
                await educationController.CreateAsync();
                break;
            case (int)OperationType.EducationGetAll:
                await educationController.GetAllAsync();
                break;
            case (int)OperationType.EducationDelete:
                await educationController.DeleteAsync();
                break;
            case (int)OperationType.EducationGetById:
                await educationController.GetByIdAsync();
                break;


















        }
    }
}
static void GetMenues()
{
    ConsoleColor.Cyan.WriteConsole("Choose one operation :  1-Education create, 2- EducationGetAll, 3-EducationDelete, 4-EducationGetById, 5-GetAllByRoom,6-GetAllByTeacher, 7-SearchGroupByName,8-Student create,9-StudentGetAll,10-Student delete,11-GetStudentById,12-SearchStudentByNameOrSurname,13-GetAllStudentByAge,14-GetAllStudentByGroupId");
}
