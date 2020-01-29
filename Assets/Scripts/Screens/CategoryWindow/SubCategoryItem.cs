using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using UnityEngine;
using UnityEngine.UI;

public class SubCategoryItem : MonoBehaviour
{
    [SerializeField] Text titleLabel = null;
    [SerializeField] Rectangle icon= null;

    public void SetData(LessonData.Lesson _data)
    {
        titleLabel.text = _data.title;
        icon.Sprite = Resources.Load<Sprite>("Images/lesson_" + _data.id);

        // scale animation when item created
        RectTransform rTransform = transform as RectTransform;
        rTransform.localScale = new Vector3(0.02f, 0.02f, 1f);

        var scale = rTransform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.Linear)
            .SetUpdate(true);
    }
}
