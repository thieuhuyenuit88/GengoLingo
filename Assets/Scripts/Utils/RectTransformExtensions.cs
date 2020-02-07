using DG.Tweening;
using UnityEngine;

public enum AnchorPresets
{
    TopLeft,
    TopCenter,
    TopRight,

    MiddleLeft,
    MiddleCenter,
    MiddleRight,

    BottomLeft,
    BottonCenter,
    BottomRight,
    BottomStretch,

    VertStretchLeft,
    VertStretchRight,
    VertStretchCenter,

    HorStretchTop,
    HorStretchMiddle,
    HorStretchBottom,

    StretchAll
}

public enum PivotPresets
{
    TopLeft,
    TopCenter,
    TopRight,

    MiddleLeft,
    MiddleCenter,
    MiddleRight,

    BottomLeft,
    BottomCenter,
    BottomRight,
}

public static class RectTransformExtensions
{
    /// <summary>
    /// Set rect transform anchor
    /// </summary>
    /// <param name="source"></param>
    /// <param name="allign"></param>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    public static void SetAnchor(this RectTransform source, AnchorPresets allign, int offsetX = 0, int offsetY = 0)
    {
        source.anchoredPosition = new Vector3(offsetX, offsetY, 0);

        switch (allign)
        {
            case (AnchorPresets.TopLeft):
                {
                    source.anchorMin = new Vector2(0, 1);
                    source.anchorMax = new Vector2(0, 1);
                    break;
                }
            case (AnchorPresets.TopCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 1);
                    source.anchorMax = new Vector2(0.5f, 1);
                    break;
                }
            case (AnchorPresets.TopRight):
                {
                    source.anchorMin = new Vector2(1, 1);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
            case (AnchorPresets.MiddleLeft):
                {
                    source.anchorMin = new Vector2(0, 0.5f);
                    source.anchorMax = new Vector2(0, 0.5f);
                    break;
                }
            case (AnchorPresets.MiddleCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0.5f);
                    source.anchorMax = new Vector2(0.5f, 0.5f);
                    break;
                }
            case (AnchorPresets.MiddleRight):
                {
                    source.anchorMin = new Vector2(1, 0.5f);
                    source.anchorMax = new Vector2(1, 0.5f);
                    break;
                }
            case (AnchorPresets.BottomLeft):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(0, 0);
                    break;
                }
            case (AnchorPresets.BottonCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0);
                    source.anchorMax = new Vector2(0.5f, 0);
                    break;
                }
            case (AnchorPresets.BottomRight):
                {
                    source.anchorMin = new Vector2(1, 0);
                    source.anchorMax = new Vector2(1, 0);
                    break;
                }
            case (AnchorPresets.HorStretchTop):
                {
                    source.anchorMin = new Vector2(0, 1);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
            case (AnchorPresets.HorStretchMiddle):
                {
                    source.anchorMin = new Vector2(0, 0.5f);
                    source.anchorMax = new Vector2(1, 0.5f);
                    break;
                }
            case (AnchorPresets.HorStretchBottom):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(1, 0);
                    break;
                }
            case (AnchorPresets.VertStretchLeft):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(0, 1);
                    break;
                }
            case (AnchorPresets.VertStretchCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0);
                    source.anchorMax = new Vector2(0.5f, 1);
                    break;
                }
            case (AnchorPresets.VertStretchRight):
                {
                    source.anchorMin = new Vector2(1, 0);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
            case (AnchorPresets.StretchAll):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
        }
    }

    /// <summary>
    /// Set anchor with Tween
    /// </summary>
    /// <param name="source"></param>
    /// <param name="allign"></param>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    /// <param name="duration"></param>
    /// <param name="ease"></param>
    /// <returns></returns>
    public static Tween SetAnchorTween(this RectTransform source, AnchorPresets allign, float offsetX = 0, float offsetY = 0, float duration = 1, Ease ease = Ease.OutCubic)
    {
        Vector2 anchorMin = Vector2.zero;
        Vector2 anchorMax = Vector2.zero;
        switch (allign)
        {
            case (AnchorPresets.TopLeft):
                {
                    anchorMin = new Vector2(0, 1);
                    anchorMax = new Vector2(0, 1);
                    break;
                }
            case (AnchorPresets.TopCenter):
                {
                    anchorMin = new Vector2(0.5f, 1);
                    anchorMax = new Vector2(0.5f, 1);
                    break;
                }
            case (AnchorPresets.TopRight):
                {
                    anchorMin = new Vector2(1, 1);
                    anchorMax = new Vector2(1, 1);
                    break;
                }
            case (AnchorPresets.MiddleLeft):
                {
                    anchorMin = new Vector2(0, 0.5f);
                    anchorMax = new Vector2(0, 0.5f);
                    break;
                }
            case (AnchorPresets.MiddleCenter):
                {
                    anchorMin = new Vector2(0.5f, 0.5f);
                    anchorMax = new Vector2(0.5f, 0.5f);
                    break;
                }
            case (AnchorPresets.MiddleRight):
                {
                    anchorMin = new Vector2(1, 0.5f);
                    anchorMax = new Vector2(1, 0.5f);
                    break;
                }
            case (AnchorPresets.BottomLeft):
                {
                    anchorMin = new Vector2(0, 0);
                    anchorMax = new Vector2(0, 0);
                    break;
                }
            case (AnchorPresets.BottonCenter):
                {
                    anchorMin = new Vector2(0.5f, 0);
                    anchorMax = new Vector2(0.5f, 0);
                    break;
                }
            case (AnchorPresets.BottomRight):
                {
                    anchorMin = new Vector2(1, 0);
                    anchorMax = new Vector2(1, 0);
                    break;
                }
            case (AnchorPresets.HorStretchTop):
                {
                    anchorMin = new Vector2(0, 1);
                    anchorMax = new Vector2(1, 1);
                    break;
                }
            case (AnchorPresets.HorStretchMiddle):
                {
                    anchorMin = new Vector2(0, 0.5f);
                    anchorMax = new Vector2(1, 0.5f);
                    break;
                }
            case (AnchorPresets.HorStretchBottom):
                {
                    anchorMin = new Vector2(0, 0);
                    anchorMax = new Vector2(1, 0);
                    break;
                }
            case (AnchorPresets.VertStretchLeft):
                {
                    anchorMin = new Vector2(0, 0);
                    anchorMax = new Vector2(0, 1);
                    break;
                }
            case (AnchorPresets.VertStretchCenter):
                {
                    anchorMin = new Vector2(0.5f, 0);
                    anchorMax = new Vector2(0.5f, 1);
                    break;
                }
            case (AnchorPresets.VertStretchRight):
                {
                    anchorMin = new Vector2(1, 0);
                    anchorMax = new Vector2(1, 1);
                    break;
                }
            case (AnchorPresets.StretchAll):
                {
                    anchorMin = new Vector2(0, 0);
                    anchorMax = new Vector2(1, 1);
                    break;
                }
        }

        source.DOAnchorMin(anchorMin, duration).SetEase(ease);
        source.DOAnchorMax(anchorMax, duration).SetEase(ease);

        return source.DOAnchorPos(new Vector2(offsetX, offsetY), duration).SetEase(ease);
    }

    /// <summary>
    /// Set rect transform pivot
    /// </summary>
    /// <param name="source"></param>
    /// <param name="preset"></param>
    public static void SetPivot(this RectTransform source, PivotPresets preset)
    {
        switch (preset)
        {
            case (PivotPresets.TopLeft):
                {
                    source.pivot = new Vector2(0, 1);
                    break;
                }
            case (PivotPresets.TopCenter):
                {
                    source.pivot = new Vector2(0.5f, 1);
                    break;
                }
            case (PivotPresets.TopRight):
                {
                    source.pivot = new Vector2(1, 1);
                    break;
                }
            case (PivotPresets.MiddleLeft):
                {
                    source.pivot = new Vector2(0, 0.5f);
                    break;
                }
            case (PivotPresets.MiddleCenter):
                {
                    source.pivot = new Vector2(0.5f, 0.5f);
                    break;
                }
            case (PivotPresets.MiddleRight):
                {
                    source.pivot = new Vector2(1, 0.5f);
                    break;
                }
            case (PivotPresets.BottomLeft):
                {
                    source.pivot = new Vector2(0, 0);
                    break;
                }
            case (PivotPresets.BottomCenter):
                {
                    source.pivot = new Vector2(0.5f, 0);
                    break;
                }
            case (PivotPresets.BottomRight):
                {
                    source.pivot = new Vector2(1, 0);
                    break;
                }
        }
    }

    /// <summary>
    /// Set pivot with tween
    /// </summary>
    /// <param name="source"></param>
    /// <param name="preset"></param>
    /// <param name="duration"></param>
    /// <param name="ease"></param>
    /// <returns></returns>
    public static Tween SetPivotTween(this RectTransform source, PivotPresets preset, float duration = 1, Ease ease = Ease.OutCubic)
    {
        Vector2 pivotVector2 = Vector2.zero;
        switch (preset)
        {
            case (PivotPresets.TopLeft):
                {
                    pivotVector2 = new Vector2(0, 1);
                    break;
                }
            case (PivotPresets.TopCenter):
                {
                    pivotVector2 = new Vector2(0.5f, 1);
                    break;
                }
            case (PivotPresets.TopRight):
                {
                    pivotVector2 = new Vector2(1, 1);
                    break;
                }
            case (PivotPresets.MiddleLeft):
                {
                    pivotVector2 = new Vector2(0, 0.5f);
                    break;
                }
            case (PivotPresets.MiddleCenter):
                {
                    pivotVector2 = new Vector2(0.5f, 0.5f);
                    break;
                }
            case (PivotPresets.MiddleRight):
                {
                    pivotVector2 = new Vector2(1, 0.5f);
                    break;
                }
            case (PivotPresets.BottomLeft):
                {
                    pivotVector2 = new Vector2(0, 0);
                    break;
                }
            case (PivotPresets.BottomCenter):
                {
                    pivotVector2 = new Vector2(0.5f, 0);
                    break;
                }
            case (PivotPresets.BottomRight):
                {
                    pivotVector2 = new Vector2(1, 0);
                    break;
                }
        }
        return source.DOPivot(pivotVector2, duration).SetEase(ease);
    }

    public static RectTransform Left(this RectTransform rt, float x)
    {
        rt.offsetMin = new Vector2(x, rt.offsetMin.y);
        return rt;
    }

    public static RectTransform Right(this RectTransform rt, float x)
    {
        rt.offsetMax = new Vector2(-x, rt.offsetMax.y);
        return rt;
    }

    public static RectTransform Bottom(this RectTransform rt, float y)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, y);
        return rt;
    }

    public static RectTransform Top(this RectTransform rt, float y)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -y);
        return rt;
    }

    public static void SetOffset(this RectTransform rt, float left, float top, float right, float bottom)
    {
        rt.offsetMin = new Vector2(left, bottom);
        rt.offsetMax = new Vector2(-right, -top);
    }
}
