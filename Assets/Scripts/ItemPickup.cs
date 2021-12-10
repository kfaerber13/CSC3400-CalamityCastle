using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public PlayerController player;
    public int healAmount = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called when some object enters the collider bounds
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the object is the player, allow interaction and enable optional prompt text
        if (other.CompareTag("Player"))
        {
            player.Heal(healAmount);
            gameObject.SetActive(false);
            this.enabled = false;
        }
    }
}
