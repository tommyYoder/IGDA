using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman
{
    // Possible moves the King can accomplish.
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;

        // Topside

        i = CurrentX - 1;
        j = CurrentY + 1;

        if (CurrentY != 7)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = BoardManager.Instance.Chessmans[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }
                i++;

            }
        }
        // Downside

        i = CurrentX - 1;
        j = CurrentY - 1;

        if (CurrentY != 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = BoardManager.Instance.Chessmans[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }
                i++;

            }
        }
        // Middle left
        if (CurrentX != 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            if (c == null)
                r[CurrentX - 1, CurrentY] = true;

            else if (isWhite != c.isWhite)
                r[CurrentX - 1, CurrentY] = true;
        }
        // Middle Right
        if (CurrentX != 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
            if (c == null)
                r[CurrentX + 1, CurrentY] = true;

            else if (isWhite != c.isWhite)
                r[CurrentX + 1, CurrentY] = true;
        }


        return r;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ChessPiece")
        {
            Chessman otherChessman = other.GetComponent<Chessman>();
            if ((this.isWhite != otherChessman.isWhite) && (otherChessman.isTarget))
            {
                BoardManager.Instance.activeChessMan.Remove(otherChessman.gameObject);
                Destroy(otherChessman.gameObject);

                if (this.isWhite)
                {
                    GameManager.WhitePlayerAddScore(GameManager.GetPieceWorth(otherChessman.GetType().ToString()));
                    GameManager.BlackPlayerRemovePiece(otherChessman.GetType().ToString());
                }
                else
                {
                    GameManager.BlackPlayerAddScore(GameManager.GetPieceWorth(otherChessman.GetType().ToString()));
                    GameManager.WhitePlayerRemovePiece(otherChessman.GetType().ToString());
                }
                if (otherChessman.GetType() == typeof(King))
                {
                    BoardManager.Instance.EndGame();
                    return;
                }

            }
            else
            {
                return;
            }

        }
    }*/
}
