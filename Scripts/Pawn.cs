using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman
{
    // Possible moves the Pawn can accomplish.
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c, c2;
        int[] e = BoardManager.Instance.enPassantMove;

        // White team move

        if (isWhite)
        {
            // Diagnoal left 
            if (CurrentX != 0 && CurrentY != 7)
            {
            
                if(e[0] == CurrentX -1 && e[1] == CurrentY + 1)
                 r[CurrentX - 1, CurrentY + 1] = true;
                


                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                    r[CurrentX - 1, CurrentY + 1] = true;
            }
            //Diaganol Right
            if (CurrentX != 7 && CurrentY != 7)
            {
            
                if (e[0] == CurrentX + 1 && e[1] == CurrentY + 1)
                    r[CurrentX + 1, CurrentY + 1] = true;


                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                    r[CurrentX + 1, CurrentY + 1] = true;
            }
            // Middle
            if (CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                if (c == null)
                    r[CurrentX, CurrentY + 1] = true;
            }

            // Middle on first move
            if (CurrentY == 1)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 2];
                if (c == null & c2 == null)
                    r[CurrentX, CurrentY + 2] = true;
            }
        }

        else
        {

            // Diagnoal left 
            if (CurrentX != 0 && CurrentY != 0)
            {
                if (e[0] == CurrentX - 1 && e[1] == CurrentY - 1)
                    r[CurrentX - 1, CurrentY - 1] = true;

                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    r[CurrentX - 1, CurrentY - 1] = true;
            }
            //Diaganol Right
            if (CurrentX != 7 && CurrentY != 0)
            {
                if (e[0] == CurrentX + 1 && e[1] == CurrentY - 1)
                    r[CurrentX + 1, CurrentY - 1] = true;

                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    r[CurrentX + 1, CurrentY - 1] = true;
            }
            // Middle
            if (CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                if (c == null)
                    r[CurrentX, CurrentY - 1] = true;
            }

            // Middle on first move
            if (CurrentY == 6)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 2];
                if (c == null & c2 == null)
                    r[CurrentX, CurrentY - 2] = true;

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




