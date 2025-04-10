using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObstacleEditorWindow : EditorWindow
{
    private ObstacleData obstacleData; //ref to obstacle data 
    [MenuItem("Tools/Obstacle Editor")] //adds a menu under tools 
    public static void ShowWindow()
    {
        GetWindow<ObstacleEditorWindow>("Obstacle Editor");
    }
    private void OnGUI() //basic UI for editor
    {
        GUILayout.Label("Obstacle Tool", EditorStyles.boldLabel); //labelling
        obstacleData = (ObstacleData)EditorGUILayout.ObjectField("Obstcle Data", obstacleData, typeof(ObstacleData), false); //takes intput for obstacle data 
        if (obstacleData == null) //null = warning
        {
            EditorGUILayout.HelpBox("Assign an ObstacleData", MessageType.Warning);
            return;
        }
        GUILayout.Space(10);
        int gridSize = 10;
        for (int y = 0; y < gridSize; y++) //column
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < gridSize; x++) //row
            {
                int index = y * gridSize + x; //obstacle array is 1d, but since we are working on a 2d grid thats x, y we need to combine this 2d to 1darray
                obstacleData.obstacleArray[index] = GUILayout.Toggle(obstacleData.obstacleArray[index], ""); //toggle button

            }
            GUILayout.EndHorizontal();
        }
        if (GUILayout.Button("Save")) //save button
        {
            EditorUtility.SetDirty(obstacleData);
            AssetDatabase.SaveAssets(); //save assets help to mark changes
        }
    }

}
