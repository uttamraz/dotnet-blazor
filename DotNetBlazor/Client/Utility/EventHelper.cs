using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Client.Utility
{
    public interface IEventHelper
    {
        public void SetValidationError(List<Error> list);
        public void SessionExpiredPopup();

        public event Action<List<Error>>? ValidationError;

        public event Action? SessionExpired;
    }

    public class EventHelper : IEventHelper
    {
        public event Action<List<Error>>? ValidationError;

        public event Action? SessionExpired;

        public void SetValidationError(List<Error> list)
        {
            ValidationError?.Invoke(list);
        }

        public void SessionExpiredPopup()
        {
            SessionExpired?.Invoke();

        }
    }
}