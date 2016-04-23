using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour
{
	public LayerMask resourceMask;
	public float gatherDelay = 0;

	private float counter = 0;

	void Update()
	{
		if (counter > 0) counter -= Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.E) && counter <= 0) GatherResource();
	}

	private void GatherResource()
	{
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1, resourceMask))
		{
			PowerUp pu = hit.collider.GetComponent<PowerUp>();

			if (pu != null)
			{
				pu.Gather(gameObject);
				counter = gatherDelay;
			}
		}
	}
}
