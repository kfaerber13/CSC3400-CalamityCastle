using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, IControllableObject
{
    public int disableAfter = 2;
    public TrailRenderer trail;
    public UIManager uiManager;

    private Transform item;

    // Start is called before the first frame update
    void Start()
    {
        item = GetComponent<Transform>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            item.Translate(Vector2.up * Time.deltaTime);
        }
    }

    public void TriggerObject()
    {
        gameObject.SetActive(true);
        // trail.emitting = true;
        // particles.Play();
        Invoke(nameof(DisableObject), disableAfter);
    }

    private void DisableObject()
    {
        /*uiManager.AddItemToInventory(gameObject);*/
        gameObject.SetActive(false);
        this.enabled = false;
    }
}
