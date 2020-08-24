namespace TakeCare
{
    using UnityEngine;


    public class FractalTextureGenerator
    {
        public static Sprite GenerateSprite(Fractal fractal, int width, int height)
        {
            return GenerateSprite(fractal, width, height, new Vector2(0.5f, 0.5f));
        }
        
        public static Sprite GenerateSprite(Fractal fractal, int width, int height, Vector2 pivot)
        {
            
            
            Material fractalMaterial = new Material(Shader.Find("Fractal Shader"));
            fractal.ApplyToMaterial(fractalMaterial);
            Texture2D texture = ShaderToTexture(fractalMaterial, width, height);
            
            Material transparentMaterial = new Material(Shader.Find("Transparent Color"));
            transparentMaterial.SetTexture("_MainTex", texture);
            transparentMaterial.SetColor("_TransparentColor", Color.white);
            transparentMaterial.SetFloat("_Threshold", 0.75f);
            texture = ShaderToTexture(transparentMaterial, width, height);

            Color[] pixels = texture.GetPixels();
            Color32[] newPixels = new Color32[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i] == Color.black)
                {
                    newPixels[i] = Color.clear;
                }
                else
                {
                    newPixels[i] = pixels[i];
                }
            }
            texture.SetPixels32(newPixels);
            texture.Apply();

            Sprite sprite = Sprite.Create(
                texture: texture, 
                rect: new Rect(0, 0, width, height), 
                pivot: pivot, 
                pixelsPerUnit: 16);

            return sprite;
        }


        public static Texture2D ShaderToTexture(Material shaderMaterial, int width, int height)
        {
            //render material to rendertexture
            RenderTexture renderTexture = RenderTexture.GetTemporary(width, height);
            Graphics.Blit(null, renderTexture, shaderMaterial);

            //transfer image from rendertexture to texture
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(Vector2.zero, new Vector2(width, height)), 0, 0);

            //save texture to file
            byte[] png = texture.EncodeToPNG();
            texture.LoadImage(png);

            //clean up variables
            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(renderTexture);

            return texture;
        }
    }
}