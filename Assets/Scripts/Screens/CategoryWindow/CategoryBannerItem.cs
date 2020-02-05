using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryBannerItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mTitleLabel = null;
    [SerializeField] Rectangle mIcon = null;
    [SerializeField] GameObject mContentRoot = null;

    public GameObject ContentRootObj { get {return mContentRoot; }}

    public void SetData(TopicMaster.Topic _data)
    {
        mTitleLabel.text = _data.en;
        mIcon.sprite = _data.icon;
    }
}
