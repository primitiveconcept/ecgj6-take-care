namespace TakeCare.Editor
{
    using UnityEngine;
    using UnityEditor;
    
    public class SpriteImporter : AssetPostprocessor
    {
        public void OnPreprocessTexture()
        {
            TextureImporter textureImporter = (TextureImporter)this.assetImporter;
            if (textureImporter.textureType != TextureImporterType.Sprite)
            {
                textureImporter.textureType = TextureImporterType.Sprite;
                textureImporter.spriteImportMode = SpriteImportMode.Single;
                textureImporter.spritePixelsPerUnit = 16;
                TextureImporterSettings textureSettings = new TextureImporterSettings();
                
                textureImporter.ReadTextureSettings(textureSettings);
                //textureSettings.spriteAlignment = (int)SpriteAlignment.BottomCenter;
                textureImporter.SetTextureSettings(textureSettings);
            }
            
            textureImporter.filterMode = FilterMode.Point;
            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            textureImporter.mipmapEnabled = false;
        }
    }
}