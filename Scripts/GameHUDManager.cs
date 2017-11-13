using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUDManager : MonoBehaviour
{
    public Text MonsterScoreTxt;
    public Text VictimScoreTxt;
    public Text DescriptiveTxt;
    public GameObject PieceSwitcherUI;
    public GameObject MatchWonDialogBox;
    private string chosenPiece;
 


    public static GameHUDManager instance;

	// Use this for initialization
	void Start ()
    {
        MonsterScoreTxt.text = "0";
        VictimScoreTxt.text = "0";
        instance = this;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdateScores()
    {
        MonsterScoreTxt.text = GameManager.BlackPlayerScore.ToString();
        VictimScoreTxt.text = GameManager.WhitePlayerScore.ToString();
    }

    public void ActivatePieceSwitcherUI()
    {
        PieceSwitcherUI.SetActive(true);
    }

    public void UpdateDescriptiveText(string text)
    {
        DescriptiveTxt.text = text;
        chosenPiece = text;
    }

    public void ActivateMatchWonDialogBox()
    {
        MatchWonDialogBox.SetActive(true);
    }

    public void UpdateGameManager()
    {
        PieceSwitcherUI.SetActive(false);
        BoardManager.Instance.PieceSwitcherup = false;
        if (!BoardManager.Instance.isWhiteTurn)
        {
            GameManager.WhitePlayerAddPiece(chosenPiece);
            GameManager.WhitePlayerRemovePiece("Pawn");
            GameManager.WhitePlayerAddScore(GameManager.PlayerHasRevivedPiece());
            chosenPiece = ("WHITE " + chosenPiece).ToUpper();
        }
        else
        {
            GameManager.BlackPlayerAddPiece(chosenPiece);
            GameManager.BlackPlayerRemovePiece("Pawn");
            GameManager.BlackPlayerAddScore(GameManager.PlayerHasRevivedPiece());
            chosenPiece = ("BLACK " + chosenPiece).ToUpper();
        }

        BoardManager.Instance.SpawnSelectedPiece(chosenPiece);
    }

}
