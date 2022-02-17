using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;


public class EnemyGem : MonoBehaviour
{
    public EnemyFindMatches2 enemyFindMatches;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public float swipeAngle = 0;
    private GameObject otherGem;
    private const float TweenDuration = 0.25f;
    public bool isMoving = false;
    public int movingTo;
    public string matchedWith;
    private EnemyBoard enemyBoard;
    public int targetX;
    public int targetY;
    public bool clickOnGem = false;
    public List<GameObject> currentMatches = new List<GameObject>();

    private Vector2 tempPosition;


    public int row;
    public int column;
    public int previousColumn;
    public int previousRow;

    [SerializeField] public bool isMatched = false;

    public float swipeResist = 1f;

    private void Start()
    {

        enemyFindMatches = FindObjectOfType<EnemyFindMatches2>();
        enemyBoard = FindObjectOfType<EnemyBoard>();
        //  targetX = (int)transform.position.x;
        // targetY = (int)transform.position.y;
        // row = targetY;
        // column = targetX;
        // previousRow = row;
        // previousColumn = column;
    }



    private void OnMouseDown()
    {
        if (enemyBoard.currentState == GameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        clickOnGem = true;
        // Debug.Log("firstTouchPosition" + firstTouchPosition);
        // board.GenerateUpperGems();

    }

    private void OnMouseUp()
    {
        if (enemyBoard.currentState == GameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //   Debug.Log("finalTouchPosition" + name);
            CalculateAngle();
        }


        clickOnGem = false;

        // isMatched = true;

    }

    private void Update()
    {
        //  FindMatches();


        targetX = column;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            // Move towards the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (enemyBoard.allEnemyGems[column, row] != this.gameObject)
            {
                enemyBoard.allEnemyGems[column, row] = this.gameObject;
            }
            FindAllMatches();
 
        }
        else
        {
            //directly set the position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            enemyBoard.allEnemyGems[column, row] = this.gameObject;

        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            // Move towards the target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (enemyBoard.allEnemyGems[column, row] != this.gameObject)
            {
                enemyBoard.allEnemyGems[column, row] = this.gameObject;
            }

            FindAllMatches();
  
        }
        else
        {
            //directly set the position
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;


        }
    }



    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MoveGems();
            enemyBoard.currentState = GameState.wait;
        }
        else
        {
            enemyBoard.currentState = GameState.move;
        }

    }



    private async Task MoveGems()
    {
        Debug.Log("MoveGems");
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            if (swipeAngle > -45 && swipeAngle <= 45 && column < enemyBoard.width - 1)
            {
                //Right Swipe
                //   Debug.Log("right " + name);
                otherGem = enemyBoard.allEnemyGems[column + 1, row];


                var icon1Transform = transform;
                var icon2Transform = otherGem.transform;
                var sequence = DOTween.Sequence();
                sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                        .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));
                await sequence.Play().AsyncWaitForCompletion();






                previousRow = row;
                previousColumn = column;

                otherGem.GetComponent<EnemyGem>().column -= 1;
                column += 1;
                //  Swap(this.GetComponent<EnemyGem>(), otherGem.GetComponent<EnemyGem>());

                //  StartCoroutine(CheckMoveCo(this, otherDot));

            }
            else if (swipeAngle > 45 && swipeAngle <= 135 && row < enemyBoard.height - 1)
            {
                // Сдвиг вверх

                otherGem = enemyBoard.allEnemyGems[column, row + 1];


                var icon1Transform = transform;
                var icon2Transform = otherGem.transform;
                var sequence = DOTween.Sequence();
                sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                        .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));
                await sequence.Play().AsyncWaitForCompletion();




                previousRow = row;
                previousColumn = column;


                otherGem.GetComponent<EnemyGem>().row -= 1;
                row += 1;
                //  Swap(this.GetComponent<EnemyGem>(), otherGem.GetComponent<EnemyGem>());
                //  StartCoroutine(CheckMoveCo(this, otherDot));


            }
            else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
            {
                // Сдвиг влево
                //      Debug.Log("left " + name);

                otherGem = enemyBoard.allEnemyGems[column - 1, row];


                var icon1Transform = transform;
                var icon2Transform = otherGem.transform;
                var sequence = DOTween.Sequence();
                sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                        .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));
                await sequence.Play().AsyncWaitForCompletion();




                previousRow = row;
                previousColumn = column;

                otherGem.GetComponent<EnemyGem>().column += 1;
                column -= 1;
                //    Swap(this.GetComponent<EnemyGem>(), otherGem.GetComponent<EnemyGem>());
                //  StartCoroutine(CheckMoveCo(this, otherDot));

            }
            else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
            {
                // Сдвиг вниз
                Debug.Log("down " + name);

                otherGem = enemyBoard.allEnemyGems[column, row - 1];


                var icon1Transform = transform;
                var icon2Transform = otherGem.transform;
                var sequence = DOTween.Sequence();
                sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                        .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));
                await sequence.Play().AsyncWaitForCompletion();




                previousRow = row;
                previousColumn = column;

                otherGem.GetComponent<EnemyGem>().row += 1;
                row -= 1;
                //  Swap(this.GetComponent<EnemyGem>(), otherGem.GetComponent<EnemyGem>());
                //   StartCoroutine(CheckMoveCo(this, otherDot));

            }
        }
        StartCoroutine(CheckMoveCo());

    }

    public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.1f);
        if (otherGem != null)
        {
            if (!isMatched && !otherGem.GetComponent<EnemyGem>().isMatched)
            {
                Swap(this, otherGem.GetComponent<EnemyGem>());
                yield return new WaitForSeconds(.1f);
                otherGem.GetComponent<EnemyGem>().row = row;
                otherGem.GetComponent<EnemyGem>().column = column;

                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                enemyBoard.currentState = GameState.move;
            }
            else
            {
                enemyBoard.DestroyMatches();

            }
            otherGem = null;
        }
    }



    public async Task Swap(EnemyGem gem1, EnemyGem gem2)
    {
        //int x1 = (int)gem1.transform.position.x;
        //int y1 = (int)gem1.transform.position.y;

        //int x2 = (int)gem2.transform.position.x;
        //int y2 = (int)gem2.transform.position.y;

        //  Debug.Log("G1 = " + gem1.name + ",  G2 = " + gem2.name); 


        var icon1Transform = gem1.transform;
        var icon2Transform = gem2.transform;



        var sequence = DOTween.Sequence();


        sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));

        await sequence.Play().AsyncWaitForCompletion();



    }


    void FindMatches()
    {
        if (column > 0 && column < enemyBoard.width - 1)
        {
            GameObject leftGem1 = enemyBoard.allEnemyGems[column - 1, row];
            GameObject rightGem1 = enemyBoard.allEnemyGems[column + 1, row];
            if (leftGem1 != null && rightGem1 != null)
                if (leftGem1.tag == this.gameObject.tag && rightGem1.tag == this.gameObject.tag)
                {
                    leftGem1.GetComponent<EnemyGem>().isMatched = true;
                    rightGem1.GetComponent<EnemyGem>().isMatched = true;
                    isMatched = true;
                }

        }
        if (row > 0 && row < enemyBoard.height - 1)
        {
            GameObject upGem1 = enemyBoard.allEnemyGems[column, row + 1];
            GameObject downGem1 = enemyBoard.allEnemyGems[column, row - 1];
            if (upGem1 != null && downGem1 != null)
                if (upGem1.tag == this.gameObject.tag && downGem1.tag == this.gameObject.tag)
                {
                    upGem1.GetComponent<EnemyGem>().isMatched = true;
                    downGem1.GetComponent<EnemyGem>().isMatched = true;
                    isMatched = true;
                }

        }
    }


    public void FindAllMatches()
    {

        StartCoroutine(FindAllMatchesCo());
    }

    private void AddToListAndMatch(GameObject gem)
    {
        if (!currentMatches.Contains(gem))
        {
            currentMatches.Add(gem);
        }
        gem.GetComponent<EnemyGem>().isMatched = true;
    }

    private void GetNearbyPieces(GameObject gem1, GameObject gem2, GameObject gem3)
    {

        AddToListAndMatch(gem1);
        AddToListAndMatch(gem2);
        AddToListAndMatch(gem3);


    }



    public IEnumerator FindAllMatchesCo()
    {

        yield return new WaitForSeconds(.1f);
        for (int x = 0; x < enemyBoard.width; x++)
            for (int y = 0; y < enemyBoard.height; y++)
            {
                GameObject currentGem = enemyBoard.allEnemyGems[x, y];
                if (currentGem != null)
                {
                    if (x > 0 && x < enemyBoard.width - 1)
                    {
                        GameObject leftGem = enemyBoard.allEnemyGems[x - 1, y];
                        GameObject rightGem = enemyBoard.allEnemyGems[x + 1, y];
                        if (leftGem != null && rightGem != null)
                        {
                            if (leftGem.tag == currentGem.tag && rightGem.tag == currentGem.tag)
                            {
                          //             Debug.Log("fffffffffff");
                                GetNearbyPieces(leftGem, currentGem, rightGem);
                            }
                        }
                    }

                    if (y > 0 && y < enemyBoard.height - 1)
                    {
                        GameObject upGem = enemyBoard.allEnemyGems[x, y + 1];
                        GameObject downGem = enemyBoard.allEnemyGems[x, y - 1];

                        if (downGem != null && upGem != null)
                        {
                            if (downGem.tag == currentGem.tag && upGem.tag == currentGem.tag)
                            {
                                ///   Debug.Log("ggggggggggggg");
                                GetNearbyPieces(upGem, currentGem, downGem);
                            }
                        }
                    }
                }
            }
    }



}