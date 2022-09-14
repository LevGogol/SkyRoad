using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Azur.PlayableTemplate.Sound
{
    [CustomEditor(typeof(Audio))]
    public class AudioEditor : Editor
    {
        private const string _enumFile = "TrackName";
        
        private Audio _audio;
        private string _pathToEnumFile;
        private string _trackName = "HERE_NAME_TRACK";
        private AudioClip _clip;

        private void OnEnable()
        {
            TracksSection.DeletePressed += RemoveTrack;
            _pathToEnumFile = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(_enumFile)[0]);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            _audio = (Audio) target;
            var tracks = _audio.Tracks;
            tracks = RefreshClips(tracks);
            for (var i = 0; i < tracks.Count; i++)
            {
                var track = tracks[i];
                track.ID = ((TrackName) i).ToString();
                track.Name = (TrackName) i;
            }

            TracksSection.Draw(tracks);
            
            NewClipSection();

            if (GUILayout.Button("Add"))
            {
                AddTrack();
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(_audio.gameObject);
                EditorSceneManager.MarkSceneDirty(_audio.gameObject.scene);
            }

            _audio.Tracks = tracks;
        }

        private List<AudioTrack> RefreshClips(List<AudioTrack> oldClips)
        {
            int countTrack = Enum.GetNames(typeof(TrackName)).Length;
            List<AudioTrack> clips = new List<AudioTrack>(countTrack);

            for (int i = 0; i < countTrack; i++)
            {
                var enumName = (TrackName) i;
                AudioTrack track = TryRestoreTrack(oldClips, enumName.ToString());
                if (track == null)
                {
                    track = CreateNewTrack(enumName);
                    _clip = null;
                }

                clips.Add(track);
            }

            return clips;
        }

        private void NewClipSection()
        {
            _trackName = EditorGUILayout.TextField("Name", _trackName);
            _clip = (AudioClip) EditorGUILayout.ObjectField("Clip", _clip, typeof(AudioClip), false);
        }

        private static AudioTrack TryRestoreTrack(List<AudioTrack> oldClips, string ID)
        {
            return oldClips.FirstOrDefault(o => o.ID == ID);
        }

        private void AddTrack()
        {
            if (_trackName == String.Empty)
                return;

            if (!Regex.IsMatch(_trackName, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
                return;

            EnumEditor.WriteToFile(_trackName, _pathToEnumFile);
            Refresh();

            _trackName = String.Empty;
        }

        private void RemoveTrack(AudioTrack track)
        {
            if (!EnumEditor.TryRemoveFromFile(track.Name.ToString(), _pathToEnumFile))
                return;

            Refresh();
        }

        private void Refresh()
        {
            Debug.Log("WAIT");
            var relativePath = _pathToEnumFile.Substring(_pathToEnumFile.IndexOf("Assets"));
            AssetDatabase.ImportAsset(relativePath);
        }

        private AudioTrack CreateNewTrack(TrackName enumName)
        {
            var track = new AudioTrack
            {
                Name = enumName,
                ClipsWithSettings = new AudioClipWithSettings[1]
                {
                    new AudioClipWithSettings(_clip)
                }
            };

            return track;
        }

        private void OnDisable()
        {
            TracksSection.DeletePressed -= RemoveTrack;
        }
    }
}