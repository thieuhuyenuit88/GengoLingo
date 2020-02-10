using deVoid.Utils;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubCategoryItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mTitleLabel = null;
    [SerializeField] Rectangle mIcon= null;

    private LessonMaster.Lesson mLessonData;
    private ScrollRect mScrollRect;

    public void SetData(LessonMaster.Lesson _data, ScrollRect _scrollRect, bool _enableAnimate = true)
    {
        mLessonData = _data;
        mScrollRect = _scrollRect;
        if (mLessonData == null) return;
        
        mTitleLabel.text = _data.title + "\n" + _data.en;
        mIcon.sprite = _data.icon;

        if (_enableAnimate)
        {
            // scale animation when item created
            RectTransform rTransform = transform as RectTransform;
            rTransform.localScale = new Vector3(0.02f, 0.02f, 1f);

            var scale = rTransform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnComplete(() => {
                });
        }
    }

    public void UI_BtnClick()
    {
        if (mLessonData == null) return;
        if (mScrollRect != null)
        {
            mScrollRect.StopMovement();
        }

        // Show main menu window
        Signals.Get<MainMenuWindow_ShowSignal>().Dispatch(new MainMenuWindowProperties(mLessonData,
            null,
            transform.GetComponent<RectTransform>()));
    }
}
