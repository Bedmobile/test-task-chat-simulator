using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UsersConfig))]
public class UsersConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        UsersConfig myTarget = (UsersConfig) target;

        ShowCurrentUserInfo(myTarget);
        GUILayout.Space(30);
        UsersList.Show(serializedObject.FindProperty("_users"), myTarget);
        GUILayout.Space(50);
        ShowAddingNewUser(myTarget);
        serializedObject.ApplyModifiedProperties();
    }

    public void ShowCurrentUserInfo(UsersConfig config)
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.Label("Current user ID:");
        GUILayout.Label(config.CurrentUser != null ? config.CurrentUser.ID.ToString() : "-");
        GUILayout.Label("Current user Name:");
        GUILayout.Label(config.CurrentUser != null ? config.CurrentUser.Name : "-");
        GUILayout.EndVertical();
        Texture2D avatar = AssetPreview.GetAssetPreview(config.CurrentUser.Avatar);
        GUILayout.Label(avatar);
        GUILayout.EndHorizontal();
    }

    public void ShowAddingNewUser(UsersConfig config)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name");
        config.NewUserName = EditorGUILayout.TextField(config.NewUserName);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Avatar");
        config.NewUserAvatar = EditorGUILayout.ObjectField(config.NewUserAvatar, typeof(Sprite), false) as Sprite;
        EditorGUILayout.EndHorizontal();

        GUIContent addButtonContent = new GUIContent("Add user", "Adding a new user");
        var addButton = GUILayout.Button(addButtonContent);

        if (addButton) {
            config.AddNewUser();
        }
    } 
}
