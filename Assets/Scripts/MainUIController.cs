using System;
using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using deVoid.UIFramework.Utils;
using deVoid.Utils;
using DG.Tweening;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private UISettings mMainUISettings = null;
    [SerializeField] private TopicMaster mTopicMasterData = null;
    [SerializeField] private LessonMaster mLessonMasterData = null;
    [SerializeField] private VocaMaster mVocaMasterData = null;

    private UIFrame mMainUIManager;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        DOTween.Init(true, true, LogBehaviour.Default).SetCapacity(200, 10);
        mMainUIManager = mMainUISettings.CreateUIInstance();

        Signals.Get<TestWindow_ReloadSignal>().AddListener(ReloadTestWindow);
        Signals.Get<TestPopup_ShowSignal>().AddListener(ShowTestPopup);

        Signals.Get<CategoryWindow_ShowSignal>().AddListener(ShowCategoryWindow);
        Signals.Get<MainMenuWindow_ShowSignal>().AddListener(ShowMainMenuWindow);
        Signals.Get<VocaListWindow_ShowSignal>().AddListener(ShowVocaListWindow);
    }

    private void OnDestroy()
    {
        Signals.Get<TestWindow_ReloadSignal>().RemoveListener(ReloadTestWindow);
        Signals.Get<TestPopup_ShowSignal>().RemoveListener(ShowTestPopup);

        Signals.Get<CategoryWindow_ShowSignal>().RemoveListener(ShowCategoryWindow);
        Signals.Get<MainMenuWindow_ShowSignal>().RemoveListener(ShowMainMenuWindow);
        Signals.Get<VocaListWindow_ShowSignal>().RemoveListener(ShowVocaListWindow);
    }

    private void ShowCategoryWindow()
    {
        mMainUIManager.OpenWindow(ScreenIds.CategoriesWindow,
            new CategoryWindowProperties(mTopicMasterData, mLessonMasterData));
    }

    /// <summary>
    /// Reload test window screen
    /// </summary>
    private void ReloadTestWindow()
    {
        mMainUIManager.OpenWindow(ScreenIds.TestWindow);
    }

    /// <summary>
    /// Show test popup
    /// </summary>
    /// <param name="_testPopupProperties"></param>
    private void ShowTestPopup(TestPopupProperties _testPopupProperties)
    {
        mMainUIManager.OpenWindow(ScreenIds.TestPopup, _testPopupProperties);
    }

    /// <summary>
    /// Open main menu window
    /// </summary>
    /// <param name="_properties"></param>
    private void ShowMainMenuWindow(MainMenuWindowProperties _properties)
    {
        _properties.UICamera = mMainUIManager.UICamera;
        mMainUIManager.OpenWindow(ScreenIds.MainMenuWindow, _properties);
    }

    /// <summary>
    /// Open vocabulary list window
    /// </summary>
    /// <param name="_properties"></param>
    private void ShowVocaListWindow(VocaListWindowProperties _properties)
    {
        _properties.VocaMasterData = mVocaMasterData;
        mMainUIManager.OpenWindow(ScreenIds.VocaListWindow, _properties);
    }

    // Start is called before the first frame update
    private void Start()
    {
        ShowCategoryWindow();
    }
}
