// -----------------------------------------------------------------------
//  <copyright ScreenManager.cs company="Kahuna Studios">
//      Copyright (c) 2016 Kahuna Studios. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using Game.Scripts.MonoBehavior;
using Game.Scripts.UI.Screens;

namespace Game.Scripts.UI
{
	public class ScreenManager
	{
		private GameObject uiSystem;
		private RectTransform Layers;
		private RectTransform LayerGame;
		private RectTransform LayerWindow;
		private RectTransform LayerHud;
		private RectTransform Popup;
		private RectTransform BlackScreen;

		private Dictionary<string, BaseScreen> screens;

		public ScreenManager(GameObject uiSystem)
		{
			this.uiSystem = uiSystem;
			Layers = UnityUtils.FindChildByName(this.uiSystem, Constants.Layers.LAYERS).GetComponent<RectTransform>();
			LayerGame = UnityUtils.FindChildByName(Layers.gameObject, Constants.Layers.GAME).GetComponent<RectTransform>();
			LayerWindow = UnityUtils.FindChildByName(Layers.gameObject, Constants.Layers.WINDOW).GetComponent<RectTransform>();
			LayerHud = UnityUtils.FindChildByName(Layers.gameObject, Constants.Layers.HUD).GetComponent<RectTransform>();
			Popup = UnityUtils.FindChildByName(Layers.gameObject, Constants.Layers.POPUP).GetComponent<RectTransform>();
			BlackScreen = UnityUtils.FindChildByName(Layers.gameObject, Constants.Layers.BLACKSCREEN).GetComponent<RectTransform>();

			screens = new Dictionary<string, BaseScreen>();
		}

		public void AddScreen(BaseScreen screen)
		{
			if (screen == null)
			{
				return;
			}

			screens.Add(screen.ScreenName, screen);

			switch (screen.Layer)
			{
				case Constants.Layers.GAME:
					screen.RectTransform.SetParent(LayerGame, false);
					break;
				case Constants.Layers.WINDOW:
					screen.RectTransform.SetParent(LayerWindow, false);
					break;
				case Constants.Layers.HUD:
					screen.RectTransform.SetParent(LayerHud, false);
					break;
				case Constants.Layers.POPUP:
					screen.RectTransform.SetParent(Popup, false);
					break;
				case Constants.Layers.BLACKSCREEN:
					screen.RectTransform.SetParent(BlackScreen, false);
					break;
				default:
					break;
			}

			screen.transform.SetAsLastSibling();
			screen.transform.localScale = Vector3.one;
			screen.transform.localPosition = Vector3.zero;
			screen.transform.localRotation = Quaternion.identity;
		}

		public BaseScreen GetScreen(string screenName)
		{
			return screens[screenName];
		}

		public void RemoveScreen(BaseScreen screen)
		{
			screens.Remove(screen.ScreenName);
			GameObject.Destroy(screen.Root);
			screen = null;
		}

		public void FadeOut(Action onComplete)
		{
			var blackScreen = screens["BlackScreen"] as BlackScreen;

			if (blackScreen != null)
			{
				blackScreen.Enable();
				blackScreen.FadeOut();
			}

			var core = Service.Get<Core>();

			if (core != null)
			{
				core.GameStartCoroutine(DoFadeDelayOut(onComplete));
			}
		}

		public void FadeIn(Action onComplete)
		{
			var blackScreen = screens["BlackScreen"] as BlackScreen;

			if (blackScreen != null)
			{
				blackScreen.Enable();
				blackScreen.FadeIn();
			}

			var core = Service.Get<Core>();

			if (core != null)
			{
				core.GameStartCoroutine(DoFadeDelayIn(onComplete));
			}
		}

		private IEnumerator DoFadeDelayIn(Action onComplete)
		{
			yield return new WaitForSeconds(Constants.ScreenManager.FADE_IN_TIME);

			if (onComplete != null)
			{
				onComplete();
			}

			var blackScreen = GetScreen("BlackScreen") as BlackScreen;

			if (blackScreen != null)
			{
				blackScreen.Disable();
			}
		}

		private IEnumerator DoFadeDelayOut(Action onComplete)
		{
			yield return new WaitForSeconds(Constants.ScreenManager.FADE_IN_TIME);

			if (onComplete != null)
			{
				onComplete();
			}

			var blackScreen = GetScreen("BlackScreen") as BlackScreen;

			if (blackScreen != null)
			{
				blackScreen.Disable();
			}
		}
	}
}

