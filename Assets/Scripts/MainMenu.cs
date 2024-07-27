using ButchersGames;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public static MainMenu Instance;

	void Start()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	public void StartLevel()
	{
		LevelManager.Default.SelectLevel(0);
		gameObject.SetActive(false);
	}

	public void ShowMainMenu()
	{
		LevelManager.Default.ClearChilds();
		gameObject.SetActive(true);
	}
}
