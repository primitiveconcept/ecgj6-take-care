namespace TakeCare
{
    using System;
    using UnityEngine;


    [Serializable]
    public class Fractal
    {
        public int Iterations = 100;
        public float ConvergenceThreshold = 4f;
        public Vector2 Center = new Vector2(0, 0);
        public float Scale = 2;
        public int Multibrot = 4;


        public void ApplyToMaterial(Material fractalMaterial)
        {
            fractalMaterial.SetInt("iterations", this.Iterations);
            fractalMaterial.SetFloat("convergenceThreshold", this.ConvergenceThreshold);
            fractalMaterial.SetInt("multibrot", this.Multibrot);
            fractalMaterial.SetFloat("scale", this.Scale);
            fractalMaterial.SetFloat("centreX", this.Center.x);
            fractalMaterial.SetFloat("centreY", this.Center.y);
        }
    }
}