using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryBannerItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TitleLabel = null;
    [SerializeField] Rectangle icon = null;
    [SerializeField] GameObject m_ContentRoot = null;

    public GameObject ContentRootObj { get {return m_ContentRoot; }}

    public void SetData(TopicData.Topic _data)
    {
        m_TitleLabel.text = _data.en;
        icon.sprite = _data.icon;
    }
}
