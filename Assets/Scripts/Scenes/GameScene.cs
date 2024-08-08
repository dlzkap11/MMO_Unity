using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameScene : BaseScene
{
    // coroutine�� ���
    // 1. �Լ��� ���¸� ����/���� ����
    // -> ��û ���� �ɸ��� �۾��� ��� ���ų�, ���ϴ� Ÿ�ֿ̹� �Լ��� ��� stop �� �����ϴ� ���
    // 2. return -> ���� ���ϴ� Ÿ������ ���� (class�� ����)

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
