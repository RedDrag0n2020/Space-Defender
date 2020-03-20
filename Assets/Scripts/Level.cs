using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

	// configuration parameters

	[SerializeField] float delayInSeconds = 2f;

	
	public void LoadGameOver()

	{
		StartCoroutine(WaitAndLoad(delayInSeconds));
		
	}

	IEnumerator WaitAndLoad(float delayInSeconds)

	{
		yield return new WaitForSeconds(delayInSeconds);
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
