using CSharpFunctionalExtensions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Core.ValueObjects;

namespace GetUrCourse.Services.CourseAPI.Infrastructure.Data;

public class RatingEntity
{

    public Guid Id { get;  set; }
    public double RatingNumber { get; set; }
    
    public Guid FilmId { get; set; }
    public Course Course { get; set; }
    
    public Guid UserId { get; set; }
    public Student Student { get; set; }
    
    public DateTime Date { get; set; }

}
