using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ChessPiece")
        {
            Chessman otherChessman = other.GetComponent<Chessman>();
            if ((this.GetComponent<Chessman>().isWhite != otherChessman.isWhite) && (otherChessman.isTarget))
            {
                
                BoardManager.Instance.activeChessMan.Remove(otherChessman.gameObject);
                Destroy(otherChessman.gameObject);

                Instantiate(BoardManager.Instance.DeathParticles, transform.position, Quaternion.identity);
                BoardManager.Instance.AddFog(0.03f);
                GetComponent<AudioSource>().Play();

                if (this.GetComponent<Chessman>().isWhite)
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
                    GameHUDManager.instance.ActivateMatchWonDialogBox();
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
