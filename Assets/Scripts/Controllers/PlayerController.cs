using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill,
    }

    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)(Define.Layer.Monster));
    GameObject _lockTarget;

    PlayerStat _stat;
    Vector3 _desPos;


    [SerializeField]
    PlayerState _state = PlayerState.Idle;

    public PlayerState State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case PlayerState.Die:
                    break;
                case PlayerState.Idle:                                    
                    anim.CrossFade("WAIT", 0.1f);

                    
                    //anim.SetFloat("speed", 0);
                    //anim.SetBool("attack", false);
                    
                    break;
                case PlayerState.Moving:
                    anim.CrossFade("RUN", 0.1f);

                    //anim.SetFloat("speed", _stat.MoveSpeed);
                    //anim.SetBool("attack", false);
                    break;
                case PlayerState.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0);

                    //anim.SetBool("attack", true);
                    break;
                

            }
        }
    }

    void Start()
    {
        _stat = GetComponent<PlayerStat>();
        // 방향벡터
        // 1. 거리(크기)    5
        // 2. 실제 방향     ->

        Managers.Input.MouseEventAction -= OnMouseEvent;
        Managers.Input.MouseEventAction += OnMouseEvent;



        //Managers.Resource.Instantiate("UI/UI_Button");

        //Temp
        //UI_Button ui= Managers.UI.ShowPopupUI<UI_Button>();
        //Managers.UI.ClosePopupUI(ui); //크로스 체크

        //Managers.UI.ShowSceneUI<UI_Inven>();

    }
    // GameObject (player)
    // Transfrom
    // PlayerController ()...  

    void UpdateDie()
    {
        
    }

    void UpdateMoving()
    {
        //몬스터가 내 사정거리보다 가까우면 공격
        if(_lockTarget != null)
        {
            float distance = (_desPos - transform.position).magnitude;
            if(distance <= 1)
            {
                State = PlayerState.Skill;
                return;
            }
        }


        //이동
        Vector3 dir = _desPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            //nma.CalculatePath
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block"))) 
            {
                if(!Input.GetMouseButton(0))
                    State = PlayerState.Idle;
                return;
            }

            //transform.position += dir.normalized * moveDist;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
        
    }

    void UpdateIdle()
    {
        
    }

    void UpdateSkill()
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

        if (_stopSkill)
        {
            State = PlayerState.Idle;
        }
        else
        {
            State = PlayerState.Skill;
        }

        
    }

    void OnRunEvent()
    {
        Debug.Log($"뚜벅뚜벅 --");
    }


    void Update()
    {
        switch (State)
        {
            case PlayerState.Die:
                UpdateDie();
                break;

            case PlayerState.Moving:
                UpdateMoving();
                break;

            case PlayerState.Idle:
                UpdateIdle(); 
                break;

            case PlayerState.Skill:
                UpdateSkill();
                break;

        }

    }

    bool _stopSkill = false;

    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case PlayerState.Idle:
                OnMouseEvent_IdleRun(evt);
                break;

            case PlayerState.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Skill:
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
        if (State == PlayerState.Die)
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
                        State = PlayerState.Moving;
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
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;

        }
    }
}
