namespace TakeCare
{
    using System;


    [Serializable]
    public struct PlantResources
    {
        public ResourceNode Leaves;
        public ResourceNode Branches;
        public ResourceNode Features;
    }

    [Serializable]
    public struct ResourceNode
    {
        public float Carbon; // Size
        public float Oxygen; // R
        public float Nitrogen; // G
        public float Hydrogen; // B
    }

}