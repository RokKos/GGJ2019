using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header( "Puzzle portraits" )]
    [SerializeField] List<GameObject> puzzlePortraits = new List<GameObject>();

    [Header( "Puzzle Items" )]
    [SerializeField] List<GameObject> puzzle1Items = new List<GameObject>();
    [SerializeField] List<GameObject> puzzle2Items = new List<GameObject>();
    [SerializeField] List<GameObject> puzzle3Items = new List<GameObject>();
    [SerializeField] List<GameObject> puzzle4Items = new List<GameObject>();
    [SerializeField] List<GameObject> puzzle5Items = new List<GameObject>();

    private void Start()
    {
        ResetPortraits();
    }

    private void ResetPortraits()
    {
        if ( puzzlePortraits.Count > 0 )
        {
            foreach ( var portrait in puzzlePortraits )
            {
                portrait.SetActive( false );
            }
            // activate the first puzzle
            puzzlePortraits[0].SetActive( true );
        }
    }

    public void SolvePuzzle(GameObject whichPuzzle, GameObject collidingObj)
    {
        int puzzleIndex = puzzlePortraits.FindIndex(p => p == whichPuzzle);
        List<GameObject> goCheck;
        switch (puzzleIndex)
        {
            case 0:
                goCheck = puzzle1Items; break;
            case 1:
                goCheck = puzzle2Items; break;
            case 2:
                goCheck = puzzle3Items; break;
            case 3:
                goCheck = puzzle4Items; break;
            case 4:
                goCheck = puzzle5Items; break;
            default:
                goCheck = null; Debug.LogError("something went wrong while checking for puzzle solutions"); break;
        }
        if ( goCheck != null && goCheck.Contains( collidingObj ) )
        {
            Debug.Log("you solved the puzzle #" + ( puzzleIndex + 1 ) );
            // to next puzzle
            puzzlePortraits[puzzleIndex].SetActive( false );
            if ( puzzleIndex < puzzlePortraits.Count - 1 )
            {
                puzzlePortraits[puzzleIndex + 1].SetActive( true );
            }
            else
            {
                Debug.Log( "YOU WIN" );
            }
        }
    }
}
