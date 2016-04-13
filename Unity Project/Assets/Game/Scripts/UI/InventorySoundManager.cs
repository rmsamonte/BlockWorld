using Game.Scripts.Audio;
using System;
using UnityEngine;

namespace Game.Scripts.UI
{
	class InventorySoundManager
	{
		public enum Blocks { NONE = 0, BLOCK1 = 1, BLOCK2, BLOCK3, BLOCK4, BLOCK5, BLOCK6, BLOCK7, BLOCK8, BLOCK9 };
		private Blocks currentBlockSound = Blocks.NONE;

		public InventorySoundManager()
		{

		}

		public Blocks CurrentBlockSound
		{
			get { return currentBlockSound; }
			set { currentBlockSound = value; }
		}

		public void PlayBuildSound()
		{
			var audioManager = Service.Get<AudioManager>();

			if (audioManager == null)
			{
				Debug.Log("InventorySoundManager::PlayBuildSound - Invalid AudioManager.");
				return;
			}

			switch (currentBlockSound)
			{
				case Blocks.BLOCK1:
				case Blocks.BLOCK2:
					audioManager.PlaySound("GAME_BUILD");
					break;
				case Blocks.BLOCK3:
				case Blocks.BLOCK4:
				case Blocks.BLOCK5:
				case Blocks.BLOCK6:
					audioManager.PlaySound("GAME_STONE_BUILD");
					break;
				case Blocks.BLOCK7:
					audioManager.PlaySound("GAME_DOOR_BUILD");
					break;
				case Blocks.BLOCK8:
				case Blocks.BLOCK9:
					audioManager.PlaySound("GAME_PLANTS");
					break;
				default:
					break;
			}
		}
	}
}
