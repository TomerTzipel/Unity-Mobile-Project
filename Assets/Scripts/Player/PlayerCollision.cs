using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private TimerManager scoreManager;
    [SerializeField] private PowerUpSettings powerUpSettings;

    private int hp;
    [SerializeField] private int MaxHp; 
    [SerializeField] private int healValue;
    
    private bool _isInvulnerable = false;
    [SerializeField] private float invulnerabilityDuration;

    private bool _isShielded = false;

    private void Awake()
    {
        hp = MaxHp;
        //set up hp visual
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (_isInvulnerable) return;

            if (_isShielded)
            {
                RemoveShield();
                return;
            }

            TakeDamage();
        }

        if (other.CompareTag("PowerUp"))
        {
            switch (powerUpSettings.CurrentPowerUp)
            {
                case PowerUpType.Heal:
                    ActivateHeal();
                    break;
                case PowerUpType.Invulnerability:
                    StartCoroutine(ActivateInvulnerability());
                    break;
                case PowerUpType.Shield:
                    ActivateShield();
                    break;
            }
        }
    }

    private void TakeDamage()
    {
        hp--;

        //update visual

        if (hp <= 0)
        {
            Lose();
        }
       
    }

    private IEnumerator ActivateInvulnerability()
    {
        _isInvulnerable = true;
        //activate invul effect visual
        yield return new WaitForSeconds(invulnerabilityDuration);
        //deactivate invul effect visual
        _isInvulnerable = false;
    }

    private void ActivateHeal()
    {
        hp+= healValue;
        if (hp > MaxHp) 
        {
            hp = MaxHp;
        }

        //update hp visual

    }

    private void ActivateShield()
    {
        _isShielded = true;
        //Add shield visual
    }

    private void RemoveShield()
    {
        _isShielded = false;
        //Remove shield visual
    }

    private void Lose()
    {
        //start lose sequence   
    }
}
