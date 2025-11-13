using System;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    public ItemData itemData;
    public Action addItem;

    public int clearCount;
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();

        addItem += ApplyItemEffect;
    }

    private void ApplyItemEffect()
    {
        if (itemData == null || condition == null)
            return;

        foreach (var consumable in itemData.Consumables)
        {
            if (consumable.type == ConsumableType.Health)
            {
                if (itemData.type == ItemType.Consumable)
                {
                    StartCoroutine(GradualHeal(consumable.value, 10f));
                }
            }
            if (consumable.type == ConsumableType.ClearResource)
            {
                if (itemData.type == ItemType.Consumable)
                {
                    condition.Heal(consumable.value);

                    clearCount++;
                    Debug.Log("클리어 아이템 획득! clearCount = " + clearCount);

                    if (clearCount >= 3)
                    {
                        Debug.Log("GAME Clear!");
                    }
                }
            }
        }
    }

    private IEnumerator GradualHeal(float totalAmount, float duration)
    {
        float elapsed = 0f;
        float healPerSecond = totalAmount / duration;

        while (elapsed < duration)
        {
            condition.Heal(healPerSecond * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}