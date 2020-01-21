using deVoid.UIFramework;
using deVoid.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWindow_ReloadSignal : ASignal { }

public class TestWindowController : AWindowController
{
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
        Signals.Get<TestWindow_ReloadSignal>().Dispatch();
    }

    public void UI_ReloadBtn()
    {
        CloseRequest(this);
    }
}
