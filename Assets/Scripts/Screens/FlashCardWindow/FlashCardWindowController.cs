using deVoid.UIFramework;
using deVoid.Utils;
using SuperScrollView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// EVENT
public class FlashCardWindow_ShowSignal : ASignal<VocaListWindowProperties> { }

public class FlashCardWindowController : AWindowController<VocaListWindowProperties>
{
    [SerializeField] private TextMeshProUGUI mTitleLabel = null;
    [SerializeField] private LoopListView2 mFlashCardSnapListView = null;
    [SerializeField] private GameObject mFlashCardItemOrigin = null;
    [SerializeField] private Slider mProgressBar = null;

    private LessonMaster.Lesson mLessonData;
    private VocaMaster mVocaMasterData;

    private List<VocaMaster.Voca> mListVocas = null;
    private bool mIsInitCalled = false;

    private float mCurrentProgress = 0f;
    public float CurrentProgress
    {
        get
        {
            return mCurrentProgress;
        }
        set
        {
            mCurrentProgress = value;
            mProgressBar.value = mCurrentProgress;
        }
    }

    public LessonMaster.Lesson LessonData { get => mLessonData; set => mLessonData = value; }
    public VocaMaster VocaMasterData { get => mVocaMasterData; set => mVocaMasterData = value; }

    protected override void OnPropertiesSet()
    {
        LessonData = Properties.mLessonData;
        VocaMasterData = Properties.VocaMasterData;
        OnUpdateData();
    }

    private void OnUpdateData()
    {
        if (LessonData == null || VocaMasterData == null) return;

        mTitleLabel.text = LessonData.title + "\n" + LessonData.en;
        mListVocas = VocaMasterData.mListVoca.Where(x => x.lesson_id == LessonData.id).ToList();
        mListVocas.OrderBy(x => x.id);

        if (!mIsInitCalled)
        {
            LoopListViewInitParam initParam = LoopListViewInitParam.CopyDefaultInitParam();
            initParam.mSnapVecThreshold = 99999f;
            initParam.mSnapFinishThreshold = 0.5f;

            mFlashCardSnapListView.mOnEndDragAction = OnEndDrag;
            mFlashCardSnapListView.mOnSnapNearestChanged = OnSnapNearestChanged;

            mFlashCardSnapListView.InitListView(mListVocas.Count, OnFlashCardSnapListViewUpdate);
            mIsInitCalled = true;
        }
        else
        {
            mFlashCardSnapListView.SetListItemCount(mListVocas.Count, false);
            mFlashCardSnapListView.MovePanelToItemIndex(0, 0);
        }
    }

    private LoopListViewItem2 OnFlashCardSnapListViewUpdate(LoopListView2 _listView, int _index)
    {
        if (_index < 0 || _index >= mListVocas.Count) return null;
        if (mListVocas == null) return null;

        LoopListViewItem2 itemObj = _listView.NewListViewItem(mFlashCardItemOrigin.name);
        VocaMaster.Voca vocaData = mListVocas[_index];
        FlashCardItemController itemController = itemObj.GetComponent<FlashCardItemController>();
        itemController.SetItemData(vocaData);

        return itemObj;
    }

    private void LateUpdate()
    {
        mFlashCardSnapListView.UpdateAllShownItemSnapData();
        int count = mFlashCardSnapListView.ShownItemCount;
        for (int i = 0; i < count; i++)
        {
            LoopListViewItem2 itemObj = mFlashCardSnapListView.GetShownItemByIndex(i);
            float diff = 1 - Mathf.Abs(itemObj.DistanceWithViewPortSnapCenter) / 1180f;
            FlashCardItemController itemController = itemObj.GetComponent<FlashCardItemController>();
            float fade = Mathf.Clamp(diff, 0.9f, 1f);
            itemController.ContentObj.GetComponent<CanvasGroup>().alpha = fade;
            float scale = Mathf.Clamp(diff, 0.85f, 1f);
            itemController.ContentObj.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }

    private void OnSnapNearestChanged(LoopListView2 _listView, LoopListViewItem2 _item)
    {
        int curNearestItemIndex = _listView.CurSnapNearestItemIndex;
        if (curNearestItemIndex < 0 || curNearestItemIndex >= mListVocas.Count) return;

        float progressValue = (float)((curNearestItemIndex + 1)) / (float)(mListVocas.Count);
        CurrentProgress = Mathf.Clamp(progressValue, 0f, 1f);
    }

    private void OnEndDrag()
    {
        float vec = mFlashCardSnapListView.ScrollRect.velocity.x;
        int curNearestItemIndex = mFlashCardSnapListView.CurSnapNearestItemIndex;
        LoopListViewItem2 item = mFlashCardSnapListView.GetShownItemByItemIndex(curNearestItemIndex);
        if (item == null)
        {
            mFlashCardSnapListView.ClearSnapData();
            return;
        }
        if (Mathf.Abs(vec) < 50f)
        {
            mFlashCardSnapListView.SetSnapTargetItemIndex(curNearestItemIndex);
            return;
        }
        Vector3 pos = mFlashCardSnapListView.GetItemCornerPosInViewPort(item, ItemCornerEnum.LeftTop);
        if (pos.x > 0)
        {
            if (vec > 0)
            {
                mFlashCardSnapListView.SetSnapTargetItemIndex(curNearestItemIndex - 1);
            }
            else
            {
                mFlashCardSnapListView.SetSnapTargetItemIndex(curNearestItemIndex);
            }
        }
        else if (pos.x < 0)
        {
            if (vec > 0)
            {
                mFlashCardSnapListView.SetSnapTargetItemIndex(curNearestItemIndex);
            }
            else
            {
                mFlashCardSnapListView.SetSnapTargetItemIndex(curNearestItemIndex + 1);
            }
        }
        else
        {
            if (vec > 0)
            {
                mFlashCardSnapListView.SetSnapTargetItemIndex(curNearestItemIndex - 1);
            }
            else
            {
                mFlashCardSnapListView.SetSnapTargetItemIndex(curNearestItemIndex + 1);
            }
        }
    }
}
