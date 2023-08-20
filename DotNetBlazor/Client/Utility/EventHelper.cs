using DotNetBlazor.Shared.Models.Common;
using DotNetBlazor.Shared.Models.Profile;

namespace DotNetBlazor.Client.Utility
{
    public interface IEventHelper
    {
        public void SetValidationError(List<Error> list);
        public void HandleSessionExpire();

        public event Action<List<Error>>? ValidationError;

        public event Action? SessionExpired;

        public event Action<bool> LoadingState;

        public event Action<UserDetail> ProfileProgress;
        public void SetLoadingState(bool state);
        public void SetProfileProgress(UserDetail user);
    }

    public class EventHelper : IEventHelper
    {
        public event Action<List<Error>>? ValidationError;

        public event Action? SessionExpired;

        public event Action<bool>? LoadingState;

        public event Action<UserDetail>? ProfileProgress;

        public void SetValidationError(List<Error> list)
        {
            ValidationError?.Invoke(list);
        }

        public void HandleSessionExpire()
        {
            SessionExpired?.Invoke();

        }

        public void SetLoadingState(bool state)
        {
            LoadingState?.Invoke(state);
        }

        public void SetProfileProgress(UserDetail user)
        {
            ProfileProgress?.Invoke(user);
        }

    }
}