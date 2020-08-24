namespace TakeCare
{
    using UnityEngine;


    public interface IItem
    {
        Sprite Icon { get; }
        void Use();
    }
}