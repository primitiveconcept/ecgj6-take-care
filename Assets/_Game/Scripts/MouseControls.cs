namespace TakeCare
{
    using UnityEngine;


    public class MouseControls
    {
        private static Camera _mainCamera;


        private static Camera MainCamera
        {
            get
            {
                if (_mainCamera == null)
                    _mainCamera = Camera.main;
                return _mainCamera;
            }
        }
        
        public static Vector2 GetCursorWorldPosition()
        {
            Vector2 mousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            return mousePosition;
        }


        public static Vector3Int GetCursorGridPosition(Grid grid)
        {
            return grid.WorldToCell(GetCursorWorldPosition());
        }
    }
}