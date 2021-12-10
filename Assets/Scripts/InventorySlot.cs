using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IControllableObject
{
    public GameObject itemImage;

    // Start is called before the first frame update
    void Start()
    {
        HideInventoryImage();
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    public void RenderInventoryImage()
    {
        itemImage.SetActive(true);
    }

    public void HideInventoryImage()
    {
        itemImage.SetActive(false);
    }

    public void TriggerObject()
    {
        if (!itemImage.activeSelf)
        {
            RenderInventoryImage();
        } 
        else
        {
            HideInventoryImage();
        }
    }
}
