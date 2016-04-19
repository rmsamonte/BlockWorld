using UnityEngine;
using System.Collections;

public class PowerUpAnimator : MonoBehaviour
{
	public bool isAnimated = false;

	void Update()
	{
		float height = Mathf.PerlinNoise(Time.time, 0);

		Vector3 pos = transform.localPosition;
		pos.y = height;
		transform.localPosition = pos;
	}
}
