using UnityEngine;

public class Spikes : MonoBehaviour
{
    public PlayerController player;

    // Called when some object enters the collider bounds
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the object is the player, allow interaction and enable optional prompt text
        if (other.CompareTag("Player"))
        {
            // Spikes immediately do full damage to the player (insta-death)
            player.Damage(player.GetMaxHealth());
        }
    }
}
