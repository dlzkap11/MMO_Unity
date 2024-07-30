using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    Vector3 _desPos;


    void Start()
    {          

        // ���⺤��
        // 1. �Ÿ�(ũ��)    5
        // 2. ���� ����     ->

        Managers.Input.KeyAction -= OnKeyboard; //������ �ʱ�ȭ
        Managers.Input.KeyAction += OnKeyboard;

        Managers.Input.MouseEventAction -= OnMouseClicked;
        Managers.Input.MouseEventAction += OnMouseClicked;

        //Managers.Resource.Instantiate("UI/UI_Button");

        //Temp
        //UI_Button ui= Managers.UI.ShowPopupUI<UI_Button>();
        //Managers.UI.ClosePopupUI(ui); //ũ�ν� üũ

        Managers.UI.ShowSceneUI<UI_Inven>();

    }

    // GameObject (player)
    // Transfrom
    // PlayerController ()...  


    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
    }

    PlayerState _state = PlayerState.Idle;


    void UpdateDie()
    {
        
    }

    void UpdateMoving()
    {
        Vector3 dir = _desPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }

        //�ִϸ��̼� ó��
        Animator anim = GetComponent<Animator>();
        //���� ���� ���¿� ���� ������ �Ѱ��ش�
        anim.SetFloat("speed", _speed);
    }

    void UpdateIdle()
    {
        //�ִϸ��̼� ó��
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }



    void OnRunEvent(string a)
    {
        Debug.Log($"�ѹ��ѹ� -- {a}");
    }


    void Update()
    {
            

        switch (_state)
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

        }

    }

    void OnKeyboard()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            //transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            //transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            //transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        //if (evt != Define.MouseEvent.Click)
        //    return;

        if (_state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _desPos = hit.point;
            _state = PlayerState.Moving;           
        }

    }
}
