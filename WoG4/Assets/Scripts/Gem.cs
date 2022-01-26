using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;


public class Gem : MonoBehaviour
{
    private FindMatches findMatches;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public float swipeAngle = 0;
    private GameObject otherGem;
    private const float TweenDuration = 0.25f;
    public bool isMoving = false;
    public int movingTo;
    public string matchedWith;
    private Board board;
    public int targetX;
    public int targetY;
    public bool clickOnGem = false;

    private Vector2 tempPosition;


    public int row;
    public int column;
    public int previousColumn;
    public int previousRow;

    [SerializeField] public bool isMatched = false;

    public float swipeResist = 1f;

    private void Start()
    {
        findMatches = FindObjectOfType<FindMatches>();
        board = FindObjectOfType<Board>();
      //  targetX = (int)transform.position.x;
       // targetY = (int)transform.position.y;
        // row = targetY;
        // column = targetX;
        // previousRow = row;
        // previousColumn = column;
    }



    private void OnMouseDown()
    {
        if (board.currentState == GameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
      clickOnGem = true;
    // Debug.Log("firstTouchPosition" + firstTouchPosition);
    // board.GenerateUpperGems();

}

    private void OnMouseUp()
    {
        if (board.currentState == GameState.move)
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
            if(board.allGems[column,row] != this.gameObject)
            {
                board.allGems[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else
        {
            //directly set the position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            board.allGems[column, row] = this.gameObject;
          
        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            // Move towards the target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allGems[column, row] != this.gameObject)
            {
                board.allGems[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
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
            board.currentState = GameState.wait;
        }
        else
        {
            board.currentState = GameState.move;
        }

    }



    private async Task MoveGems()
    {
        Debug.Log("MoveGems");
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
            {
                //Right Swipe
             //   Debug.Log("right " + name);
                otherGem = board.allGems[column + 1, row];


                var icon1Transform = transform;
                var icon2Transform = otherGem.transform;
                var sequence = DOTween.Sequence();
                sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                        .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));
                await sequence.Play().AsyncWaitForCompletion();






                previousRow = row;
                previousColumn = column;
                
                otherGem.GetComponent<Gem>().column -= 1;
                column += 1;
                //  Swap(this.GetComponent<Gem>(), otherGem.GetComponent<Gem>());

                //  StartCoroutine(CheckMoveCo(this, otherDot));

            }
            else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
            {
                // Сдвиг вверх
                
                otherGem = board.allGems[column, row + 1];


                var icon1Transform = transform;
                var icon2Transform = otherGem.transform;
                var sequence = DOTween.Sequence();
                sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                        .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));
                await sequence.Play().AsyncWaitForCompletion();




                previousRow = row;
                previousColumn = column;
                

                otherGem.GetComponent<Gem>().row -= 1;
                row += 1;
                //  Swap(this.GetComponent<Gem>(), otherGem.GetComponent<Gem>());
                //  StartCoroutine(CheckMoveCo(this, otherDot));


            }
            else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
            {
                // Сдвиг влево
          //      Debug.Log("left " + name);

                otherGem = board.allGems[column - 1, row];


                var icon1Transform = transform;
                var icon2Transform = otherGem.transform;
                var sequence = DOTween.Sequence();
                sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                        .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));
                await sequence.Play().AsyncWaitForCompletion();




                previousRow = row;
                previousColumn = column;
              
                otherGem.GetComponent<Gem>().column += 1;
                column -= 1;
            //    Swap(this.GetComponent<Gem>(), otherGem.GetComponent<Gem>());
                //  StartCoroutine(CheckMoveCo(this, otherDot));

            }
            else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
            {
                // Сдвиг вниз
                Debug.Log("down " + name);

                otherGem = board.allGems[column, row - 1];


                var icon1Transform = transform;
                var icon2Transform = otherGem.transform;
                var sequence = DOTween.Sequence();
                sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                        .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));
                await sequence.Play().AsyncWaitForCompletion();




                previousRow = row;
                previousColumn = column;
            
                otherGem.GetComponent<Gem>().row += 1;
                row -= 1;
                //  Swap(this.GetComponent<Gem>(), otherGem.GetComponent<Gem>());
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
            if (!isMatched && !otherGem.GetComponent<Gem>().isMatched)
            {
                Swap(this, otherGem.GetComponent<Gem>());
                yield return new WaitForSeconds(.1f);
                otherGem.GetComponent<Gem>().row = row;
                otherGem.GetComponent<Gem>().column = column;
                
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentState = GameState.move;
            }
            else
            {
                board.DestroyMatches();
 
            }
            otherGem = null;
        }
    }



    public async Task Swap(Gem gem1, Gem gem2)
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
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftGem1 = board.allGems[column - 1, row];
            GameObject rightGem1 = board.allGems[column + 1, row];
            if (leftGem1 != null && rightGem1 !=null)
                if (leftGem1.tag == this.gameObject.tag && rightGem1.tag == this.gameObject.tag)
                {
                    leftGem1.GetComponent<Gem>().isMatched = true;
                    rightGem1.GetComponent<Gem>().isMatched = true;
                    isMatched = true;
                }

        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject upGem1 = board.allGems[column, row + 1];
            GameObject downGem1 = board.allGems[column, row - 1];
            if (upGem1 !=null && downGem1!= null)
                if (upGem1.tag == this.gameObject.tag && downGem1.tag == this.gameObject.tag)
                {
                    upGem1.GetComponent<Gem>().isMatched = true;
                    downGem1.GetComponent<Gem>().isMatched = true;
                    isMatched = true;
                }

        }
    }






















    //private void CheckMoveState()
    //{
    //    var x = transform.position.x;
    //    var y = transform.position.y;
    //    var bottomGem = GameObject.Find($"{x},{y - 1}");
    //    if (bottomGem || y == 0)
    //    {
    //        isMoving = false;

    //    }
    //    else
    //    {
    //        isMoving = true;

    //    }
    //}


    //private Board board;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    board = FindObjectOfType<Board>();

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //   CheckMoveState();
    //    //name = $"{(int)transform.position.x},{(int)transform.position.y}";


    //    FindMatches();
    //    //   board.DestroyMatches();

    //    if (isMatched)
    //    {

    //          SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
    //          mySprite.color = new Color(0f, 0f, 0f, .2f);
    //        //  StartCoroutine(board.DestroyMatchesAt(this));
    //  //      StartCoroutine(board.DestroyMatches());

    //    }
    //}



    //public void FindMatches()
    //{

    //    var x = (int)this.transform.position.x;
    //    var y = (int)this.transform.position.y;



    //    if (x > 0 && x < board._width - 1 && !isMoving)
    //    {

    //        var mainGem = GameObject.Find($"{x},{y}");
    //        var rightGem1 = GameObject.Find($"{x + 1},{y}");
    //        var leftGem1 = GameObject.Find($"{x - 1},{y}");




    //        if (mainGem && rightGem1 && leftGem1)
    //        {
    //            var mainGemGem = GameObject.Find($"{x},{y}").GetComponent<Gem>();
    //            var rightGem1Gem = GameObject.Find($"{x + 1},{y}").GetComponent<Gem>();
    //            var leftGem1Gem = GameObject.Find($"{x - 1},{y}").GetComponent<Gem>();
    //            if (!mainGemGem.isMoving && !rightGem1Gem.isMoving && !leftGem1Gem.isMoving)
    //            {
    //                if (mainGem.tag == rightGem1.tag && mainGem.tag == leftGem1.tag)
    //                {

    //                    rightGem1.GetComponent<Gem>().isMatched = true;
    //                    leftGem1.GetComponent<Gem>().isMatched = true;


    //                    Debug.Log("Match x " + $"{x},{y}");
    //                    mainGem.GetComponent<Gem>().isMatched = true;
    //                    mainGem.GetComponent<Gem>().matchedWith = $"left = {leftGem1.GetComponent<Gem>().name}, right {rightGem1.GetComponent<Gem>().name}";
    //                    isMatched = true;
    //                }
    //            }
    //        }

    //    }


    //    if (y > 0 && y < board._height - 1 && !isMoving)
    //    {
    //        var mainGem = GameObject.Find($"{x},{y}");
    //        var upGem1 = GameObject.Find($"{x},{y + 1}");
    //        var downGem1 = GameObject.Find($"{x},{y - 1}");



    //        //  if ((bottomMainGem && bottomDownGem1) || y < 2)
    //        if (mainGem && upGem1 && downGem1)
    //        {
    //            var mainGemGem = GameObject.Find($"{x},{y}").GetComponent<Gem>();
    //            var upGem1Gem = GameObject.Find($"{x},{y + 1}").GetComponent<Gem>();
    //            var downGem1Gem = GameObject.Find($"{x},{y - 1}").GetComponent<Gem>();
    //            if (!mainGemGem.isMoving && !upGem1Gem.isMoving && !downGem1Gem.isMoving)
    //            {
    //                if (mainGem.tag == upGem1.tag && mainGem.tag == downGem1.tag)
    //                {

    //                    Debug.Log("Match y " + $"{x},{y}");
    //                    upGem1.GetComponent<Gem>().isMatched = true;
    //                    downGem1.GetComponent<Gem>().isMatched = true;
    //                    mainGem.GetComponent<Gem>().isMatched = true;
    //                    isMatched = true;
    //                }
    //            }
    //        }

    //    }


    //}

    //private void OnMouseDown()
    //{

    //    firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    //  Debug.Log(firstTouchPosition);
    //    // board.GenerateUpperGems();
    //    StartCoroutine(board.DecreaseRowCo());
    //}

    //private void OnMouseUp()
    //{
    //    finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    CalculateAngle();
    //    MoveGems();
    //   // isMatched = true;

    //}

    //void CalculateAngle()
    //{


    //    swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;


    //    // Debug.Log(swipeAngle);
    //}

    //void MoveGems()
    //{
    //    Debug.Log("MoveGems");
    //    if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
    //    {
    //        if (swipeAngle > -45 && swipeAngle <= 45 && this.transform.position.x < board._width - 1)
    //        {
    //            //Right Swipe


    //            var gem2 = GameObject.Find($"{(int)this.transform.position.x + 1},{this.transform.position.y}").GetComponent<Gem>();
    //            Swap(this, gem2);
    //            StartCoroutine(CheckMoveCo(this, gem2));

    //        }
    //        else if (swipeAngle > 45 && swipeAngle <= 135 && this.transform.position.y < board._height - 1)
    //        {
    //            // Сдвиг вверх
    //            var gem2 = GameObject.Find($"{(int)this.transform.position.x},{this.transform.position.y + 1}").GetComponent<Gem>();
    //            Swap(this, gem2);
    //            StartCoroutine(CheckMoveCo(this, gem2));

    //        }
    //        else if ((swipeAngle > 135 || swipeAngle <= -135) && this.transform.position.x > 0)
    //        {
    //            // Сдвиг влево
    //            var gem2 = GameObject.Find($"{(int)this.transform.position.x - 1},{this.transform.position.y}").GetComponent<Gem>();
    //            Swap(this, gem2);
    //            StartCoroutine(CheckMoveCo(this, gem2));

    //        }
    //        else if (swipeAngle < -45 && swipeAngle >= -135 && this.transform.position.y > 0)
    //        {
    //            // Сдвиг вниз
    //            var gem2 = GameObject.Find($"{(int)this.transform.position.x},{this.transform.position.y - 1}").GetComponent<Gem>();
    //            Swap(this, gem2);
    //            StartCoroutine(CheckMoveCo(this, gem2));

    //        }
    //    }


    //}




    //public async Task Swap(Gem gem1, Gem gem2)
    //{
    //    //int x1 = (int)gem1.transform.position.x;
    //    //int y1 = (int)gem1.transform.position.y;

    //    //int x2 = (int)gem2.transform.position.x;
    //    //int y2 = (int)gem2.transform.position.y;

    //    //  Debug.Log("G1 = " + x1 + "," + y1 + "; G2 = " + x2 + "," + y2); 


    //    var icon1Transform = gem1.transform;
    //    var icon2Transform = gem2.transform;



    //    var sequence = DOTween.Sequence();


    //    sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
    //            .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));

    //    await sequence.Play().AsyncWaitForCompletion();


    //    var name1 = gem1.name;
    //    var name2 = gem2.name;
    //    gem1.name = name2;
    //    gem2.name = name1;

    //}

    //public IEnumerator CheckMoveCo(Gem gem1, Gem gem2)
    //{
    // //   Debug.Log("CheckMoveCo");
    //    yield return new WaitForSeconds(.3f);

    //    if (gem2 != null)
    //    {

    //        if (!gem1.isMatched && !gem2.isMatched)
    //        {

    //            Swap(gem1, gem2);
    //        }
    //    }

    //}

}