using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerController : BaseController
{
    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)(Define.Layer.Monster));

    PlayerStat _stat;
    bool _stopSkill = false;


    public override void Init()
    {

        WorldObjectType = Define.WorldObject.Player;
        _stat = GetComponent<PlayerStat>();
        // ���⺤��
        // 1. �Ÿ�(ũ��)    5
        // 2. ���� ����     ->
        Managers.Input.MouseEventAction -= OnMouseEvent;
        Managers.Input.MouseEventAction += OnMouseEvent;

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);


        //Managers.Resource.Instantiate("UI/UI_Button");

        //Temp
        //UI_Button ui= Managers.UI.ShowPopupUI<UI_Button>();
        //Managers.UI.ClosePopupUI(ui); //ũ�ν� üũ

        //Managers.UI.ShowSceneUI<UI_Inven>();

    }
    // GameObject (player)
    // Transfrom
    // PlayerController ()...  



    protected override void UpdateMoving()
    {
        //���Ͱ� �� �����Ÿ����� ������ ����
        if(_lockTarget != null)
        {
            //_desPos = _lockTarget.transform.position;
            float distance = (_desPos - transform.position).magnitude;
            if(distance <= 1)
            {
                State = Define.State.Skill;
                return;
            }
        }


        //�̵�
        Vector3 dir = _desPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block"))) 
            {
                if(!Input.GetMouseButton(0))
                    State = Define.State.Idle;
                return;
            }

            //transform.position += dir.normalized * moveDist;
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
        
    }

    protected override void UpdateSkill()
    {
        if(_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
        
    }

    void OnHitEvent()
    {

        if(_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttackted(_stat);
        }

        if (_stopSkill)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill;
        }

        
    }

    void OnRunEvent()
    {
        Debug.Log($"�ѹ��ѹ� --");
    }

    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;

            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
                {
                    if(evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
        }
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        //if (evt != Define.MouseEvent.Click)
        //    return;
        if (State == Define.State.Die)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        _desPos = hit.point;
                        _desPos.y = 0;
                        State = Define.State.Moving;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;

            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _desPos = hit.point;
                    _desPos.y = 0;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }
}
