﻿using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class UpdateRoomRequest : BaseRequest
{
    private RoomPanel roomPanel;
    public override void Awake()
    {
        roomPanel = GetComponent<RoomPanel>();
        requestCode = RequestCode.Room;
        actionCode = ActionCode.UpdateRoom;
        base.Awake();
    }

    public override void OnResponse(string data)
    {
        UserData ud1 = null;
        UserData ud2 = null;

            string[] udStrArray = data.Split('|');
            ud1 = new UserData(udStrArray[0]);
        if(udStrArray.Length>1)
            ud2 = new UserData(udStrArray[1]);

        roomPanel.SetAllPlayerResSync(ud1,ud2);
    }
}
