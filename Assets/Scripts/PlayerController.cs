using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacterController
{
    public CombatController combatController;
    public MovementController movementController;
    public UIManager uiManager;
    public HealthBar healthBar;
    public int lightAttackDamage = 10;
    public int heavyAttackDamage = 20;
    public float speed = 40;
    public int maxHealth = 100;
    public float attackDelay = 1.0f;   // Delay added between attacks

    float nextAttackTime = 0f;
    int currentHealth;

    private Animator animator;
    private float horizontalInput = 0;   // Moves player right (positive values) & left (negative values)
    private bool isJump = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame (use to get input)
    void Update()
    {
        // Get user input
        horizontalInput = Input.GetAxisRaw("Horizontal") * speed;

        // Set speed for run animation
        animator.SetFloat("speed", Mathf.Abs(horizontalInput));        

        // Trigger jump animation when "Jump" button (currently, spacebar) is pressed
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isJump", true);
            isJump = true;
        }

        // Light attack
        if (Input.GetButtonDown("Fire1"))
        {
            // Can only attack if enough time has elapsed since last attack
            if (Time.time >= nextAttackTime)
            {
                animator.SetTrigger("attack1");
                combatController.Attack(lightAttackDamage);
                // Reset next attack time
                nextAttackTime = Time.time + attackDelay;
            }
        }
        // Heavy attack
        if (Input.GetButtonDown("Fire2"))
        {
            if (Time.time >= nextAttackTime)
            {
                animator.SetTrigger("attack2");
                combatController.Attack(heavyAttackDamage);
                nextAttackTime = Time.time + attackDelay;
            }
        }
    }

    // Called once per frame before physics calculations (use to move player)
    void FixedUpdate()
    {
        // Control player movement
        movementController.Move(horizontalInput * Time.fixedDeltaTime, isJump);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void Heal(int health)
    {
        currentHealth += health;
        healthBar.SetHealth(currentHealth);
    }

    public void onLand()
    {
        animator.SetBool("isJump", false);
        isJump = false;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("hit");
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Triggers death animation and disables the player
    public void Die()
    {
        animator.SetBool("isDead", true);
        this.enabled = false;
        // Remove physics interactions stop all motion on death
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        uiManager.ShowGameOver();
    }
}