﻿using UnityEditor;
using UnityEngine;
using System.Collections;

[InitializeOnLoad]
public class FullscreenPlayMode : MonoBehaviour {
    
	//The size of the toolbar above the game view, excluding the OS border.
	private static int tabHeight = 22;

	static FullscreenPlayMode()
	{
		EditorApplication.playmodeStateChanged -= CheckPlayModeState;
		EditorApplication.playmodeStateChanged += CheckPlayModeState;

	}

	static void CheckPlayModeState()
	{
		if (EditorApplication.isPlaying)
		{
			FullScreenGameWindow();
		}
		else 
		{
			CloseGameWindow();
		}
	}

	static EditorWindow GetMainGameView(){
		EditorApplication.ExecuteMenuItem("Window/Game");

		System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
		System.Reflection.MethodInfo GetMainGameView = T.GetMethod("GetMainGameView",System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
		System.Object Res = GetMainGameView.Invoke(null,null);
		return (EditorWindow)Res;
	}

	static void FullScreenGameWindow(){

		int alto = 1360, ancho = 768;//alto = 1280 * 80 / 100, ancho = 768 * 80 / 100
		EditorWindow gameView = GetMainGameView();

		gameView.title = "Game (Stereo)";
		Rect newPos = new Rect(0, 0 - tabHeight, alto, ancho + tabHeight);
		//Rect newPos = new Rect(0, 0 - tabHeight, Screen.currentResolution.width, Screen.currentResolution.height + tabHeight);

		gameView.position = newPos;
		gameView.minSize = new Vector2(alto, ancho + tabHeight);
		//gameView.minSize = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height + tabHeight);
		gameView.maxSize = gameView.minSize;
		gameView.position = newPos;

	}

	static void CloseGameWindow(){
		EditorWindow gameView = GetMainGameView();
		gameView.Close();
	} 
    
}