using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
	public Transform[] powerupModels;

	private float strength = 0;
	private PowerUpManager manager;
	private float counter = 0;

	void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player")) return;

		other.SendMessage("AddEnergy", strength);

		manager.Taken();
		Destroy(gameObject);
	}

	public void Take()
	{
		manager.Taken();
	}

	public void Init(float strength, PowerUpManager manager)
	{
		this.strength = strength;
		this.manager = manager;
		InsertModel();
	}

	private void InsertModel()
	{
		Transform modelObj = Instantiate(powerupModels[Random.Range(0, powerupModels.Length)]);
		modelObj.parent = transform;
		modelObj.localPosition = Vector3.zero;
	}
}
