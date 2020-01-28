using DG.Tweening;
using SuperScrollView;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryItemController : MonoBehaviour
{
    [SerializeField] CategoryBannerItem categoryBannerItem = null;
    [SerializeField] LoopListView2 subCategoryListView = null;
    [SerializeField] GameObject subCategoryItemOrigin = null;

    public LoopListView2 SubCategoryListView { get { return subCategoryListView; } }
    private List<int> subCategoryData = null;

    private void Awake()
    {
        subCategoryListView.InitListView(0, OnSubCategoryListUpdate);
    }

    public void SetData(ItemData _data)
    {
        categoryBannerItem.SetData(_data);

        subCategoryData = new List<int>();
        int rand = UnityEngine.Random.Range(50, 91);
        for (int i = 0; i < rand; i++)
        {
            subCategoryData.Add(i);
        }

        subCategoryListView.SetListItemCount(subCategoryData.Count, true);
    }

    private LoopListViewItem2 OnSubCategoryListUpdate(LoopListView2 _listview, int _index)
    {
        if (_index < 0 || _index >= subCategoryData.Count) return null;

        LoopListViewItem2 itemObj = _listview.NewListViewItem(subCategoryItemOrigin.name);
        itemObj.GetComponent<SubCategoryItem>().SetData(_index.ToString());

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
