using UnityEngine;
using System.Collections;
using Game.Scripts;
using Game.Scripts.UI;
using Game.Scripts.Audio;
using Game.Scripts.UI.Screens;
using System;

public class Core : MonoBehaviour
{
	public GameObject uiSystem;

	[HideInInspector]
	public static Core Instance;

	public delegate void OnCoreFrameUpdateHandler(float deltaTime);
	public event OnCoreFrameUpdateHandler FrameUpdate;

	// Use this for initialization
	void Start()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			DestroyObject(this);
			return;
		}

		Initialize();
		LoadTitleScreen();
	}

	// Update is called once per frame
	void Update()
	{
		float deltaTime = Time.deltaTime;

		Update(deltaTime);
	}

	public void Update(float dt)
	{
		if (FrameUpdate != null)
		{
			FrameUpdate(dt);
		}
	}

	public IEnumerator Wait(float time, Action callback)
	{
		yield return new WaitForSeconds(time);

		if (callback != null)
		{
			callback();
		}
	}

	public void DestroyObject(GameObject obj)
	{
		GameObject.DestroyObject(obj);
	}

	public void GameStartCoroutine(IEnumerator routine)
	{
		StartCoroutine(routine);
	}

	private void Initialize()
	{
		Service.Set<Core>(Instance);
		var screenManager = new ScreenManager(uiSystem);
		Service.Set<ScreenManager>(screenManager);

		var audioManager = new AudioManager();
		audioManager.Initialize();
		Service.Set<AudioManager>(audioManager);

		audioManager.PlayMusic(true, "GAME_MUSIC");

		var inventorySoundManager = new InventorySoundManager();
		Service.Set<InventorySoundManager>(inventorySoundManager);

		//Service.Set<ResetManager>(new ResetManager());

		var blackScreen = new BlackScreen();
		Service.Get<ScreenManager>().AddScreen(blackScreen);
	}

	private void LoadTitleScreen()
	{
		var titleScreen = new TitleScreen();
		Service.Get<ScreenManager>().AddScreen(titleScreen);
	}
}
