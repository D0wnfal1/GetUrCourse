using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Commands.Delete;

public record DeleteCertificateCommand(Guid Id) : ICommand;
