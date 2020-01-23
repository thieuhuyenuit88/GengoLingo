using System;
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
        Signals.Get<TestPopup_ShowSignal>().AddListener(ShowTestPopup);
    }

    private void OnDestroy()
    {
        Signals.Get<TestWindow_ReloadSignal>().RemoveListener(ReloadTestWindow);
        Signals.Get<TestPopup_ShowSignal>().RemoveListener(ShowTestPopup);
    }

    /// <summary>
    /// Reload test window screen
    /// </summary>
    private void ReloadTestWindow()
    {
        mainUIFrame.OpenWindow(ScreenIds.TestWindow);
    }

    /// <summary>
    /// Show test popup
    /// </summary>
    /// <param name="testPopupData"></param>
    private void ShowTestPopup(TestPopupProperties _testPopupProperties)
    {
        mainUIFrame.OpenWindow(ScreenIds.TestPopup, _testPopupProperties);
    }

    // Start is called before the first frame update
    private void Start()
    {
        mainUIFrame.OpenWindow(ScreenIds.CategoriesWindow);
    }
}
