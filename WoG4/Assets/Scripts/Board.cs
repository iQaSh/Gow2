using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;

public enum GameState
{
    wait,
    move
}

public class Board : MonoBehaviour
{
    private HintManager hintManager;
    private FindMatches findMatches;
    public GameState currentState = GameState.move;
    [SerializeField] public int width, height;
    [SerializeField] private Tile _tilePrefab;
    public int offSet;
    public GameObject tilePrefab;
    public Tile tile;

    private BackgroundTile[,] allTiles;
    public GameObject[] gems;
    public GameObject[,] allGems;
    private float rotationDuration = 0.5f;
    private PlayerStatsManager playerStatsManager;





    private void Start()
    {
     //   GenerateGrid();
        tile = FindObjectOfType<Tile>();
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
        hintManager = FindObjectOfType<HintManager>();
        findMatches = FindObjectOfType<FindMatches>();
        allTiles = new BackgroundTile[width, height];
        allGems = new GameObject[width, height];
        SetUp();

    }


    private void SetUp()
    {

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
            
                Vector2 tempPosition = new Vector2(x, y + offSet);
                Vector2 tilePosition = new Vector2(x, y);
                GameObject backgroundTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;//Возможно тут точка отсчета
                backgroundTile.name = $"{x},{y}";

                int gemToUse = Random.Range(0, gems.Length);
                int maxIterations = 0;
                while (MatchesAt(x,y, gems[gemToUse]) && maxIterations < 100)
                {
                    gemToUse = Random.Range(0, gems.Length);
                    maxIterations++;
                }
                maxIterations = 0;
                GameObject gem = Instantiate(gems[gemToUse], tempPosition, Quaternion.identity);
                gem.GetComponent<Gem>().row = y;
                gem.GetComponent<Gem>().column = x;
                gem.transform.parent = this.transform;
                gem.name = $"({x},{y})";
                allGems[x, y] = gem;
             //   gem.GetComponent<Gem>().row = y;
             //   gem.GetComponent<Gem>().column = x;

            }



    }

    private bool MatchesAt (int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (allGems[column -1, row].tag == piece.tag && allGems[column - 2, row].tag == piece.tag)
            {
                return true;
            }
            if (allGems[column, row - 1].tag == piece.tag && allGems[column, row - 2].tag == piece.tag)
            {
                return true;
            }
        } else if(column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allGems[column, row -1].tag == piece.tag && allGems[column, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }
            if (column > 1)
            {
                if (allGems[column -1, row].tag == piece.tag && allGems[column - 2, row].tag == piece.tag)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void DestroyMatchesAt(int column, int row)
    {
        if (allGems[column, row].GetComponent<Gem>().isMatched)
        {
            var icon1Transform = allGems[column, row].transform;


            playerStatsManager.RecieveMana(allGems[column, row]);
            icon1Transform.DORotate(new Vector3(0, 360, 0), rotationDuration, RotateMode.FastBeyond360).OnComplete(() =>
            {
        //        Debug.Log("DestroyMatchesAt");
        
                hintManager.sequence.Kill();
                DOTween.Kill(hintManager.icon1Transform);
                DOTween.Kill(hintManager.icon2Transform);
                findMatches.currentMatches.Remove(allGems[column, row]);


                
                Destroy(allGems[column, row]);
                
                allGems[column, row] = null;

                hintManager.isHint = false;


            });
              

        }
    }

    public void DestroyMatches()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allGems[x, y] != null)
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
        yield return new WaitForSeconds(.5f);
        int nullCount = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allGems[x, y] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {

                    var icon1Transform = allGems[x, y].transform;
                    icon1Transform.DOMove(new Vector2(x, y - nullCount), .2f).SetEase(Ease.Linear).OnComplete(() =>
                     {
                     
                 //        Debug.Log("**************************Done");
                     });
                    allGems[x, y].GetComponent<Gem>().row -= nullCount;
                    allGems[x, y] = null;
                    //allGems[x, y].GetComponent<Gem>().row -= nullCount;
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
                if (allGems[x, y] == null)
                {
                    Vector2 tempPosition = new Vector2(x, y + offSet);
                    int gemToUse = Random.Range(0, gems.Length);
                    GameObject piece = Instantiate(gems[gemToUse], tempPosition, Quaternion.identity);
                    allGems[x, y] = piece;
                    piece.GetComponent<Gem>().row = y;
                    piece.GetComponent<Gem>().column = x;
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
                if (allGems[x,y] != null)
                {
                    if (allGems[x, y].GetComponent<Gem>().isMatched)
                    {
                        return true;
                    }
                }

            }
        }
        return false;

    }

    private IEnumerator FillBoardCo()
    {
   
        RefillBoard();
        yield return new WaitForSeconds(.5f);

        while (MatchesOnBoard())
        {
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }

        yield return new WaitForSeconds(.5f);

        if (IsDeadlocked())
        {
           
           StartCoroutine(ShuffleBoard());
        }

        currentState = GameState.move;
    }



    private void SwitchPieces(int column, int row, Vector2 direction)
    {
        if (allGems[column + (int)direction.x, row + (int)direction.y] != null)
        {
            //Take the second piece and save it in a holder
            GameObject holder = allGems[column + (int)direction.x, row + (int)direction.y] as GameObject;
            //switching the first dot to be the second position
            allGems[column + (int)direction.x, row + (int)direction.y] = allGems[column, row];
            //Set the first dot to be the second dot
            allGems[column, row] = holder;
        }
    }

    private bool CheckForMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allGems[i, j] != null)
                {
                    //Make sure that one and two to the right are in the
                    //board
                    if (i < width - 2)
                    {
                        //Check if the dots to the right and two to the right exist
                        if (allGems[i + 1, j] != null && allGems[i + 2, j] != null)
                        {
                            if (allGems[i + 1, j].tag == allGems[i, j].tag
                               && allGems[i + 2, j].tag == allGems[i, j].tag)
                            {
                                return true;
                            }
                        }

                    }
                    if (j < height - 2)
                    {
                        //Check if the dots above exist
                        if (allGems[i, j + 1] != null && allGems[i, j + 2] != null)
                        {
                            if (allGems[i, j + 1].tag == allGems[i, j].tag
                               && allGems[i, j + 2].tag == allGems[i, j].tag)
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


    public bool SwitchAndCheck(int column, int row, Vector2 direction)
    {
        if ((column + direction.y <= width) || (column - direction.y >= 0) || (row + direction.x <= height) || (row - direction.x >= 0))
        {
            SwitchPieces(column, row, direction);
            if (CheckForMatches())
            {
                SwitchPieces(column, row, direction);
                return true;
            }
            SwitchPieces(column, row, direction);
        }

        return false;
    }

    private bool IsDeadlocked()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allGems[i, j] != null)
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
                if (allGems[i, j] != null)
                {
                    newBoard.Add(allGems[i, j]);
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
                    Gem piece = newBoard[pieceToUse].GetComponent<Gem>();
                    maxIterations = 0;
                    piece.column = i;
                    //Assign the row to the piece
                    piece.row = j;
                    //Fill in the dots array with this new piece
                    allGems[i, j] = newBoard[pieceToUse];
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