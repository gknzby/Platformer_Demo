using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region UnityEngine Attributes
[HelpURL("https://github.com/gknzby")]
[RequireComponent(typeof(Camera))]
#endregion
public class CameraController : MonoBehaviour
{
    [Tooltip("Set target transform for the camera to follow.")]
    [SerializeField]
    private Transform targetTransform;

    [Tooltip("Minimum XY-Value the Camera can go.")]
    [SerializeField]
    private Vector2 minXY;

    [Tooltip("The strenght of the Camera to follow the target.")]
    [Range(0.0f, 5f)]
    [SerializeField]
    private float FollowStrenght;

    private Vector3 targetVec;

    private void Awake()
    {
        targetVec = targetTransform.position;
        targetVec.z = this.transform.position.z;
        this.transform.position = targetVec;
    }

    private void FixedUpdate()
    {
        targetVec.x = (targetTransform.position.x < minXY.x) ? minXY.x : targetTransform.position.x;
        targetVec.y = (targetTransform.position.y < minXY.y) ? minXY.y : targetTransform.position.y;
        
        
        this.transform.position = Vector3.Lerp(this.transform.position, targetVec, FollowStrenght*Time.fixedDeltaTime);
    }

#if UNITY_EDITOR
    #region INSPECTOR RESET
    private void Reset()
    {
        this.minXY.y = 0.5f;
        this.minXY.x = 0f;
        this.FollowStrenght = 2f;
    }
    #endregion
#endif
}
