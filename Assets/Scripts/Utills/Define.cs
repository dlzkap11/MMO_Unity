using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Layer
    {
        Monster = 6,
        Ground = 7,
        Block = 8,

    }   

    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        BGM,
        Effact,
        MaxCount,
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }


    public enum CameraMode
    {
        QuarterView,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }
}
