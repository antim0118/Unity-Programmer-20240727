using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

	public bool IsFinished = false;

	[SerializeField] AudioClip CoinSound, BadSound, WinSound;

    readonly Dictionary<int, PlayerStateInfo> playerStates = new Dictionary<int, PlayerStateInfo>
    {
        { 0, new PlayerStateInfo("ÁÅÄÍÛÉ", PlayerModelVariations.casual) },
        { 70, new PlayerStateInfo("ÑÎÑÒÎßÒÅËÜÍÛÉ", PlayerModelVariations.middle) },
        { 100, new PlayerStateInfo("ÁÎÃÀÒÛÉ", PlayerModelVariations.bling) },
    };

    public int TotalMoney
    {
        get => PlayerPrefs.GetInt("money");
        set => PlayerPrefs.SetInt("money", value);
    }
    [HideInInspector] public int LevelMoney = 0;
    int Keys = 0, MaxKeys = 3;
    float MoneyMultiplier = 1f;

    UIManager ui;
	PlayerModelManager playerModel;
    AudioSource audioSource;

	void Start()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);

        ui = GetComponent<UIManager>();
        audioSource = gameObject.AddComponent<AudioSource>();
        playerModel = transform.parent.GetComponentInChildren<PlayerModelManager>();
        UpdateText();
	}

    public void RaiseCollectable(int count)
    {
        LevelMoney += (int)(count * MoneyMultiplier);
        if (LevelMoney < 0)
            LevelMoney = 0;

		UpdateText();

        if (count > 0f)
            audioSource.PlayOneShot(CoinSound, 0.8f);
        else
			audioSource.PlayOneShot(BadSound, 0.8f);
	}

    public void AddMoneyMultiplier(float mult)
    {
		MoneyMultiplier *= mult;
    }

    void UpdateText()
    {
        ui.SetStats(LevelMoney.ToString(), TotalMoney.ToString(), $"{Keys}/{MaxKeys}");

        float progress = LevelMoney / 100f;

		ui.SetProgressAmount(progress);

        var info = playerStates.LastOrDefault(psi => LevelMoney > psi.Key);
        if (info.Value.IsValid())
        {
            ui.SetProgressText(progress, info.Value.Name);
            playerModel.SetVariation(info.Value.Variation);
		}

        if (progress < 0.2f)
            playerModel.SetSpineWeight(1f - (progress * 5f));
        else
            playerModel.SetSpineWeight(0f);
	}

    public void Finish()
    {
		IsFinished = true;
		ui.SetStats("ÇÀÂÅÐØÅÍÎ", TotalMoney.ToString(), $"{Keys}/{MaxKeys}");
        ui.SetFinishWindowActive(true);
        ui.SetProgressActive(false);
	}
}
