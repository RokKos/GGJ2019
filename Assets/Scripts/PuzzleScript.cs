using UnityEngine;

public class PuzzleScript : MonoBehaviour
{
    [SerializeField] GameController game;

    private void OnTriggerEnter( Collider other )
    {
        Debug.Log( "trigger entered!" );
        if ( other != null )
        {
            game.SolvePuzzle( this.gameObject, other.gameObject );
        }
    }
}
