using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header( "Puzzle portraits" )]
    [SerializeField] List<GameObject> puzzlePortraits = new List<GameObject>();

    [Header("Puzzle spites")]
    [SerializeField] List<GameObject> puzzleSpites = new List<GameObject>();

    [Header( "Portrait lights" )]
    [SerializeField] List<Light> portraitLights = new List<Light>();

    [Header( "Puzzle Items" )]
    [SerializeField] List<GameObject> puzzle1Items = new List<GameObject>();
    [SerializeField] List<GameObject> puzzle2Items = new List<GameObject>();
    [SerializeField] List<GameObject> puzzle3Items = new List<GameObject>();
    [SerializeField] List<GameObject> puzzle4Items = new List<GameObject>();
    //[SerializeField] List<GameObject> puzzle5Items = new List<GameObject>();

    [Header( "Player" )]
    [SerializeField] CharacterController characterController;

    private int currPuzzle = 0;

    private void Start()
    {
        ResetPortraits();
    }

    private void ResetPortraits()
    {
        currPuzzle = 0;
        if ( puzzlePortraits.Count > 0 )
        {
            foreach ( var portrait in puzzlePortraits )
            {
                portrait.SetActive( false );
            }

            foreach (var sprite in puzzleSpites) {
                sprite.SetActive(false);
            }



            if ( portraitLights.Count > 0 )
            {
                foreach ( var light in portraitLights )
                {
                    light.gameObject.SetActive( false );
                }
            }

            SwitchPortrait( 0, true );
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
            //case 4:
            //    goCheck = puzzle5Items; break;
            default:
                goCheck = null; Debug.LogError("something went wrong while checking for puzzle solutions"); break;
        }
        if ( goCheck != null && goCheck.Contains( collidingObj ) )
        {
            currPuzzle++;
            Debug.Log("you solved the puzzle #" + ( puzzleIndex + 1 ) );
            // to next puzzle
            SwitchPortrait( puzzleIndex, false );
            characterController.InsertPuzzleObject();
            if ( puzzleIndex < puzzlePortraits.Count - 1 )
            {
                SwitchPortrait( puzzleIndex + 1, true );

                characterController.SwitchGravity();
                // TODO: remove puzzle piece from the world/hand and setup the new picture in the old portrait
            }
            else
            {
                PlayEnding();
            }
        }
    }

    private void SwitchPortrait(int whichPortrait, bool toActivate)
    {
        puzzlePortraits[whichPortrait].SetActive( toActivate );
        portraitLights[whichPortrait].gameObject.SetActive( toActivate );
        puzzleSpites[whichPortrait].SetActive(toActivate);
    }

    private void PlayEnding()
    {
        //TODO: do proper endgame stuff
        Debug.Log( "YOU WIN" );
        UnityEngine.SceneManagement.SceneManager.LoadScene( this.gameObject.scene.buildIndex );
    }

    public GameObject GetCurrentPuzzlePiece() {
        
        switch (currPuzzle) {
            case 0:
                return puzzle1Items[0];
            case 1:
                return puzzle2Items[0];
            case 2:
                return puzzle3Items[0];
            case 3:
                return puzzle4Items[0];
            default:
                Debug.LogError("something went wrong while checking for puzzle solutions");
                return null;
        }
    }
}
