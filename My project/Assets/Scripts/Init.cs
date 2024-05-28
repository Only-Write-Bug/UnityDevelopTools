using UnityEditor;
using UnityEngine;

public class Init : MonoBehaviour
{
    private void Start()
    {
        AssetsManagerTool.PackPrefabsToAssetBundles();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
#if !UNITY_EDITOR
        if (pauseStatus)
        {
            ApplicationLifeCycle.onPause?.Invoke();
        }
        else
        {
            ApplicationLifeCycle.onReopen?.Invoke();
        }
#endif
    }

    private void OnApplicationQuit()
    {
        ApplicationLifeCycle.onQuit?.Invoke();
    }
}