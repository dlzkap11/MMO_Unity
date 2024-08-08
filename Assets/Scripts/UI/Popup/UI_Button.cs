using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_Button : UI_Popup
{

    enum Buttons
    {
        PointBotton,
    }

    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons)); //enum 값 보내기
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));



        GetButton((int)Buttons.PointBotton).gameObject.BindEvent(OnButtonClicked);

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

        //UI_EventHandler evt  = go.GetComponent<UI_EventHandler>();
        //evt.OnDragHandler += (PointerEventData data) => { evt.gameObject.transform.position = data.position; };
    }



    int _score = 0;

    public void OnButtonClicked(PointerEventData data)
    {
        Debug.Log("Button Click!");

        _score++;
        GetText((int)Texts.ScoreText).text = $"점수 : {_score}점";
    }

}
