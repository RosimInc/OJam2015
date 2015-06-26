using UnityEngine;
using System.Collections;
using UnityEditor;

namespace InputHandler
{
    public class CreateXboxMapperAsset
    {
        [MenuItem("InputHandler/Create/XboxMapper")]
        public static void CreateInputAsset()
        {
            XboxMapperAsset asset = XboxMapperAsset.CreateInstance<XboxMapperAsset>();
            AssetDatabase.CreateAsset(asset, "Assets/XboxMapper.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}