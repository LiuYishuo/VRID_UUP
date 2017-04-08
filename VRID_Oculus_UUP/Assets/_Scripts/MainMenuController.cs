using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {

    private Categories currentCategory = Categories.Tables;

    private Transform[] categories;
    private Transform[] contents;

    // Use this for initialization
    void Start () {
        // instantiate the private parts
        currentCategory = Categories.Tables;

        // get categories and contents
        setCategoriesContents();

        // populate the furniture
        populateObjects();
    }

    private void setCategoriesContents()
    {
        this.categories = this.getFirstChildren(this.gameObject.transform.Find("CategoryPanel"));
        this.contents = this.getFirstChildren(this.gameObject.transform.Find("FurniturePanel"));

        updateContent();
    }

    // Update is called once per frame
    void Update () {
        // this is for changing categories when user hit the bumpers
        updateCategory();
        
        // automatic highlighting of the category
        highlightCategory();
    }

    // TODO make the content change when the category changes
    private void updateContent()
    {      
        for (int i = 0; i < contents.Length; i++)
            contents[i].gameObject.SetActive(false);

        // turn on content based on category
        contents[(int) currentCategory].gameObject.SetActive(true);
    }

    private void updateCategory()
    {
        // Left Bumper
        if (Input.GetKeyUp("joystick button 4"))
        {
            int index = (((int) currentCategory - 1) % 4 + 4) % 4;
            currentCategory = (Categories) index;
            updateContent();
        }

        // Right Bumper
        if (Input.GetKeyUp("joystick button 5"))
        {
            currentCategory = (Categories)((int)(currentCategory + 1) % 4);
            updateContent();
        }

    }

    // this function should populate the furnitures of the current selected category
    private void populateObjects()
    {
        // contents
        //Debug.Log(contents[0].transform.name);
        GameObject testDummy = new GameObject("Brandon long schlong");
        Text test =  testDummy.AddComponent<Text>();
        test.text = "Brandon big schlong";
        test.font = new Font();
        RectTransform rect =  test.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(1000,500); 
        rect.position = new Vector3(0, 0, 0);
        testDummy.transform.SetParent(contents[0].gameObject.transform);
        testDummy.transform.position = new Vector3(0, 0, 0);
        Debug.Log(rect.position.x);
        Debug.Log(rect.position.y);
        Debug.Log(rect.position.z);
        //testDummy.transform.parent = contents[0].transform;
    }

    // highlight the current category
    private void highlightCategory()
    {
        for (int i = 0; i < categories.Length; i++)
            categories[i].gameObject.GetComponent<Text>().color = Color.black;

        // highlight current category
        categories[(int) currentCategory].gameObject.GetComponent<Text>().color = Color.yellow;
    }

    // This is a helper function that gets the first immediate children of a certain game component
    private Transform[] getFirstChildren(Transform parent)
    {
        Transform[] children = new Transform[parent.childCount];
        int index = 0;
        
        foreach (Transform child in parent)
        {
            children[index] = child;
            index++;
        }

        return children;
    }
}

enum Categories { Tables, Chairs, Sofas, Lamps};
