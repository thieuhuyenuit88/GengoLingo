using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using deVoid.UIFramework.Utils;
using deVoid.Utils;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private UISettings mainUISettings = null;

    private UIFrame mainUIFrame;

    private void Awake()
    {
        mainUIFrame = mainUISettings.CreateUIInstance();
        Signals.Get<TestWindow_ReloadSignal>().AddListener(ReloadTestWindow);
    }

    private void OnDestroy()
    {
        Signals.Get<TestWindow_ReloadSignal>().RemoveListener(ReloadTestWindow);
    }

    private void ReloadTestWindow()
    {
        mainUIFrame.OpenWindow(ScreenIds.TestWindow);
    }

    // Start is called before the first frame update
    private void Start()
    {
        mainUIFrame.OpenWindow(ScreenIds.TestWindow);
    }
}
