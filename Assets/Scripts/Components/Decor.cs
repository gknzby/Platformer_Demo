using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Decor : MonoBehaviour
{
    private ObjectPool.ObjType objType;

    public void SetValues(Transform transform, float gravityScale, Material mat, ObjectPool.ObjType objType)
    {
        this.transform.SetPositionAndRotation(transform.position, transform.rotation);
        this.transform.localScale = transform.localScale;
        this.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        this.gameObject.GetComponent<SpriteRenderer>().material = mat;
        this.objType = objType;
        this.gameObject.SetActive(true);
    }
}
