using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.Scripts.UI.Widgets
{
    public class BlockWindow
    {
        private GameObject normalWindow;
        private GameObject highlightWindow;

        public BlockWindow(GameObject parent, string normal, string highlight)
        {
            normalWindow = UnityUtils.FindChildByName(parent, normal);
            highlightWindow = UnityUtils.FindChildByName(parent, highlight);

            Reset();
        }

        public void Reset()
        {
            if( normalWindow != null )
            {
                normalWindow.SetActive(true);
            }

            if (highlightWindow != null)
            {
                highlightWindow.SetActive(false);
            }
        }

        public void ShowHighlight()
        {
            if (normalWindow != null)
            {
                normalWindow.SetActive(false);
            }

            if (highlightWindow != null)
            {
                highlightWindow.SetActive(true);
            }
        }
    }
}
