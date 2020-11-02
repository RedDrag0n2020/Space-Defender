using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	int score = 0;
	
	private void Awake()

	{
		SetUpSingleton();

	}

	private void SetUpSingleton()
	{
		int numberGameSessions = FindObjectsOfType<GameController>().Length;

		if (numberGameSessions > 1)

		{

			Destroy(gameObject);
		}

		else
		{

			DontDestroyOnLoad(gameObject);

		}
	}

	public int GetScore()

	{
		return score;

	}

	
	
	public void AddToScore(int ScoreValue)
	{

		score += ScoreValue;
	}

	

	public void ResetGame()
	{
		Destroy(gameObject);

	}
}
