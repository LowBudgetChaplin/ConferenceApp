using ConferenceAPI.Models;

namespace ConferenceAPI.Services
{
    public interface INotificationService
    {
        void Send(Notification notification);
    }
}
