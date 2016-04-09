// -----------------------------------------------------------------------
//  <copyright AudioManager.cs company="Kahuna Studios">
//      Copyright (c) 2016 Kahuna Studios. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using Fabric;
//using Game.Scripts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.Scripts.Audio
{
    class AudioManager
    {
        public const string PREFAB_PATH = "Data/Game/AudioManager";
        private GameObject audioManager;

        private const string MUTED = "Mute";
        private const string UNMUTED = "Unmute";

        private const string MUSIC = "Music";
        private const string GAME_SOUND = "Game";
        private const string SOUND_EFFECT = "UI";

        private GroupComponent musicGroupComponent;
        private GroupComponent gameGroupComponent;
        private GroupComponent sfxGroupComponent;


        public AudioManager()
        {
            audioManager = UnityUtils.CreateGameObject( PREFAB_PATH );
        }

        public void Initialize()
        {
            musicGroupComponent = GetGroupComponent(MUSIC);
            gameGroupComponent = GetGroupComponent(GAME_SOUND);
            sfxGroupComponent = GetGroupComponent(SOUND_EFFECT);
        }

        private GroupComponent GetGroupComponent(string groupName)
        {
            var groupTransform = audioManager.transform.FindChild(groupName);

            if (groupTransform != null)
            {
                var groupComponent = groupTransform.GetComponent<GroupComponent>();

                if (groupComponent != null)
                {
                    return groupComponent;
                }
            }

            return null;
        }

        private void LoadSnapShot(GroupComponent group, string snapShotName, float time)
        {
            if (group == null)
            {
                Debug.LogWarning("UNABLE TO LOAD SNAPSHOT -----> LoadSnapShot: " + snapShotName);
                return;
            }

            group._audioMixerGroup.audioMixer.FindSnapshot(snapShotName).TransitionTo(time);
        }

        public void PlaySound(string eventName)
        {
            Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.PlaySound);
        }

        public void PlayMusic(bool status, string eventName)
        {
            if (status)
            {
                Fabric.EventManager.Instance.PostEvent(eventName);
            }
            else
            {
                Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.StopSound);
            }
        }

        public void EnableMusic(bool val, float time = 0.35f)
        {
            if (val)
            {
                LoadSnapShot(musicGroupComponent, UNMUTED, time);
            }
            else
            {
                LoadSnapShot(musicGroupComponent, MUTED, time);
            }
        }

        public void EnableSoundEffects(bool val, float time = 0.35f)
        {
            if (val)
            {
                LoadSnapShot(sfxGroupComponent, UNMUTED, time);
                LoadSnapShot(gameGroupComponent, UNMUTED, time);
            }
            else
            {
                LoadSnapShot(sfxGroupComponent, MUTED, time);
                LoadSnapShot(gameGroupComponent, MUTED, time);
            }
        }

        public void MuteAllSounds()
        {
            EnableMusic(false, 0.0f);
            EnableSoundEffects(false, 0.0f);
        }
    }
}
