using SuperScrollView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MainCatergoryController : MonoBehaviour
{
    [SerializeField] private LoopListView2 catergoryListView = null;
    [SerializeField] private GameObject categoryItemOrigin = null;
    [SerializeField] private TopicData topicData = null;

    private List<TopicData.Topic> listTopics = null;

    private int oldNearestItemIndex = int.MinValue;

    private void Start()
    {
        listTopics = topicData.mListTopics.OrderBy(x => x.order).ToList();

        LoopListViewInitParam initParam = LoopListViewInitParam.CopyDefaultInitParam();
        initParam.mSnapVecThreshold = 99999f;
        initParam.mItemDefaultWithPaddingSize = 1080f;
        initParam.mSnapFinishThreshold = 0.5f;

        catergoryListView.mOnBeginDragAction = OnBeginDrag;
        catergoryListView.mOnDragingAction = OnDraging;
        catergoryListView.mOnEndDragAction = OnEndDrag;
        catergoryListView.mOnSnapNearestChanged = OnSnapNearestChanged;

        catergoryListView.InitListView(-1, OnCategoryListViewUpdate, initParam);
    }

    /// <summary>
    /// On create(recreate) new item for category list view, and setting data
    /// </summary>
    /// <param name="_listview"></param>
    /// <param name="_index"></param>
    /// <returns></returns>
    private LoopListViewItem2 OnCategoryListViewUpdate(LoopListView2 _listview, int _index)
    {
        int itemCount = listTopics.Count;
        int actualyIndex = _index < 0 ? itemCount + ((_index + 1) % itemCount) - 1 : _index % itemCount;
        LoopListViewItem2 itemObj = _listview.NewListViewItem(categoryItemOrigin.name);
        TopicData.Topic itemData = listTopics[actualyIndex];
        itemObj.GetComponent<CategoryItemController>().SetData(itemData);

        return itemObj;
    }

    private void LateUpdate()
    {
        catergoryListView.UpdateAllShownItemSnapData();
        int count = catergoryListView.ShownItemCount;
        for (int i = 0; i < count; i++)
        {
            LoopListViewItem2 itemObj = catergoryListView.GetShownItemByIndex(i);
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

        if (index != oldNearestItemIndex)
        {
            LoopListViewItem2 prevItem = _listView.GetShownItemByItemIndex(oldNearestItemIndex);
            if (prevItem != null)
            {
                CategoryItemController itemController = prevItem.GetComponent<CategoryItemController>();
                itemController.ResetSubCategoryListView();
            }
        }
        oldNearestItemIndex = index;
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
        float vec = catergoryListView.ScrollRect.velocity.x;
        int curNearestItemIndex = catergoryListView.CurSnapNearestItemIndex;
        LoopListViewItem2 item = catergoryListView.GetShownItemByItemIndex(curNearestItemIndex);
        if (item == null)
        {
            catergoryListView.ClearSnapData();
            return;
        }
        if (Mathf.Abs(vec) < 50f)
        {
            catergoryListView.SetSnapTargetItemIndex(curNearestItemIndex);
            return;
        }
        Vector3 pos = catergoryListView.GetItemCornerPosInViewPort(item, ItemCornerEnum.LeftTop);
        if (pos.x > 0)
        {
            if (vec > 0)
            {
                catergoryListView.SetSnapTargetItemIndex(curNearestItemIndex - 1);
            }
            else
            {
                catergoryListView.SetSnapTargetItemIndex(curNearestItemIndex);
            }
        }
        else if (pos.x < 0)
        {
            if (vec > 0)
            {
                catergoryListView.SetSnapTargetItemIndex(curNearestItemIndex);
            }
            else
            {
                catergoryListView.SetSnapTargetItemIndex(curNearestItemIndex + 1);
            }
        }
        else
        {
            if (vec > 0)
            {
                catergoryListView.SetSnapTargetItemIndex(curNearestItemIndex - 1);
            }
            else
            {
                catergoryListView.SetSnapTargetItemIndex(curNearestItemIndex + 1);
            }
        }
    }
}
