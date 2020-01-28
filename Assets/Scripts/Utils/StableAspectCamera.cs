using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StableAspectCamera : MonoBehaviour
{
    [SerializeField] public Vector2 aspect = new Vector2(1080, 1920);

    private float mAspectRate;
    private Camera mCamera;

    void Start()
    {
        mAspectRate = (float)aspect.x / aspect.y;
        mCamera = GetComponent<Camera>();

        if (mCamera != null)
            UpdateCameraRect();
    }

    void UpdateCameraRect()
    {
        float baseAspect = aspect.y / aspect.x;
        float nowAspect = (float)Screen.height / Screen.width;

        if (baseAspect > nowAspect)
        {
            var changeAspect = nowAspect / baseAspect;
            mCamera.rect = new Rect((1 - changeAspect) * 0.5f, 0, changeAspect, 1);
        }
        else
        {
            var changeAspect = baseAspect / nowAspect;
            mCamera.rect = new Rect(0, (1 - changeAspect) * 0.5f, 1, changeAspect);
        }
    }

    bool IsChangeAspect()
    {
        return mCamera.aspect != mAspectRate;
    }

    void Update()
    {
        if (!IsChangeAspect() || mCamera == null)
            return;

        UpdateCameraRect();
        mCamera.ResetAspect();
    }
}
