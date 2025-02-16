using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManagerScript : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public List<Sprite> spritesFood;
    private SnakeScript snakeScript;
    private void Awake()
    {
        snakeScript = FindObjectOfType<SnakeScript>();
    }
    void Start()
    {
        InstantiateFoodRandom();
    }
    public void InstantiateFoodRandom()
    {
        #region Random Instantiate Food in the grid area
        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        int xInt = Mathf.RoundToInt(x);
        int yInt = Mathf.RoundToInt(y);
        while (snakeScript.OccupyPosition(xInt, yInt))
        {
            xInt++;
            if (xInt > bounds.max.x)
            {
                xInt = Mathf.RoundToInt(bounds.min.x);
                yInt++;
                if(yInt > bounds.max.y)
                {
                    yInt = Mathf.RoundToInt(bounds.min.y);
                }
            }
        }
        this.transform.position = new Vector3(xInt, yInt, 0);
        GetComponent<SpriteRenderer>().sprite = spritesFood[Random.Range(0, spritesFood.Count)];
        #endregion
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            InstantiateFoodRandom();
        }
    }
}
