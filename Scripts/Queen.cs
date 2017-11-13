using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Chessman
{
    // Possible moves the Queen can accomplish.
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i,j;

        // right 
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)
                break;

            c = BoardManager.Instance.Chessmans[i, CurrentY];
            if (c == null)
                r[i, CurrentY] = true;

            else
            {
                if (c.isWhite != isWhite)
                    r[i, CurrentY] = true;
                break;
            }
        }


        // Left
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0)
                break;

            c = BoardManager.Instance.Chessmans[i, CurrentY];
            if (c == null)
                r[i, CurrentY] = true;

            else
            {
                if (c.isWhite != isWhite)
                    r[i, CurrentY] = true;
                break;
            }
        }
        // Up
        i = CurrentY;
        while (true)
        {
            i++;
            if (i >= 8)
                break;

            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null)
                r[CurrentX, i] = true;

            else
            {
                if (c.isWhite != isWhite)
                    r[CurrentX, i] = true;
                break;
            }
        }
        // Down
        i = CurrentY;
        while (true)
        {
            i--;
            if (i < 0)
                break;

            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null)
                r[CurrentX, i] = true;

            else
            {
                if (c.isWhite != isWhite)
                    r[CurrentX, i] = true;
                break;
            }
        }

        // Top Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j++;
            if (i < 0 || j >= 8)
                break;

            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;
                break;
            }
        }

        // Top Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j++;
            if (i >= 8 || j >= 8)
                break;

            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;
                break;
            }
        }
        // Down Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j--;
            if (i < 0 || j < 0)
                break;

            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;
                break;
            }
        }
        // Down Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j--;
            if (i >= 8 || j < 0)
                break;

            // Allows each side to move and take over each piece. 
            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;
                break;
            }
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
