using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPannel,
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

            // item.GetOrAddComponent...
            
            UI_Inven_Item invenitem = Item.GetOrAddComponent<UI_Inven_Item>();
            invenitem.SetInfo($"집행검 {i + 1}번");
        }
    }

}
