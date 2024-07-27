using ButchersGames;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	[SerializeField] RectTransform Progress;
	[SerializeField] Image ProgressFill;
	[SerializeField] Text ProgressText, LevelMoney, TotalMoney, Keys, Level;
	[SerializeField] GameObject Tutorial;
	[SerializeField] FinishWindow FinishWindow;

	void Start()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	void Update()
	{
		if (Input.touchCount > 0)
			Tutorial.SetActive(false);
	}

	public void SetProgressAmount(float progress)
	{
		ProgressFill.fillAmount = progress;
		if (progress < 0.5f)
			ProgressFill.color = Color.Lerp(Color.red, Color.yellow, progress * 2f);
		else
			ProgressFill.color = Color.Lerp(Color.yellow, Color.green, progress * 2f - 1f);
	}

	public void SetProgressText(float progress, string text)
	{
		ProgressText.text = text;
		if (progress < 0.5f)
			ProgressFill.color = Color.Lerp(Color.red, Color.yellow, progress * 2f);
		else
			ProgressFill.color = Color.Lerp(Color.yellow, Color.green, progress * 2f - 1f);
	}

	public void SetProgressPosition(float position)
	{
		Progress.anchoredPosition = new Vector2(position, Progress.anchoredPosition.y);
	}

	public void SetProgressActive(bool active) => Progress.gameObject.SetActive(active);

	public void SetStats(string money, string totalMoney, string keys)
	{
		Level.text = "Уровень " + (LevelManager.Default.CurrentLevelIndex + 1).ToString();
		LevelMoney.text = money.ToString();
		TotalMoney.text = totalMoney.ToString();
		Keys.text = keys;
	}

	public void SetFinishWindowActive(bool active) => FinishWindow.gameObject.SetActive(active);

	public void ExitGame()
	{
		MainMenu.Instance.ShowMainMenu();
	}
}
