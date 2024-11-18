using GetUrCourse.Services.UserAPI.Core.Enums;
using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Core.Models;

public class Manager 
{
    private Manager() { }
    private Manager(Guid userId, Department department)
    {
        UserId = userId;
        Department = department;
    }
    public Guid UserId { get; private set; }
    public Department Department { get; private set; }
    
    public virtual User User { get; init; }
    
    public static Result<Manager> Create(Guid userId, Department department)
    {
        return Result.Success(new Manager(userId, department));
    }
    
    public void UpdateDepartment(Department department)
    {
        Department = department;
    }
    
    
}