using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{

    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseEventAction = null;


    bool _press = false;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(Input.anyKey && KeyAction != null )
            KeyAction.Invoke();

        if( MouseEventAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                MouseEventAction.Invoke(Define.MouseEvent.Press);
                _press = true;
            }
            else
            {
                if (_press)
                    MouseEventAction.Invoke(Define.MouseEvent.Click);
                _press = false;
            }
        }
    }
}
