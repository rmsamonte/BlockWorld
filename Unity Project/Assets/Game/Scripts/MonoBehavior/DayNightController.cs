using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour
{
	public Light sun;
	public float secondsInFullDay = 120f;
	[Range(0, 1)]
	public float currentTimeOfDay = 0;

	public Color nightFogColor;

	private float timeMultiplier = 1f;
	private float sunInitialIntensity;

	private Color fogColor;
	private float timer = 0;

	void Start()
	{
		sunInitialIntensity = sun.intensity;
		fogColor = RenderSettings.fogColor;
	}

	void Update()
	{
		UpdateSun();

		currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

		if (currentTimeOfDay >= 1)
			currentTimeOfDay = 0;
	}

	void UpdateSun()
	{
		sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

		float intensityMultiplier = 1;

		if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
		{
			intensityMultiplier = 0; //night
			RenderSettings.fogColor = Color.black;
			timer = 0;
		}
		else if (currentTimeOfDay <= 0.25f)
		{
			intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f)); // dawn

			timer += Time.deltaTime;
			RenderSettings.fogColor = Color.Lerp(nightFogColor, fogColor, timer);
		}
		else if (currentTimeOfDay >= 0.73f)
		{
			intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f))); // dusk

			timer += Time.deltaTime;
			RenderSettings.fogColor = Color.Lerp(fogColor, nightFogColor, timer);
		}
		else
		{
			RenderSettings.fogColor = fogColor;
			timer = 0;
		}

		sun.intensity = sunInitialIntensity * intensityMultiplier;
	}
}