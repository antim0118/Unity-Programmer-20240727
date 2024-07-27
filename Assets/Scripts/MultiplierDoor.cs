using UnityEngine;

public class MultiplierDoor : MonoBehaviour
{
	public float Multiplier = 2f;

	[SerializeField] AudioClip PointSound;

	AudioSource audioSource;
	Animator animator;

	void Start()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
		animator = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			GameManager.Instance.AddMoneyMultiplier(Multiplier);
			audioSource.PlayOneShot(PointSound);
			animator.Play("MultiplierDoor");
		}
	}
}
