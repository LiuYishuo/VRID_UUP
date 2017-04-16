using UnityEngine;
using System.Collections;

public class Furniture {
    public string name;
    public Category category;
    public int xPos, yPos;
    public string resourcePath;

    // constructor
    public Furniture(string name, Category category, string resourcePath)
    {
        this.name = name;
        this.category = category;
        this.xPos = 0;
        this.yPos = 0;
        this.resourcePath = resourcePath;
    }

    public Furniture(string name, Category category)
    {
        this.name = name;
        this.category = category;
        this.xPos = 0;
        this.yPos = 0;
        this.resourcePath = "";
    }

    public Furniture(string name, Category category, int xPos, int yPos)
    {
        this.name = name;
        this.category = category;
        this.xPos = xPos;
        this.yPos = yPos;
        this.resourcePath = "";
    }
}
