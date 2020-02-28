using deVoid.UIFramework;
using deVoid.Utils;
using SuperScrollView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// DATA
[Serializable]
public class VocaListWindowProperties : WindowProperties
{
    public readonly LessonMaster.Lesson mLessonData;
    private VocaMaster mVocaMasterData;

    public VocaListWindowProperties(LessonMaster.Lesson _lessonData, VocaMaster _vocaMaster)
    {
        mLessonData = _lessonData;
        VocaMasterData = _vocaMaster;
    }

    public VocaMaster VocaMasterData { get => mVocaMasterData; set => mVocaMasterData = value; }
}

// EVENT
public class VocaListWindow_ShowSignal : ASignal<VocaListWindowProperties> { }

public class VocaListWindowController : AWindowController<VocaListWindowProperties>
{
    [SerializeField] private TextMeshProUGUI mTitleLabel = null;
    [SerializeField] private LoopListView2 mVocaListView = null;
    [SerializeField] private GameObject mGridRowItemOrigin = null;

    private LessonMaster.Lesson mLessonData;
    private VocaMaster mVocaMasterData;

    private List<VocaMaster.Voca> mListVocas = null;
    const int mItemCountPerRow = 2;
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

        int count = mListVocas.Count / mItemCountPerRow;
        if (mListVocas.Count % mItemCountPerRow != 0) count++;
        if (!mIsInitCalled)
        {
            mVocaListView.InitListView(count, OnVocaListViewUpdate);
            mIsInitCalled = true;
        }
        else
        {
            mVocaListView.SetListItemCount(count, false);
            mVocaListView.MovePanelToItemIndex(0, 0);
        }
    }

    private LoopListViewItem2 OnVocaListViewUpdate(LoopListView2 _listview, int _index)
    {
        if (_index < 0) return null;

        LoopListViewItem2 itemObj = _listview.NewListViewItem(mGridRowItemOrigin.name);
        GridRowVocaItemController itemGridRow = itemObj.GetComponent<GridRowVocaItemController>();
        if (!itemObj.IsInitHandlerCalled)
        {
            itemObj.IsInitHandlerCalled = true;
            itemGridRow.Init();
        }

        for (int i = 0; i < mItemCountPerRow; i++)
        {
            int itemIndex = _index * mItemCountPerRow + i;
            if (itemIndex >= mListVocas.Count)
            {
                itemGridRow.ItemList[i].gameObject.SetActive(false);
                continue;
            }

            VocaMaster.Voca vocaData = mListVocas[itemIndex];
            if (vocaData != null)
            {
                itemGridRow.ItemList[i].gameObject.SetActive(true);
                itemGridRow.ItemList[i].SetItemData(vocaData);
            }
            else
            {
                itemGridRow.ItemList[i].gameObject.SetActive(false);
            }
        }

        return itemObj;
    }
}
