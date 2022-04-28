using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformMove : MonoBehaviour
{
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private float moveTime;

    private Vector2 initialPos;
    private Vector2 currentPos;

    [SerializeField] private float timer;
    [SerializeField] private float lerpDist;
    [SerializeField] private float easyInOut;

    private void Awake()
    {
        initialPos.x = transform.position.x;
        initialPos.y = transform.position.y;
        currentPos = transform.position;
        targetPos = initialPos + targetPos;
        timer = 0;
    }

    void Update()
    {

        timer += Time.deltaTime;

        if (timer > moveTime*2) timer -= moveTime*2;

        if (timer > moveTime)
        {
            lerpDist = 1 - (timer - moveTime)/moveTime;
            easyInOut = (lerpDist - 0.5f) < 0 ? -1 : 1;
            easyInOut = (((lerpDist - 0.5f) * 2) * (lerpDist - 0.5f) * easyInOut + 1) / 2;
            currentPos = Vector2.Lerp(initialPos, targetPos, lerpDist);
        }
        else
        {
            lerpDist = timer / moveTime;
            easyInOut = (lerpDist - 0.5f) < 0 ? -1 : 1;
            easyInOut = (((lerpDist - 0.5f) * 2) * (lerpDist - 0.5f) * easyInOut + 1) / 2;
            currentPos = Vector2.Lerp(initialPos, targetPos, lerpDist);
        }
    }

    private void FixedUpdate()
    {
        this.GetComponent<Rigidbody2D>().MovePosition(currentPos);
    }
}
