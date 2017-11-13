using System.Collections;
using UnityEngine;

public abstract class Chessman : MonoBehaviour {

	public int CurrentX { set; get; }
    public int CurrentY { set; get; }
    public bool isWhite;
    public bool isTarget;

    // Sets the position in the x and y directions when game is played.
    public void SetPosition( int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    // Sets a Array or possible moves that each chess piece calls upon.
    public virtual bool[,] PossibleMove()
    {
        return new bool [8,8];
    }
}
