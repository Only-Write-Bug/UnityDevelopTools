using System;
using UnityEngine;

public static class ApplicationLifeCycle
{
    public static Action onPause;
    public static Action onReopen;
    public static Action onQuit;
}