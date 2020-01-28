using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
public class CSVImportExamplePostprocessor : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            if (str.IndexOf("/lesson_data.csv") != -1)
            {
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                string assetfile = str.Replace(".csv", ".asset");
                LessonData gm = AssetDatabase.LoadAssetAtPath<LessonData>(assetfile);
                if (gm == null)
                {
                    gm = new LessonData();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }

                gm.mListLessons = CSVSerializer.Deserialize<LessonData.Lesson>(data.text);

                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
#if DEBUG_LOG || UNITY_EDITOR
                Debug.Log("ReImported Asset: " + str);
#endif
            }
            if (str.IndexOf("/topic_data.csv") != -1)
            {
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                string assetfile = str.Replace(".csv", ".asset");
                TopicData gm = AssetDatabase.LoadAssetAtPath<TopicData>(assetfile);
                if (gm == null)
                {
                    gm = new TopicData();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }

                gm.mListTopics = CSVSerializer.Deserialize<TopicData.Topic>(data.text);

                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
#if DEBUG_LOG || UNITY_EDITOR
                Debug.Log("ReImported Asset: " + str);
#endif
            }
        }
    }
}
#endif