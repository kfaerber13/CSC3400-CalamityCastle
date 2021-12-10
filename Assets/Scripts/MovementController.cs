using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    [SerializeField] public float jumpAmount = 1000;
    public UnityEvent OnLand;   // Event to call when landing (from a jump or fall)
    private Rigidbody2D objectRigidbody;    // Character's rigid body
    [SerializeField] private LayerMask groundLayerMask;  // Game layers that are defined as ground
    [SerializeField] private Transform groundCheck;      // GameObject for checking if character is touching ground
    [SerializeField] private bool isFacingRight = true;
    private bool isOnGround;
    private Vector3 zeroVector = Vector3.zero;

    // Awake is called before the first frame update
    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody2D>();
        if (OnLand == null) 
        {
            OnLand = new UnityEvent();
        }
    }

    // FixedUpdate is called once per frame, checks if character is touching ground
    private void FixedUpdate()
    {
        bool wasOnGround = isOnGround;
        isOnGround = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, groundLayerMask);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isOnGround = true;
				if (!wasOnGround)
                {
                    OnLand.Invoke();
                }
			}
		}
    }

    // Handles character movement
    public void Move(float moveAmount, bool isJump)
    {
        objectRigidbody.velocity = new Vector2(moveAmount * 10, objectRigidbody.velocity.y);

        // If character moves right and is facing left or if character moves left and is facing right, flip them
        if ((moveAmount > 0 && !isFacingRight) || (moveAmount < 0 && isFacingRight))
        {
            Flip();
            // Also flip the character's hit detector
            GetComponent<CombatController>().hitDetector.localScale *= -1;
            
        }

        // Can't jump in air (double jump)
        if (isOnGround && isJump)
		{
            // Add a vertical force to the character for jumping
            objectRigidbody.AddForce(new Vector2(0, jumpAmount));
            isOnGround = false;
		}
    }

    // Flip the player's local scale when turning
    private void Flip()
	{
		isFacingRight = !isFacingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
