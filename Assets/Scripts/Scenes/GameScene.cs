using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameScene : BaseScene
{
    // coroutine의 기능
    // 1. 함수의 상태를 저장/복원 가능
    // -> 엄청 오래 걸리는 작업을 잠시 끊거나, 원하는 타이밍에 함수를 잠시 stop 후 복원하는 경우
    // 2. return -> 내가 원하는 타입으로 가능 (class도 가능)

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();


        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
        Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
    }



    public override void Clear()
    {
        
    }
}
