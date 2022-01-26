using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatches : MonoBehaviour
{
    private Board board;
    public List<GameObject> currentMatches = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
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
        gem.GetComponent<Gem>().isMatched = true;
    }

    private void GetNearbyPieces(GameObject gem1, GameObject gem2, GameObject gem3)
    {

        AddToListAndMatch(gem1);
        AddToListAndMatch(gem2);
        AddToListAndMatch(gem3);


    }

        private IEnumerator FindAllMatchesCo()
    {
        
        yield return new WaitForSeconds(.1f);
        for (int x = 0; x < board.width; x++)
            for (int y = 0; y < board.height; y++)
            {
                GameObject currentGem = board.allGems[x, y];
                if (currentGem != null)
                {
                    if (x > 0 && x < board.width - 1)
                    {
                        GameObject leftGem = board.allGems[x - 1, y];
                        GameObject rightGem = board.allGems[x + 1, y];
                        if (leftGem != null && rightGem != null)
                        {
                            if (leftGem.tag == currentGem.tag && rightGem.tag == currentGem.tag)
                            {
                     //           Debug.Log("fffffffffff");
                                GetNearbyPieces(leftGem, currentGem, rightGem);        
                            }
                        }
                    }

                    if (y > 0 && y < board.height - 1)
                    {
                        GameObject upGem = board.allGems[x, y + 1];
                        GameObject downGem = board.allGems[x, y - 1];
                        
                        if (downGem != null && upGem != null)
                        {
                            if (downGem.tag == currentGem.tag && upGem.tag == currentGem.tag)
                            {
                       //         Debug.Log("ggggggggggggg");
                                GetNearbyPieces(upGem, currentGem, downGem);
                            }
                        }
                    }
                }
            }
    }

}
