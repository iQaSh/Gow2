using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;



public class EnemyBoard : MonoBehaviour
{
    private FindMatches findMatches;
    public GameState currentState = GameState.move;
    [SerializeField] public int width, height;
    [SerializeField] private Tile _tilePrefab;
    public int offSet;
    public GameObject tilePrefab;
    public Tile tile;

    private BackgroundTile[,] allTiles;
    public GameObject[] gems;
    public GameObject[,] allEnemyGems;
    private float rotationDuration = 0.2f;
    private MoveManager moveManager;

    private bool isDestroyedAtThisMove = false;//был ли матч в этом ходе, для проверки перехода хода


    private void Start()
    {
        moveManager = FindObjectOfType<MoveManager>();
        //   GenerateGrid();
        tile = FindObjectOfType<Tile>();
      //  enemyMove = FindObjectOfType<EnemyMove>();
        findMatches = FindObjectOfType<FindMatches>();
        allTiles = new BackgroundTile[width, height];
        allEnemyGems = new GameObject[width, height];
        SetUp();

    }


    private void SetUp()
    {

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {

                Vector2 tempPosition = new Vector2(x, y + offSet);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;//Возможно тут точка отсчета
                backgroundTile.name = $"{x},{y}";

                int gemToUse = Random.Range(0, gems.Length);
                int maxIterations = 0;
                while (MatchesAt(x, y, gems[gemToUse]) && maxIterations < 100)
                {
                    gemToUse = Random.Range(0, gems.Length);
                    maxIterations++;
                }
                maxIterations = 0;
                GameObject gem = Instantiate(gems[gemToUse], tempPosition, Quaternion.identity);
                gem.GetComponent<EnemyGem>().row = y;
                gem.GetComponent<EnemyGem>().column = x;
                gem.transform.parent = this.transform;
                gem.name = $"({x},{y})";
                allEnemyGems[x, y] = gem;
                //   gem.GetComponent<EnemyGem>().row = y;
                //   gem.GetComponent<EnemyGem>().column = x;

            }



    }

