using Objects;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Editor
{
    public class EnemySettings : EditorWindow
    {
        public GameObject EnemyPrefab;

        [MenuItem("/GameSettings/EnemySettings")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(EnemySettings));
        }

        void OnGUI()
        {
            EnemyPrefab = EditorGUILayout.ObjectField("Enemy Prefab", EnemyPrefab, typeof(GameObject), true) as GameObject;
            GUILayout.Label("Settings", EditorStyles.boldLabel);
            if (EnemyPrefab != null && EnemyPrefab.gameObject.tag == "Enemy")
            {
                EnemyPrefab.gameObject.GetComponent<Health>().HitpointMax = EditorGUILayout.FloatField("Hitpoints at the start", EnemyPrefab.gameObject.GetComponent<Health>().HitpointMax);
                EnemyPrefab.gameObject.GetComponent<Health>().FireResistance = EditorGUILayout.FloatField("Fire Resistance", EnemyPrefab.gameObject.GetComponent<Health>().FireResistance);
                EnemyPrefab.gameObject.GetComponent<Health>().IceResistance = EditorGUILayout.FloatField("Ice Resistance", EnemyPrefab.gameObject.GetComponent<Health>().IceResistance);
                EnemyPrefab.gameObject.GetComponent<Health>().ElecticResistance = EditorGUILayout.FloatField("Electric Resistance", EnemyPrefab.gameObject.GetComponent<Health>().ElecticResistance);
                EnemyPrefab.gameObject.GetComponent<Enemy>().Damage = EditorGUILayout.FloatField("Damage", EnemyPrefab.gameObject.GetComponent<Enemy>().Damage);
                EnemyPrefab.gameObject.GetComponent<Movement>().Speed = EditorGUILayout.FloatField("Speed", EnemyPrefab.gameObject.GetComponent<Movement>().Speed);
                EnemyPrefab.gameObject.GetComponent<Enemy>().CostInCoins = EditorGUILayout.IntField("Cost in coins", EnemyPrefab.gameObject.GetComponent<Enemy>().CostInCoins);
            }
            else
            {
                GUILayout.Label("This is object is not enemy", EditorStyles.centeredGreyMiniLabel);
            }
        }
    }
}

