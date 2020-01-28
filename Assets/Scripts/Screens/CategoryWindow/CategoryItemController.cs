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
    [SerializeField] private CategoryBannerItem categoryBannerItem = null;
    [SerializeField] private LoopListView2 subCategoryListView = null;
    [SerializeField] private GameObject subCategoryItemOrigin = null;
    [SerializeField] private LessonData lessonData = null;

    public LoopListView2 SubCategoryListView { get { return subCategoryListView; } }

    private List<LessonData.Lesson> listLessons = new List<LessonData.Lesson>();

    private void Awake()
    {
        subCategoryListView.InitListView(0, OnSubCategoryListUpdate);
    }

    public void SetData(TopicData.Topic _data)
    {
        categoryBannerItem.SetData(_data);

        listLessons = lessonData.mListLessons.Where(x => x.topic_id == _data.id).ToList();
        listLessons = listLessons.OrderBy(x => x.order).ToList();

        subCategoryListView.SetListItemCount(listLessons.Count, true);
    }

    private LoopListViewItem2 OnSubCategoryListUpdate(LoopListView2 _listview, int _index)
    {
        if (_index < 0 || _index >= listLessons.Count) return null;

        LoopListViewItem2 itemObj = _listview.NewListViewItem(subCategoryItemOrigin.name);
        itemObj.GetComponent<SubCategoryItem>().SetData(listLessons[_index]);

        return itemObj;
    }

    public void UpdateAnimation(float _diff, float distanceWithCenter)
    {
        categoryBannerItem.ContentRootObj.GetComponent<CanvasGroup>().alpha = _diff;
        categoryBannerItem.ContentRootObj.transform.localScale = new Vector3(_diff, _diff, 1f);

        subCategoryListView.UpdateAllShownItemSnapData();
        int count = subCategoryListView.ShownItemCount;
        for (int i = 0; i < count; i++)
        {
            LoopListViewItem2 subCategoryItem = subCategoryListView.GetShownItemByIndex(i);
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
}
