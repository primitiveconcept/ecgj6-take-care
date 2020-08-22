namespace Spineless
{
    using System.Collections.Generic;
    using UnityEngine;


    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private bool spawnOnStart = true;

        private List<GameObject> spawnedObjects = new List<GameObject>();


        #region Properties
        public GameObject Prefab
        {
            get { return this.prefab; }
            set { this.prefab = value; }
        }


        public bool SpawnOnStart
        {
            get { return this.spawnOnStart; }
            set { this.spawnOnStart = value; }
        }
        #endregion


        public void Despawn(GameObject spawnedObject)
        {
            if (spawnedObject.activeSelf)
                PoolManager.Despawn(spawnedObject);

            if (this.spawnedObjects.Contains(spawnedObject))
                this.spawnedObjects.Remove(spawnedObject);
        }


        public void DespawnAll()
        {
            for (int i = 0; i < this.spawnedObjects.Count; i++)
            {
                var spawnedObject = this.spawnedObjects[i];
                if (spawnedObject.activeSelf)
                    PoolManager.Despawn(spawnedObject);
            }

            this.spawnedObjects.Clear();
        }


        public GameObject Spawn()
        {
            GameObject spawnedObject = PoolManager.Spawn(this.prefab, this.transform.position, this.transform.rotation);
            this.spawnedObjects.Add(spawnedObject);
            SpawnTracker spawnTracker = spawnedObject.GetComponent<SpawnTracker>();
            if (spawnTracker == null)
                spawnTracker = spawnedObject.AddComponent<SpawnTracker>();
            spawnTracker.Spawner = this;

            return spawnedObject;
        }


        public void Start()
        {
            if (this.spawnOnStart)
                Spawn();
        }


        /*
        public void OnDrawGizmos()
        {
            if (this.prefab == null)
                return;

            SpriteRenderer spriteRenderer = this.prefab.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
                return;

            SpriteRenderer sprite = spriteRenderer.sprite;
            if (sprite == null)
                return;

            Rect rect = sprite.rect;

            rect.width /= (sprite.pixelsPerUnit * Camera.main.orthographicSize);
            rect.height /= (sprite.pixelsPerUnit * Camera.main.orthographicSize);
            rect.center = Camera.main.WorldToViewportPoint(this.transform.position);
            
        
            Gizmos.DrawGUITexture(rect, sprite.texture, spriteRenderer.sharedMaterial);
        }
        */
    }
}