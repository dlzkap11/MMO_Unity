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

    Coroutine co;
    #region Coroutine
    IEnumerator CoStopExplode(float seconds)
    {
        Debug.Log("Stop Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Stop Execute!");
        if (co != null)
        {
            StopCoroutine(co);
            co = null;
        }
    }

    IEnumerator ExplodeAfterSeconds(float seconds)
    {
        Debug.Log("Explode Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Explode Execute!");
        co = null;
    }
    #endregion

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        co = StartCoroutine("ExplodeAfterSeconds", 4.0f);

        StartCoroutine("CoStopExplode", 2.0f);

        Dictionary<int, Stat> dict = Managers.Data.StatDict;
    }



    public override void Clear()
    {
        
    }
}
