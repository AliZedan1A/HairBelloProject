

namespace ClientSide.Services.Class
{
    public enum AlertType
    {
        Success,
        Error,
        Info
    }
    public class AlertService
    {
        public event Action<string, AlertType>? OnShow;

        public void ShowAlert(string message, AlertType type = AlertType.Info)
        {
            OnShow?.Invoke(message, type);
        }
    }
}
