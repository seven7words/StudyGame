using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Common
{
    public enum ActionCode
    {
        None,
        Login,
        Register,
        ListRoom,
        CreateRoom,
        JoinRoom,
        UpdateRoom,
        QuitRoom,
        StartGame,
        ShowTimer,
        StartPlay,
        Move,
        Shoot,
        Attack,
        GameOver,
    }
}
