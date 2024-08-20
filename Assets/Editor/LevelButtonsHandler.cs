using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelButtonsHandler : EditorWindow
{

    [MenuItem("tools/levelButtons")]

    public static void Open() {
        GetWindow<LevelButtonsHandler>();
    }


    Transform contentHolder;
    GameObject buttonPrefab;
    int numberOfButtons;
    int offset;


    private void OnGUI()
    {

        SerializedObject win = new SerializedObject(this);

        contentHolder = (Transform)EditorGUILayout.ObjectField(contentHolder, typeof(Transform), true);
        buttonPrefab = (GameObject)EditorGUILayout.ObjectField(buttonPrefab, typeof(GameObject), false);

        numberOfButtons = EditorGUILayout.IntField(numberOfButtons);
        offset = EditorGUILayout.IntField(offset);
        CreateButtons();
    }




    void CreateButtons() {

        if (GUILayout.Button("create")) {
            CreateLevelsButtons();
        }
    }



    void CreateLevelsButtons() {

        if (contentHolder != null && buttonPrefab != null && numberOfButtons != 0) {

            for (int i=0; i<numberOfButtons; i++)
            {
                var buttonInstance = Instantiate(buttonPrefab, contentHolder);
                buttonInstance.GetComponent<LevelButton>().SetIndex(i + offset);
                int ind = i + offset;
                buttonInstance.name = "button" + ind;
            }
        }
    }
}
