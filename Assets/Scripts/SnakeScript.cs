using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    #region Attributes and Variables
    public Vector2 _direction;
    private Vector2 _input;
    private List<Transform> segmentsSnake;
    public Transform snakePrefab;
    private bool endGame;
    //private GameManagerScript gameManager;
    //private SnakeScript snakeScript;
    #endregion
    private void Awake()
    {
        endGame = false;
        segmentsSnake = new List<Transform>();
        segmentsSnake.Add(this.transform);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region snake movement
        if (!endGame)
        {
            if (_direction.x != 0f)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _input = Vector2.up;
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _input = Vector2.down;
                }
            }
            else if (_direction.y != 0f)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    _input = Vector2.left;
                }
                else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    _input = Vector2.right;
                }
            }
        }
        #endregion
    }
    private void FixedUpdate()
    {
        if (!endGame)
        {
            if (_input != Vector2.zero)
            {
                _direction = _input;
            }
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
    }
    private void GrowSnake()
    {
        Transform segment = Instantiate(this.snakePrefab);
        segment.position = segmentsSnake[segmentsSnake.Count-1].position;
        segmentsSnake.Add(segment);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Food")
        {
            GameManagerScript.instance.IncreaseScore();
            GrowSnake();
        }
        else if (collision.tag == "Obstacle")
        {
            GameManagerScript.instance.GameOver();
            endGame = true;
        }
        else if(collision.tag == "Wall")
        {
            TraverseDirection(collision.transform);
        }
    }

    private void TraverseDirection(Transform wall)
    {
        Vector3 position = transform.position;
        if (_direction.x != 0f)
        {
            position.x = Mathf.RoundToInt(-wall.position.x+_direction.x);
        }
        else if(_direction.y != 0f)
        {
            position.y = Mathf.RoundToInt(-wall.position.y+_direction.y);
        }
        transform.position = position;
    }
}
