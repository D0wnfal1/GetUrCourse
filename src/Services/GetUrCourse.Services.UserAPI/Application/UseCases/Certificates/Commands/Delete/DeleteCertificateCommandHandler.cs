using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Commands.Delete;

public class DeleteCertificateCommandHandler(UserDbContext context) : ICommandHandler<DeleteCertificateCommand>
{
    public async Task<Result> Handle(DeleteCertificateCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.Certificates
                .Where(u => u.StudentId == request.Id)
                .ExecuteDeleteAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(new Error("delete_certificate",e.Message));
        }
        
        return Result.Success();
    }
}