using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{

    // 1) ������ RigidBody�� �־�� �Ѵ� (is kinematic off)
    // 2) ������ Collider�� �־�� �Ѵ� (isTrigger off)
    // 3) ������� Collider�� �־�� �Ѵ� (isTrigger off)

    private void OnCollisionEnter(Collision collision) //�浹�ߴ°�?
    {
        Debug.Log($"Collision @ {collision.gameObject.name}");
    }


    // 1) �� �� Collider�� �־�� �Ѵ�.
    // 2) �� �� �ϳ��� isTrigger On
    // 3) �� �� �ϳ��� RigidBody�� �־�� �Ѵ�.
    private void OnTriggerEnter(Collider other) //���� �ȿ� �ִ°�?
    {
        
        Debug.Log($"Trigger @ {other.gameObject.name}");
    }

    void Start()
    {
        
    }


    void Update()
    {

        // Local <-> World <-> (Viewport <-> Screen)(ȭ��)


        //Debug.Log(Input.mousePosition); //��ũ�� ��ǥ

        //Debug.Log(Camera.main.ScreenToViewportPoint( Input.mousePosition )); //Viewport ����?

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);


            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");
            //int mask = (1 << 6) | (1 << 7);
            
            
            RaycastHit hit;         
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {

                
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.tag}");
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{


        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //    Vector3 dir = mousePos - Camera.main.transform.position;
        //    dir = dir.normalized;


        //    Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);

        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
        //    {
        //        Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        //    }
        //}



        /*
        Vector3 look = transform.TransformDirection(Vector3.forward);

        Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

        

        RaycastHit[] hits;

        hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);


        foreach(RaycastHit hit in hits)
        {
            Debug.Log($"Raycast {hit.collider.gameObject.name}!");
        }
        */
        /*
        if (Physics.Raycast(transform.position + Vector3.up, look, out hit ,10)) 
        {
            Debug.Log($"Raycast {hit.collider.gameObject.name}!");
        }
        */
    }
}
