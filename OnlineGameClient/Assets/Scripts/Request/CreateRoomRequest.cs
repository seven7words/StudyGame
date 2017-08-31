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

    public void SetPanel(BasePanel panel)
    {
        roomPanel = panel as RoomPanel;
        
    }
    
    public override void SendRequest()
    {
        base.SendRequest("r");
    }

    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        ReturnCode returnCode = (ReturnCode) int.Parse(strs[0]);
        RoleType roleType = (RoleType)int.Parse(strs[1]);
        facade.SetCurrentRoleType(roleType);
        if (returnCode == ReturnCode.Success)
        {
            UserData ud = facade.GetUserData();
            roomPanel.SetSetLocalPlayerResSync();
            //roomPanel.SetLocalPlayerRes(ud.Username,ud.WinCount.ToString(),ud.TotalCount.ToString());
        }
    }
}
