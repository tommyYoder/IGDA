using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    /*#region End Game Text Variables
    [Header("Victim Values")]
    [Header("Score Text Variables")]
    public Text VictimPlayerScoreTxt;
    public Text VictimMovementScoreTxt;
    public Text VictimKingPieceScoreTxt;
    public Text VictimQueenPieceScoreTxt;
    public Text VictimBishopPieceScoreTxt;
    public Text VictimKnightPieceScoreTxt;
    public Text VictimRookPieceScoreTxt;
    public Text VictimPawnPieceScoreTxt;
    public Text VictimGameClearedScoreTxt;
    public Text VictimFinalScoreTxt;

    [Header("Piece Counters")]
    public Text VictimMovesCounterTxt;
    public Text VictimKingCounterTxt;
    public Text VictimQueenCounterTxt;
    public Text VictimBishopCounterTxt;
    public Text VictimKnightCounterTxt;
    public Text VictimRookCounterTxt;
    public Text VictimPawnCounterTxt;

    [Header("Monster Values")]
    [Header("Score Text Variables")]
    public Text MonsterPlayerScoreTxt;
    public Text MonsterMovementScoreTxt;
    public Text MonsterKingPieceScoreTxt;
    public Text MonsterQueenPieceScoreTxt;
    public Text MonsterBishopPieceScoreTxt;
    public Text MonsterKnightPieceScoreTxt;
    public Text MonsterRookPieceScoreTxt;
    public Text MonsterPawnPieceScoreTxt;
    public Text MonsterGameClearedScoreTxt;
    public Text MonsterFinalScoreTxt;

    [Header("Piece Counters")]
    public Text MonsterMovesCounterTxt;
    public Text MonsterKingCounterTxt;
    public Text MonsterQueenCounterTxt;
    public Text MonsterBishopCounterTxt;
    public Text MonsterKnightCounterTxt;
    public Text MonsterRookCounterTxt;
    public Text MonsterPawnCounterTxt;

    public GameObject PlayAgainBox;
    private float PlayBoxSpawnTimer;
    public float PlayBoxSpawnMaxTime;
    public Text ReplayBoxMessage;
    #endregion*/

    public Text GameRoundsTxt;
    public Text VictimScoreTxt;
    public Text VictimFinalScoreTxt;
    public Text MonsterScoreTxt;
    public Text MonsterFinalScoreTxt;
    public Text EndMessageTxt;
    // Use this for initialization
    void Start ()
    {
        /*PlayBoxSpawnTimer = 0f;
        WriteEndVictimValues();
        WriteEndMonsterValues();*/
        GameRoundsTxt.text = GameManager.WhitePlayerMoveCount.ToString();
        VictimScoreTxt.text = GameManager.WhitePlayerScore.ToString();
        VictimFinalScoreTxt.text = GameManager.CalculateWhiteFinalScore().ToString();
        MonsterScoreTxt.text = GameManager.BlackPlayerScore.ToString();
        MonsterFinalScoreTxt.text = GameManager.CalculateBlackFinalScore().ToString();

        if (GameManager.WhitePlayerWon)
        {
            EndMessageTxt.text = "Victims have triumphed!";
        }
        else
        {
            EndMessageTxt.text = "Monsters have slain all!";
        }	
	}
	
	// Update is called once per frame
	void Update ()
    {
		/*if(PlayBoxSpawnTimer >= PlayBoxSpawnMaxTime)
        {
            PlayAgainBox.SetActive(true);
            if (GameManager.WhitePlayerWon)
                ReplayBoxMessage.text = "The Victims have Truimphed!";
            else if (GameManager.BlackPlayerWon)
                ReplayBoxMessage.text = "The Monsters have slain all!";

        }
        else
        {
            PlayBoxSpawnTimer += Time.deltaTime;
        }*/
	}

    /*void WriteEndVictimValues()
    {
        VictimPlayerScoreTxt.text = GameManager.WhitePlayerScore.ToString();
        VictimMovesCounterTxt.text = GameManager.WhitePlayerMoveCount.ToString();
        VictimMovementScoreTxt.text = GameManager.GetWhiteAdjMoveScore().ToString();
        VictimKingCounterTxt.text = GameManager.WhitePlayerChessPieceCounts["KING"].ToString();
        VictimKingPieceScoreTxt.text = (GameManager.WhitePlayerChessPieceCounts["KING"] * GameManager.GetPieceWorth("KING")).ToString();
        VictimQueenCounterTxt.text = GameManager.WhitePlayerChessPieceCounts["QUEEN"].ToString();
        VictimQueenPieceScoreTxt.text = (GameManager.WhitePlayerChessPieceCounts["QUEEN"] * GameManager.GetPieceWorth("Queen")).ToString();
        VictimBishopCounterTxt.text = GameManager.WhitePlayerChessPieceCounts["BISHOP"].ToString();
        VictimBishopPieceScoreTxt.text = (GameManager.WhitePlayerChessPieceCounts["BISHOP"] * GameManager.GetPieceWorth("Bishop")).ToString();
        VictimKnightCounterTxt.text = GameManager.WhitePlayerChessPieceCounts["KNIGHT"].ToString();
        VictimKnightPieceScoreTxt.text = (GameManager.WhitePlayerChessPieceCounts["KNIGHT"] * GameManager.GetPieceWorth("Knight")).ToString();
        VictimRookCounterTxt.text = GameManager.WhitePlayerChessPieceCounts["ROOK"].ToString();
        VictimRookPieceScoreTxt.text = (GameManager.WhitePlayerChessPieceCounts["ROOK"] * GameManager.GetPieceWorth("Rook")).ToString();
        VictimPawnCounterTxt.text = GameManager.WhitePlayerChessPieceCounts["PAWN"].ToString();
        VictimPawnPieceScoreTxt.text = (GameManager.WhitePlayerChessPieceCounts["PAWN"] * GameManager.GetPieceWorth("Rook")).ToString();

        if (GameManager.WhitePlayerWon)
            VictimGameClearedScoreTxt.text = GameManager.PlayerHasWon().ToString();
        else
            VictimGameClearedScoreTxt.text = "0";

        VictimFinalScoreTxt.text = GameManager.CalculateWhiteFinalScore().ToString();
    }

    void WriteEndMonsterValues()
    {
        MonsterPlayerScoreTxt.text = GameManager.BlackPlayerScore.ToString();
        MonsterMovesCounterTxt.text = GameManager.BlackPlayerMoveCount.ToString();
        MonsterMovementScoreTxt.text = GameManager.GetBlackAdjMoveScore().ToString();
        MonsterKingCounterTxt.text = GameManager.BlackPlayerChessPieceCounts["KING"].ToString();
        MonsterKingPieceScoreTxt.text = (GameManager.BlackPlayerChessPieceCounts["KING"] * GameManager.GetPieceWorth("KING")).ToString();
        MonsterQueenCounterTxt.text = GameManager.BlackPlayerChessPieceCounts["QUEEN"].ToString();
        MonsterQueenPieceScoreTxt.text = (GameManager.BlackPlayerChessPieceCounts["QUEEN"] * GameManager.GetPieceWorth("Queen")).ToString();
        MonsterBishopCounterTxt.text = GameManager.BlackPlayerChessPieceCounts["BISHOP"].ToString();
        MonsterBishopPieceScoreTxt.text = (GameManager.BlackPlayerChessPieceCounts["BISHOP"] * GameManager.GetPieceWorth("Bishop")).ToString();
        MonsterKnightCounterTxt.text = GameManager.BlackPlayerChessPieceCounts["KNIGHT"].ToString();
        MonsterKnightPieceScoreTxt.text = (GameManager.BlackPlayerChessPieceCounts["KNIGHT"] * GameManager.GetPieceWorth("Knight")).ToString();
        MonsterRookCounterTxt.text = GameManager.BlackPlayerChessPieceCounts["ROOK"].ToString();
        MonsterRookPieceScoreTxt.text = (GameManager.BlackPlayerChessPieceCounts["ROOK"] * GameManager.GetPieceWorth("Rook")).ToString();
        MonsterPawnCounterTxt.text = GameManager.BlackPlayerChessPieceCounts["PAWN"].ToString();
        MonsterPawnPieceScoreTxt.text = (GameManager.BlackPlayerChessPieceCounts["PAWN"] * GameManager.GetPieceWorth("Rook")).ToString();

        if (GameManager.BlackPlayerWon)
            MonsterGameClearedScoreTxt.text = GameManager.PlayerHasWon().ToString();
        else
            MonsterGameClearedScoreTxt.text = "0";

        MonsterFinalScoreTxt.text = GameManager.CalculateBlackFinalScore().ToString();
    }*/
}
