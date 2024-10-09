using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.setActive(true);
        pointsText.text = playerKillCount.ToString() + " POINTS";
    }
}
