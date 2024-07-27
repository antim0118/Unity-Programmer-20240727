using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static PlayerController Instance;

	Camera cam;
	[HideInInspector] public PlayerModelManager playerModel;
	public float limitBounds = 1.75f;
	public float touchIntensity = 6f;
	public float speed = 10.0f;

	Vector3 modelPos = Vector3.zero;
	Vector3 initCameraPos = Vector3.zero;

	bool isRotating = false;
	Vector3 rotationPos;
	Quaternion rotationRot;

	void Start()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);

		cam = GetComponentInChildren<Camera>();
		initCameraPos = cam.transform.localPosition;
		playerModel = GetComponentInChildren<PlayerModelManager>();
	}

	void Update()
	{
		if (GameManager.Instance.IsFinished)
			return;

		if (Input.touchCount > 0)
			modelPos.x += Input.touches[0].deltaPosition.x / (float)Screen.width * touchIntensity;
		modelPos.x = Mathf.Clamp(modelPos.x, -limitBounds, limitBounds);

		float difX = (modelPos - playerModel.transform.localPosition).x;
		UIManager.Instance.SetProgressPosition(difX * 50f);

		//плавное движение игрока
		playerModel.transform.localEulerAngles = new Vector3(0, difX * 20f, 0);
		playerModel.transform.localPosition += (modelPos - playerModel.transform.localPosition) * Time.deltaTime * 10f;

		//плавное движение камеры
		cam.transform.localPosition += (modelPos - cam.transform.localPosition + initCameraPos) * Time.deltaTime * 5f;

		if (isRotating)
		{
			//плавный поворот
			Vector3 dif = rotationPos - transform.position;
			if (dif.magnitude < 0.05f)
			{
				isRotating = false;
				transform.position = rotationPos;
				transform.rotation = rotationRot;
			}
			else
			{
				transform.position += (rotationPos - transform.position) * Time.deltaTime * speed * 8f;
				transform.rotation = Quaternion.Lerp(transform.localRotation, rotationRot, Time.deltaTime * speed * 8f);
			}
		}
		else
		{
			//движение вперед
			transform.localPosition += transform.forward * Time.deltaTime * speed;
		}
	}

	public void Rotate(Vector3 worldPosition, float rotationAngle)
	{
		Debug.Log($"Rotate called: {worldPosition}; {rotationAngle}");
		rotationPos = worldPosition;
		rotationRot = Quaternion.Euler(0, rotationAngle, 0);
		isRotating = true;
	}
}
