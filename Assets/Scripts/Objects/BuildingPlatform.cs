using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class BuildingPlatform : MonoBehaviour
    {
        public GameObject ShopPanel;
        public BuildManagerScriptableObject BuildingManager;

        public bool CanBuild = true;

        private void OnEnable()
        {
            CanBuild = true;
        }

        private void OnMouseDown()
        {
            if (CanBuild)
            {
                ShopPanel.SetActive(true);
                BuildingManager.BuildingTarget = gameObject;
            }
        }
    }
}

