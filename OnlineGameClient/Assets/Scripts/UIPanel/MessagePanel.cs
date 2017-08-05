using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    private Text text;
    private float showTime = 1;
    private string message = null;
    private void Awake()
    {
        
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(message))
        {
            ShowMessage(message);
            message = null;
        }
    }
    public override void OnEnter()
    {
        base.OnEnter();
        text = GetComponent<Text>();
        text.enabled = false;
        uiManager.InjectPanel(this);

    }

    public void ShowMessageSync(string msg)
    {
        message = msg;
    }
    public void ShowMessage(string msg)
    {
        text.DOFade(1, showTime);
        text.text = msg;
        text.enabled = true;
        Invoke("Hide", showTime);
    }

    private void Hide()
    {
        text.DOFade(0, showTime);
    }
}
