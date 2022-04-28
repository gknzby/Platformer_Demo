using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject endMenu;

    public enum UICommand
    {
        Start,
        Finish,
        Restart
    }

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        startMenu.SetActive(true);
        endMenu.SetActive(false);
    }

    public void CommandHandler(UICommand uic)
    {
        switch (uic)
        {
            case UICommand.Start:
                startMenu.SetActive(false);
                GameManager.Instance.CommandHandler(GameManager.GMCommand.Start);
                break;
            case UICommand.Finish:
                endMenu.SetActive(true);
                endMenu.GetComponent<UIEndMenu>().SetScoreText(GameManager.Instance.PData.deathCount, (Time.time - GameManager.Instance.PData.startTime));
                break;
            case UICommand.Restart:
                GameManager.Instance.CommandHandler(GameManager.GMCommand.Restart);
                break;
            default:
                break;
        }
    }

    public void CommandHandler(UICommandComponent uicc)
    {
        CommandHandler(uicc.Command);
    }
}
