using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Widgets
{
	public class TextWindow
	{
		private GameObject textObj;

		public TextWindow(GameObject parent, string textObjName, string text)
		{
			textObj = UnityUtils.FindChildByName(parent, textObjName);

			Reset();
		}

		public void Reset()
		{
			if (textObj != null)
			{
				textObj.GetComponent<Text>().text = "";
			}
		}

		public void UpdateText(string text)
		{
			if (textObj != null)
			{
				textObj.GetComponent<Text>().text = text;
			}
		}
	}
}
