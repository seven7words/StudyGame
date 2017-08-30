using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class StartGameRequest : BaseRequest {
    private RoomPanel roomPanel;
    public override void Awake()
    {
        roomPanel = GetComponent<RoomPanel>();
        requestCode = RequestCode.Game;
        actionCode = ActionCode.StartGame;
        base.Awake();
    }

    public override void SendRequest()
    {
        base.SendRequest("r");
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
   roomPanel.OnStartResponse(returnCode);
      
    }
}
