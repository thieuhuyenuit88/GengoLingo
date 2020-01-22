using deVoid.UIFramework;
using deVoid.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// DATA
[Serializable]
public class TestPopupProperties : WindowProperties
{
    public readonly string title;

    public readonly Action CloseAction;

    public TestPopupProperties(string _title, Action _closeAction) {
        title = _title;
        CloseAction = _closeAction;
    }
}

// EVENT
public class TestPopup_ShowSignal : ASignal<TestPopupProperties> { }

// CONTROLLER
public class TestPopupController : AWindowController<TestPopupProperties>
{
    [SerializeField] private TextMeshProUGUI titleLabel = null;
    [SerializeField] private GameObject closeBtn = null;

    protected override void OnPropertiesSet()
    {
        Properties.IsPopup = true;
        base.OnPropertiesSet();
        titleLabel.text = Properties.title;

        closeBtn.SetActive(Properties.CloseAction != null);
    }

    public void UI_CloseBtn()
    {
        UI_Close();
        if (Properties.CloseAction != null)
        {
            Properties.CloseAction();
        }
    }
}
