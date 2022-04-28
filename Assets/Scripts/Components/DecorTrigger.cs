using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorTrigger : Trigger2D
{
    [SerializeField] private Transform decorTransform;
    [SerializeField] private ObjectPool.ObjType objType = ObjectPool.ObjType.None;
    [SerializeField] private Material objMaterial;
    [SerializeField] private float gravityScale;

    [SerializeField] private GameObject gObj;

    private void Start()
    {
        GameManager.Instance.defMng_Event.AddListener(DefaultManager_Handler);
    }

    private void DefaultManager_Handler(DefaultManager_Command dmc)
    {
        switch (dmc)
        {
            case DefaultManager_Command.Load:
                if (triggered && gObj.activeSelf)
                {
                    ObjectPool.Instance.ReturnObject(gObj, objType);

                    if (gObj.GetComponent<Decor>())
                        Destroy(gObj.GetComponent<Decor>());
                }
                    
                triggered = false;
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered)
            return;

        if (collision.CompareTag("Player"))
        {
            gObj = ObjectPool.Instance.GetObject(objType);
            Decor decor = gObj.AddComponent<Decor>();

            decor.SetValues(decorTransform, gravityScale, objMaterial, objType);
            triggered = true;
        }
    }
}
