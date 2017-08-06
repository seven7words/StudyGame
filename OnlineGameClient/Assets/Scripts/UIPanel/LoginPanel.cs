using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    private Button closeButton;
    private InputField usernameInputField;
    private InputField passwordInputField;

    private LoginRequest loginRequest;
    //private Button loginButton;
    ///private Button registerButton;
    private void Start()
    {
        loginRequest = GetComponent<LoginRequest>();
        usernameInputField = transform.Find("UsernameLabel/UsernameInput").GetComponent<InputField>();
        passwordInputField = transform.Find("PasswordLabel/PasswordInput").GetComponent<InputField>();
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);
        transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(OnLoginClick);

        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRigisterClick);

    }
    public override void OnEnter()
    {
        base.OnEnter();
      EnterAnim();
       

    }

    private void OnRigisterClick()
    {
        PlayClickSound();
        uiManager.PushPanel(UIPanelType.Register);
    }

    private void OnLoginClick()
    {
        PlayClickSound();
        string msg = "";
        if (string.IsNullOrEmpty(usernameInputField.text))
        {
            msg += "用户名不能为空";
        }
        if (string.IsNullOrEmpty(passwordInputField.text))
        {
            msg += "密码不能为空";
        }
        if (string.IsNullOrEmpty(msg))
        {
            //TODO 发送到服务器端进行验证
            loginRequest.SendRequest(usernameInputField.text,passwordInputField.text);
            
        }
        else
        {
            uiManager.ShowMessage(msg);
            return;
        }
    }

    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            //TODO
            uiManager.PushPanelSync(UIPanelType.RoomList);
        }
        else
        {
            uiManager.ShowMessageSync("用户名或密码错误，无法登陆，请重新输入");
        }
    }

    public override void OnPause()
    {
        HideAnim();
    }

    public override void OnResume()
    {
        EnterAnim();
    }

    private void OnCloseClick()
    {
        PlayClickSound();
       uiManager.PopPanel();
    }

    public override void OnExit()
    {
        base.OnExit();
        HideAnim();
    }

    private void EnterAnim()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.5f);
    }

    private void HideAnim()
    {
        gameObject.SetActive(false);
    
        transform.DOScale(0, 0.2f);

        transform.DOLocalMoveX(1000, 0.2f).OnComplete(() => gameObject.SetActive(false));
    }
}
