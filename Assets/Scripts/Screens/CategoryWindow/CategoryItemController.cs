using DG.Tweening;
using SuperScrollView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CategoryItemController : MonoBehaviour
{
    [SerializeField] private CategoryBannerItem mCategoryBannerItem = null;
    [SerializeField] private LoopListView2 mSubCategoryListView = null;
    [SerializeField] private GameObject mSubCategoryItemOrigin = null;

    public LoopListView2 SubCategoryListView { get { return mSubCategoryListView; } }

    private bool mEnableSubListAnimate;
    public bool EnableSubListAnimate { get { return mEnableSubListAnimate; } set { mEnableSubListAnimate = value; } }

    private TopicMaster.Topic mTopicData = null;
    private List<LessonMaster.Lesson> mListLessons = null;

    private void Awake()
    {
        EnableSubListAnimate = true;
        mSubCategoryListView.InitListView(0, OnSubCategoryListUpdate);
    }

    public void SetData(TopicMaster.Topic _topic, LessonMaster _lessonData)
    {
        mTopicData = _topic;
        if (mTopicData == null) return;

        mCategoryBannerItem.SetData(_topic);
        
        mListLessons = _lessonData.mListLessons.Where(x => x.topic_id == _topic.id).ToList();
        mListLessons = mListLessons.OrderBy(x => x.order).ToList();

        mSubCategoryListView.SetListItemCount(mListLessons.Count, true);
    }

    private LoopListViewItem2 OnSubCategoryListUpdate(LoopListView2 _listview, int _index)
    {
        if (mListLessons == null) return null;
        if (_index < 0 || _index >= mListLessons.Count) return null;

        LoopListViewItem2 itemObj = _listview.NewListViewItem(mSubCategoryItemOrigin.name);
        itemObj.GetComponent<SubCategoryItem>().SetData(mListLessons[_index], _listview.ScrollRect, EnableSubListAnimate);

        return itemObj;
    }

    public void UpdateAnimation(float _diff, float distanceWithCenter)
    {
        float bannerDiff = Mathf.Clamp(_diff, 0.9f, 1f);
        mCategoryBannerItem.ContentRootObj.GetComponent<CanvasGroup>().alpha = Mathf.Clamp(_diff, 0.6f, 1f);
        mCategoryBannerItem.ContentRootObj.transform.localScale = new Vector3(bannerDiff, bannerDiff, 1f);

        mSubCategoryListView.UpdateAllShownItemSnapData();
        int count = mSubCategoryListView.ShownItemCount;
        for (int i = 0; i < count; i++)
        {
            LoopListViewItem2 subCategoryItem = mSubCategoryListView.GetShownItemByIndex(i);
            subCategoryItem.GetComponentInChildren<CanvasGroup>().alpha = _diff;
            Transform contentTransform = subCategoryItem.transform.Find("ContentObj").transform;
            contentTransform.localScale = new Vector3(_diff, 1f, 1f);
            contentTransform.localPosition = new Vector3(
                -distanceWithCenter / ((float)(i + 2f) * 0.75f),
                contentTransform.localPosition.y,
                contentTransform.localPosition.z
                );
        }
    }

    public void ResetSubCategoryListView()
    {
        EnableSubListAnimate = false;
        SubCategoryListView.MovePanelToItemIndex(0, 0);
    }
}
