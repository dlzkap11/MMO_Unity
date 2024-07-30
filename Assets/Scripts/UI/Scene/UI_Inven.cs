using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPannel,
    }


    
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPannel = Get<GameObject>((int)GameObjects.GridPannel);
        foreach(Transform child in gridPannel.transform)
            Managers.Resource.Destroy(child.gameObject);


        for (int i = 0; i < 8; i++)
        {
            GameObject Item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPannel.transform).gameObject;

            // item.GetorAddComponet...
            
            UI_Inven_Item invenitem = Item.GetOrAddComponet<UI_Inven_Item>();
            invenitem.SetInfo($"����� {i + 1}��");
        }
    }

}
