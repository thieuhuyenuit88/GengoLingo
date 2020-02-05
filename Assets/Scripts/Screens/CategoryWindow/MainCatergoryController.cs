using SuperScrollView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MainCatergoryController : MonoBehaviour
{
    [SerializeField] private LoopListView2 mCatergoryListView = null;
    [SerializeField] private GameObject mCategoryItemOrigin = null;

    private List<TopicMaster.Topic> mListTopics = null;
    private LessonMaster mLessonMasterData;

    private int mOldNearestItemIndex = int.MinValue;

    /// <summary>
    /// Apply master data
    /// </summary>
    /// <param name="_topicData"></param>
    /// <param name="_lessonData"></param>
    public void SetData(TopicMaster _topicData, LessonMaster _lessonData)
    {
        if (_topicData == null || _lessonData == null) return;

        mListTopics = _topicData.mListTopics.OrderBy(x => x.order).ToList();
        mLessonMasterData = _lessonData;
    }

    /// <summary>
    /// Init category list view 
    /// </summary>
    private void Start()
    {
        LoopListViewInitParam initParam = LoopListViewInitParam.CopyDefaultInitParam();
        initParam.mSnapVecThreshold = 99999f;
        initParam.mItemDefaultWithPaddingSize = 1080f;
        initParam.mSnapFinishThreshold = 0.5f;

        mCatergoryListView.mOnBeginDragAction = OnBeginDrag;
        mCatergoryListView.mOnDragingAction = OnDraging;
        mCatergoryListView.mOnEndDragAction = OnEndDrag;
        mCatergoryListView.mOnSnapNearestChanged = OnSnapNearestChanged;

        mCatergoryListView.InitListView(-1, OnCategoryListViewUpdate, initParam);
    }

    /// <summary>
    /// On create(recreate) new item for category list view, and setting data
    /// </summary>
    /// <param name="_listview"></param>
    /// <param name="_index"></param>
    /// <returns></returns>
    private LoopListViewItem2 OnCategoryListViewUpdate(LoopListView2 _listview, int _index)
    {
        if (mListTopics == null) return null;

        int itemCount = mListTopics.Count;
        int actualyIndex = _index < 0 ? itemCount + ((_index + 1) % itemCount) - 1 : _index % itemCount;
        LoopListViewItem2 itemObj = _listview.NewListViewItem(mCategoryItemOrigin.name);
        TopicMaster.Topic itemData = mListTopics[actualyIndex];
        itemObj.GetComponent<CategoryItemController>().SetData(itemData, mLessonMasterData);

        return itemObj;
    }

    private void LateUpdate()
    {
        mCatergoryListView.UpdateAllShownItemSnapData();
        int count = mCatergoryListView.ShownItemCount;
        for (int i = 0; i < count; i++)
        {
            LoopListViewItem2 itemObj = mCatergoryListView.GetShownItemByIndex(i);
            float diff = 1 - Mathf.Abs(itemObj.DistanceWithViewPortSnapCenter) / 1180f;
            diff = Mathf.Clamp(diff, 0.2f, 1f);
            CategoryItemController itemController = itemObj.GetComponent<CategoryItemController>();
            itemController.UpdateAnimation(diff, itemObj.DistanceWithViewPortSnapCenter);
        }
    }

    private void OnSnapNearestChanged(LoopListView2 _listView, LoopListViewItem2 _item)
    {
        int index = _listView.CurSnapNearestItemIndex;
        _item.GetComponent<CategoryItemController>().EnableSubListAnimate = true;

        if (index != mOldNearestItemIndex)
        {
            LoopListViewItem2 prevItem = _listView.GetShownItemByItemIndex(mOldNearestItemIndex);
            if (prevItem != null)
            {
                CategoryItemController itemController = prevItem.GetComponent<CategoryItemController>();
                itemController.ResetSubCategoryListView();
            }
        }
        mOldNearestItemIndex = index;
    }

    private void OnBeginDrag()
    {
        // Do nothing
    }

    private void OnDraging()
    {
        // Do nothing
    }

    private void OnEndDrag()
    {
        float vec = mCatergoryListView.ScrollRect.velocity.x;
        int curNearestItemIndex = mCatergoryListView.CurSnapNearestItemIndex;
        LoopListViewItem2 item = mCatergoryListView.GetShownItemByItemIndex(curNearestItemIndex);
        if (item == null)
        {
            mCatergoryListView.ClearSnapData();
            return;
        }
        if (Mathf.Abs(vec) < 50f)
        {
            mCatergoryListView.SetSnapTargetItemIndex(curNearestItemIndex);
            return;
        }
        Vector3 pos = mCatergoryListView.GetItemCornerPosInViewPort(item, ItemCornerEnum.LeftTop);
        if (pos.x > 0)
        {
            if (vec > 0)
            {
                mCatergoryListView.SetSnapTargetItemIndex(curNearestItemIndex - 1);
            }
            else
            {
                mCatergoryListView.SetSnapTargetItemIndex(curNearestItemIndex);
            }
        }
        else if (pos.x < 0)
        {
            if (vec > 0)
            {
                mCatergoryListView.SetSnapTargetItemIndex(curNearestItemIndex);
            }
            else
            {
                mCatergoryListView.SetSnapTargetItemIndex(curNearestItemIndex + 1);
            }
        }
        else
        {
            if (vec > 0)
            {
                mCatergoryListView.SetSnapTargetItemIndex(curNearestItemIndex - 1);
            }
            else
            {
                mCatergoryListView.SetSnapTargetItemIndex(curNearestItemIndex + 1);
            }
        }
    }
}
