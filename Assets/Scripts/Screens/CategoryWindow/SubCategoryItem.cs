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

    public void SetData(LessonMaster.Lesson _data, bool _enableAnimate = true)
    {
        mLessonData = _data;
        if (mLessonData == null) return;
        
        mTitleLabel.text = _data.en;
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
        Signals.Get<MainMenuWindow_ShowSignal>().Dispatch(new MainMenuWindowProperties(mLessonData));
    }
}
