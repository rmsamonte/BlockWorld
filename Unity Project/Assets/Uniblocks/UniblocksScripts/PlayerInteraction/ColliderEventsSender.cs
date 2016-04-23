using UnityEngine;
using System.Collections;

namespace Uniblocks
{
	public class ColliderEventsSender : MonoBehaviour
	{
		private Chunk lastChunk;
		private VoxelInfo voxelInfo;

		public VoxelInfo VoxelInfo
		{
			get { return voxelInfo; }
		}

		public Index LastIndex
		{
			get { return LastIndex; }
		}

		public Chunk LastChunk
		{
			get { return lastChunk; }
		}

		public void Update()
		{

			// check if chunk is not null
			GameObject chunkObject = Engine.PositionToChunk(transform.position);
			if (chunkObject == null) return;

			// get the voxelInfo from the transform's position
			Chunk chunk = chunkObject.GetComponent<Chunk>();
			Index voxelIndex = chunk.PositionToVoxelIndex(transform.position);
			voxelInfo = new VoxelInfo(voxelIndex, chunk);

			// create a local copy of the collision voxel so we can call functions on it
			GameObject voxelObject = Instantiate(Engine.GetVoxelGameObject(voxelInfo.GetVoxel())) as GameObject;

			VoxelEvents events = voxelObject.GetComponent<VoxelEvents>();
			if (events != null)
			{

				// OnEnter
				if (chunk != lastChunk || voxelIndex.IsEqual(voxelIndex) == false)
				{
					events.OnBlockEnter(gameObject, voxelInfo);
				}

				// OnStay
				else {
					events.OnBlockStay(gameObject, voxelInfo);
				}
			}

			lastChunk = chunk;

			Destroy(voxelObject);

		}

	}

}

























