using UnityEngine;
using System.Collections;

public class PowerUpAnimator : MonoBehaviour
{
	public bool isAnimated = false;

	void Update()
	{
		if (!isAnimated) return;

		float height = Mathf.PerlinNoise(Time.time, 0);

		Vector3 pos = transform.localPosition;
		pos.y = height;
		transform.localPosition = pos;
	}

	public void Blink()
	{
		StartCoroutine(Blinking());
	}

	private IEnumerator Blinking()
	{
		float timer = 0;
		while (timer < .3f)
		{
			float floor = 0f;
			float ceiling = 1f;
			float emission = floor + Mathf.PingPong(Time.time * 5f, ceiling - floor);

			Color newColor = new Color(emission, emission, emission);

			foreach (Renderer r in GetComponentsInChildren<Renderer>())
			{
				foreach (Material m in r.materials)
				{
					if (m.HasProperty("_EmissionColor"))
					{
						m.EnableKeyword("_EMISSION");
						m.SetColor("_EmissionColor", newColor);
					}
				}
			}

			timer += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		foreach (Renderer r in GetComponentsInChildren<Renderer>())
		{
			foreach (Material m in r.materials)
			{
				if (m.HasProperty("_EmissionColor"))
				{
					m.EnableKeyword("_EMISSION");
					m.SetColor("_EmissionColor", Color.black);
				}
			}
		}
	}
}
