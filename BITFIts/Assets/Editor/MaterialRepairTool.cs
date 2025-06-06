using UnityEditor;
using UnityEngine;
using System.IO;

public class MaterialRepairTool : EditorWindow
{
    [MenuItem("Tools/Fix Missing Materials")]
    public static void ShowWindow()
    {
        GetWindow<MaterialRepairTool>("Fix Missing Materials");
    }

    private string searchFolder = "Assets/";

    void OnGUI()
    {
        GUILayout.Label("Material Auto-Recovery Tool", EditorStyles.boldLabel);

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Search Folder (default: Assets/)");
        searchFolder = EditorGUILayout.TextField(searchFolder);

        if (GUILayout.Button("Start Fix"))
        {
            FixMissingMaterials(searchFolder);
        }
    }

    private void FixMissingMaterials(string folderPath)
    {
        string[] prefabGUIDs = AssetDatabase.FindAssets("t:Prefab", new[] { folderPath });

        int fixedCount = 0;

        foreach (string guid in prefabGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;

            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            bool changed = false;

            Renderer[] renderers = instance.GetComponentsInChildren<Renderer>(true);

            foreach (var renderer in renderers)
            {
                Material[] materials = renderer.sharedMaterials;
                bool updated = false;

                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i] == null)
                    {
                        string matPath = Path.GetDirectoryName(path) + "/Materials/" + prefab.name + ".mat";
                        Material foundMat = AssetDatabase.LoadAssetAtPath<Material>(matPath);

                        if (foundMat != null)
                        {
                            materials[i] = foundMat;
                        }
                        else
                        {
                            materials[i] = new Material(Shader.Find("Standard"));
                        }

                        updated = true;
                        changed = true;
                        fixedCount++;
                    }
                }

                if (updated)
                    renderer.sharedMaterials = materials;
            }

            if (changed)
            {
                PrefabUtility.SaveAsPrefabAsset(instance, path);
                Debug.Log($"Fixed prefab: {path}");
            }

            DestroyImmediate(instance);
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"Material fixing complete. Total fixed materials: {fixedCount}");
    }
}
