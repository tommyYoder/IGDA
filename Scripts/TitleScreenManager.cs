using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject HowToPlayPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TurnOnPlayPanel()
    {
        HowToPlayPanel.SetActive(true);
    }

    public void TurnOffPlayPanel()
    {
        HowToPlayPanel.SetActive(false);
    }
}
