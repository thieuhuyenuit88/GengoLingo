using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CategoryBannerItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TitleLabel = null;
    [SerializeField] GameObject m_ContentRoot = null;

    public GameObject ContentRootObj { get {return m_ContentRoot; }}

    public void SetData(ItemData _data)
    {
        m_TitleLabel.text = _data.title;
    }
}
