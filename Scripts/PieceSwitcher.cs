using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceSwitcher : MonoBehaviour
{
    public Text DescriptiveTxt;
    private string chosenPiece;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateDescriptiveText(string text)
    {
        DescriptiveTxt.text = text;
        chosenPiece = text;
    }

    public void UpdateGameManager()
    {

    }
}
