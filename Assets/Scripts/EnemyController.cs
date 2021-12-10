using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ICharacterController
{
    public CombatController combatController;
    public MovementController movementController;
    public Transform player;
    public int damage = 10;
    public float speed = 5;
    public int maxHealth = 30;
    public float attackDelay = 1.0f;   // Delay added between attacks
    public float distanceToActive = 10f;

    float nextAttackTime = 0f;
    int currentHealth;

    private Animator animator;
    private float distanceToPlayer;
    private int direction = -1;
    private bool isAbovePlayer = true;
    private bool isJump = false;

    // TODO: Add constructor and spawner

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Update enemy's distance to the player
        distanceToPlayer = Vector2.Distance(player.localPosition, transform.localPosition);
        // Check if enemy is too far above player to react/move
        isAbovePlayer = (player.localPosition.y - transform.localPosition.y) < -3;

        if (!isAbovePlayer && (Mathf.Abs(distanceToPlayer) <= distanceToActive)) {
            // Update enemy direction to move towards the player
            direction = (player.localPosition.x - transform.localPosition.x) > 0 ? 1 : -1;

            // Set speed for walk animation
            animator.SetFloat("speed", Mathf.Abs(speed)); 
        }
        else 
        {
            animator.SetFloat("speed", 0); 
        }

        // Only attack when in range of player
        if (Mathf.Abs(distanceToPlayer) <= 1.5f) 
        {
            // Can only attack if enough time has elapsed since last attack
            if (Time.time >= nextAttackTime)
            {
                animator.SetTrigger("attack");
                combatController.Attack(damage);
                // Reset next attack time
                nextAttackTime = Time.time + attackDelay;
            }
        }

        // Disable the enemy if the player is dead
        if (player.GetComponent<PlayerController>().GetCurrentHealth() == 0)
        {
            animator.SetFloat("speed", 0);
            this.enabled = false;
        }
    }

    // Called once per frame before physics calculations (use for movement)
    void FixedUpdate()
    {
        if (!isAbovePlayer && Mathf.Abs(distanceToPlayer) <= distanceToActive) {
            movementController.Move(direction * speed * Time.fixedDeltaTime, false);
        }
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

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Triggers death animation and disables the enemy
    public void Die()
    {
        animator.SetBool("isDead", true);
        this.enabled = false;
        // Remove physics interactions stop all motion on death
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
