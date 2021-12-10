using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Optional prompt text (e.g., 'Press E')
    public GameObject promptText;
    // Other objects that are controlled by this InteractableObject (e.g., a gate controlled by a lever)
    // all controlledObjects must implement the IControllableObject interface
    public GameObject[] controlledObjectList;

    private Animator animator;
    private bool isInteractable = false;
    private bool isDisabled = false;

    void Start() {
        animator = GetComponent<Animator>();
        SetPromptTextVisibility(false);
    }

    void Update()
    {
        // If interaction is enabled and player presses the E key, trigger the controlled object and remove the prompt
        if (!isDisabled && isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("interact");
            SetPromptTextVisibility(false);
            TriggerControlledObject();
            isDisabled = true;
        }
    }

    // Called when some object enters the collider bounds
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the object is the player, allow interaction and enable optional prompt text
        if (!isDisabled && other.CompareTag("Player"))
        {
            SetPromptTextVisibility(true);
            isInteractable = true;
        }
    }

    // Called when some object exits the collider bounds
    private void OnTriggerExit2D(Collider2D other)
    {
        // If the object is the player, disable interaction and disable optional prompt text
        if (!isDisabled && other.CompareTag("Player"))
        {
            SetPromptTextVisibility(false);
            isInteractable = false;
        }
    }

    // Enables or disables the prompt
    private void SetPromptTextVisibility(bool value)
    {
        if (promptText != null)
        {
            promptText.SetActive(value);
        }
    }

    // Triggers the action defined on each controlled object, if there are any
    private void TriggerControlledObject()
    {
        if (controlledObjectList != null && controlledObjectList.Length != 0)
        {
            foreach(GameObject controlledObject in controlledObjectList)
            controlledObject.GetComponent<IControllableObject>().TriggerObject();
        }
    }
}
