using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class ImgFormatCover
{
    [MenuItem("Tools/ImgCoverToSprite")]
    public static void CoverImgToSprite()
    {
        Object[] targetObjs = Selection.objects;
        foreach (var targetObj in targetObjs)
        {
            if (targetObj != null && targetObj is Texture)
            {
                string path = AssetDatabase.GetAssetPath(targetObj);

                TextureImporter texture = AssetImporter.GetAtPath(path) as TextureImporter;
                texture.textureType = TextureImporterType.Sprite;
                texture.spritePixelsPerUnit = 1;
                texture.filterMode = FilterMode.Trilinear;
                texture.mipmapEnabled = false;
                texture.textureFormat = TextureImporterFormat.AutomaticTruecolor;
                AssetDatabase.ImportAsset(path);
            }
        }
    }
}
