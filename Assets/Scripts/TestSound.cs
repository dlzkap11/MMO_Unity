using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public AudioClip audioclip;
    public AudioClip audioclip2;

    int i = 0;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource audio = GetComponent<AudioSource>();

        //audio.PlayOneShot(audioclip2);
        //float lf = Mathf.Max(audioclip.length, audioclip2.length);
        //GameObject.Destroy(gameObject, lf); //������� ��� �� ������Ʈ�� ������� ������� ���� �����

        

        i++;

        if(i%2==0)
            Managers.Sound.Play(audioclip, Define.Sound.BGM);
        else
            Managers.Sound.Play("UnityChan/univ0002", Define.Sound.BGM);
       
        
    }
}
