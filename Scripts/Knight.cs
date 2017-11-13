using System.Collections;

using UnityEngine;

public class Knight : Chessman
{
    // Possible moves the Knight can accomplish.
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        // UpLeft
        KightMove(CurrentX - 1, CurrentY + 2, ref r);

        // UpRight
        KightMove(CurrentX + 1, CurrentY + 2, ref r);

        // RightUp
        KightMove(CurrentX + 2, CurrentY + 1, ref r);

        // RightDown
        KightMove(CurrentX +2, CurrentY -1, ref r);

        // DownLeft
        KightMove(CurrentX - 1, CurrentY - 2, ref r);

        // DownRight
        KightMove(CurrentX + 1, CurrentY - 2, ref r);

        // LeftUp
        KightMove(CurrentX - 2, CurrentY + 1, ref r);

        // LeftDown
        KightMove(CurrentX - 2, CurrentY - 1, ref r);



        return r;
    }

    // Allows each side to move and take over each piece. 
    public void KightMove(int x, int y, ref bool[,]r)
    {
        Chessman c;
        if(x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = BoardManager.Instance.Chessmans[x, y];
            if (c == null)
                r[x, y] = true;
            else if (isWhite != c.isWhite)
                r[x, y] = true;
        }
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
