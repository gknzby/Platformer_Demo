using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BoobyTrap : MonoBehaviour
{
    private Vector2 force = new Vector2(0,0);
    private ForceMode2D forceMode = ForceMode2D.Impulse;
    private float disableTime = 3f;
    private ObjectPool.ObjType objType;

    public void SetValues(Transform transform, Vector2 force, ForceMode2D forceMode, float disableTime, Material mat, ObjectPool.ObjType objType)
    {
        this.transform.SetPositionAndRotation(transform.position, transform.rotation);
        this.force = force;
        this.forceMode = forceMode;
        this.disableTime = disableTime;
        this.gameObject.GetComponent<SpriteRenderer>().material = mat;
        this.objType = objType;
    }
    
    public void TriggerTrap()
    {
        if(!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);
        this.GetComponent<Rigidbody2D>().AddForce(force, forceMode);

        if (disableTime > 0)
            StartCoroutine(SetDisable());         
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.CommandHandler(GameManager.GMCommand.Death);
        }
    }

    IEnumerator SetDisable()
    {
        yield return new WaitForSeconds(disableTime);
        if(gameObject.activeSelf)
        {
            ObjectPool.Instance.ReturnObject(this.gameObject, objType);
            Destroy(this);
        }
    }
}
