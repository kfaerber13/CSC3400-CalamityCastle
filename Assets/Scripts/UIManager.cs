using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject exitGame;

    GameObject[] pauseObjects;
    GameObject[] gameOverScreen;

    GameObject[] activeTab;
    GameObject[] characterTab;
    GameObject[] skillsTab;
    GameObject[] objectivesTab;
    GameObject[] mapTab;

    GameObject[] armorSlots;
    GameObject[] weaponsSlots;

    GameObject[] inventorySlots;
    List<GameObject> inventoryItems;

    // Start is called before the first frame update
    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        gameOverScreen = GameObject.FindGameObjectsWithTag("GameOver");
        characterTab = GameObject.FindGameObjectsWithTag("CharacterTab");
        skillsTab = GameObject.FindGameObjectsWithTag("SkillsTab");
        objectivesTab = GameObject.FindGameObjectsWithTag("ObjectivesTab");
        mapTab = GameObject.FindGameObjectsWithTag("MapTab");
        armorSlots = GameObject.FindGameObjectsWithTag("ArmorSlots");
        weaponsSlots = GameObject.FindGameObjectsWithTag("WeaponsSlots");
        inventorySlots = GameObject.FindGameObjectsWithTag("Inventory");
        inventoryItems = new List<GameObject>();

        foreach (GameObject gameObject in gameOverScreen)
        {
            gameObject.SetActive(false);
        }

        foreach (GameObject gameObject in skillsTab)
        {
            gameObject.SetActive(false);
        }

        foreach(GameObject gameObject in objectivesTab)
        {
            gameObject.SetActive(false);
        }

        foreach(GameObject gameObject in mapTab)
        {
            gameObject.SetActive(false);
        }

        Time.timeScale = 1;
		HidePaused();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
		{
			PauseControl();
		}
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseControl()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowPaused();
        } 
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HidePaused();
        }
    }

    public void ShowPaused()
    {
        foreach(GameObject gameObject in pauseObjects)
        {
			gameObject.SetActive(true);
		}

        healthBar.SetActive(false);
        ShowCharacterTab();
        ShowArmorSlots();
    }

    public void HidePaused()
    {
        foreach(GameObject gameObject in pauseObjects)
        {
			gameObject.SetActive(false);
		}

        healthBar.SetActive(true);
    }

    public void ShowGameOver()
    {
        foreach (GameObject gameObject in gameOverScreen)
        {
            gameObject.SetActive(true);
        }
    }

    public void ShowCharacterTab()
    {
        if (activeTab != null && activeTab.Length != 0)
        {
            foreach(GameObject gameObject in activeTab)
            {
                gameObject.SetActive(false);
            }
        }
        
        foreach(GameObject gameObject in characterTab)
        {
            gameObject.SetActive(true);
            /*RenderInventory();*/
        }

        activeTab = characterTab;
    }

    public void ShowSkillsTab()
    {
        foreach(GameObject gameObject in activeTab)
        {
            gameObject.SetActive(false);
        }
        
        foreach(GameObject gameObject in skillsTab)
        {
            gameObject.SetActive(true);
        }

        activeTab = skillsTab;
    }

    public void ShowObjectivesTab()
    {
        foreach(GameObject gameObject in activeTab)
        {
            gameObject.SetActive(false);
        }
        
        foreach(GameObject gameObject in objectivesTab)
        {
            gameObject.SetActive(true);
        }

        activeTab = objectivesTab;
    }

    public void ShowMapTab()
    {
        foreach(GameObject gameObject in activeTab)
        {
            gameObject.SetActive(false);
        }
        
        foreach(GameObject gameObject in mapTab)
        {
            gameObject.SetActive(true);
        }

        activeTab = mapTab;
    }

    public void ShowArmorSlots()
    {
        foreach(GameObject gameObject in armorSlots)
        {
            gameObject.SetActive(true);
        }

        foreach(GameObject gameObject in weaponsSlots)
        {
            gameObject.SetActive(false);
        }
    }

    public void ShowWeaponsSlots()
    {
        foreach(GameObject gameObject in weaponsSlots)
        {
            gameObject.SetActive(true);
        }

        foreach(GameObject gameObject in armorSlots)
        {
            gameObject.SetActive(false);
        }
    }

    /*public void RenderInventory()
    {
        Debug.Log(inventoryItems.Count);
        // Assign each item in the inventory to a slot and render the sprite in the slot's image
        for(int i = 0; i < inventoryItems.Count; i++)
        {
            Debug.Log("rendering");
            Sprite item = inventoryItems[i].GetComponent<Sprite>();
            inventorySlots[i].GetComponent<InventorySlot>().RenderInventoryImage(item);
        }
    }*/

    public void AddItemToInventory(GameObject item) {
        {
            inventoryItems.Add(item);
            Debug.Log(inventoryItems.Count);
        }
    }

    public void RemoveItemFromInventory(GameObject item) {
        {
            inventoryItems.Remove(item);
        }
    }
}
