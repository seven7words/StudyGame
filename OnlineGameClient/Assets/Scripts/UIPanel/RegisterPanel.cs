using System.Collections;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    private InputField usernameInputField;
    private InputField passwordInputField;
    private InputField rePasswordInputField;
    private RegisterRequest registerRequest;
    private void Start()
    {
        registerRequest = GetComponent<RegisterRequest>();
        usernameInputField = transform.Find("UsernameLabel/UsernameInput").GetComponent<InputField>();
        passwordInputField = transform.Find("PasswordLabel/PasswordInput").GetComponent<InputField>();
        rePasswordInputField = transform.Find("RePasswordLabel/RePasswordInput").GetComponent<InputField>();
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
        transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
        
    }
    public override void OnEnter()
    {
       base.OnEnter();
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.5f);

    }

    private void OnRegisterClick()
    {
        string msg = "";
        if (string.IsNullOrEmpty(usernameInputField.text))
        {
            msg += "用户名不能为空";
        }
        if (string.IsNullOrEmpty(passwordInputField.text))
        {
            msg += "\n密码不能为空";
        }
        if (passwordInputField.text!=rePasswordInputField.text)
        {
            msg += "\n密码不一致";
        }
        if (string.IsNullOrEmpty(msg))
        {
            //进行注册，发送到服务器端
            registerRequest.SendRequest(usernameInputField.text,passwordInputField.text);
        }
        else
        {
            uiManager.ShowMessage(msg);
            return;
        }
    }

    public void OnRegisterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            uiManager.ShowMessageSync("注册成功");
        }
        else
        {
            uiManager.ShowMessageSync("用户名重复，注册失败");
        }
    }
    private void OnCloseClick()
    {
        transform.DOScale(0, 0.5f);

        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.5f);
        tweener.OnComplete(() => uiManager.PopPanel());
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }

}
