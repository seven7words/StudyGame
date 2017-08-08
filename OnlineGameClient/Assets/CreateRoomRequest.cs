using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class CreateRoomRequest : BaseRequest
{
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.CreateRoom;
        roomPanel = GetComponent<RoomPanel>();

        base.Awake();
    }

    
    public override void SendRequest()
    {
        base.SendRequest("r");
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode) int.Parse(data);
        if (returnCode == ReturnCode.Success)
        {
            UserData ud = facade.GetUserData();
            roomPanel.SetSetLocalPlayerResSync();
            //roomPanel.SetLocalPlayerRes(ud.Username,ud.WinCount.ToString(),ud.TotalCount.ToString());
        }
    }
}
