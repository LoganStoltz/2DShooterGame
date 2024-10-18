using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    public HealthBar healthBar;
    private float currentHealth;
    private int eliminations;
    private int score;
    public GameObject gameObject;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMax(maxHealth);
    }

    private void Update()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (currentHealth <= 0)
            Die();

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(10);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetSlider(currentHealth);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        healthBar.SetSlider(currentHealth);
    }

    private void Die()
    {
        Debug.Log("You died!");
        Destroy(gameObject);
        GameController.manager.GameOver();
    }
}