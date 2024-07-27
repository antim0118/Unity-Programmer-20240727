using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
			GameManager.Instance.Finish();
	}
}
