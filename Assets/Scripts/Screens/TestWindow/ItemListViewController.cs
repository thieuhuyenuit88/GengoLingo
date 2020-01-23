using deVoid.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

class ItemListViewController: MonoBehaviour
{
    [SerializeField] Text titleLabel = null;

    private TestPopupProperties testPopupData = null;

    public void SetData(string _title)
    {
        titleLabel.text = _title;

        // scale animation when item created
        RectTransform rTransform = transform as RectTransform;
        rTransform.localScale = new Vector3(0.02f, 0.02f, 1f);

        rTransform.DOScale(new Vector3(1f, 1f, 1f), 0.25f)
            .SetEase(Ease.Linear)
            .SetUpdate(true)
            .OnComplete(() =>
            {

            });

        testPopupData = new TestPopupProperties(_title, () =>
        {
            // Test popup close callback
            // Nothing to do
        });
    }

    public void UI_ItemBtn()
    {
        Signals.Get<TestPopup_ShowSignal>().Dispatch(testPopupData);
    }
}
