using UnityEngine;
using System;
using System.Collections;
using System.Timers;
using UnityEngine.UI;

namespace Game.Scripts.UI.Screens
{
	public class BlackScreen : BaseScreen
	{
		public const string PREFAB_PATH = "Data/UI/Prefabs/BlackScreen/ui_black_screen";

		private GameObject blackScreenObj;
		private Image blackscreenImage;

		public BlackScreen() : base(PREFAB_PATH)
		{
			blackScreenObj = UnityUtils.FindChildByName(Root, "background");

			if (blackScreenObj != null)
			{
				blackscreenImage = blackScreenObj.GetComponent<Image>();
			}
		}

		public override string ScreenName
		{
			get { return "BlackScreen"; }
		}

		public override string Layer
		{
			get { return Constants.Layers.BLACKSCREEN; }
		}

		public override void OnLoaded()
		{
			transitionIndex.Add("Fade Out", 0);
			transitionIndex.Add("Fade In", 1);
		}

		public override void Close(object modalResult)
		{
			Disable();
		}

		public void FadeOut()
		{
			ScreenAnimator.Play("Fade Out");
		}

		public void FadeIn()
		{
			ScreenAnimator.Play("Fade In");
		}

		public void Disable()
		{
			if (blackscreenImage != null)
			{
				blackscreenImage.raycastTarget = false;
			}
		}

		public void Enable()
		{
			if (blackscreenImage != null)
			{
				blackscreenImage.raycastTarget = true;
			}
		}
	}
}


