using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Contracts;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Queries.GetCourse.Public;

public class GetCoursePublicDetailsQueryHandler: IQueryHandler<GetCoursePublicDetailsQuery, CourseResponse>
{
    public Task<Result<CourseResponse>> Handle(GetCoursePublicDetailsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}