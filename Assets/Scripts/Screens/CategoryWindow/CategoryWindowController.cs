using deVoid.UIFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryWindowController : AWindowController
{
    private void Start()
    {
        
    }

    protected override void AddListeners()
    {
        OutTransitionFinished += OnOutAnimationFinished;
    }

    protected override void RemoveListeners()
    {
        OutTransitionFinished -= OnOutAnimationFinished;
    }

    private void OnOutAnimationFinished(IUIScreenController screen)
    {
       
    }
}
