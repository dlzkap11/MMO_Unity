using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;


//위치 벡터 (단순 좌표)
//방향 벡터 (힘의 크기도 포함)
struct MyVector
{
    public float x;
    public float y;
    public float z;

    public float magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } } //방향 크기
    public MyVector normalized { get { return new MyVector(x / magnitude, y / magnitude, z / magnitude); } } //단위벡터


    public MyVector(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }

    public static MyVector operator +(MyVector a, MyVector b)
    {
        return new MyVector(a.x + b.x, a.y + b.y, a.z + b.z);
    }
    public static MyVector operator -(MyVector a, MyVector b)
    {
        return new MyVector(a.x - b.x, a.y - b.y, a.z - b.z);
    }
    
    public static MyVector operator *(MyVector a, float d)
    {
        return new MyVector(a.x * d, a.y * d, a.z * d);
    }
    
    
    public static MyVector operator *(float d, MyVector a)
    {
        return new MyVector(a.x * d, a.y * d, a.z * d);
    }
    
    


}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    void Start()
    {          
        MyVector to = new MyVector(10.0f, 0.0f, 0.0f);
        MyVector from = new MyVector(5.0f, 0.0f, 0.0f);
        MyVector dir = to - from; //(5.0f, 0.0f, 0.0f)

        dir = dir.normalized; //(1.0f, 0.0f, 0.0f)

        MyVector newPos = from + dir * _speed;

        // 방향벡터
        // 1. 거리(크기)    5
        // 2. 실제 방향     ->

        Managers.input.KeyAction -= OnKeyboard; //일종의 초기화
        Managers.input.KeyAction += OnKeyboard;
 
    }

    // GameObject (player)
    // Transfrom
    // PlayerController ()...

    float _yAngle = 0.0f;

    void Update()
    {
        //Local -> World
        //TransformDirection

        //world -> Local
        //InverseTransfromDirection

        _yAngle += Time.deltaTime * _speed;

        //절대 회전값
        //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);
        //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

        // +- delta
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * _speed, 0.0f));


            
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
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.back * Time.deltaTime * _speed;
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
}
