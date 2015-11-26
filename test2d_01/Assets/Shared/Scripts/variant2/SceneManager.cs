using System.IO;
using FullInspector;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[InitializeOnLoad]
public class SceneManager
{
	private const string EDITOR_LEVELS_FILE_DIRECTORY = "Assets/_Scenes/";
	private const string BUILD_LEVELS_FILE_DIRECTORY = "";
	private const string LEVEL_FILE_NAME = "Scenes";
	private static string pathToAsset;
	
	private static SceneCollection cachedAsset;
	private static string serializedState;
	private static int hash;
	private static bool objectChanged;
	
	static SceneManager()
	{
		GameEvents.OnEndPlay += GameEventsOnOnEndPlay;
		GameEvents.OnStartPlay += GameEventsOnOnStartPlay;
		GameEvents.OnStartEdit += GameEventsOnOnStartEdit;
	}
	
	private static void GameEventsOnOnStartEdit()
	{
		if (objectChanged)
		{
			Debug.Log("Loading State..");
			cachedAsset.RestoreState();
		}
	}
	
	private static void GameEventsOnOnStartPlay()
	{
		//Debug.Log("Caching..");
		
		var sceneNames = GetSceneNames();
		var asset = SceneCollectionAsset;
		asset.Refresh(sceneNames);
		asset.SaveState();
		cachedAsset = asset;
		serializedState = Serialize(asset.MyScenes2);
	}
	
	private static string Serialize(SceneHolder sceneHolder)
	{
		return SerializationHelpers.SerializeToContent<SceneHolder, JsonNetSerializer>(sceneHolder);
	}
	
	private static void GameEventsOnOnEndPlay()
	{
		if (Serialize(cachedAsset.MyScenes2) != serializedState)
		{
			//Debug.Log("Object has changed, Saving State...");
			
			cachedAsset.SaveState();
			objectChanged = true;
		}
		else
		{
			objectChanged = false;
		}
	}
	
	private static string PathToAsset
	{
		get
		{
			return Path.Combine(EDITOR_LEVELS_FILE_DIRECTORY, LEVEL_FILE_NAME + ".asset");
		}
	}
	
	private static SceneCollection SceneCollectionAsset
	{
		get
		{
			return AssetDatabase.LoadMainAssetAtPath(PathToAsset) as SceneCollection ??
				ScriptableObjectCreator.CreateAsset<SceneCollection>(PathToAsset);
		}
	}
	
	private static List<string> GetSceneNames()
	{
		// Collect the names of all levels in the build settings.
		return (from buildSettingsScene in EditorBuildSettings.scenes
		        where buildSettingsScene.enabled
		        select buildSettingsScene.path.Substring(buildSettingsScene.path.LastIndexOf(Path.AltDirectorySeparatorChar) + 1)
		        into name
		        select name.Substring(0, name.Length - 6)).ToList();
	}
	
	public static class ScriptableObjectCreator
	{
		public static T CreateAsset<T>(string path) where T : ScriptableObject, new()
		{
			var asset = ScriptableObject.CreateInstance<T>();
			
			if (string.IsNullOrEmpty(path))
			{
				Debug.Log("No path provided");
				return new T();
			}
			
			string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path);
			AssetDatabase.CreateAsset(asset, assetPathAndName);
			AssetDatabase.SaveAssets();
			
			return AssetDatabase.LoadMainAssetAtPath(path) as T;
		}
	}
}