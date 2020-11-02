using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	Text scoreText;
	GameController gameController;
	
	// Use this for initialization
	void Start () 
	{
		scoreText = GetComponent<Text>();
		gameController = FindObjectOfType<GameController>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		scoreText.text = gameController.GetScore().ToString();
	}
}