    private bool MatchesAt(int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (allEnemyGems[column - 1, row].tag == piece.tag && allEnemyGems[column - 2, row].tag == piece.tag)
            {
                return true;
            }
            if (allEnemyGems[column, row - 1].tag == piece.tag && allEnemyGems[column, row - 2].tag == piece.tag)
            {
                return true;
            }
        }
        else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allEnemyGems[column, row - 1].tag == piece.tag && allEnemyGems[column, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }
            if (column > 1)
            {
                if (allEnemyGems[column - 1, row].tag == piece.tag && allEnemyGems[column - 2, row].tag == piece.tag)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void DestroyMatchesAt(int column, int row)
    {
        
        if (allEnemyGems[column, row].GetComponent<EnemyGem>().isMatched)
        {
            var icon1Transform = allEnemyGems[column, row].transform;
            Debug.Log($"DestroyMatchesAt +++++++++++++++++++++++++++++++++++");


            icon1Transform.DORotate(new Vector3(0, 360, 0), rotationDuration, RotateMode.FastBeyond360).OnComplete(() =>
            {
                Debug.Log($"DestroyMatchesAt {column}, {row}");

                findMatches.currentMatches.Remove(allEnemyGems[column, row]);
                Destroy(allEnemyGems[column, row]);
                allEnemyGems[column, row] = null;
                isDestroyedAtThisMove = true;


            });


        }
    }

    public void DestroyMatches()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allEnemyGems[x, y] != null)
                {
                    
                    DestroyMatchesAt(x, y);


                }
            }
        }
        findMatches.currentMatches.Clear();
        StartCoroutine(DecreaseRowCo());

    }

    private IEnumerator DecreaseRowCo()
    {
        yield return new WaitForSeconds(.2f);
        int nullCount = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allEnemyGems[x, y] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {

                    var icon1Transform = allEnemyGems[x, y].transform;
                    icon1Transform.DOMove(new Vector2(x, y - nullCount), .2f).SetEase(Ease.Linear).OnComplete(() =>
                    {

                        //        Debug.Log("**************************Done");
                    });
                    allEnemyGems[x, y].GetComponent<EnemyGem>().row -= nullCount;
                    allEnemyGems[x, y] = null;
                    //allGems[x, y].GetComponent<EnemyGem>().row -= nullCount;
                    //allGems[x, y] = null;
                }
            }
            nullCount = 0;
        }

        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoardCo());


    }

    private void RefillBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allEnemyGems[x, y] == null)
                {
                    Vector2 tempPosition = new Vector2(x, y + offSet);
                    int gemToUse = Random.Range(0, gems.Length);
                    GameObject piece = Instantiate(gems[gemToUse], tempPosition, Quaternion.identity);
                    allEnemyGems[x, y] = piece;
                    piece.GetComponent<EnemyGem>().row = y;
                    piece.GetComponent<EnemyGem>().column = x;


                    piece.transform.parent = this.transform;
                }
            }
        }
    }



    private bool MatchesOnBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allEnemyGems[x, y] != null)
                {
                    if (allEnemyGems[x, y].GetComponent<EnemyGem>().isMatched)
                    {
                        return true;
                    }
                }

            }
        }
        return false;

    }

    private bool CheckAllGemsOnPlace() //проверка на месте ли все гемы
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allEnemyGems[x, y] == null)
                {
                    return false;
                }
            }
        }
        return true;

    }

    private IEnumerator FillBoardCo()
    {

        RefillBoard();
        yield return new WaitForSeconds(.5f);

        while (MatchesOnBoard())
        {
            yield return new WaitForSeconds(.3f);
            DestroyMatches();
        }

        yield return new WaitForSeconds(.5f);

        if (IsDeadlocked())
        {

            StartCoroutine(ShuffleBoard());
        }

        currentState = GameState.move;

        yield return new WaitForSeconds(.5f);

        yield return new WaitForSeconds(1f);

        if (CheckAllGemsOnPlace() == true && MatchesOnBoard() == false)
        {
            Debug.Log("конец хода врага");
            moveManager.EndEnemyMove();// конец хода
            isDestroyedAtThisMove = false;
        }

   
    }



    //private void SwitchPieces(int column, int row, Vector2 direction)
    //{
    //    if (allEnemyGems[column + (int)direction.x, row + (int)direction.y] != null)
    //    {
    //        //Take the second piece and save it in a holder
    //        GameObject holder = allEnemyGems[column + (int)direction.x, row + (int)direction.y] as GameObject;
    //        //switching the first dot to be the second position
    //        allEnemyGems[column + (int)direction.x, row + (int)direction.y] = allEnemyGems[column, row];
    //        //Set the first dot to be the second dot
    //        allEnemyGems[column, row] = holder;
    //    }
    //}

    private void SwitchPieces(int column, int row, Vector2 direction)
    {
        //Take the second piece and save it in a holder
        GameObject holder = allEnemyGems[column + (int)direction.x, row + (int)direction.y] as GameObject;
        //switching the first dot to be the second position
        allEnemyGems[column + (int)direction.x, row + (int)direction.y] = allEnemyGems[column, row];
        //Set the first dot to be the second dot
        allEnemyGems[column, row] = holder;
    }

    private bool CheckForMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allEnemyGems[i, j] != null)
                {
                    //Make sure that one and two to the right are in the
                    //board
                    if (i < width - 2)
                    {
                        //Check if the dots to the right and two to the right exist
                        if (allEnemyGems[i + 1, j] != null && allEnemyGems[i + 2, j] != null)
                        {
                            if (allEnemyGems[i + 1, j].tag == allEnemyGems[i, j].tag
                               && allEnemyGems[i + 2, j].tag == allEnemyGems[i, j].tag)
                            {
                                return true;
                            }
                        }

                    }
                    if (j < height - 2)
                    {
                        //Check if the dots above exist
                        if (allEnemyGems[i, j + 1] != null && allEnemyGems[i, j + 2] != null)
                        {
                            if (allEnemyGems[i, j + 1].tag == allEnemyGems[i, j].tag
                               && allEnemyGems[i, j + 2].tag == allEnemyGems[i, j].tag)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }


    //public bool SwitchAndCheck(int column, int row, Vector2 direction)
    //{
    //    Debug.Log($"SwitchAndCheck {column}, {row} | direction.x = {direction.x}, direction.y = {direction.y} ");
    //    if ((column + direction.x <= width - 1) || (column - direction.x >= 0) || (row + direction.y <= height - 1) || (row - direction.y >= 0))
    //    {
    //        Debug.Log($"SwitchAndCheck  вошел {direction}");
    //        SwitchPieces(column, row, direction);
    //        if (CheckForMatches())
    //        {
    //            Debug.Log("CheckForMatches  true");
    //            SwitchPieces(column, row, direction);
    //            return true;
    //        }
    //        SwitchPieces(column, row, direction);
    //    }
    //    Debug.Log("SwitchAndCheck false");
    //    return false;
    //}

    public bool SwitchAndCheck(int column, int row, Vector2 direction)
    {
      //  Debug.Log($"SwitchAndCheck {column}, {row} | direction.x = {direction.x}, direction.y = {direction.y} ");
        if (column + direction.x >= 0 && column + direction.x <= width - 1 && row + direction.y >= 0 && row + direction.x <= height - 1 )
        {
         //   Debug.Log($"SwitchAndCheck ПРОВЕРКУ ПРОШЕЛ ");
            SwitchPieces(column, row, direction);
            if (CheckForMatches())
            {
                SwitchPieces(column, row, direction);
         //       Debug.Log("SwitchAndCheck true");
                return true;
            }
       //     Debug.Log("SwitchAndCheck false");
            SwitchPieces(column, row, direction);
            return false;
        }
       // Debug.Log("SwitchAndCheck ПРОВЕРКУ НЕЕЕЕЕЕЕЕЕЕЕ ПРОШЕЛ");
        return false;
    }

    private bool IsDeadlocked()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allEnemyGems[i, j] != null)
                {
                    if (i < width - 1)
                    {
                        if (SwitchAndCheck(i, j, Vector2.right))
                        {
                            return false;
                        }
                    }
                    if (j < height - 1)
                    {
                        if (SwitchAndCheck(i, j, Vector2.up))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    private IEnumerator ShuffleBoard()
    {
        //    Debug.Log("ShuffleBoard");
        yield return new WaitForSeconds(0.5f);
        //Create a list of game objects
        List<GameObject> newBoard = new List<GameObject>();
        //Add every piece to this list
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allEnemyGems[i, j] != null)
                {
                    newBoard.Add(allEnemyGems[i, j]);
                }
            }
        }
        yield return new WaitForSeconds(0.5f);
        //for every spot on the board. . . 
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //if this spot shouldn't be blank
                //    if (!blankSpaces[i, j] && !concreteTiles[i, j] && !slimeTiles[i, j])
                //   {
                //Pick a random number
                int pieceToUse = Random.Range(0, newBoard.Count);

                //Assign the column to the piece
                int maxIterations = 0;

                while (MatchesAt(i, j, newBoard[pieceToUse]) && maxIterations < 100)
                {
                    pieceToUse = Random.Range(0, newBoard.Count);
                    maxIterations++;
                }
                //Make a container for the piece
                EnemyGem piece = newBoard[pieceToUse].GetComponent<EnemyGem>();
                maxIterations = 0;
                piece.column = i;
                //Assign the row to the piece
                piece.row = j;
                //Fill in the dots array with this new piece
                allEnemyGems[i, j] = newBoard[pieceToUse];
                //Remove it from the list
                newBoard.Remove(newBoard[pieceToUse]);
                //  }
            }
        }
        //Check if it's still deadlocked
        if (IsDeadlocked())
        {
            StartCoroutine(ShuffleBoard());
        }



    }



}