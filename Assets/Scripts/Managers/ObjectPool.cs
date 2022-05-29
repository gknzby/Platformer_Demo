using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public enum ObjType
    {
        None,
        Mace,
        Arrow
    }
    [System.Serializable]
    public class PoolObj
    {
        public GameObject obj;
        public ObjType objType;
        public int poolCount;
        public List<GameObject> passiveObjs = new List<GameObject>();
    }
    
    public List<PoolObj> pools = new List<PoolObj>();

    private void Awake()
    {
        Instance = this;

        CreatePool();
    }

    private void CreatePool()
    {
        for(int i = 0; i < pools.Count; i++)
        {
            for(int j = 0; j < pools[i].poolCount; j++)
            {
                pools[i].passiveObjs.Add(GameObject.Instantiate(pools[i].obj, this.transform));
            }
        }
    }

    public void ReturnObject(GameObject gObj, ObjType objType)
    {
        gObj.SetActive(false);
        for(int i = 0; i < pools.Count; i++)
        {
            if(pools[i].objType == objType)
            {
                if(pools[i].passiveObjs.Count < pools[i].poolCount)
                    pools[i].passiveObjs.Add(gObj);
                else
                    GameObject.Destroy(gObj);
            }
        }
    }

    public GameObject GetObject(ObjType objType)
    {
        if(objType == ObjType.None)
            return null;
        GameObject gObj = null;
        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i].objType == objType)
            {
                if(pools[i].passiveObjs.Count > 0)
                {
                    gObj = pools[i].passiveObjs[0];
                    pools[i].passiveObjs.Remove(gObj);
                }
                else
                {
                    gObj = GameObject.Instantiate(pools[i].obj, this.transform);
                }
            }
        }
        return gObj;
    }
}
