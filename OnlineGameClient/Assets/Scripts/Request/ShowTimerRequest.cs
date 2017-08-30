using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class ShowTimerRequest : BaseRequest {

    private GamePanel gamePanel;
    public override void Awake()
    {
        gamePanel = GetComponent<GamePanel>();
        //requestCode = RequestCode.Game;
        actionCode = ActionCode.ShowTimer;
        base.Awake();
    }

    public override void OnResponse(string data)
    {
        int time = int.Parse(data);
        gamePanel.ShowTimeSync(time);
    }
}
