using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    [HideInInspector] public DefaultManager_Event defMng_Event;

    private UIManager uiMng;

    public enum GMCommand
    {
        Start,
        Reset,
        GameOver,
        Checkpoint,
        Victory,
        Death,
        Restart
    }

    public struct PlayerData
    {
        public int deathCount;
        public float startTime;
    }
    private PlayerData pData;
    public PlayerData PData { get => pData; private set => pData = value; }

    private void Awake()
    {
        GameManager.Instance = this;

        #region DefaultManager
        defMng_Event = new DefaultManager_Event();
        #endregion

        Time.timeScale = 0;
    }

    private void Start()
    {
        uiMng = UIManager.Instance;
    }

    public void CommandHandler(GMCommand gmc)
    {
        switch (gmc)
        {
            case GMCommand.Start:
                StartGame();
                break;
            case GMCommand.Checkpoint:
                Checkpoint();
                break;
            case GMCommand.Victory:
                Victory();
                break;
            case GMCommand.Death:
                PlayerDeath();
                break;
            case GMCommand.Reset:
                ResetToCheckpoint();
                break;
            case GMCommand.GameOver:
                GameOver();
                break;
            case GMCommand.Restart:
                Restart();
                break;
            default:
                break;
        }
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        pData.startTime = Time.time;
    }

    private void Checkpoint()
    {
        defMng_Event.Invoke(DefaultManager_Command.Set);
    }

    private void Victory()
    {
        Time.timeScale = 0;

        uiMng.CommandHandler(UIManager.UICommand.Finish); //UI Command
    }

    private void PlayerDeath()
    {
        pData.deathCount++;
        CommandHandler(GMCommand.Reset);
    }

    private void ResetToCheckpoint()
    {
        defMng_Event.Invoke(DefaultManager_Command.Load);
    }

    private void GameOver()
    {
        //There is no gameover for now
    }

    private void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
