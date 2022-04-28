using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefaultManager : MonoBehaviour
{    
    public bool IsEnable { get => isEnable; set => isEnable = value; }

    [SerializeField] private bool autoSetDefaults = true;
    [SerializeField] private bool isEnable = true;
    [SerializeField] private bool transformIsLocal = true;
    [SerializeField] private Transform dTransform;

    private Vector3 position;
    private Quaternion rotation;
    private Vector3 scale;

    private void Start()
    {
        GameManager.Instance.defMng_Event.AddListener(CommandHandler);

        CommandHandler(DefaultManager_Command.FirstSet);
    }
    private void CommandHandler(DefaultManager_Command dmc)
    {
        switch (dmc)
        {
            case DefaultManager_Command.Load:
                LoadDefaults();
                break;
            case DefaultManager_Command.Set:
                SetDefaults();
                break;
            case DefaultManager_Command.FirstSet:
                FirstSet();
                break;
            default:
                break;
        }
    }
    private void FirstSet()
    {
        if(autoSetDefaults || dTransform == null)
            dTransform = this.transform;
        SetTransform(dTransform);
        if(autoSetDefaults)
        {
            isEnable = gameObject.activeSelf;
        }
        else
        {
            LoadDefaults();
        }
    }
    public void SetDefaults(Transform _transform)
    {
        SetTransform(_transform);
        this.isEnable = gameObject.activeSelf;
    }
    public void SetDefaults()
    {
        SetDefaults(this.dTransform);
    }
    public void SetTransform(Transform transform)
    {
        dTransform = transform;
        if (transformIsLocal)
        {
            this.position = dTransform.localPosition;
            this.rotation = dTransform.localRotation;
        }
        else
        {
            this.position = dTransform.position;
            this.rotation = dTransform.rotation;
        }
        this.scale = dTransform.localScale;
    }
    public void LoadDefaults()
    {
        if(this.isEnable && gameObject.activeSelf)
            gameObject.SetActive(false);

        if (transformIsLocal)
        {
            dTransform.localPosition = this.position;
            dTransform.localRotation = this.rotation;
        }
        else
        {
            dTransform.position = this.position;
            dTransform.rotation = this.rotation;
        }
        dTransform.localScale = this.scale;
        gameObject.SetActive(this.isEnable);
    }
}
