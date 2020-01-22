using deVoid.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

class ItemListViewController: MonoBehaviour
{
    [SerializeField] Text titleLabel = null;
    [SerializeField] Button itemBtn = null;

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

        TestPopupProperties testPopupData = new TestPopupProperties(_title, () =>
        {
            // Test popup close callback
            // Nothing to do
        });

        itemBtn.onClick.RemoveAllListeners();
        itemBtn.onClick.AddListener(() => {
            Signals.Get<TestPopup_ShowSignal>().Dispatch(testPopupData);
        });
    }

    private void OnDestroy()
    {
        itemBtn.onClick.RemoveAllListeners();
    }
}
