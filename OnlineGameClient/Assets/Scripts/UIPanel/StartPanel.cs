using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private Button loginButton;
    private Animator btnAnimator;
    public override void OnEnter()
    {
        base.OnEnter();
         loginButton = transform.Find("LoginButton").GetComponent<Button>();
        btnAnimator = loginButton.GetComponent<Animator>();
        loginButton.onClick.AddListener(OnLoginClick);
    }

    private void OnLoginClick()
    {
        uiManager.PushPanel(UIPanelType.Login);
    }

    public override void OnPause()
    {
        base.OnPause();
        btnAnimator.enabled = false;
       Tweener tweener =  loginButton.transform.DOScale(0, 0.4f);
        tweener.OnComplete(() => loginButton.gameObject.SetActive(false));
    }

    public override void OnResume()
    {
        base.OnResume();
        
        loginButton.gameObject.SetActive(true);

        loginButton.transform.DOScale(1, 0.4f).OnComplete(()=> btnAnimator.enabled = true);

    }
}
