using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class MainMenuController : MonoBehaviour {

    private Category currentCategory = Category.Beds;

    private Transform[] categories;
    private Transform[] contents;

    // this is for the highlighter/cursor/selector
    private int selector_x = 0;
    private int selector_y = 0;

    // Use this for initialization
    void Start () {
        // instantiate the private parts
        currentCategory = Category.Beds;

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

        // highlight the current selection depicted by the selector
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
            currentCategory = (Category) index;
            updateContent();
        }

        // Right Bumper
        if (Input.GetKeyUp("joystick button 5"))
        {
            currentCategory = (Category)((int)(currentCategory + 1) % 4);
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