namespace MoneyNote.Maui;

public sealed class NavigationCoordinator
{
    public event Action<AppRoute>? NavigationRequested;

    public void NavigateTo(AppRoute route)
    {
        NavigationRequested?.Invoke(route);
    }
}
