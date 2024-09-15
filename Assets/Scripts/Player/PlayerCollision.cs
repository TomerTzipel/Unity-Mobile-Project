using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour,ISaveable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TimerManager scoreManager;
    [SerializeField] private PowerUpSettings powerUpSettings;

    [SerializeField] private GameObject shieldVisual;

    [SerializeField] private SkinnedMeshRenderer[] playerHairParts;
    [SerializeField] private Material normalHairMaterial;
    [SerializeField] private Material invulHairMaterial;

    [SerializeField] private HpVisualManager hpVisualManager;
    private int _hp;
    [SerializeField] private int MaxHp; 
    [SerializeField] private int healValue;
    
    private bool _isInvulnerable = false;
    [SerializeField] private float invulnerabilityDuration;

    private bool _isShielded = false;

    private void Awake()
    {
        shieldVisual.SetActive(false);
        hpVisualManager.UpdateHpVisual(MaxHp);
        _hp = MaxHp;  
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
            AnalyticsManager.RecordPowerUpAnalytic(powerUpSettings.CurrentPowerUp);
            switch (powerUpSettings.CurrentPowerUp)
            {
                case PowerUpType.Heal:
                    ActivateHeal();
                    break;
                case PowerUpType.Invulnerability:
                    ActivateInvulnerabilityVisual();
                    StartCoroutine(ActivateInvulnerability(invulnerabilityDuration));
                    break;
                case PowerUpType.Shield:
                    ActivateShield();
                    break;
            }
        }
    }

    private void TakeDamage()
    {
        _hp--;

        hpVisualManager.UpdateHpVisual(_hp);

        if (_hp <= 0)
        {
            Lose();
        }
        StartCoroutine(ActivateInvulnerability(0.5f));
    }

    private IEnumerator ActivateInvulnerability(float duration)
    {
        _isInvulnerable = true;
       
        yield return new WaitForSeconds(duration);
        DeactivateInvulnerabilityVisual();
        _isInvulnerable = false;
    }

    private void ActivateInvulnerabilityVisual()
    {
        foreach (var hair in playerHairParts)
        {
            hair.material = invulHairMaterial;
        }
    }
    private void DeactivateInvulnerabilityVisual()
    {
        foreach (var hair in playerHairParts)
        {
            hair.material = normalHairMaterial;
        }
    }
    private void ActivateHeal()
    {
        _hp += healValue;
        if (_hp > MaxHp) 
        {
            _hp = MaxHp;
        }
        hpVisualManager.UpdateHpVisual(_hp);
    }

    private void ActivateShield()
    {
        _isShielded = true;
        shieldVisual.SetActive(true);
    }

    private void RemoveShield()
    {
        _isShielded = false;
        shieldVisual.SetActive(false);
    }

    private void Lose()
    {
        gameManager.GameOver();
    }

    public void LoadData(GameData data)
    {
        transform.position = new Vector3(data.PlayerData.PositionX, 0.2f, 0f);
         _hp = data.PlayerData.Hp;
        hpVisualManager.UpdateHpVisual(_hp);
    }
    public void SaveData(ref GameData data)
    {
        data.PlayerData.PositionX = transform.position.x;
        data.PlayerData.Hp = _hp;
    }
}
