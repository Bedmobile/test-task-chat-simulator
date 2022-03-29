using UnityEditor;
using UnityEngine;

public static class UsersList
{
	public static void Show(SerializedProperty list, UsersConfig config)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(list, false);
		EditorGUILayout.LabelField(list.arraySize.ToString());
		EditorGUILayout.EndHorizontal();
		if (list.isExpanded) {
			EditorGUI.indentLevel += 1;
			for (int i = 0; i < list.arraySize; i++) {
				EditorGUILayout.BeginHorizontal();
				var item = EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
				EditorGUILayout.BeginVertical();
				
				GUIContent currentButtonContent = new GUIContent("Set current", "Set this user as the current");
				GUIContent removeButtonContent = new GUIContent("Delete user", "Deleting this user");
				GUILayout.Space(21);

				if (GUILayout.Button(currentButtonContent, GUILayout.Width(100f))) {
                    config.SetCurrentUserByIndex(i);
                }

                if (GUILayout.Button(removeButtonContent, GUILayout.Width(100f))) {
                    config.DeleteUserByIndex(i);
                }

                EditorGUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
			}
			EditorGUI.indentLevel -= 1;
		}
	}
}