using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;


public class HintManager : MonoBehaviour
{
    private const float TweenDuration = 0.25f;
    private Gem gemScript;
    private GameObject gem;
    private GameObject otherGem;
    private Board board;
    public float hintDelay;
    private float hintDelaySeconds;
    public bool isHint = false;
    public Sequence sequence = DOTween.Sequence();

    public Transform icon1Transform;
    public Transform icon2Transform;

    private Vector2 tempPosition;
    // Use this for initialization
    void Start()
    {
        gemScript = FindObjectOfType<Gem>();
        board = FindObjectOfType<Board>();
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
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                if (board.allGems[i, j] != null)
                {
                    if (i < board.width - 1)
                    {
                        if (board.SwitchAndCheck(i, j, Vector2.right))
                        {
                            possibleMoves.Add(board.allGems[i, j]);
                        }
                    }
                    if (j < board.height - 1)
                    {
                        if (board.SwitchAndCheck(i, j, Vector2.up))
                        {
                            possibleMoves.Add(board.allGems[i, j]);

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
    private async Task ShakeGems(GameObject gem, GameObject otherGem)
    {
        isHint = true;
        Debug.Log("ISHINT +++++++++++++++++++ " + isHint);
        icon1Transform = gem.transform;
        icon2Transform = otherGem.transform;
      //  var sequence = DOTween.Sequence();

        sequence.Join(icon1Transform.DOShakeScale(100f, new Vector3(0.1f, 0.1f, 0f), 10, 0f, false))
            .Join(icon2Transform.DOShakeScale(100f, new Vector3(0.1f, 0.1f, 0f), 10, 0f, false));
        await sequence.Play().AsyncWaitForCompletion();
        Debug.Log("Shake Shake Shake");
        DOTween.Kill(icon1Transform);
        DOTween.Kill(icon2Transform);


    }

    private void MarkHint()
    {

        GameObject move = PickOneRandomly();
        Debug.Log("ISHINT ------------------- " + isHint);
        if (isHint == false)
            if (move != null)
            {

                //   currentHint = Instantiate(hintParticle, move.transform.position, Quaternion.identity);
                // currentHint2 = Instantiate(hintParticle, move.transform.position, Quaternion.identity);

                //ТЕСТ!!!


                Debug.Log("hint" + move.transform.position.x + " - " + move.transform.position.y);


                if (move.transform.position.y < board.height - 1 && move.transform.position.y > 0 && move.transform.position.x > 0 && move.transform.position.x < board.width - 1)
                {


                    if (board.SwitchAndCheck((int)move.transform.position.x, (int)move.transform.position.y, Vector2.up))
                    {

                        isHint = true;
                        Debug.Log("вверх");
                        tempPosition = new Vector2(move.transform.position.x, move.transform.position.y + 1);
                        //currentHint2 = Instantiate(hintParticle, tempPosition, Quaternion.identity);

                        gem = board.allGems[(int)move.transform.position.x, (int)move.transform.position.y];
                        otherGem = board.allGems[(int)move.transform.position.x, (int)move.transform.position.y + 1];

                        ShakeGems(gem, otherGem);

                    }
                    else if (board.SwitchAndCheck((int)move.transform.position.x, (int)move.transform.position.y, Vector2.down))
                    {

                        Debug.Log("вниз");
                        tempPosition = new Vector2(move.transform.position.x, move.transform.position.y - 1);

                        gem = board.allGems[(int)move.transform.position.x, (int)move.transform.position.y];
                        otherGem = board.allGems[(int)move.transform.position.x, (int)move.transform.position.y - 1];
                        ShakeGems(gem, otherGem);


                    }
                    else if (board.SwitchAndCheck((int)move.transform.position.x, (int)move.transform.position.y, Vector2.left))
                    {


                        Debug.Log("влево");
                        tempPosition = new Vector2(move.transform.position.x - 1, move.transform.position.y);

                        gem = board.allGems[(int)move.transform.position.x, (int)move.transform.position.y];
                        otherGem = board.allGems[(int)move.transform.position.x - 1, (int)move.transform.position.y];
                        ShakeGems(gem, otherGem);
                    }
                    else if (board.SwitchAndCheck((int)move.transform.position.x, (int)move.transform.position.y, Vector2.right))
                    {


                        Debug.Log("вправо");
                        tempPosition = new Vector2(move.transform.position.x + 1, move.transform.position.y);

                        gem = board.allGems[(int)move.transform.position.x, (int)move.transform.position.y];
                        otherGem = board.allGems[(int)move.transform.position.x + 1, (int)move.transform.position.y];
                        ShakeGems(gem, otherGem);
                    }
                }

      
                board.DestroyMatches();



                //   DestroyHint();

            }

    }
}

//            icon1Transform.DOShakeScale(10f, new Vector3(0.1f, 0.1f, 0f), 4, 0f, false);
//            currentHint = Instantiate(hintParticle, move.transform.position, Quaternion.identity);
//        }
//    }
//    //Destroy the hint.
//    public void DestroyHint()
//    {
//        if (currentHint != null)
//        {
//            Destroy(currentHint);
//            currentHint = null;
//            hintDelaySeconds = hintDelay;
//        }
//    }
//}




//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;

//public class HintManager : MonoBehaviour
//{
//    private Board board;
//    private GameObject gem;
//    private GameObject otherGem;
//    private GameObject[,] gemsToMove;
//    public float hintDelay;
//    private float hintDelaySeconds;
//    public GameObject hintParticle;
//    public GameObject currentHint;
//    public GameObject currentHint2;
//    private Vector2 tempPosition;
//    private bool playerMove;


//    // Start is called before the first frame update
//    void Start()
//    {
//        void Start()
//        {

//            board = FindObjectOfType<Board>();

//            hintDelaySeconds = hintDelay;
//        }
//    }
//    //First, i want to find all possible matches on the board
//    List<GameObject> FindAllMatches()
//    {

//        //return null;
//        List<GameObject> possibleMoves = new List<GameObject>();


//        for (int i = 0; i < board.width; i++)
//        {
//            for (int j = 0; j < board.height; j++)
//            {
//                if (board.allGems[i, j] != null)
//                {
//                    if (i < board.width - 1)
//                    {
//                        if (board.SwitchAndCheck(i, j, Vector2.right))
//                        {
//                            // Debug.Log("right");
//                            possibleMoves.Add(board.allGems[i, j]);



//                        }
//                    }
//                    if (j < board.height - 1)
//                    {
//                        if (board.SwitchAndCheck(i, j, Vector2.up))
//                        {
//                            // Debug.Log("up");
//                            possibleMoves.Add(board.allGems[i, j]);
//                        }
//                    }

//                }

//            }
//        }
//        return possibleMoves;
//    }

//    //Pick one of those matches randomly
//    public GameObject PickOneRandomly()
//    {
//        List<GameObject> possibleMoves = new List<GameObject>();
//        possibleMoves = FindAllMatches();
//        if (possibleMoves.Count > 0)
//        {
//            int pieceToUse = Random.Range(0, possibleMoves.Count);
//            return possibleMoves[pieceToUse];
//        }
//        return null;
//    }


//    //Create te hint behind the chosen match
//    private void MarkHint()
//    {

//        GameObject move = PickOneRandomly();
//        if (move != null)
//        {
//            Debug.Log("MarkHint");
//            currentHint = Instantiate(hintParticle, move.transform.position, Quaternion.identity);
//            currentHint2 = Instantiate(hintParticle, move.transform.position, Quaternion.identity);
//            //GameObject gem1 = board.allGems[(int)move.transform.position.x, (int)move.transform.position.y];
//            Debug.Log($"{(int)move.transform.position.x}, {(int)move.transform.position.y}");
//         //   var icon1Transform = gem1.transform;
//            // icon1Transform.DOShakeScale(0.1f, new Vector3(0.4f, 0.3f, 0f), 0, 0f, false);
//           // icon1Transform.DORotate(new Vector3(0, 360, 0), .5f, RotateMode.FastBeyond360);


//        //    DestroyHint();

//        }
//    }

//    //Destroy the hint
//    public void DestroyHint()
//    {
//        if (currentHint != null)
//        {
//            Destroy(currentHint);
//            currentHint = null;
//            hintDelaySeconds = hintDelay;
//        }

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Debug.Log("00000000000000000000000000 " + hintDelaySeconds);
//        hintDelaySeconds -= Time.deltaTime;
//        if (hintDelaySeconds <= 0)
//        {

//            MarkHint();
//            hintDelaySeconds = hintDelay;
//        }
//    }
//}
