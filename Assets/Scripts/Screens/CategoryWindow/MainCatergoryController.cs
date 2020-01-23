using SuperScrollView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCatergoryController : MonoBehaviour
{
    [SerializeField] LoopListView2 mainCatergoryListView = null;
    [SerializeField] GameObject itemOrigin = null;

    private List<ItemData> listItemData = null;

    private void Start()
    {
        listItemData = new List<ItemData>();

        for (int i = 0; i < 10; i++)
        {
            ItemData temp = new ItemData("Flower " + (i + 1));
            listItemData.Add(temp);
        }

        LoopListViewInitParam initParam = LoopListViewInitParam.CopyDefaultInitParam();
        initParam.mSnapVecThreshold = 99999;
        initParam.mItemDefaultWithPaddingSize = 610f;
        //initParam.mSnapFinishThreshold = 1f;
        //initParam.mSnapVecThreshold = 300f;
        //initParam.mSmoothDumpRate = 0.5f;
        mainCatergoryListView.InitListView(-1, OnListViewUpdate, initParam);
    }

    private LoopListViewItem2 OnListViewUpdate(LoopListView2 _listview, int _index)
    {
        int itemCount = listItemData.Count;
        int actualyIndex = _index < 0 ? itemCount + ((_index + 1) % itemCount) - 1 : _index % itemCount;
        LoopListViewItem2 itemObj = _listview.NewListViewItem(itemOrigin.name);
        ItemData itemData = listItemData[actualyIndex];
        itemObj.GetComponent<MainCategoryItem>().SetData(itemData);

        return itemObj;
    }

    private void LateUpdate()
    {
        mainCatergoryListView.UpdateAllShownItemSnapData();
        int count = mainCatergoryListView.ShownItemCount;
        for (int i = 0; i < count; i++)
        {
            LoopListViewItem2 itemObj = mainCatergoryListView.GetShownItemByIndex(i);
            MainCategoryItem mainCategoryItem = itemObj.GetComponent<MainCategoryItem>();
            float diff = 1 - Mathf.Abs(itemObj.DistanceWithViewPortSnapCenter) / 1200f;
            diff = Mathf.Clamp(diff, 0.2f, 1f);
            mainCategoryItem.ContentRootObj.GetComponent<CanvasGroup>().alpha = diff;
            mainCategoryItem.ContentRootObj.transform.localScale = new Vector3(diff, diff, 1f);
        }
    }
}
