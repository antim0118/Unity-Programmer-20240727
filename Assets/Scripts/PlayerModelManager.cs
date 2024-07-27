using System.Collections.Generic;
using UnityEngine;

public class PlayerModelManager : MonoBehaviour
{
	public static PlayerModelManager Instance;

	List<SkinnedMeshRenderer> modelVariations = new List<SkinnedMeshRenderer>();
	Animator animator;

	int _current = 0;
	public int CurrentVariation
	{
		get => _current;
		set
		{
			Debug.Log($"setting variant to {_current}->{value}");
			value = Mathf.Clamp(value, 0, modelVariations.Count - 1);
			if (value == _current) return;
			_current = Mathf.Clamp(value, 0, modelVariations.Count - 1);
			for (int i = 0; i < modelVariations.Count; i++)
				modelVariations[i].enabled = i == _current;
		}
	}

	void Start()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);

		animator = GetComponent<Animator>();

		foreach (SkinnedMeshRenderer smr in GetComponentsInChildren<SkinnedMeshRenderer>(true))
		{
			modelVariations.Add(smr);
			smr.enabled = smr.gameObject.activeSelf;
			smr.gameObject.SetActive(true);
		}
	}

	public void SetVariation(PlayerModelVariations var) => CurrentVariation = (int)var;

	public void SetSpineWeight(float weight)
	{
		if (animator != null)
			animator.SetLayerWeight(1, weight);
	}
}