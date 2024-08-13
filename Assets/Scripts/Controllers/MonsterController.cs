using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;
    Coroutine _coroutine;

    [SerializeField]
    float _scanRange = 10;
    [SerializeField]
    float _attackRange = 2;

    public void OnEnable()
    {
        _stat.Hp = _stat.MaxHp;
        State =Define.State.Idle;
        _lockTarget = null;
    }
    public override void Init()
    {

        WorldObjectType = Define.WorldObject.Monster;
        _stat = GetComponent<Stat>();
        
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null )        
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected override void UpdateIdle()
    {
        GameObject player = Managers.Game.GetPlayer();
        if (player == null)
            return;
      
        float distance = (player.transform.position - transform.position).magnitude;

        if(distance <= _scanRange)
        {
            _lockTarget = player;

            State = Define.State.Moving;
            return;
        }
    }

    protected override void UpdateMoving()
    {
        //�÷��̾ �� �����Ÿ����� ������ ����
        if (_lockTarget != null)
        {
            _desPos = _lockTarget.transform.position;
            float distance = (_desPos - transform.position).magnitude;
            if (distance <= _attackRange)
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);
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
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();        

            nma.SetDestination(_desPos);
            nma.speed = _stat.MoveSpeed;

            //transform.position += dir.normalized * moveDist;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }

    }
    protected override void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<PlayerStat>();
            targetStat.OnAttackted(_stat);

            if (targetStat.Hp <= 0)
            {
                //Managers.Game.Despawn(targetStat.gameObject);
            }
            else
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _attackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }

        }

        else
        {
            _lockTarget = null;
            State = Define.State.Idle;
        }

    }
}