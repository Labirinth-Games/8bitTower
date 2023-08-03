using Accessories;
using Enemy;
using Helpers;
using Item;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class Zone : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BlackBoxHelper blackBox;
    [SerializeField] private GameObject mobPrefab;
    [SerializeField] private GameObject spawnMobPoint;

    [Header("Settings")]
    [SerializeField] private int dificulty = 1;
    [SerializeField] private GameObject unlock;
    [Space()]
    [Header("Unlock with item")]
    [SerializeField] private bool isUnlockWithItem;
    [SerializeField] private GameObject unlockZonePoint;
    [SerializeField] private List<ItemBase> items;
    [Space()]
    [SerializeField] private List<BlackBoxCondition> conditions = new List<BlackBoxCondition>();

    private void MobSpawn()
    {
        var instance = Instantiate(mobPrefab);
        instance.GetComponent<EnemyBase>().SetAggressive(true);
        instance.transform.position = spawnMobPoint.transform.position;
    }

    private bool HasItemsToOpen(Bag bag)
    {
        var exists = items.Except(bag.GetItems());

        return exists.Count() == 0;
    }

    private void CanUnlock(Player player)
    {
        if (HasItemsToOpen(player.bag)) unlock.GetComponent<IBlackBoxOutput>().Unlock();
    }

    private void Start()
    {
        if (isUnlockWithItem)
        {
            unlockZonePoint.GetComponent<Unlock>().OnStayPlace.AddListener(CanUnlock);
            return;
        }

        blackBox.Load(conditions, unlock, dificulty, () => MobSpawn());
    }

    private void OnValidate()
    {
        if(blackBox == null)
            blackBox = GetComponent<BlackBoxHelper>();
    }
}

[System.Serializable]
public class BlackBoxCondition
{
    public BlackBoxInput input;
    public bool condition;
}