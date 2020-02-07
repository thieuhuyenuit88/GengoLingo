using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using deVoid.UIFramework;

public class MainMenuWindowExpandTransition : ATransitionComponent
{
    [SerializeField] protected float durationWidth = 0.15f;
    [SerializeField] protected float durationHeight = 0.15f;
    [SerializeField] protected Ease ease = Ease.Linear;

    public override void Animate(Transform target, Action callWhenFinished)
    {
        RectTransform rTransform = target as RectTransform;
        CanvasGroup canvasGroup = rTransform.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = rTransform.gameObject.AddComponent<CanvasGroup>();
        }

        MainMenuWindowController controller = rTransform.GetComponent<MainMenuWindowController>();
        var uiCamera = controller.UICamera;
        var rootTargetTransform = controller.RootTargetTransform;

        if (uiCamera != null)
        {
            var canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
            {
                throw new System.Exception("Couldn't find attached canvas??");
            }

            var canvasRectTransform = canvas.GetComponent<RectTransform>();

            // Calculate viewport/anchor points
            var min = rootTargetTransform.rect.min;
            var max = rootTargetTransform.rect.max;
            var targetA = rootTargetTransform.TransformPoint(min.x, min.y, 0.0f);
            var targetB = rootTargetTransform.TransformPoint(max.x, min.y, 0.0f);
            var targetC = rootTargetTransform.TransformPoint(min.x, max.y, 0.0f);
            var targetD = rootTargetTransform.TransformPoint(max.x, max.y, 0.0f);

            var renderMode = rootTargetTransform.GetComponentInParent<Canvas>().renderMode;
            var viewportPointA = WorldToViewportPoint(uiCamera, targetA, renderMode);
            var viewportPointB = WorldToViewportPoint(uiCamera, targetB, renderMode);
            var viewportPointC = WorldToViewportPoint(uiCamera, targetC, renderMode);
            var viewportPointD = WorldToViewportPoint(uiCamera, targetD, renderMode);

            var minX = Mathf.Min(Mathf.Min(viewportPointA.x, viewportPointB.x), Mathf.Min(viewportPointC.x, viewportPointD.x));
            var minY = Mathf.Min(Mathf.Min(viewportPointA.y, viewportPointB.y), Mathf.Min(viewportPointC.y, viewportPointD.y));
            var maxX = Mathf.Max(Mathf.Max(viewportPointA.x, viewportPointB.x), Mathf.Max(viewportPointC.x, viewportPointD.x));
            var maxY = Mathf.Max(Mathf.Max(viewportPointA.y, viewportPointB.y), Mathf.Max(viewportPointC.y, viewportPointD.y));

            // Convert viewport points to canvas points
            var canvasRect = canvasRectTransform.rect;
            var canvasXA = canvasRect.xMin + canvasRect.width * minX;
            var canvasYA = canvasRect.yMin + canvasRect.height * minY;
            var canvasXB = canvasRect.xMin + canvasRect.width * maxX;
            var canvasYB = canvasRect.yMin + canvasRect.height * maxY;

            // Find center, set anchor stretch, and convert canvas point to world point
            var canvasX = (canvasXA + canvasXB) * 0.5f;
            var canvasY = (canvasYA + canvasYB) * 0.5f;

            rTransform.anchorMin = Vector2.zero;
            rTransform.anchorMax = Vector2.one;
            rTransform.position = canvasRectTransform.TransformPoint(canvasX, canvasY, 0.0f);
            rTransform.SetOffset(canvasRect.width * minX,
                canvasRect.height * (1f - maxY),
                canvasRect.width * (1f - maxX),
                canvasRect.height * minY);

            // Animation: set fit size to parent with tween
            rTransform.DOKill();
            float leftOffset = rTransform.offsetMin.x;
            float botOffset = rTransform.offsetMin.y;
            float rightOffset = -rTransform.offsetMax.x;
            float topOffset = -rTransform.offsetMax.y;
            controller.Background.RoundedProperties.UniformRadius = 50f;

            Sequence lrScaleSequence = DOTween.Sequence()
                .Append(DOTween.To(() => leftOffset, value => leftOffset = value, 0f, durationWidth)
                        .SetEase(ease)
                        .OnUpdate(() => { rTransform.Left(leftOffset); }))
                .Join(DOTween.To(() => rightOffset, value => rightOffset = value, 0f, durationWidth)
                        .SetEase(ease)
                        .OnUpdate(() => { rTransform.Right(rightOffset); }));

            Sequence tbScaleSequence = DOTween.Sequence()
                .Append(DOTween.To(() => topOffset, value => topOffset = value, 0f, durationHeight)
                        .SetEase(ease)
                        .OnUpdate(() => { rTransform.Top(topOffset); }))
                .Join(DOTween.To(() => botOffset, value => botOffset = value, 0f, durationHeight)
                        .SetEase(ease)
                        .OnUpdate(() => { rTransform.Bottom(botOffset); }))
                .Join(DOTween.To(() => controller.Background.RoundedProperties.UniformRadius, value => controller.Background.RoundedProperties.UniformRadius = value, 0f, durationHeight)
                        .SetEase(ease));

            Sequence sequence = DOTween.Sequence();
            sequence.Append(lrScaleSequence).Append(tbScaleSequence).OnComplete(
                () => Cleanup(callWhenFinished, rTransform, canvasGroup)
            ).SetUpdate(true);

            sequence.Play();
        }
    }

    /// <summary>
    /// Convert position from world to viewport
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="point"></param>
    /// <param name="renderMode"></param>
    /// <returns></returns>
    private static Vector3 WorldToViewportPoint(Camera camera, Vector3 point, RenderMode renderMode)
    {
        if (renderMode == RenderMode.ScreenSpaceOverlay)
        {
            point = RectTransformUtility.WorldToScreenPoint(null, point);
            return camera.ScreenToViewportPoint(point);
        }
        else if (renderMode == RenderMode.ScreenSpaceCamera)
        {
            point = RectTransformUtility.WorldToScreenPoint(camera, point);
            return camera.ScreenToViewportPoint(point);
        }

        return camera.WorldToViewportPoint(point);
    }

    /// <summary>
    /// Reset value when animation completed
    /// </summary>
    /// <param name="callWhenFinished"></param>
    /// <param name="rTransform"></param>
    /// <param name="canvasGroup"></param>
    private void Cleanup(Action callWhenFinished, RectTransform rTransform, CanvasGroup canvasGroup)
    {
        callWhenFinished();
        rTransform.localScale = Vector3.one;
        rTransform.SetOffset(0f, 0f, 0f, 0f);
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        }
    }
}
