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
        base.SendRequest(data);

    }

    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');

        base.OnResponse(data);
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        loginPanel.OnLoginResponse(returnCode);
        if (returnCode == ReturnCode.Success)
        {
            string username = strs[1];
            int totalCount = int.Parse(strs[2]);
            int winCount = int.Parse(strs[3]);
            UserData user = new UserData(username,totalCount,winCount);
            facade.SetUserData(user);
        }
    }
}
