using System;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Conditions health { get { return uiCondition.health; } }

    public event Action onTakeDamage;

    public GameObject ClearMessage;
    public GameObject GameOverMessage;

    public bool isdead;
    public bool isclear;

    private void Update()
    {
        Player player = CharacterManager.Instance.Player;

        if (health.curValue <= 0f)
        {
            Die();
            GameOverMessage.SetActive(true);
            Time.timeScale = 0f;
            CharacterManager.Instance.Player.clearCount = 0;
        }

        if (CharacterManager.Instance.Player.clearCount >= 3)
        {
            Clear();
            ClearMessage.SetActive(true);
            CharacterManager.Instance.Player.clearCount = 0;
            Time.timeScale = 0f;
        }
    }

    private void Clear()
    {
        if (isclear) return;
        isclear = true;
        Debug.Log("클리어!");
        Time.timeScale = 0f;
        ClearMessage.SetActive(true);

    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Die()
    {
        if (isdead) return; 
        isdead = true;
        Debug.Log("플레이어가 죽었다.");
        Time.timeScale = 0f;
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}