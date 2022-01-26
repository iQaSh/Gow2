using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    private const float TweenDuration = 0.25f;

    private GameObject gem;
    private GameObject otherGem;
    private EnemyBoard enemyBoard;
    public float hintDelay;
    private float hintDelaySeconds;

    private Vector2 tempPosition;
    // Use this for initialization
    void Start()
    {
        enemyBoard = FindObjectOfType<EnemyBoard>();
        hintDelaySeconds = hintDelay;
    }

    // Update is called once per frame
    void Update()
    {

        hintDelaySeconds -= Time.deltaTime;
        if (hintDelaySeconds <= 0)
        {

            MarkHint();
            hintDelaySeconds = hintDelay;
        }

    }

    //First, I want to find all possible matches on the board
    List<GameObject> FindAllMatches()
    {
        List<GameObject> possibleMoves = new List<GameObject>();
        for (int i = 0; i < enemyBoard.width; i++)
        {
            for (int j = 0; j < enemyBoard.height; j++)
            {
                if (enemyBoard.allEnemyGems[i, j] != null)
                {
                    if (i < enemyBoard.width - 1)
                    {
                        if (enemyBoard.SwitchAndCheck(i, j, Vector2.right))
                        {
                            possibleMoves.Add(enemyBoard.allEnemyGems[i, j]);
                        }
                    }
                    if (j < enemyBoard.height - 1)
                    {
                        if (enemyBoard.SwitchAndCheck(i, j, Vector2.up))
                        {
                            possibleMoves.Add(enemyBoard.allEnemyGems[i, j]);

                        }
                    }
                }
            }
        }
        return possibleMoves;
    }
    //Pick one of those matches randomly
    GameObject PickOneRandomly()
    {
        List<GameObject> possibleMoves = new List<GameObject>();
        possibleMoves = FindAllMatches();
        if (possibleMoves.Count > 0)
        {
            int pieceToUse = Random.Range(0, possibleMoves.Count);
            return possibleMoves[pieceToUse];
        }
        return null;
    }
    //Create the hint behind the chosen match
    //Create te hint behind the chosen match
    private async Task MarkHint()
    {
        Debug.Log("MarkHint");
        GameObject move = PickOneRandomly();
        if (move != null && enemyBoard.currentState == GameState.move);
        {
         //   currentHint = Instantiate(hintParticle, move.transform.position, Quaternion.identity);
           // currentHint2 = Instantiate(hintParticle, move.transform.position, Quaternion.identity);

            //ТЕСТ!!!


                Debug.Log("Ход противника " + move.transform.position.x + "," + move.transform.position.y);


            if (move.transform.position.y <= enemyBoard.height - 1 && move.transform.position.y >= 0 && move.transform.position.x >= 0 && move.transform.position.x <= enemyBoard.width - 1)
            {
                Debug.Log($"Ход противника ===================================================================== {move.transform.position.x}, {move.transform.position.y}");

                if (enemyBoard.SwitchAndCheck((int)move.transform.position.x, (int)move.transform.position.y, Vector2.up))
                {


                    Debug.Log("вверх");
                    tempPosition = new Vector2(move.transform.position.x, move.transform.position.y + 1);
                    //currentHint2 = Instantiate(hintParticle, tempPosition, Quaternion.identity);

                    gem = enemyBoard.allEnemyGems[(int)move.transform.position.x, (int)move.transform.position.y];
                    otherGem = enemyBoard.allEnemyGems[(int)move.transform.position.x, (int)move.transform.position.y + 1];


                    var icon1Transform = gem.transform;
                    var icon2Transform = otherGem.transform;
                    var sequence = DOTween.Sequence();
                    sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                            .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));
                    await sequence.Play().AsyncWaitForCompletion();
                    gem.GetComponent<EnemyGem>().row += 1;
                    otherGem.GetComponent<EnemyGem>().row -= 1;
                    Debug.Log("вверх готово - " + move.transform.position.x + " - " + move.transform.position.y);



                }
                else if (enemyBoard.SwitchAndCheck((int)move.transform.position.x, (int)move.transform.position.y, Vector2.down))
                {

                    Debug.Log("вниз");
                    tempPosition = new Vector2(move.transform.position.x, move.transform.position.y - 1);

                    gem = enemyBoard.allEnemyGems[(int)move.transform.position.x, (int)move.transform.position.y];
                    otherGem = enemyBoard.allEnemyGems[(int)move.transform.position.x, (int)move.transform.position.y - 1];

                    var icon1Transform = gem.transform;
                    var icon2Transform = otherGem.transform;
                    var sequence = DOTween.Sequence();
                    sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                            .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));
                    await sequence.Play().AsyncWaitForCompletion();
                    gem.GetComponent<EnemyGem>().row -= 1;
                    otherGem.GetComponent<EnemyGem>().row += 1;

                    Debug.Log("вниз - готово - " + move.transform.position.x + " - " + move.transform.position.y);



                }
                else if (enemyBoard.SwitchAndCheck((int)move.transform.position.x, (int)move.transform.position.y, Vector2.left))
                {


                    Debug.Log("влево");
                    tempPosition = new Vector2(move.transform.position.x - 1, move.transform.position.y);

                    gem = enemyBoard.allEnemyGems[(int)move.transform.position.x, (int)move.transform.position.y];
                    otherGem = enemyBoard.allEnemyGems[(int)move.transform.position.x - 1, (int)move.transform.position.y];

                    var icon1Transform = gem.transform;
                    var icon2Transform = otherGem.transform;
                    var sequence = DOTween.Sequence();
                    sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                            .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));
                    await sequence.Play().AsyncWaitForCompletion();
                    gem.GetComponent<EnemyGem>().column -= 1;
                    otherGem.GetComponent<EnemyGem>().column += 1;
                    Debug.Log("влево - готово - " + move.transform.position.x + " - " + move.transform.position.y);



                }
                else if (enemyBoard.SwitchAndCheck((int)move.transform.position.x, (int)move.transform.position.y, Vector2.right))
                {


                    Debug.Log("вправо");
                    tempPosition = new Vector2(move.transform.position.x + 1, move.transform.position.y);

                    gem = enemyBoard.allEnemyGems[(int)move.transform.position.x, (int)move.transform.position.y];
                    otherGem = enemyBoard.allEnemyGems[(int)move.transform.position.x + 1, (int)move.transform.position.y];

                    var icon1Transform = gem.transform;
                    var icon2Transform = otherGem.transform;
                    var sequence = DOTween.Sequence();
                    sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                            .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));
                    await sequence.Play().AsyncWaitForCompletion();
                    gem.GetComponent<EnemyGem>().column += 1;
                    otherGem.GetComponent<EnemyGem>().column -= 1;
                    Debug.Log("вправо готово - " + move.transform.position.x + " - " + move.transform.position.y);



                }
            }


                enemyBoard.DestroyMatches();
            


         //   DestroyHint();

        }
    }
    //Destroy the hint.
    public void DestroyHint()
    {
        //if (currentHint != null)
        //{
        //    Destroy(currentHint);
        //    currentHint = null;
        //    hintDelaySeconds = hintDelay;
        //}
    }
}
