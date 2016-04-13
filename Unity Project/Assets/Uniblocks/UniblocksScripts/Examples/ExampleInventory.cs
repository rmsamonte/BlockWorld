using UnityEngine;
using System.Collections;
using Game.Scripts;
using Game.Scripts.Audio;
using Game.Scripts.UI;
using Game.Scripts.UI.Screens;

// stores the currently held block, and switches it with 1-9 keys

namespace Uniblocks
{
	public class ExampleInventory : MonoBehaviour
	{
		public static ushort HeldBlock;

		public void Update()
		{

			// change held block with 1-9 keys
			for (ushort i = 0; i < 10; i++)
			{
				if (Input.GetKeyDown(i.ToString()))
				{
					if (Engine.GetVoxelType(i) != null)
					{
						ExampleInventory.HeldBlock = i;

						//Update the current selected block in the inventory sound manager
						UpdateCurrentlySelectedSound(i);

						PlaySelectSound();
						SelectBlock(i);
					}
				}
			}
		}

		private void PlaySelectSound()
		{
			var audioManager = Service.Get<AudioManager>();

			if (audioManager != null)
			{
				audioManager.PlaySound("UI_SELECTOR");
			}
		}

		private void UpdateCurrentlySelectedSound(int id)
		{
			var inventorySoundManager = Service.Get<InventorySoundManager>();

			if (inventorySoundManager != null)
			{
				inventorySoundManager.CurrentBlockSound = (Game.Scripts.UI.InventorySoundManager.Blocks)id;
			}
		}

		private void SelectBlock(int index)
		{
			var screenManager = Service.Get<ScreenManager>();

			if (screenManager != null)
			{
				var gameScreen = screenManager.GetScreen("GameScreen") as GameScreen;

				if (gameScreen != null)
				{
					gameScreen.SelectBlock(index);
				}
			}
		}
	}
}
