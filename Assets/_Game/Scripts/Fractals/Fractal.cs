namespace TakeCare
{
    using System.Collections.Generic;
    using UnityEngine;


    public class Fractal
    {
        public List<Quad> Quads;
        
        
        
        public struct Quad
        {
            public readonly Vector2Int[] Points;

            public Quad(Vector2Int point1, Vector2Int point2, Vector2Int point3, Vector2Int point4)
            {
                this.Points = new Vector2Int[4];
                this.Points[0] = point1;
                this.Points[1] = point2;
                this.Points[2] = point3;
                this.Points[3] = point4;
            }
        }
        
        
    }
}