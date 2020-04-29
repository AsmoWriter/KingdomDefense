using Objects;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Editor
{
    public class TowerSettings : EditorWindow
    {
        public GameObject TowerPrefab;

        [MenuItem("/GameSettings/TowerSettings")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(TowerSettings));
        }

        void OnGUI()
        {
            TowerPrefab = EditorGUILayout.ObjectField("Tower Prefab", TowerPrefab, typeof(GameObject), true) as GameObject;
            GUILayout.Label("Settings", EditorStyles.boldLabel);
            if (TowerPrefab != null && TowerPrefab.gameObject.tag == "Tower")
            {
                TowerPrefab.gameObject.GetComponent<TowerShopItem>().BuildCost = EditorGUILayout.IntField("Build cost", TowerPrefab.gameObject.GetComponent<TowerShopItem>().BuildCost);
                TowerPrefab.gameObject.GetComponent<TowerShopItem>().DestroyCost = EditorGUILayout.IntField("Destroy cost", TowerPrefab.gameObject.GetComponent<TowerShopItem>().DestroyCost);
                TowerPrefab.gameObject.GetComponent<TowerShopItem>().Weapon.AttackDistance = EditorGUILayout.FloatField("Attack distance", TowerPrefab.gameObject.GetComponent<TowerShopItem>().Weapon.AttackDistance);
                TowerPrefab.gameObject.GetComponent<TowerShopItem>().Weapon.AttackSpeed = EditorGUILayout.FloatField("Attack speed", TowerPrefab.gameObject.GetComponent<TowerShopItem>().Weapon.AttackSpeed);
                TowerPrefab.gameObject.GetComponent<TowerShopItem>().Weapon.Damage = EditorGUILayout.FloatField("Damage", TowerPrefab.gameObject.GetComponent<TowerShopItem>().Weapon.Damage);
                TowerPrefab.gameObject.GetComponent<TowerShopItem>().Weapon.UsedDamageMode = (TowrerWeapon.DamageMode)EditorGUILayout.EnumFlagsField("Damage mode", TowerPrefab.gameObject.GetComponent<TowerShopItem>().Weapon.UsedDamageMode);
                TowerPrefab.gameObject.GetComponent<TowerShopItem>().Weapon.UsedWeaponType = (TowrerWeapon.WeaponType)EditorGUILayout.EnumFlagsField("Damage mode", TowerPrefab.gameObject.GetComponent<TowerShopItem>().Weapon.UsedWeaponType);
                if (TowerPrefab.gameObject.GetComponent<TowerShopItem>().Weapon.UsedWeaponType == TowrerWeapon.WeaponType.Ranged)
                {
                    TowerPrefab.gameObject.GetComponent<TowerShopItem>().Weapon.BulletPrefab = EditorGUILayout.ObjectField("Bullet Prefab", TowerPrefab.gameObject.GetComponent<TowerShopItem>().Weapon.BulletPrefab, typeof(GameObject), true) as GameObject;
                }
            }
            else
            {
                GUILayout.Label("This is object is not tower", EditorStyles.centeredGreyMiniLabel);
            }
        }
    }
}
