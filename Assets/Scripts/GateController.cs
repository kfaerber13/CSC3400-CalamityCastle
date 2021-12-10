using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour, IControllableObject
{
    public int speed = 1;
    public int disableAfter = 5;

    private Transform gate;
    private bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        gate = GetComponent<Transform>();
    }

    void Update()
    {
        // While triggered, move gate upwards
        if (isTriggered)
        {
            gate.Translate(speed * Time.deltaTime * Vector2.up);
        }
    }

    public void TriggerObject()
    {
        gate.GetComponent<BoxCollider2D>().enabled = false;
        isTriggered = true;
        // Disable after disableAfter seconds (once gate is off the screen)
        Invoke(nameof(DisableObject), disableAfter);
    }

    private void DisableObject()
    {
        isTriggered = false;
        this.enabled = false;
    }
}
