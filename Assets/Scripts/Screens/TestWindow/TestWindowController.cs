using deVoid.UIFramework;
using deVoid.Utils;
using SuperScrollView;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemData
{
    public string title { get; set; }

    public ItemData(string _title) { title = _title; }
}

public class TestWindow_ReloadSignal : ASignal { }

public class TestWindowController : AWindowController
{
    [SerializeField] LoopListView2 listView = null;

    private List<ItemData> listItemDatas = null;

    private void Start()
    {
        listItemDatas = new List<ItemData>();

        for (int i = 0; i < 100; i++)
        {
            ItemData temp = new ItemData("Item " + (i + 1));
            listItemDatas.Add(temp);
        }

        listView.InitListView(listItemDatas.Count, OnListViewUpdate);
    }

    private LoopListViewItem2 OnListViewUpdate(LoopListView2 _listview, int _index)
    {
        if (_index < 0 || _index >= listItemDatas.Count) return null;

        var itemObj = _listview.NewListViewItem("ItemTest");
        var itemData = listItemDatas[_index];
        itemObj.GetComponent<ItemListViewController>().SetData(itemData.title);

        return itemObj;
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
        Signals.Get<TestWindow_ReloadSignal>().Dispatch();
    }

    public void UI_ReloadBtn()
    {
        CloseRequest(this);
    }
}
