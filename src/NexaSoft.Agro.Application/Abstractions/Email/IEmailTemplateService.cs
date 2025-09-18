using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Abstractions.Email;

public interface IEmailTemplateService
{
    string GenerateWelcomeEmail(string name, string organization);
    string GenerateImportantAnnouncement(string title, string content, string actionUrl = "");
    string GenerateReportNotification(string reportName, string downloadUrl);
    string GenerateMeetingInvitation(string meetingTitle, DateTime date, string location);
    string GeneratePasswordResetEmail(string name, string resetUrl);
    string GenerateProjectUpdate(string projectName, string updateMessage, string projectUrl);

    string GenerateCustomTemplate(TemplateData templateData);
}
