using UnityEngine;
using UnityEngine.UI;

public class DailyButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite canClaimSprite;
    [SerializeField] private Sprite claimedSprite;
    [SerializeField] private Sprite unclaimedSprite;

    [SerializeField] private GameObject canClaimIcon;
    [SerializeField] private GameObject claimedIcon;
    [SerializeField] private GameObject unclaimedIcon;

    [SerializeField] private AudioSource failSFX;

    [SerializeField] private DailyRewardsMenager rewardsMenager;
    [SerializeField] private int _day;
    private void Awake()
    {
        if (PlayerPrefsManager.IsDayClaimed(_day))
        {
            ClaimedVisual();
            return;
        }
        int currentDay = PlayerPrefsManager.GetCurrentDay();

        if (currentDay >= _day)
        {
            CanClaimVisual(); 
            return;
        }

        UnclaimedVisual();
    }

    public void OnClick()
    {
        int currentDay = PlayerPrefsManager.GetCurrentDay();
        if (PlayerPrefsManager.IsDayClaimed(_day) || currentDay != _day)
        {
            failSFX.Play();
            return;
        }

        ClaimedVisual();
        rewardsMenager.ClaimReward(_day);
    }

    private void ClaimedVisual()
    {
        image.sprite = claimedSprite;
        canClaimIcon.SetActive(false);
        claimedIcon.SetActive(true);
        unclaimedIcon.SetActive(false);
    }
    
    private void CanClaimVisual()
    {
        image.sprite = canClaimSprite;
        canClaimIcon.SetActive(true);
        claimedIcon.SetActive(false);
        unclaimedIcon.SetActive(false);
    }
    private void UnclaimedVisual()
    {
        image.sprite = unclaimedSprite;
        canClaimIcon.SetActive(false);
        claimedIcon.SetActive(false);
        unclaimedIcon.SetActive(true);
    }
}
