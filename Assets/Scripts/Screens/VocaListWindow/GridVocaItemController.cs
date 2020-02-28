using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridVocaItemController : MonoBehaviour
{
    [SerializeField] private Image mIcon = null;
    [SerializeField] private TextMeshProUGUI mWordLabel = null;

    public void Init()
    {
       // TODO: Add button/toggle event listener
    }

    public void SetItemData(VocaMaster.Voca _vocaData)
    {
        if (_vocaData == null) return;

        mWordLabel.text = _vocaData.word + "\n" + _vocaData.en;
        mIcon.sprite = _vocaData.icon;
    }
}
