using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlashCardItemController : MonoBehaviour
{
    [SerializeField] GameObject mContentObj = null;
    [SerializeField] TextMeshProUGUI mTitleLabel = null;
    [SerializeField] Image mIcon = null;

    public GameObject ContentObj { get { return mContentObj; } }

    public void SetItemData(VocaMaster.Voca _data)
    {
        if (_data == null) return;

        mTitleLabel.text = _data.word + "\n" + _data.en;
        mIcon.sprite = _data.icon;
    }
}
