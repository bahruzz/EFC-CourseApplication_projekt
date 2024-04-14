
using EFCourseDbProjekt.Controllers;
using Service.Helpers.Extensions;
using Service.Helpers.Enums;

EducationController educationController = new EducationController();
GroupController groupController = new GroupController();    

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
            case (int)OperationType.EducationUpdate:
                await educationController.UpdateAsync();
                break;
            case (int)OperationType.EducationSearchByName:
                await educationController.SearchByNameAsync();
                break;
            case (int)OperationType.EducationSortWithCreatedDate:
                await educationController.SortWithCreatedDateAsync();
                break;
            case (int)OperationType.EducationGetAllWithGroup:
                await educationController.GetAllWithGroupAsync();
                break;
            case (int)OperationType.GroupCreate:
                await groupController.CreateAsync();
                break;
            case (int)OperationType.GroupGetAll:
                await groupController.GetAllAsync();
                break;
            case (int)OperationType.GroupDelete:
                await groupController.DeleteAsync();
                break;


















        }
    }
}
static void GetMenues()
{
    ConsoleColor.Cyan.WriteConsole("Choose one operation :  1-Education create, 2- EducationGetAll, 3-EducationDelete, 4-EducationGetById, 5-EducationUpdate,6-EducationSearchByName, 7-EducationSortWithCreatedDate,8-GetAllWithGroupAsync,9-GroupCreate,10-GroupGetAll,11-GroupDelete,12-SearchStudentByNameOrSurname,13-GetAllStudentByAge,14-GetAllStudentByGroupId");
}
