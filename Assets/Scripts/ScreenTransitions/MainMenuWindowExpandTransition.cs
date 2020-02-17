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
                rTransform.SetAnchor(AnchorPresets.TopCenter);
                rTransform.position = canvasRectTransform.TransformPoint(canvasX, canvasY, 0.0f);
                rTransform.sizeDelta = new Vector2(canvasXB - canvasXA, canvasYB - canvasYA);

                //// Animation: set fit size to parent with tween
                rTransform.DOKill();
                Vector2 targetSize = new Vector2(1080f, 570f);
                Vector2 targetPos = new Vector2(0f, -285f);
                controller.TopHeader.RoundedProperties.UniformRadius = 50f;

                Sequence lrScaleSequence = DOTween.Sequence()
                    .Append(rTransform.DOSizeDelta(new Vector2(targetSize.x, rTransform.sizeDelta.y), mDurationHeight)
                        .SetEase(mEase));

                Sequence tbScaleSequence = DOTween.Sequence()
                    .Append(rTransform.DOAnchorPos(targetPos, mDurationHeight)
                            .SetEase(mEase))
                    .Join(rTransform.DOSizeDelta(new Vector2(targetSize.x, targetSize.y), mDurationHeight)
                        .SetEase(mEase))
                    .Join(DOTween.To(() => controller.TopHeader.RoundedProperties.UniformRadius, value => controller.TopHeader.RoundedProperties.UniformRadius = value, 0f, mDurationHeight)
                            .SetEase(mEase));

                Sequence sequence = DOTween.Sequence();
                sequence.Append(lrScaleSequence).Append(tbScaleSequence).OnComplete(
                    () => {
                        canvasGroup.alpha = 1f;
                        if (callWhenFinished != null)
                        {
                            callWhenFinished();
                        }
                    }
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
                rTransform.SetAnchor(AnchorPresets.TopCenter);
                Vector2 targetSize = new Vector2(canvasXB - canvasXA, canvasYB - canvasYA); ;
                controller.TopHeader.RoundedProperties.UniformRadius = 0f;

                canvasGroup.DOFade(0.5f, mDurationWidth + mDurationHeight).SetEase(Ease.InQuad)
                    .OnComplete(() => canvasGroup.alpha = 1f);

                Sequence lrScaleSequence = DOTween.Sequence()
                    .Append(rTransform.DOSizeDelta(new Vector2(targetSize.x, targetSize.y), mDurationWidth)
                            .SetEase(mEase));

                Sequence tbScaleSequence = DOTween.Sequence()
                    .Append(rTransform.DOMove(canvasRectTransform.TransformPoint(canvasX, canvasY, 0.0f), mDurationHeight)
                            .SetEase(mEase))
                    .Join(rTransform.DOSizeDelta(new Vector2(rTransform.sizeDelta.x, targetSize.y), mDurationHeight)
                            .SetEase(mEase))
                    .Join(DOTween.To(() => 0f, value => controller.TopHeader.RoundedProperties.UniformRadius = value, 50f, mDurationHeight)
                            .SetEase(mEase));

                Sequence sequence = DOTween.Sequence();
                sequence.Append(tbScaleSequence).Append(lrScaleSequence)
                    .OnComplete(
                    () =>
                    {
                        if (callWhenFinished != null)
                        {
                            callWhenFinished();
                        }
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
}
