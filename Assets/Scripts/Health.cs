using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public float maxHP = 100;
    public float currentHP = 100;
    public UnityEvent OnHealthChanged;
    public UnityEvent OnDeath;

    [Header("Gears")]
    public int maxGears = 15;
    public int currentGears = 0;
    public int gearsNeededToHeal = 5;
    public UnityEvent OnGearsChanged;
    public UnityEvent OnRepair;
    public int GearsOnHit = 0;
    public int GearsChance = 0;
    public GameObject GearCollectiblePrefab;


    [Header("Invinciblity")]
    public bool canBeInvincible;
    public float invinciblityTime;
    public bool isInvincible;

    [Header("Repair")]
    public bool CanRepair = false;
    public float RepairTime = 0.5f;
    public bool HasBeenHit = false;
    public float RepairedHealth = 30;

    [Header("Sprites")]
    public Sprite DefaultSprite;
    public Sprite InvincibiltySprite;
    public Sprite RepairSprite;
    public SpriteRenderer sprite;

    public void AddHealth(float AddedHealth)
    {
        print("Health Changed:" + AddedHealth);
        if (AddedHealth < 0 && isInvincible)
        {
            return;
        }
        currentHP += AddedHealth;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        OnHealthChanged.Invoke();
        if (currentHP <= 0)
        {
            OnDeath.Invoke();
            StartCoroutine(PlayerManager.Instance.RespawnPlayer());
        }
        if (AddedHealth < 0 && currentHP > 0)
        {
            HasBeenHit = true;
            sprite.sprite = DefaultSprite;
            if (canBeInvincible)
            {
                StartCoroutine(Invincibilty());
            }
        }
    }
    public IEnumerator Invincibilty()
    {
        isInvincible = true;
        sprite.sprite = InvincibiltySprite;
        yield return new WaitForSeconds(invinciblityTime);
        isInvincible = false;
        sprite.sprite = DefaultSprite;
    }
    public void AddGears(int AddedGears)
    {
        currentGears += AddedGears;
        if (currentGears > maxGears)
        {
            currentGears = maxGears;
        }
        OnGearsChanged.Invoke();
    }

    public void CreateGears()
    {
        int Chance = Random.Range(1, 100);
        if (Chance <= GearsChance)
        {
            for (int i = 0; i < GearsOnHit - 1; i++)
            {
                GameObject GearCollectible = Instantiate(GearCollectiblePrefab);
                int RandomXDistance = Random.Range(-100, 100);
                GearCollectible.transform.position = transform.position + new Vector3(RandomXDistance / 100, 1f, 0f);
                Vector3 direction = (GearCollectible.transform.position - transform.position).normalized;
                GearCollectible.GetComponent<Rigidbody2D>().AddForce((direction) * 100);
            }
        }
    }

    private void Update()
    {
        if (CanRepair && currentGears >= gearsNeededToHeal)
        {

            if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                StartCoroutine(Repair());
            }
        }
    }

    public IEnumerator Repair()
    {
        HasBeenHit = false;
        currentGears -= gearsNeededToHeal;
        PlayerManager.Instance.UI.UpdateGears();
        PlayerManager.Instance.player.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        PlayerManager.Instance.player.gameObject.GetComponent<Movement>().enabled = false;
        sprite.sprite = RepairSprite;
        yield return new WaitForSeconds(RepairTime);
        if (HasBeenHit == false)
        {
            AddHealth(30);
            PlayerManager.Instance.UI.UpdateHealth();
            sprite.sprite = DefaultSprite;
        }
    }
}
