using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    #region Attributes and Variables
    private Vector2 _direction = Vector2.right;
    private List<Transform> segmentsSnake;
    public Transform snakePrefab;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        segmentsSnake = new List<Transform>();
        segmentsSnake.Add(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        #region snake movement
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        } 
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        } 
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        } 
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
        #endregion
    }
    private void FixedUpdate()
    {
        for (int i = segmentsSnake.Count - 1; i > 0; i--)
        {
            segmentsSnake[i].position = segmentsSnake[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }
    private void GrowSnake()
    {
        Transform segment = Instantiate(this.snakePrefab);
        segment.position = segmentsSnake[segmentsSnake.Count-1].position;
        segmentsSnake.Add(segment);
    }
    private void ResetGame()
    {
        for(int i = 1; i < segmentsSnake.Count; i++)
        {
            Destroy(segmentsSnake[i].gameObject);
        }
        segmentsSnake.Clear();
        segmentsSnake.Add(this.transform);
        this.transform.position = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Food")
        {
            GrowSnake();
        }
        else if (collision.tag == "Obstacle")
        {
            ResetGame();
        }
    }
}
