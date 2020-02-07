using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

	public void LoadGameOver()

	{
		SceneManager.LoadScene("Game Over");


	}

	public void LoadGame()

	{

		SceneManager.LoadScene("Game");

	}

	public void LoadStartMenu()

	{

		SceneManager.LoadScene(0);

	}

	public void QuitGame()

	{

		Application.Quit();

	}
}
