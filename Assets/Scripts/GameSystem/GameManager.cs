using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject lose;
    [SerializeField] GameObject win;
    private void OnEnable()
    {
        Player.GameOver += OnGameOver;
        Player.Victory += OnVictory;
    }

    private void OnGameOver()
    {
        lose.SetActive(true);
    }
    private void OnVictory()
    {
        win.SetActive(true);
    }

}
