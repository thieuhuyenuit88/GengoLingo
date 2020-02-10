using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using deVoid.UIFramework;

public class MainMenuWindowExpandTransition : ATransitionComponent
{
    [SerializeField] protected bool mIsOutAnimation = false;
    [SerializeField] protected float mDurationWidth  = 0.15f;
    [SerializeField] protected float mDurationHeight = 0.25f;
    [SerializeField] protected Ease mEase = Ease.Linear;

    public override void Animate(Transform target, Action callWhenFinished)
    {
        CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = target.gameObject.AddComponent<CanvasGroup>();
        }

        MainMenuWindowController controller = target.GetComponent<MainMenuWindowController>();
        RectTransform rTransform = controller.TopHeader.GetComponent<RectTransform>();

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

            if (!mIsOutAnimation)
            {
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
                controller.TopHeader.RoundedProperties.UniformRadius = 50f;

                Sequence lrScaleSequence = DOTween.Sequence()
                    .Append(DOTween.To(() => leftOffset, value => leftOffset = value, 0f, mDurationWidth)
                            .SetEase(mEase)
                            .OnUpdate(() => { rTransform.Left(leftOffset); }))
                    .Join(DOTween.To(() => rightOffset, value => rightOffset = value, 0f, mDurationWidth)
                            .SetEase(mEase)
                            .OnUpdate(() => { rTransform.Right(rightOffset); }));

                Sequence tbScaleSequence = DOTween.Sequence()
                    .Append(DOTween.To(() => topOffset, value => topOffset = value, 0f, mDurationHeight)
                            .SetEase(mEase)
                            .OnUpdate(() => { rTransform.Top(topOffset); }))
                    .Join(DOTween.To(() => botOffset, value => botOffset = value, 1300f, mDurationHeight)
                            .SetEase(mEase)
                            .OnUpdate(() => { rTransform.Bottom(botOffset); }))
                    .Join(DOTween.To(() => controller.TopHeader.RoundedProperties.UniformRadius, value => controller.TopHeader.RoundedProperties.UniformRadius = value, 0f, mDurationHeight)
                            .SetEase(mEase));

                Sequence sequence = DOTween.Sequence();
                sequence.Append(lrScaleSequence).Append(tbScaleSequence).OnComplete(
                    () => Cleanup(callWhenFinished, rTransform, canvasGroup)
                ).SetUpdate(true);

                sequence.Play();

                TopHeaderController topHeader = controller.TopHeader.GetComponent<TopHeaderController>();
                if (topHeader != null)
                {
                    topHeader.PlayOpenAnimation();
                }
            }
            else
            {
                // Zoom out
                // Animation: set fit size to root target with tween
                rTransform.DOKill();
                float leftOffset = canvasRect.width * minX;
                float botOffset = canvasRect.height * minY;
                float rightOffset = canvasRect.width * (1f - maxX);
                float topOffset = canvasRect.height * (1f - maxY);
                controller.TopHeader.RoundedProperties.UniformRadius = 0f;

                canvasGroup.DOFade(0.5f, mDurationWidth + mDurationHeight).SetEase(Ease.InQuad)
                    .OnComplete(() =>
                    {
                        canvasGroup.alpha = 1f;
                    });

                Sequence lrScaleSequence = DOTween.Sequence()
                    .Append(DOTween.To(() => 0f, value => rTransform.Left(value), leftOffset, mDurationWidth)
                            .SetEase(mEase))
                    .Join(DOTween.To(() => 0f, value => rTransform.Right(value), rightOffset, mDurationWidth)
                            .SetEase(mEase));

                Sequence tbScaleSequence = DOTween.Sequence()
                    .Append(DOTween.To(() => 0f, value => rTransform.Top(value), topOffset, mDurationHeight)
                            .SetEase(mEase))
                    .Join(DOTween.To(() => 1300f, value => rTransform.Bottom(value), botOffset, mDurationHeight)
                            .SetEase(mEase))
                    .Join(DOTween.To(() => 0f, value => controller.TopHeader.RoundedProperties.UniformRadius = value, 50f, mDurationHeight)
                            .SetEase(mEase));

                Sequence sequence = DOTween.Sequence();
                sequence.Append(tbScaleSequence).Append(lrScaleSequence).OnComplete(
                    () =>
                    {
                        rTransform.SetOffset(leftOffset, topOffset, rightOffset, botOffset);

                        callWhenFinished();
                    }
                ).SetUpdate(true);

                sequence.Play();

                TopHeaderController topHeader = controller.TopHeader.GetComponent<TopHeaderController>();
                if (topHeader != null)
                {
                    topHeader.PlayCloseAnimation();
                }
            }
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
        rTransform.SetOffset(0f, 0f, 0f, 1300f);
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        }
    }
}
