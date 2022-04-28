using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : Trigger2D
{
    [SerializeField] private Transform trapPosition;
    [SerializeField] private ObjectPool.ObjType objType = ObjectPool.ObjType.Mace;
    [SerializeField] private Material objMaterial;

    [Tooltip("Initial velocity when triggered")]
    [SerializeField] private Vector2 force = new Vector2(0, 0);
    [Tooltip("The Mass doesn't affect Impulse Mode [(force*mass)/time], but Force Mode depends on the mass")]
    [SerializeField] private ForceMode2D forceMode = ForceMode2D.Impulse;
    [SerializeField] private float disableTime = 3f;

    private GameObject gObj;

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

                    if(gObj.GetComponent<BoobyTrap>() != null)
                        Destroy(gObj.GetComponent<BoobyTrap>());
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

        if(collision.CompareTag("Player"))
        {
            gObj = ObjectPool.Instance.GetObject(objType);
            BoobyTrap bt = gObj.AddComponent<BoobyTrap>();

            bt.SetValues(trapPosition, force, forceMode, disableTime, objMaterial, objType);
            bt.TriggerTrap();
            triggered = true;
        }
    }
}

