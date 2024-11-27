using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Commands.Create;

public record CreateCertificateCommand(
    string Title, 
    string Description, 
    string PdfUrl, 
    Guid StudentId) : ICommand;
