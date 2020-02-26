using deVoid.UIFramework;
using deVoid.Utils;
using SuperScrollView;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DATA
[Serializable]
public class VocaListWindowProperties : WindowProperties
{
    public readonly LessonMaster.Lesson mLessonData;
    public readonly VocaMaster mVocaMasterData;

    public VocaListWindowProperties(LessonMaster.Lesson _lessonData, VocaMaster _vocaMaster)
    {
        mLessonData = _lessonData;
        mVocaMasterData = _vocaMaster;
    }
}

// EVENT
public class VocaListWindow_ShowSignal : ASignal<VocaListWindowProperties> { }

public class VocaListWindowController : AWindowController<VocaListWindowProperties>
{
    [SerializeField] private LoopListView2 mVocaListView = null;

    private LessonMaster.Lesson mLessonData;
    private VocaMaster mVocaMasterData;

    public LessonMaster.Lesson LessonData { get => mLessonData; set => mLessonData = value; }
    public VocaMaster VocaMasterData { get => mVocaMasterData; set => mVocaMasterData = value; }

    private void Start()
    {
        mVocaListView.InitListView(0, OnVocaListViewUpdate);
    }

    protected override void OnPropertiesSet()
    {
        LessonData = Properties.mLessonData;
        VocaMasterData = Properties.mVocaMasterData;
        OnUpdateData();
    }

    private void OnUpdateData()
    {

    }

    private LoopListViewItem2 OnVocaListViewUpdate(LoopListView2 _listview, int _index)
    {
        return null;
    }
}
