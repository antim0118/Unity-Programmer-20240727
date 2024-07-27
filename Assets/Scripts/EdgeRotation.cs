using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeRotation : MonoBehaviour
{
    public bool isLeft = false;
    public Vector3 StartPosition => transform.position - transform.forward * transform.localScale.z / 2f;
    public Vector3 EndPositionLeft => transform.position - transform.right * transform.localScale.x / 2f - transform.forward * transform.localScale.z;
    public Vector3 EndPositionRight => transform.position - transform.right * transform.localScale.x / 2f;

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			if (isLeft)
				PlayerController.Instance.Rotate(EndPositionLeft, transform.localEulerAngles.y - 180f);
			else
				PlayerController.Instance.Rotate(EndPositionRight, transform.localEulerAngles.y);
		}
	}

#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawCube(StartPosition, Vector3.one / 2f);
		Gizmos.color = Color.red;
        if (isLeft)
            Gizmos.DrawCube(EndPositionLeft, Vector3.one / 2f);
        else
			Gizmos.DrawCube(EndPositionRight, Vector3.one / 2f);
	}
#endif
}
