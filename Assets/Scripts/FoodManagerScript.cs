using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManagerScript : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public List<Sprite> spritesFood;
    // Start is called before the first frame update
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
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
        GetComponent<SpriteRenderer>().sprite = spritesFood[Random.Range(0, spritesFood.Count)];
        #endregion
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            InstantiateFoodRandom();
        }
    }
}
