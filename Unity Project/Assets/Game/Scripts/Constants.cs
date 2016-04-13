using UnityEngine;
using System.Collections;

namespace Game.Scripts
{
	public class Constants
	{
		public class Layers
		{
			public const string LAYERS = "Layers";
			public const string GAME = "Game";
			public const string WINDOW = "Window";
			public const string HUD = "HUD";
			public const string POPUP = "Popup";
			public const string BLACKSCREEN = "Blackscreen";
		}

		public class ScreenManager
		{
			public const float FADE_IN_TIME = 1.0f;
			public const float FADE_OUT_TIME = 1.0f;
		}

		public class Game
		{
			public const string ENGINE_PREFAB_PATH = "Data/Game/UniBlocks Engine";
			public const string AUDIO_MANAGER_PREFAB_PATH = "Data/Game/AudioManager";
			public const string SINGLE_PLAYER_NAME = "single_player_object";
			public const string SINGLE_PLAYER_FEET_NAME = "Feet";

			public const int MAX_GAME_BLOCK_TYPES = 9;
			public const float MIN_BLOCK_DISTANCE = 0.75f;
			public const int BOTTOM_BLOCK_Y = -63;
		}

		public class GamePrefs
		{
			public const string SOUND_FX_KEY = "soundFx";
			public const string MUSIC_KEY = "music";
		}
	}
}

