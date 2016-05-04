using UnityEngine;
using System.Collections;

public class DropDownTrigger : MonoBehaviour
{
	private CharacterController chrCtrl;

	void Update()
	{
		if (chrCtrl == null)
		{
			chrCtrl = FindObjectOfType<CharacterController>();
			return;
		}

		Vector3 triggerPos = chrCtrl.transform.position;

		triggerPos.y = -32;

		transform.position = triggerPos;
	}

	void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player")) return;

		other.SendMessage("ResetPosition");
	}
}
