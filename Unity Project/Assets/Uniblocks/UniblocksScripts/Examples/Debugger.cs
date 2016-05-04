using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using Game.Scripts.Audio;
using Game.Scripts.UI;
using Game.Scripts.UI.Screens;

// keyboard shortcuts for some common tasks, and saving the world periodically

namespace Uniblocks
{
	public class Debugger : MonoBehaviour
	{
		public GameObject Flashlight, Torch;
		public bool ShowGUI;

		public float energy = 60f;

		private GameScreen gameScreen;

		private CharacterController chrCtrl;

		void Awake()
		{
			GetComponent<CharacterMotor>().enabled = true;
			chrCtrl = GetComponent<CharacterController>();
			chrCtrl.enabled = false;
		}

		void Start()
		{
			ScreenManager screenManager = Service.Get<ScreenManager>();

			if (screenManager != null)
			{
				gameScreen = screenManager.GetScreen("GameScreen") as GameScreen;
			}
		}

		void Update()
		{
			if (!chrCtrl.enabled && !ChunkManager.SpawningChunks)
				chrCtrl.enabled = true;

			energy -= Time.deltaTime;

			UpdateEnergy();

			// keyboard shortcuts

			if (Input.GetKeyDown("space") && Time.realtimeSinceStartup > 3.0f)
			{
				GetComponent<CharacterMotor>().enabled = true;
			}

			//if (Input.GetKeyDown("v")) {
			//    Engine.SaveWorldInstant();
			//}

			if (Input.GetKeyDown("f"))
			{
				if (Flashlight.GetComponent<Light>().enabled == true)
				{
					EnableFlashlight(false);
				}
				else
				{
					EnableFlashlight(true);
				}

				var audioManager = Service.Get<AudioManager>();

				if (audioManager != null)
				{
					audioManager.PlaySound("UI_SELECTOR");
				}

				EnableTorch(false);
			}

			if (Input.GetKeyDown("t"))
			{
				if (Torch.GetComponent<Light>().enabled == true)
				{
					EnableTorch(false);
				}
				else
				{
					EnableTorch(true);
				}

				var audioManager = Service.Get<AudioManager>();

				if (audioManager != null)
				{
					audioManager.PlaySound("UI_SELECTOR");
				}

				EnableFlashlight(false);
			}

			// world save timer
			//if (saveTimer < 0.0f) {
			//    saveTimer = 60.0f;
			//    Engine.SaveWorld();
			//}
			//else {
			//    saveTimer -= Time.deltaTime;
			//}		
		}

		public void AddEnergy(float powerup)
		{
			energy += powerup;
		}

		public void ResetPosition()
		{
			Dictionary<string, Chunk>.Enumerator e = ChunkManager.Chunks.GetEnumerator();
			e.MoveNext();
			Chunk chunk = e.Current.Value;

			transform.position = chunk.transform.position + Vector3.up * Engine.ChunkSideLength;
		}

		private void EnableFlashlight(bool on)
		{
			Flashlight.GetComponent<Light>().enabled = on;

			var screenManager = Service.Get<ScreenManager>();

			if (screenManager != null)
			{
				var gameScreen = screenManager.GetScreen("GameScreen") as GameScreen;

				if (gameScreen != null)
				{
					gameScreen.ToggleFlashlight(on);
				}
			}
		}

		private void EnableTorch(bool on)
		{
			Torch.GetComponent<Light>().enabled = on;

			var screenManager = Service.Get<ScreenManager>();

			if (screenManager != null)
			{
				var gameScreen = screenManager.GetScreen("GameScreen") as GameScreen;

				if (gameScreen != null)
				{
					gameScreen.ToggleTorch(on);
				}
			}
		}

		private void UpdateEnergy()
		{
			if (gameScreen != null)
			{
				gameScreen.UpdateEnergy(((int)energy).ToString());
			}
		}


		//void OnGUI () {

		//    // GUI info box
		//    if (ShowGUI) {
		//        GUILayout.BeginHorizontal ();
		//            GUILayout.Space (20);
		//            GUILayout.BeginVertical();
		//                GUILayout.Space (Screen.height - 200);
		//                GUILayout.BeginVertical("Box");
		//                    GUILayout.Label ("1-9 - select block");
		//                    GUILayout.Label ("RMB - place block");
		//                    GUILayout.Label ("LMB - remove block");
		//                    GUILayout.Label ("F - toggle flashlight");
		//                    GUILayout.Label ("T - toggle torch");
		//                    GUILayout.Label ("R - toggle speed boost");
		//                    GUILayout.Label ("V - save world");
		//                GUILayout.EndVertical();
		//            GUILayout.EndVertical();
		//        GUILayout.EndHorizontal();
		//    }
		//}


	}

}
