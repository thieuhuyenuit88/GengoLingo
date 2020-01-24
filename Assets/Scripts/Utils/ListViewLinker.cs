using SuperScrollView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LoopListView2))]
public class ListViewLinker : MonoBehaviour
{
    [SerializeField] private LoopListView2 controllingListView = null;

    private LoopListView2 linkerListView;

    private void Awake()
    {
        linkerListView = GetComponent<LoopListView2>();
    }

    private void LateUpdate()
    {
        int nearestItemIndexOfControlling = controllingListView.CurSnapNearestItemIndex;
        int nearestItemIndex = linkerListView.CurSnapNearestItemIndex;

        if (nearestItemIndex == nearestItemIndexOfControlling) return;

        linkerListView.SetSnapTargetItemIndex(nearestItemIndexOfControlling);
    }
}
