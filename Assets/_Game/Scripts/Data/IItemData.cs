namespace TakeCare
{
    using UnityEngine;


    public interface IItemData
    {
        Sprite Icon { get; }
        void Use();
    }
}