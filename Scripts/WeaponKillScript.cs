using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponKillScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ChessPiece")
        {
            Chessman otherChessman = other.GetComponent<Chessman>();
            if ((this.transform.parent.GetComponent<Chessman>().isWhite != otherChessman.isWhite) && (otherChessman.isTarget))
            {
                BoardManager.Instance.activeChessMan.Remove(otherChessman.gameObject);
                Destroy(otherChessman.gameObject);

                if (this.transform.parent.GetComponent<Chessman>().isWhite)
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
    }
}
