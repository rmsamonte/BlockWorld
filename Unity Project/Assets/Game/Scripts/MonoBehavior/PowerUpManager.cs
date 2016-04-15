using UnityEngine;
using System.Collections;
using Uniblocks;
using System;

public class PowerUpManager : MonoBehaviour
{
	public float powerUpCoolDown = 0;
	public float defaultPowerUpStrength = 0;
	public Transform powerUp;

	private GameObject spawnedPowerUp;
	private float counter = 0;

	private Chunk chunk;

	void Start()
	{
		chunk = GetComponent<Chunk>();
	}

	void Update()
	{
		if (!chunk.VoxelsDone || chunk.Empty || chunk.GetComponent<MeshFilter>().mesh == null) return;

		if (spawnedPowerUp != null || counter > 0)
		{
			counter -= Time.deltaTime;
			return;
		}

		SpawnPowerUp();
	}

	public void Taken()
	{
		counter = powerUpCoolDown;
	}

	private void SpawnPowerUp()
	{
		int nonEmpty = 0;
		for (int i = 0; i < chunk.VoxelData.Length; i++)
			if (chunk.VoxelData[i] == 2)
				nonEmpty++;

		if (nonEmpty < 9) return;

		bool done = false;
		for (int z = Engine.ChunkSideLength - 1; z >= 0 && !done; z--)
			for (int x = Engine.ChunkSideLength - 1; x >= 0 && !done; x--)
				for (int y = Engine.ChunkSideLength - 1; y >= 0 && !done; y--)
				{
					if (chunk.GetVoxel(x, y, z) == 2 ||
						chunk.GetVoxel(x, y, z) == 3 ||
						chunk.GetVoxel(x, y, z) == 4 ||
						chunk.GetVoxel(x, y, z) == 5 ||
						chunk.GetVoxel(x, y, z) == 6)
					{
						Transform t = Instantiate(powerUp);
						spawnedPowerUp = t.gameObject;
						spawnedPowerUp.transform.parent = chunk.transform;
						spawnedPowerUp.transform.localPosition = new Vector3(x, y + .5f, z);

						done = true;

						break;
					}
				}

		spawnedPowerUp.GetComponent<PowerUp>().Init(defaultPowerUpStrength, this);
	}
}
