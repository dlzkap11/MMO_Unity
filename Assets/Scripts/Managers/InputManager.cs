using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{

    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseEventAction = null;


    bool _press = false;

    public void OnUpdate()
    {
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
