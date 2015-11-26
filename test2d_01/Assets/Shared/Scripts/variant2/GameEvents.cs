using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

[InitializeOnLoad]
public static class GameEvents
{
	private static bool _isInPlayMode;
	private static bool _isInEditMode;
	private static bool _preloadStarted;
	
	static GameEvents()
	{
		EditorApplication.playmodeStateChanged += PlaymodeStateChanged;
	}
	
	public delegate void PlayModeStateDelegate();
	public static event PlayModeStateDelegate OnStartPlay;
	public static event PlayModeStateDelegate OnEndPlay;
	public static event PlayModeStateDelegate OnStartEdit;
	public static event PlayModeStateDelegate OnEndEdit;
	
	private static void PlaymodeStateChanged()
	{
		if (Application.isEditor && !Application.isPlaying && !_isInPlayMode)
		{
			if (_isInEditMode)
			{
				_preloadStarted = true;
				_isInEditMode = false;
				
				//Debug.Log("Entered Edit Mode");
				
				if (OnStartEdit != null)
					OnStartEdit.Invoke();
			}
			else
			{
				//Debug.Log("Exit Edit Mode");
				
				if (OnEndEdit != null)
					OnEndEdit.Invoke();
			}
		}
		
		if (Application.isEditor && _isInPlayMode)
		{
			_isInPlayMode = false;
			_isInEditMode = true;
			
			//Debug.Log("Exited Play Mode");
			
			if (OnEndPlay != null)
				OnEndPlay.Invoke();
		}
		else
		{
			if (!Application.isPlaying) return;
			if (!_isInPlayMode)
			{
				//Debug.Log("Entered Play Mode");
				
				if (OnStartPlay != null)
					OnStartPlay.Invoke();
			}
			_isInPlayMode = true;
		}
	}
}