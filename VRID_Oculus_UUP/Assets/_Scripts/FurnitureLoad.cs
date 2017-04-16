using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FurnitureLoad : MonoBehaviour {

    // Dictionary mapping Asset (Furniture) name to the object itself. Assumes Asset names are unique, but keys can be changed to [category] + '/' + [name]'
    private Dictionary<string, GameObject> Furnitures = new Dictionary<string, GameObject>();

    // Declare GameObject for our furniture
    private GameObject furnitureContainer;

    // Use this for initialization
    void Start () {

        // Test variables. Change as necessary
        string cat = "bed";
        string test = "Anes_Double__Bed-3D";
        string furnitureContainerName = "FurnitureCube";

        // Assign furniture container so we can append our furniture clones to it
        furnitureContainer = GameObject.Find(furnitureContainerName);

        SpawnFurniture(cat, test);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}


    public void SpawnFurniture(string category, string name)
    {
        // Check if the furniture itself is in the dictionary
        if (!Furnitures.ContainsKey(name))
        {
            // Create path to the Asset, starting from the '/Resources' folder
            string path = category + "/" + name;

            // Load the object 
            GameObject furniture = Resources.Load<GameObject>(path);
            if (furniture == null)
            {
                Debug.Log("Failed to load furniture: " + name);
                return;
            }

            // Pre-processing to set default position and scaling
            furniture.transform.position = furnitureContainer.transform.position + new Vector3(-100, -900, 0);
            furniture.transform.localScale = new Vector3(0.5F, 0.5F, 0.5F);

            Debug.Log("Successfully loaded furniture: " + name);
            Furnitures.Add(name, furniture);
        }

        // Instantiate the furniture in the scene but keep return value for further manipulation
        GameObject insF = GameObject.Instantiate(Furnitures[name]);

        // Assign parent object for tidiness
        insF.transform.parent = furnitureContainer.transform;
        
        // Set instantiated furniture and children to 'Active'
        insF.SetActive(true);
        foreach (Transform child in insF.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}

