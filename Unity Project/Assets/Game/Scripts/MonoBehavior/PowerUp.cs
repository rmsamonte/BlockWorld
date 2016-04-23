using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
	public Transform[] powerupModels;

	public PowerUpManager Manager
	{
		get { return manager; }
	}

	private PowerUpAnimator powerUpAnim;

	private float strength = 0;
	private int charges = 1;
	private PowerUpManager manager;
	private float counter = 0;

	public void Init(float strength, PowerUpManager manager)
	{
		this.strength = strength;
		this.manager = manager;
		InsertModel();
	}

	public float Gather(GameObject picker)
	{
		if (powerUpAnim != null) powerUpAnim.Blink();

		charges--;

		picker.SendMessage("AddEnergy", strength);

		if (charges <= 0)
		{
			manager.Taken();
			Destroy(gameObject, .5f);
		}

		return strength;
	}

	private void InsertModel()
	{
		Transform obj = Instantiate(powerupModels[Random.Range(0, powerupModels.Length)]);
		obj.parent = transform;
		obj.localPosition = Vector3.zero;

		charges = Random.Range(1, 4);

		powerUpAnim = obj.GetComponent<PowerUpAnimator>();
	}
}
