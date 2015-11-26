using System;
using System.Linq;
using FullInspector;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Scene
{
	public int BuildOrder;
	public string Name;
	public string Description;
	public ISceneProperties Properties;
}

public interface ISceneProperties
{
	
}

[Serializable]
public class MenuScene : ISceneProperties
{
	public bool Enabled;
}

[Serializable]
public class LevelScene : ISceneProperties
{
	public bool Enabled;
	public string Description;
	//public Level LevelMarker;
}

[Serializable]
public class SceneHolder
{
	[SerializeField]
	public List<Scene> Scenes;
}

[Serializable]
public class SceneCollection : BaseScriptableObject<JsonNetSerializer>
{
	public void Refresh(List<string> sceneNames)
	{
		if (MyScenes2 == null)
			MyScenes2 = new SceneHolder();            
		
		if(MyScenes2.Scenes == null)
			MyScenes2.Scenes = new List<Scene>();
		
		sceneNames.ForEach((sceneName, index) =>
		                   {
			var scene = MyScenes2.Scenes.FirstOrDefault(s => s.Name == sceneName);
			if (scene != null)
			{
				// Update
				scene.BuildOrder = index;
			}
			else
			{
				// Add
				MyScenes2.Scenes.Add(new Scene()
				                     {
					Name = sceneName,
					BuildOrder = index
				});
			}
		});
		
		MyScenes2.Scenes = MyScenes2.Scenes.OrderBy(scene => scene.BuildOrder).ToList();
		
	}
	
	[SerializeField]
	public SceneHolder MyScenes2;
	
}