using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {

    private Categories currentCategory = Categories.Tables;

    // Use this for initialization
    void Start () {
        // instantiate the private parts
        currentCategory = Categories.Tables;

        // populate the furniture
        populateObjects();
	}
	
	// Update is called once per frame
	void Update () {
        // A Button
        //if (OVRInput.GetUp(OVRInput.Button.One))
        //{
        //    highlightCategory();
        //}

        // this is for changing categories when user hit the bumpers
        updateCategory();

        // automatic highlighting of the category
        highlightCategory();
    }

    // TODO make the content change when the category changes
    private void updateCategory()
    {
        // Left Bumper
        if (Input.GetKeyUp("joystick button 4"))
        {
            int index = (((int) currentCategory - 1) % 4 + 4) % 4;
            currentCategory = (Categories) index;
            Debug.Log((int)currentCategory);
        }

        // Right Bumper
        if (Input.GetKeyUp("joystick button 5"))
        {
            currentCategory = (Categories)((int)(currentCategory + 1) % 4);
            Debug.Log((int)currentCategory);
        }
    }

    // this function should populate the furnitures of the current selected category
    private void populateObjects()
    {

    }

    // highlight the current category
    private void highlightCategory()
    {
        Transform[] children = this.gameObject.transform.GetChild(0).GetComponentsInChildren<Transform>();

        //Debug.Log(children.Length);

        //foreach (Transform t in children)
        //{
        //    Debug.Log(t.name);
        //}

        for (int i = 1; i < 5; i++)
        {
            children[i].gameObject.GetComponent<Text>().color = Color.black;
        }

        switch (currentCategory)
        {
            case Categories.Tables:
                children[1].gameObject.GetComponent<Text>().color = Color.yellow;
                break;
            case Categories.Chairs:
                children[2].gameObject.GetComponent<Text>().color = Color.yellow;
                break;
            case Categories.Sofas:
                children[3].gameObject.GetComponent<Text>().color = Color.yellow;
                break;
            case Categories.Lamps:
                children[4].gameObject.GetComponent<Text>().color = Color.yellow;
                break;
            default:
                break;
        }
    }
}

enum Categories { Tables, Chairs, Sofas, Lamps};
