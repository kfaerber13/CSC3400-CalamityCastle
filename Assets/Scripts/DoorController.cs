using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour, IControllableObject
{
    // Optional prompt text, displayed when unlocked (e.g., 'Press E')
    public GameObject promptText;
    // Optional hint text, displayed when locked (e.g., 'Needs a key')
    public GameObject hintText;
    // Optional key that unlocks the door, will be removed from inventory when used
    public GameObject key;

    private Animator animator;
    private bool isUnlocked = false;
    private bool isInteractable = false;
    private bool isDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SetPromptTextVisibility(false);
        SetHintTextVisibility(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If interaction is enabled, the door has been unlocked, and the player presses the E key, then trigger the animation
        if (isInteractable && isUnlocked && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("interact");
            SetPromptTextVisibility(false);
            key.GetComponent<IControllableObject>().TriggerObject();
            isDisabled = true;
        }

        // Once door has been unlocked, press up/W to move to next scene
        if (isUnlocked && isDisabled && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            SceneManager.LoadSceneAsync("DungeonLevel-2");
        }
    }

    // Unlocks when triggered by another object (e.g., collecting key from a chest)
    public void TriggerObject() {
        isUnlocked = true;
    }

    // TODO: Refactor and consolidate with InteractableObject
    // Called when some object enters the collider bounds
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the object is the player, allow interaction and show optional prompt text
        if (!isDisabled && other.tag == "Player")
        {
            if (!isUnlocked)
            {
                SetHintTextVisibility(true);
            }
            else
            {
                SetPromptTextVisibility(true);
            }
            isInteractable = true;
        }
    }

    // Called when some object exits the collider bounds
    private void OnTriggerExit2D(Collider2D other)
    {
        // If the object is the player, disable interaction and hide optional prompt text
        if (!isDisabled && other.tag == "Player")
        {
            if (!isUnlocked)
            {
                SetHintTextVisibility(false);
            }
            else
            {
                SetPromptTextVisibility(false);
            }
            isInteractable = false;
        }
    }

    // Shows or hides the prompt text
    private void SetPromptTextVisibility(bool value)
    {
        if (promptText != null)
        {
            promptText.SetActive(value);
        }
    }

    // Shows or hides the hint text
    private void SetHintTextVisibility(bool value)
    {
        if (hintText != null)
        {
            hintText.SetActive(value);
        }
    }
}
