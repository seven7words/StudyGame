using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class LoginRequest : BaseRequest
{
    private LoginPanel loginPanel;
	// Use this for initialization
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        loginPanel = GetComponent<LoginPanel>();
        //先赋值，然后在放进集合里
        base.Awake();
    }



    public void SendRequest(string username,string password)
    {
       
        string data = username + "," + password;
        base.SendRequset(data);

    }

    public override void OnResponse(string data)
    {
        base.OnResponse(data);
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        loginPanel.OnLoginResponse(returnCode);
    }
}
