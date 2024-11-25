using GetUrCourse.Services.CourseAPI.Core.Enums;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class StudentSubscription
{
    private StudentSubscription() { }
    public StudentSubscription(Guid studentId, int subscriptionId, DateTime endDate)
    {
        RemainingTime = null;
        StudentId = studentId;
        SubscriptionId = subscriptionId;
        StartDate = DateTime.UtcNow;
        EndDate = endDate;
        Status = SubscriptionStatus.Active;

    }
    public Guid StudentId { get; set; } 
    public int SubscriptionId { get; set; } 

    public DateTime StartDate { get; set; } 
    public DateTime? EndDate { get; set; }
    
    public TimeSpan? RemainingTime { get; private set; }
    public SubscriptionStatus Status { get; set; }
    
    public Student Student { get; set; } = null!;
    public Subscription Subscription { get; set; } = null!;
    
    public static Result<StudentSubscription> Create(Guid studentId, int subscriptionId, int? months)
    {
        var endDate = months.HasValue ? DateTime.UtcNow.AddMonths(months.Value) : DateTime.MaxValue;
        
        return new StudentSubscription(studentId, subscriptionId, endDate);
    }
    
    public void Suspend()
    {
        if (EndDate != null)
        {
            RemainingTime = EndDate - DateTime.UtcNow;
        }
        Status = SubscriptionStatus.Suspended;
        Result.Success();
    }
    
    public Result Activate()
    {
        RemainingTime = null;
        Status = SubscriptionStatus.Active;
        return Result.Success();
    }
    
    public Result Expire()
    {   
        if (EndDate != null)
        {
            return Result.Failure(new Error("expire_subscription_", "Subscription has already expired"));
        }
        RemainingTime = TimeSpan.Zero;
        Status = SubscriptionStatus.Expired;
        return Result.Success();
    }
    
    public Result<TimeSpan> GetRemainingTime()
    {
        if (EndDate is null)
        {
            return Result.Failure<TimeSpan>(new Error("get_remaining_time_", "Subscription is permanent"));
        }
        
        if (Status is SubscriptionStatus.Expired || EndDate < DateTime.UtcNow)
        {
            return Result.Failure<TimeSpan>(new Error("get_remaining_time_", "Subscription has expired"));
        }
        
        var resultEndDate = (EndDate - DateTime.UtcNow) ?? TimeSpan.Zero;
        return Result.Success(resultEndDate);
    }
    
    
}