﻿using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string EnemyHealthFileName = "EnemyHealth.json";
    private const string PlayerHealthFileName = "PlayerHealth.json";
    private const string EquipmentFileName = "Equipment.json";
    private const string InvetoryFileName = "Inventory.json";

    public void Save()
    {
        SaveHealth();
        SaveEquipment();
        SaveInventory();
    }

    public void Load()
    {
        LoadHealth();
        LoadEquipment();
        LoadInventory();
    }

    private void SaveInventory()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();

        InventoryData inventoryData = new InventoryData(inventoryManager.GetStackData());

        string json = JsonUtility.ToJson(inventoryData);

        File.WriteAllText(Path.Combine(Application.dataPath, InvetoryFileName), json);
    }

    private void LoadInventory()
    {
        string fullPath = Path.Combine(Application.dataPath, InvetoryFileName);

        if (!File.Exists(fullPath))
        {
            return;
        }

        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();

        string data = File.ReadAllText(fullPath);
        InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(data);

        inventoryManager.SetSlots(inventoryData);
    }

    private void SaveEquipment()
    {
        EquipManager equipManager = FindObjectOfType<EquipManager>();
        EquipmentData equipment = new EquipmentData(equipManager.GetBodyArmor(), equipManager.GetHeadArmor());

        string data = JsonUtility.ToJson(equipment);

        File.WriteAllText(Path.Combine(Application.dataPath, EquipmentFileName), data);
    }

    private void LoadEquipment()
    {
        string fullPath = Path.Combine(Application.dataPath, EquipmentFileName);

        if (!File.Exists(fullPath))
        {
            return;
        }

        EquipManager equipManager = FindObjectOfType<EquipManager>();

        string data = File.ReadAllText(fullPath);
        EquipmentData equipment = JsonUtility.FromJson<EquipmentData>(data);

        equipManager.Equip(equipment);
    }

    private void SaveHealth()
    {
        HealthData playerHealth = new HealthData(FindObjectOfType<Player>().Health.GetCurrentHealth());
        HealthData enemyHealth = new HealthData(FindObjectOfType<Enemy>().Health.GetCurrentHealth());

        string playerData = JsonUtility.ToJson(playerHealth);
        string enemyData = JsonUtility.ToJson(enemyHealth);

        File.WriteAllText(Path.Combine(Application.dataPath, PlayerHealthFileName), playerData);
        File.WriteAllText(Path.Combine(Application.dataPath, EnemyHealthFileName), enemyData);
    }

    private void LoadHealth()
    {
        string playerFullPath = Path.Combine(Application.dataPath, PlayerHealthFileName);
        string enemyFullPath = Path.Combine(Application.dataPath, EnemyHealthFileName);

        if (File.Exists(playerFullPath))
        {
            string playerData = File.ReadAllText(playerFullPath);

            HealthData playerHealth = JsonUtility.FromJson<HealthData>(playerData);

            FindObjectOfType<Player>().Health.SetHealth(playerHealth.Health);
        }

        if (File.Exists(enemyFullPath))
        {
            string enemyData = File.ReadAllText(enemyFullPath);

            HealthData enemyHealth = JsonUtility.FromJson<HealthData>(enemyData);

            FindObjectOfType<Enemy>().Health.SetHealth(enemyHealth.Health);
        }

    }
}