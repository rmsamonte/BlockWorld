using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Timers;
using Game.Scripts.Audio;

namespace Game.Scripts.UI.Screens
{
    public class TitleScreen : BaseScreen
    {
        public const string PREFAB_PATH = "Data/UI/Prefabs/TitleScreen/ui_title_screen";

        private const string UI_PLAY_BUTTON = "play_button";
        private const string UI_TEST_ADS_BUTTON = "ad_test_button";
        private const string UI_DEBUG_TEXT = "debug_text";

        private bool transitioning = false;

        private Text debugText;

        public TitleScreen()
            : base(PREFAB_PATH)
        {
            
        }

        public override string ScreenName
        {
            get { return "TitleScreen"; }
        }

        public override string Layer
        {
            get { return Constants.Layers.WINDOW; }
        }

        public override void OnLoaded()
        {
            base.OnLoaded();

            Service.Get<ScreenManager>().FadeOut(null);

            var playButton = UnityUtils.FindChildByName(root, UI_PLAY_BUTTON).GetComponent<Button>();

            if (playButton != null)
            {
                playButton.onClick.AddListener(OnPlayPressed);
            }            

            var debugTextObj = GetElement(UI_DEBUG_TEXT);

            if (debugTextObj != null)
            {
                debugText = debugTextObj.GetComponent<Text>();
                debugText.text = String.Empty;
            }            
        }

        public override void Close(object modalResult)
        {
            if( (bool)modalResult == false )
            {
                return;
            }

            base.Close(modalResult);

            var gameScreen = new GameScreen();
            Service.Get<ScreenManager>().AddScreen(gameScreen);           
        }

        private void OnPlayPressed()
        {
            if (transitioning)
            {
                return;
            }

            transitioning = true;

            PlayButtonSound();

            Service.Get<ScreenManager>().FadeIn(() => { Close(true); });
        }

        private void PlayButtonSound()
        {
            var audioManager = Service.Get<AudioManager>();

            if (audioManager != null)
            {
                audioManager.PlaySound("UI_SELECTOR");
            }
        }

        public void SetDebugText(string msg)
        {
        }
    }
}

