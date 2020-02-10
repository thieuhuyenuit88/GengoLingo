using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using TMPro;
using UnityEngine;

public class TopHeaderController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mTitleLabel = null;
    [SerializeField] Rectangle mIcon = null;
    [SerializeField] DOTweenAnimation openTweenAnimation = null;

    private LessonMaster.Lesson mLessonData;

    public void SetData(LessonMaster.Lesson _data)
    {
        mLessonData = _data;
        if (mLessonData == null) return;

        mTitleLabel.text = _data.title + "\n" + _data.en;
        mIcon.sprite = _data.icon;
    }

    public void PlayOpenAnimation()
    {
        openTweenAnimation.DORestartAllById("animIn");
    }

    public void PlayCloseAnimation()
    {
        openTweenAnimation.DORewindAllById("animIn");
    }
}