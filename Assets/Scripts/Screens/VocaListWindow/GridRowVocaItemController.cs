using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRowVocaItemController : MonoBehaviour
{
    [SerializeField] private List<GridVocaItemController> mItemList = null;

    public List<GridVocaItemController> ItemList { get => mItemList; set => mItemList = value; }

    public void Init()
    {
        foreach (var item in mItemList)
        {
            item.Init();
        }  
    }
}
