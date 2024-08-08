using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager
{

    GameObject _player;

    //Dictionary<int, GameObject> _player = new Dictionary<int, GameObject>();
    HashSet<GameObject> _monster = new HashSet<GameObject>();

    public GameObject GetPlayer() { return _player; }

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monster.Add(go);
                break;
                
            case Define.WorldObject.Player:
                _player = go;
                break;
        }

        return go;
    }

    public Define.WorldObject GetWorldType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();

        if(bc == null) 
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;

    }
    
    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldType(go);
        
        switch(type)
        {
            case Define.WorldObject.Monster:
                {
                    if(_monster.Contains(go))
                        _monster.Remove(go);
                }

                break;


            case Define.WorldObject.Player:
                {
                    if(_player == go)
                        _player = null;
                }
                break;

        }

        Managers.Resource.Destroy(go);
    }
}
