using deVoid.UIFramework;
using deVoid.Utils;
using SuperScrollView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// EVENT
public class FlashCardWindow_ShowSignal : ASignal<VocaListWindowProperties> { }

public class FlashCardWindowController : AWindowController<VocaListWindowProperties>
{
    [SerializeField] private TextMeshProUGUI mTitleLabel = null;
    [SerializeField] private LoopListView2 mFlashCardSnapListView = null;
    [SerializeField] private GameObject mFlashCardItemOrigin = null;

    private LessonMaster.Lesson mLessonData;
    private VocaMaster mVocaMasterData;

    private List<VocaMaster.Voca> mListVocas = null;
    private bool mIsInitCalled = false;

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
            initParam.mItemDefaultWithPaddingSize = 1080f;
            initParam.mSnapFinishThreshold = 0.5f;

            mFlashCardSnapListView.mOnEndDragAction = OnEndDrag;

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
        // if (_index < 0 || _index >= mListVocas.Count) return null;
        if (mListVocas == null) return null;

        int itemCount = mListVocas.Count;
        int actualyIndex = _index < 0 ? itemCount + ((_index + 1) % itemCount) - 1 : _index % itemCount;
        LoopListViewItem2 itemObj = _listView.NewListViewItem(mFlashCardItemOrigin.name);
        VocaMaster.Voca vocaData = mListVocas[actualyIndex];
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
