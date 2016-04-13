using System;
using UnityEngine;

namespace Game.Scripts
{
	class ResetManager
	{
		public float fallingThreshold = 5f;
		public float maxFallingThreshold = 30f;
		private float initialDistance = 0f;
		private RaycastHit hit;

		private GameObject player = null;

		private float fallingTimer = 0.0f;
		private const float MAX_FALLING_TIME = 10000.0f;

		public ResetManager()
		{

		}

		bool GetHitDistance(out float distance)
		{
			distance = 0f;
			Ray downRay = new Ray(player.transform.position, -Vector3.up); // this is the downward ray
			if (Physics.Raycast(downRay, out hit))
			{
				distance = hit.distance;
				return true;
			}
			return false;
		}

		public void Update(float dt)
		{
			if (player == null)
			{
				return;
			}

			var dist = 0f;
			if (GetHitDistance(out dist))
			{
				if (initialDistance < dist)
				{
					fallingTimer = 0.0f;

					//Get relative distance
					var relDistance = dist - initialDistance;
					//Are we actually falling?
					if (relDistance > fallingThreshold)
					{
						//How far are we falling
						if (relDistance > maxFallingThreshold)
						{
							Debug.Log("Fell off a cliff");
						}
						else
						{
							Debug.Log("basic falling!");
						}
					}
				}
			}
			else
			{
				Debug.Log("Infinite Fall");
				fallingTimer += dt;

				if (fallingTimer >= MAX_FALLING_TIME)
				{
					Debug.Log("Should reset...");
				}
			}
		}

		private void ZeroVelocities()
		{
			if (player == null)
			{
				return;
			}

			var characterController = player.GetComponent<CharacterController>();

			if (characterController != null)
			{
				Debug.Log("Y Velocity: " + characterController.velocity.y);

				if (characterController.velocity.y != 0)
				{
					//Reset();
				}

				//if (characterController.attachedRigidbody != null)
				//{
				//    characterController.attachedRigidbody.velocity = Vector3.zero;
				//    characterController.attachedRigidbody.angularVelocity = Vector3.zero;
				//}
			}
		}

		public void EnableChecker()
		{
			player = GameObject.Find(Constants.Game.SINGLE_PLAYER_NAME);

			if (player == null)
			{
				return;
			}

			//var dist = 0f;
			//GetHitDistance(out dist);
			initialDistance = 15.0f;

			Core.Instance.FrameUpdate += Update;
		}

		public void DisableChecker()
		{
			Core.Instance.FrameUpdate -= Update;
		}

		public void Reset()
		{
			//DisableChecker();

			//TODO: Figure out a way to reset the player at the specified position and restart the checker afterwards...
			if (player == null)
			{
				return;
			}

			player.transform.position = new Vector3(12, 48, -4);
			//EnableChecker();
		}
	}
}
