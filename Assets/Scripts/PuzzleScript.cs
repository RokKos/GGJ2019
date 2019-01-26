using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript : MonoBehaviour
{
    GameController game;

    private void Start()
    {
        game = FindObjectOfType<GameController>();
    }

    private void OnCollisionEnter( Collision collision )
    {
        // do smth smh
        game.SolvePuzzle( this.gameObject, collision.gameObject );
    }
}
