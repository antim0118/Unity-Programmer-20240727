using ButchersGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishWindow : MonoBehaviour
{
    [SerializeField] Text Money, Mult, MultMoney;

	Animator animator;

    float val = 0f;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
    {
        val += Time.deltaTime * 2f;
		float mul = 1f - Mathf.Abs(Mathf.Sin(val));

		animator.SetLayerWeight(1, mul);

		mul = getMultiplier(mul);

		Mult.text = $"онксвхрэ X{mul}";

        int money = GameManager.Instance.LevelMoney;
		Money.text = money.ToString();
        MultMoney.text = Mathf.FloorToInt(money * mul).ToString();
	}

    float getMultiplier(float value)
    {
		float mul = value;
		if (mul < 0.3f)
			mul = 2f;
		else if (mul < 0.6f)
			mul = 3f;
		else if (mul < 0.8f)
			mul = 4f;
		else
			mul = 5f;
        return mul;
	}

    bool canGive = true;

    public void GiveMultClick()
    {
        if (!canGive) return;
        canGive = false;

		float mul = 1f - Mathf.Abs(Mathf.Sin(val));
		mul = getMultiplier(mul);
		int money = (int)(GameManager.Instance.LevelMoney * mul);
		GameManager.Instance.TotalMoney += money;

		LevelManager.Default.NextLevel();
	}

    public void GiveClick()
    {
		if (!canGive) return;
		canGive = false;

		int money = GameManager.Instance.LevelMoney;
		GameManager.Instance.TotalMoney += money;

		LevelManager.Default.NextLevel();
	}
}
