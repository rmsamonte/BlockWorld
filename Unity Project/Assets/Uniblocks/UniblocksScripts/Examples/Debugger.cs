using UnityEngine;
using System.Collections;
using Game.Scripts;
using Game.Scripts.Audio;
using Game.Scripts.UI;
using Game.Scripts.UI.Screens;

// keyboard shortcuts for some common tasks, and saving the world periodically

namespace Uniblocks {

public class Debugger : MonoBehaviour {
	
	public GameObject Flashlight, Torch;
	private float saveTimer = 60.0f;
	public bool ShowGUI;
	
    void Awake()
    {
        GetComponent<CharacterMotor>().enabled = true;
    }
	
	void Update () 
    {
	
		// keyboard shortcuts
		
		if (Input.GetKeyDown("space") && Time.realtimeSinceStartup > 3.0f) {
			GetComponent<CharacterMotor>().enabled = true;
		}
	
        //if (Input.GetKeyDown("v")) {
        //    Engine.SaveWorldInstant();
        //}
				
		if (Input.GetKeyDown ("f")) 
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
		
		if (Input.GetKeyDown ("t")) 
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

    private void EnableFlashlight(bool on)
    {
        Flashlight.GetComponent<Light>().enabled = on;

        var screenManager = Service.Get<ScreenManager>();

        if( screenManager != null )
        {
            var gameScreen = screenManager.GetScreen("GameScreen") as GameScreen;

            if( gameScreen != null )
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
