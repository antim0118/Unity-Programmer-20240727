using UnityEngine;

public class Checkpoint : MonoBehaviour
{
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
			audioSource.PlayOneShot(PointSound);
			animator.Play("Checkpoint");
		}
	}
}
