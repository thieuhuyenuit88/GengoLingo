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
                LessonMaster gm = AssetDatabase.LoadAssetAtPath<LessonMaster>(assetfile);
                if (gm == null)
                {
                    gm = new LessonMaster();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }

                gm.mListLessons = CSVSerializer.Deserialize<LessonMaster.Lesson>(data.text);

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
                TopicMaster gm = AssetDatabase.LoadAssetAtPath<TopicMaster>(assetfile);
                if (gm == null)
                {
                    gm = new TopicMaster();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }

                gm.mListTopics = CSVSerializer.Deserialize<TopicMaster.Topic>(data.text);

                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
#if DEBUG_LOG || UNITY_EDITOR
                Debug.Log("ReImported Asset: " + str);
#endif
            }
            if (str.IndexOf("/voca_data.csv") != -1)
            {
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                string assetfile = str.Replace(".csv", ".asset");
                VocaMaster gm = AssetDatabase.LoadAssetAtPath<VocaMaster>(assetfile);
                if (gm == null)
                {
                    gm = new VocaMaster();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }

                gm.mListVoca = CSVSerializer.Deserialize<VocaMaster.Voca>(data.text);

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