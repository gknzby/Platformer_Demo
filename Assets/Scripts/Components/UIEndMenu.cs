using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndMenu : MonoBehaviour
{
    [SerializeField] private GameObject ScoreTextObj;

    public void SetScoreText(int deathCount, float passedTime)
    {
        string scoreText = "You died " + deathCount + " times\n"
                            +"in " + ((int)passedTime) + " seconds!";
        ScoreTextObj.GetComponent<UnityEngine.UI.Text>().text = scoreText;
    }
}
