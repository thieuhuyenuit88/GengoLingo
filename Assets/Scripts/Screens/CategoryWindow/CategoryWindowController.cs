using deVoid.UIFramework;
using deVoid.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DATA
[Serializable]
public class CategoryWindowProperties : WindowProperties
{
    public readonly TopicMaster mTopicMasterData;
    public readonly LessonMaster mLessonMasterData;

    public CategoryWindowProperties(TopicMaster _topicData, LessonMaster _lessonData)
    {
        mTopicMasterData = _topicData;
        mLessonMasterData = _lessonData;
    }
}

// EVENT
public class CategoryWindow_ShowSignal : ASignal { }

public class CategoryWindowController : AWindowController<CategoryWindowProperties>
{
    [SerializeField] private MainCatergoryController mainCategoryController = null;
    
    private TopicMaster topicMaster = null;
    private LessonMaster lessonMaster = null;

    protected override void OnPropertiesSet()
    {
        base.OnPropertiesSet();
        OnDataUpdated(Properties.mTopicMasterData, Properties.mLessonMasterData);
    }

    private void OnDataUpdated(TopicMaster _topicData, LessonMaster _lessonData)
    {
        topicMaster = _topicData;
        lessonMaster = _lessonData;
        if (mainCategoryController != null)
        {
            mainCategoryController.SetData(_topicData, _lessonData);
        }
    }

    protected override void AddListeners()
    {
        OutTransitionFinished += OnOutAnimationFinished;
    }

    protected override void RemoveListeners()
    {
        OutTransitionFinished -= OnOutAnimationFinished;
    }

    private void OnOutAnimationFinished(IUIScreenController screen)
    {
        // Do nothing
    }
}
