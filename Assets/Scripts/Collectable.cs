using UnityEngine;

public class Collectable : MonoBehaviour
{
	public int Count = 2;
	public bool RotateSprite = true;

	void Update()
	{
		if (RotateSprite)
			transform.LookAt(Camera.main.transform, Vector3.up);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			GameManager.Instance.RaiseCollectable(Count);
			Destroy(gameObject);
		}
	}
}
