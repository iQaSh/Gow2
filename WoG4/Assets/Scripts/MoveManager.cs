using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveManager : MonoBehaviour
{
    public GameObject playerBoard;
    public GameObject enemyBoard;
    public GameObject playerFrame;
    public GameObject enemyFrame;

    private void Start()
    {
        playerBoard.SetActive(true);
        enemyBoard.SetActive(false);
        enemyFrame.SetActive(false);
        playerFrame.SetActive(true);
    }

    
    public void EndPlayerMove()
    {
        playerBoard.SetActive(false);
        enemyBoard.SetActive(true);
        enemyFrame.SetActive(true);
        playerFrame.SetActive(false);
    }

    public void EndEnemyMove()
    {
        playerBoard.SetActive(true);
        enemyBoard.SetActive(false);
        enemyFrame.SetActive(false);
        playerFrame.SetActive(true);
    }
}
