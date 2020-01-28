using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubCategoryItem : MonoBehaviour
{
    [SerializeField] Text titleLabel = null;

    public void SetData(string _title)
    {
        titleLabel.text = _title;

        // scale animation when item created
        RectTransform rTransform = transform as RectTransform;
        rTransform.localScale = new Vector3(0.02f, 0.02f, 1f);

        var scale = rTransform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.Linear)
            .SetUpdate(true);
    }
}
