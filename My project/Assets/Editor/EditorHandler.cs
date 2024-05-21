using UnityEditor;

[InitializeOnLoad]
public class EditorHandler
{
    static EditorHandler()
    {
        EditorApplication.pauseStateChanged += OnPauseStateChanged;
    }

    private static void OnPauseStateChanged(PauseState state)
    {
        if (state == PauseState.Paused)
        {
            ApplicationLifeCycle.onPause?.Invoke();
        }
        else if (state == PauseState.Unpaused)
        {
            ApplicationLifeCycle.onReopen?.Invoke();
        }
    }
}