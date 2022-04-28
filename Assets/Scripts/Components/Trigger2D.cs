using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger2D : MonoBehaviour
{
    public enum TriggerType
    {
        Unset,
        Checkpoint,
        Trap,
        Enemy,
        Death,
        Finish
    }

    [SerializeField] private TriggerType triggerType = TriggerType.Unset;
    protected bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        switch (triggerType)
        {
            case TriggerType.Checkpoint:
                GameManager.Instance.CommandHandler(GameManager.GMCommand.Checkpoint);
                this.gameObject.SetActive(false);
                break;
            case TriggerType.Enemy:
                GameManager.Instance.CommandHandler(GameManager.GMCommand.Death);
                break;
            case TriggerType.Death:
                GameManager.Instance.CommandHandler(GameManager.GMCommand.Death);
                break;
            case TriggerType.Finish:
                GameManager.Instance.CommandHandler(GameManager.GMCommand.Victory);
                break;
            case TriggerType.Unset:
                Debug.Log("Set The Trigger Type");
                break;
            default:
                break;
        }
    }
}
