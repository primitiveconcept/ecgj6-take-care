namespace TakeCare
{
    using UnityEngine;


    public class FractalTextureGenerator
    {
        public static void GenerateTexture()
        {
            int size = 256;
            
            Material fractalMaterial = new Material(Shader.Find("Fractal Shader"));
            Texture2D texture = ShaderToTexture(fractalMaterial, size, size);
            Material transparentMaterial = new Material(Shader.Find("Transparent Color"));
            transparentMaterial.SetTexture("_MainTex", texture);
            transparentMaterial.SetColor("_TransparentColor", Color.white);
            transparentMaterial.SetFloat("_Threshold", 0.75f);
            texture = ShaderToTexture(transparentMaterial, size, size);

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


            GameObject gameObject = new GameObject("Fractal");
            SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            Sprite sprite = Sprite.Create(
                texture, 
                new Rect(0,0,size,size), 
                new Vector2(0.5f,0.5f), 16);
            
            spriteRenderer.sprite = sprite;
        }


        public static Texture2D GetFractalTexture(int width, int height, Fractal fractal)
        {
            Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false);

            
            
            foreach (Fractal.Quad quad in fractal.Quads)
            {
                
            }

            return texture;
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