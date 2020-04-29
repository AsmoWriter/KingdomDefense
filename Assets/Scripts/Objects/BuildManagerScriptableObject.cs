using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Objects
{
    [CreateAssetMenu(menuName = "BuildingManager")]
    public class BuildManagerScriptableObject : ScriptableObject
    {
        public Vector3 PlatformOffset = new Vector3(0, 3, 0);
        public GameObject BuildingTarget { get; set; }
        public GameObject DestroyTarget { get; set; }

        public void BuildTower(GameObject towerPrefab)
        {
            Vector3 TargetAndOffsetPosition = BuildingTarget.transform.position + PlatformOffset;
            Instantiate(towerPrefab, TargetAndOffsetPosition, Quaternion.identity);
        }
        public void DestroyTower(GameObject towerPrefab)
        {
            Destroy(DestroyTarget);
        }
    }
}