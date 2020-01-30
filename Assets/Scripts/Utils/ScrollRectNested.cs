using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using SuperScrollView;

public class ScrollRectNested : ScrollRect
{
    [Header("Additional Fields")]
    [SerializeField] ScrollRect parentScrollRect = null;
    [SerializeField] bool blockParentScroll = true;

    private bool routeToParent = false;
    private LoopListView2 parentLoopListView;

    protected override void Awake()
    {
        base.Awake();

        if (parentScrollRect != null)
        {
            parentLoopListView = parentScrollRect.GetComponent<LoopListView2>();
        }
    }

    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        // Always route initialize potential drag event to parent
        if (parentScrollRect != null && !blockParentScroll)
        {
            ((IInitializePotentialDragHandler)parentScrollRect).OnInitializePotentialDrag(eventData);
        }
        base.OnInitializePotentialDrag(eventData);
    }

    public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (routeToParent)
        {
            if (parentScrollRect != null && !blockParentScroll)
            {
                ((IDragHandler)parentScrollRect).OnDrag(eventData);
                if (parentLoopListView != null)
                {
                    parentLoopListView.OnDrag(eventData);
                }
            }
        }
        else
        {
            base.OnDrag(eventData);
        }
    }

    public override void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (!horizontal && Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y))
        {
            routeToParent = true;
        }
        else if (!vertical && Math.Abs(eventData.delta.x) < Math.Abs(eventData.delta.y))
        {
            routeToParent = true;
        }
        else if (parentLoopListView != null && parentLoopListView.IsDraging)
        {
            routeToParent = true;
        }
        else
        {
            routeToParent = false;
        }

        if (routeToParent)
        {
            if (parentScrollRect != null && !blockParentScroll)
            {
                ((IBeginDragHandler)parentScrollRect).OnBeginDrag(eventData);
                if (parentLoopListView != null)
                {
                    parentLoopListView.OnBeginDrag(eventData);
                }
            }
        }
        else
        {
            base.OnBeginDrag(eventData);
        }
    }

    public override void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (routeToParent)
        {
            if (parentScrollRect != null && !blockParentScroll)
            {
                ((IEndDragHandler)parentScrollRect).OnEndDrag(eventData);
                if (parentLoopListView != null)
                {
                    parentLoopListView.OnEndDrag(eventData);
                }
            }
        }
        else
        {
            base.OnEndDrag(eventData);
        }
        routeToParent = false;
    }
}
