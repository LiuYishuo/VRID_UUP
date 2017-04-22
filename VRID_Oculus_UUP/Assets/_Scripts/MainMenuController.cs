using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class MainMenuController : MonoBehaviour {

    private Category currentCategory = Category.Beds;

    private Transform[] categories;
    private Transform[] contents;
    private Transform selector;

    // this is for the highlighter/cursor/selector
    private int selector_x = 0;
    private int selector_y = 0;

    // this is the dimension of the grid
    private int GRID_X = 5;
    private int GRID_Y = 4;

    // Use this for initialization
    void Start() {
        // instantiate the private parts
        currentCategory = Category.Beds;

        // get categories and contents
        setCategoriesContents();

        // populate the furniture
        populateObjects();

        // get the selector
        setSelector();
    }

    private void setSelector()
    {
        this.selector = this.gameObject.transform.Find("Selector");
        
        highlightSelection();
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
        
        // update the location of the selector
        updateSelection();

        // when user select a specific furniture inside the grid
        selectFurniture();
    }

    // TODO When the user pressed A. find the current object selected. and render the object
    private void selectFurniture()
    {
        // TODO. still not working
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            Transform found = null;

            int x = selector_x * 200 - 400;
            int y = selector_y * -100 + 150;
            Vector3 position = new Vector3(x, y - 50, 0);

            Transform[] furnitureTiles = getFirstChildren(contents[(int)currentCategory].gameObject.transform);
            foreach (Transform item in furnitureTiles)
                if (item.GetComponent<RectTransform>().transform.localPosition == position)
            {
                //if (item.GetComponent<RectTransform>().transform.localPosition == position)
                if (item.localPosition == position)
                {
                    Debug.Log("found!");
                    found = item;
                    break;
                }
            }

            Debug.Log(found);
        }
    }

    // This updates the coordinate of the selected item based on user's dpad
    private void updateSelection()
    {
        if (OVRInput.GetUp(OVRInput.Button.DpadUp) && selector_y >= 1)
        {
            selector_y--;

            highlightSelection();
        }

        if (OVRInput.GetUp(OVRInput.Button.DpadDown) && selector_y < (GRID_Y - 1))
        {
            selector_y++;

            highlightSelection();
        }

        if (OVRInput.GetUp(OVRInput.Button.DpadLeft) && selector_x >= 1)
        {
            selector_x--;

            highlightSelection();
        }

        if (OVRInput.GetUp(OVRInput.Button.DpadRight) && selector_x < (GRID_X - 1))
        {
            selector_x++;

            highlightSelection();
        }
    }

    // This updates the models based on the coordinate of the selectors
    private void highlightSelection()
    {
        // move the location of the selector
        int x = selector_x * 200 - 400;
        int y = selector_y * -100 + 150;

        this.selector.localPosition = new Vector3(x, y-50, 0);
    }

    // make the content change when the category changes
    private void updateContent()
    {      
        for (int i = 0; i < contents.Length; i++)
            contents[i].gameObject.SetActive(false);

        // turn on content based on category
        contents[(int) currentCategory].gameObject.SetActive(true);
    }

    private void updateCategory()
    {
        int enumLength = Category.GetNames(typeof(Category)).Length;

        // Left Bumper
        if (Input.GetKeyUp("joystick button 4"))
        {
            int index = (((int) currentCategory - 1) % enumLength + enumLength) % enumLength;
            currentCategory = (Category) index;
            updateContent();
        }

        // Right Bumper
        if (Input.GetKeyUp("joystick button 5"))
        {
            currentCategory = (Category)((int)(currentCategory + 1) % enumLength);
            updateContent();
        }
    }

    // this function should populate the furnitures of the current selected category
    private void populateObjects()
    {
        // contents

        // this is a test for dynamically creating an object inside the content panel
        //GameObject testDummy = new GameObject("Brandon long schlong");
        //Text test =  testDummy.AddComponent<Text>();
        //test.text = "Brandon is gay";
        //Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        //test.font = ArialFont;
        //test.material = ArialFont.material;
        //testDummy.GetComponent<RectTransform>().transform.localPosition = new Vector3(0,-50,800);
        //testDummy.transform.SetParent(contents[0].gameObject.transform);

        // TODO this is a test for loading the items
        //GameObject furniture = Resources.Load<GameObject>("bed/obsolete/Anes_Double__Bed-3D");
        //Debug.Log(furniture);
        //Texture2D preview = AssetPreview.GetAssetPreview(furniture);
        //Debug.Log(preview);
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