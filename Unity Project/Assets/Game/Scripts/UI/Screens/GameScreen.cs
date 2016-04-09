using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Timers;
using Game.Scripts.UI.Widgets;

namespace Game.Scripts.UI.Screens
{
    public class GameScreen : BaseScreen
    {
        public const string PREFAB_PATH = "Data/UI/Prefabs/GameScreen/ui_game_screen";

        private bool transitioning = false;

        private GameObject uniblocksEngine;
        private GameObject lightSource;
        private GameObject player;
        private GameObject selectedBlockGraphics;

        //NOTE: I should have named "BlockWindow" to something more generic.
        private BlockWindow[] blockWindows;
        private BlockWindow torchWidget;
        private BlockWindow flashLightWidget;

        public GameScreen()
            : base(PREFAB_PATH)
        {
            
        }

        public override string ScreenName
        {
            get { return "GameScreen"; }
        }

        public override string Layer
        {
            get { return Constants.Layers.GAME; }
        }

        public override void OnLoaded()
        {
            base.OnLoaded();

            InitializeUniblocksGameObjects();
            InitializeBlockWindows();
            InitializeTorchAndFlashlight();

            Service.Get<ScreenManager>().FadeOut(null);
            
            //var resetManager = Service.Get<ResetManager>();

            //if( resetManager != null )
            //{
            //    resetManager.EnableChecker();
            //}
        }

        public override void Close(object modalResult)
        {
            if ((bool)modalResult == false)
            {
                return;
            }

            base.Close(modalResult);                       
        }

        private void InitializeUniblocksGameObjects()
        {
            selectedBlockGraphics = UnityUtils.CreateGameObject("Data/Game/Uniblocks/UniblocksObjects/Other/selected block graphics");
            selectedBlockGraphics.name = "selected block graphics";
            uniblocksEngine = UnityUtils.CreateGameObject("Data/Game/Uniblocks/UniblocksObjects/UniBlocks Engine");
            lightSource = UnityUtils.CreateGameObject("Data/Game/Uniblocks/UniblocksObjects/SimpleSun");
            player = UnityUtils.CreateGameObject("Data/Game/Uniblocks/UniblocksObjects/Player/Uniblocks Dude");
            player.name = Constants.Game.SINGLE_PLAYER_NAME;
            player.transform.position = new Vector3(0, 52, 0);
        }

        private void InitializeTorchAndFlashlight()
        {
            var torch = GetElement("Torch");

            if( torch != null )
            {
                torchWidget = new BlockWindow(torch, "Normal", "Highlight");
                torchWidget.Reset();
            }

            var flashLight = GetElement("Flashlight");

            if (flashLight != null)
            {
                flashLightWidget = new BlockWindow(flashLight, "Normal", "Highlight");
                flashLightWidget.Reset();
            }
        }

        private void InitializeBlockWindows()
        {
            blockWindows = new BlockWindow[Constants.Game.MAX_GAME_BLOCK_TYPES];

            for(int i = 1; i <= 9; i++)
            {
                var blockWindow = GetElement("BlockWindow" + i);

                if( blockWindow != null )
                {
                    blockWindows[i - 1] = InitializeBlockWindow(blockWindow, "Normal", "Highlight");
                }
            }
        }

        private BlockWindow InitializeBlockWindow(GameObject blockWindow, string normal, string highlight)
        {
            BlockWindow blockWindowWidget = new BlockWindow(blockWindow, normal, highlight);

            return blockWindowWidget;
        }

        private void ResetAllBlockWindows()
        {
            for (int i = 0; i < 9; i++)
            {
                var blockWindow = blockWindows[i];

                if (blockWindow != null)
                {
                    blockWindow.Reset();
                }
            }
        }

        public void SelectBlock(int blockNumber)
        {
            var blockWindow = blockWindows[blockNumber - 1];

            if( blockWindow != null )
            {
                ResetAllBlockWindows();

                blockWindow.ShowHighlight();
            }
        }

        public void ToggleFlashlight(bool on)
        {
            if( on )
            {
                flashLightWidget.ShowHighlight();
            }
            else
            {
                flashLightWidget.Reset();
            }
        }

        public void ToggleTorch(bool on)
        {
            if (on)
            {
                torchWidget.ShowHighlight();
            }
            else
            {
                torchWidget.Reset();
            }            
        }
    }
}
