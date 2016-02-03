﻿using UnityEngine;
using System.Collections;
using GILES;

namespace GILES.Example
{
	/**
	 * Simple example of a scene loading script.
	 */
	public class SceneLoader : pb_MonoBehaviourSingleton<SceneLoader>
	{
		/// Make this object persistent between scene loads.
		public override bool dontDestroyOnLoad { get { return true; } }

		/// The scene that will be opened and loaded into.
		public string sceneToLoadLevelInto;

		[HideInInspector] [SerializeField] private string json = null;

		/**
		 * Call this to load level.
		 */
		public static void LoadScene(string path)
		{
	 		string san = pb_FileUtility.SanitizePath(path);

			if(!pb_FileUtility.IsValidPath(san, ".json"))
			{
				Debug.LogWarning(san + " not found, or file is not a JSON scene.");
				return;
			}
			else
			{
				instance.json = pb_FileUtility.ReadFile(san);
			}

			Application.LoadLevel(instance.sceneToLoadLevelInto);
		}

		private void OnLevelWasLoaded(int i)
		{
			if(Application.loadedLevelName == sceneToLoadLevelInto && !string.IsNullOrEmpty(json))
				pb_Scene.LoadLevel(json);
		}
	}
}