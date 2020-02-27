using deVoid.UIFramework;
using deVoid.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using UnityEngine;

// DATA
[Serializable]
public class MainMenuWindowProperties : WindowProperties
{
    public readonly LessonMaster.Lesson mLessonData;
    public readonly RectTransform mRootTargetTransform;
    private Camera mUICamera;

    public MainMenuWindowProperties(LessonMaster.Lesson _lessonData,
        Camera _uiCamera,
        RectTransform _rootTargetTransform)
    {
        mLessonData = _lessonData;
        mRootTargetTransform = _rootTargetTransform;
        UICamera = _uiCamera;
    }

    public Camera UICamera { get => mUICamera; set => mUICamera = value; }
}

// EVENT
public class MainMenuWindow_ShowSignal : ASignal<MainMenuWindowProperties> { }

public class MainMenuWindowController : AWindowController<MainMenuWindowProperties>
{
    [SerializeField] private Rectangle mTopHeader;

    private LessonMaster.Lesson mLessonData;
    private Camera mUICamera;
    private RectTransform mRootTargetTransform;

    public LessonMaster.Lesson LessonData { get => mLessonData; set => mLessonData = value; }
    public Camera UICamera { get => mUICamera; set => mUICamera = value; }
    public RectTransform RootTargetTransform { get => mRootTargetTransform; set => mRootTargetTransform = value; }
    public Rectangle TopHeader { get => mTopHeader; set => mTopHeader = value; }

    protected override void OnPropertiesSet()
    {
        LessonData = Properties.mLessonData;
        UICamera = Properties.UICamera;
        RootTargetTransform = Properties.mRootTargetTransform;
        UpdateData();
    }

    private void UpdateData()
    {
        TopHeaderController topHeader = mTopHeader.GetComponent<TopHeaderController>();
        if (topHeader != null)
        {
            topHeader.SetData(LessonData);
        }
    }

    public void UI_VocaListBtnClick()
    {
        if (LessonData == null) return;

        // Show vocabulary list window
        Signals.Get<VocaListWindow_ShowSignal>().Dispatch(new VocaListWindowProperties(mLessonData, null));
    }
}
