using DG.Tweening;
using Lean.Gui;
using SuperScrollView;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlashCardItemController : MonoBehaviour
{
    [SerializeField] GameObject mContentObj = null;
    [SerializeField] LeanButton mButton = null;
    [SerializeField] GameObject mFrontObj = null;
    [SerializeField] GameObject mBackObj = null;
    [SerializeField] RubyTextMeshProUGUI mWordLabel = null;
    [SerializeField] RubyTextMeshProUGUI mMeanLabel = null;
    [SerializeField] Image mIcon = null;

    [SerializeField] protected float mFlipTime = 0.25f;
    [SerializeField] protected Ease mEase = Ease.Linear;

    public GameObject ContentObj { get { return mContentObj; } }
    
    private enum CardState { Front, Back }
    private CardState mCurCardState = CardState.Front;

    private void Start()
    {
        SetCardState(CardState.Front);
    }

    private void LateUpdate()
    {
        if (mCurCardState == CardState.Back)
        {
            LoopListViewItem2 itemObj = gameObject.GetComponent<LoopListViewItem2>();
            if (Mathf.Abs(itemObj.DistanceWithViewPortSnapCenter) >= (itemObj.ItemSizeWithPadding - 50f))
            {
                SetCardState(CardState.Front);
            }
        }
    }

    private void SetCardState(CardState _cardState)
    {
        mFrontObj.SetActive(_cardState == CardState.Front);
        mBackObj.SetActive(_cardState == CardState.Back);
        mCurCardState = _cardState;
    }

    public void SetItemData(VocaMaster.Voca _data)
    {
        if (_data == null) return;

        mWordLabel.UnditedText = "<ruby=" + _data.hiragana + ">" + _data.word + "</ruby>";
        mMeanLabel.UnditedText = mWordLabel.UnditedText + "\n" + _data.en;
        mIcon.sprite = _data.icon;

        SetCardState(CardState.Front);
    }
    
    public void UI_ChangeStateBtnClick()
    {
        mButton.interactable = false;
        mContentObj.transform.DORotate(new Vector3(90f, 0f, 0f), mFlipTime)
            .SetEase(mEase)
            .OnComplete(
            () =>
            {
                if (mCurCardState == CardState.Front)
                {
                    SetCardState(CardState.Back);
                }
                else
                {
                    SetCardState(CardState.Front);
                }
                mContentObj.transform.DORotate(new Vector3(0f, 0f, 0f), mFlipTime)
                    .SetEase(mEase)
                    .OnComplete(() => mButton.interactable = true)
                    .SetUpdate(true);
            })
            .SetUpdate(true);
    }
}
