using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryBannerItem : MonoBehaviour
{
    [SerializeField] Text m_TitleLabel = null;
    [SerializeField] Image icon = null;
    [SerializeField] GameObject m_ContentRoot = null;

    public GameObject ContentRootObj { get {return m_ContentRoot; }}

    public void SetData(TopicData.Topic _data)
    {
        m_TitleLabel.text = _data.title;
        icon.sprite = Resources.Load<Sprite>("Images/topic_" + _data.id);
    }
}
