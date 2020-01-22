using deVoid.UIFramework;
using deVoid.Utils;
using DG.Tweening;
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
        itemObj.GetComponentInChildren<Text>().text = itemData.title;

        // scale animation when item created
        RectTransform rTransform = itemObj.transform as RectTransform;
        rTransform.localScale = new Vector3(0.02f, 0.02f, 1f);

        rTransform.DOScale(new Vector3(1f, 1f, 1f), 0.25f)
            .SetEase(Ease.Linear)
            .SetUpdate(true)
            .OnComplete(() => {

            });

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
