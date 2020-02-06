using deVoid.UIFramework;
using deVoid.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DATA
[Serializable]
public class MainMenuWindowProperties : WindowProperties
{
    public readonly LessonMaster.Lesson mLessonData;

    public MainMenuWindowProperties(LessonMaster.Lesson _lessonData)
    {
        mLessonData = _lessonData;
    }
}

// EVENT
public class MainMenuWindow_ShowSignal : ASignal<MainMenuWindowProperties> { }

public class MainMenuWindowController : AWindowController<MainMenuWindowProperties>
{
    private LessonMaster.Lesson mLessonData;

    protected override void OnPropertiesSet()
    {
        mLessonData = Properties.mLessonData;
    }
}
