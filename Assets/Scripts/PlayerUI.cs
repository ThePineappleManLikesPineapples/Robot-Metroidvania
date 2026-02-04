using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public RectTransform maxHealthBar;
    public RectTransform currentHealthBar;
    public Image Gear1;
    public Image Gear2;
    public Image Gear3;

    public static PlayerUI Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealth()
    {
        float HealthPercentage = PlayerManager.Instance.health.currentHP / PlayerManager.Instance.health.maxHP;
        currentHealthBar.gameObject.GetComponent<Image>().fillAmount = HealthPercentage;
    }
    public void UpdateGears()
    {
        float CurrentGears = PlayerManager.Instance.health.currentGears;
        float NeededToHeal = PlayerManager.Instance.health.gearsNeededToHeal;
        Gear1.fillAmount = 0;
        Gear2.fillAmount = 0;
        Gear3.fillAmount = 0;
        if (CurrentGears >= NeededToHeal * 3)
        {
            Gear1.fillAmount = 1;
            Gear2.fillAmount = 1;
            Gear3.fillAmount = 1;
        }
        else if (CurrentGears >= NeededToHeal * 2)
        {
            Gear1.fillAmount = 1;
            Gear2.fillAmount = 1;
            Gear3.fillAmount = (CurrentGears - 2 * (NeededToHeal)) / NeededToHeal;
        }
        else if (CurrentGears >= NeededToHeal * 1)
        {
            Gear1.fillAmount = 1;
            Gear2.fillAmount = (CurrentGears - NeededToHeal) / NeededToHeal;
        }
        else
        {
            Gear1.fillAmount = (CurrentGears) / NeededToHeal;
        }
    }
}
