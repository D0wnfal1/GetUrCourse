using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Reviews.Queries.GetById;

public record GetReviewByIdQuery(Guid Id) : IQuery<ReviewResponse>;
