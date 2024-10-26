using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invicibilityTimeAfterHit = 3f;
    public float invicibilityFlashDelay = 0.2f;
    public bool isInvincible = false;

    public HealthBar healthBar;

    public SpriteRenderer graphics;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans le scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.H)) 
        {
            TakeDamage(100);
        } 
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible) 
        {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0) 
            {
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvicibilityDelay());
        }
    }

    public void HealPlayer(int amount)
    {
        currentHealth += amount;

        if (currentHealth < maxHealth) {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);
    }

    public void Die()
    {
        PlayerMovements.instance.enabled = false;
        PlayerMovements.instance.animator.SetTrigger("Die");
        PlayerMovements.instance._rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovements.instance.playerCollider.enabled = false;
        GameOverManager.instance.OnPlayerDeath();

    }

    public void Respawn()
    {
        PlayerMovements.instance.enabled = true;
        PlayerMovements.instance.animator.SetTrigger("Respawn");
        PlayerMovements.instance._rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovements.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTimeAfterHit);
        isInvincible = false;
    }
}
