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
    #endregion
    private void Awake()
    {
        #region Init Snake
        endGame = false;
        segmentsSnake = new List<Transform>();
        segmentsSnake.Add(this.transform);
        if (_direction.x == 1f)
        {
            Quaternion rotationRight = Quaternion.Euler(0f, 0f, 90f);
            transform.rotation = rotationRight;
        }
        else if (_direction.x == -1f)
        {
            Quaternion rotationLeft = Quaternion.Euler(0f, 0f, 270f);
            transform.rotation = rotationLeft;
        }
        if(_direction.y == 1f)
        {
            Quaternion rotationUp = Quaternion.Euler(0f, 0f, 180f);
            transform.rotation = rotationUp;
        }
        else if( _direction.y == -1f)
        {
            Quaternion rotationDown = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = rotationDown;
        }
        #endregion
    }
    void Update()
    {
        #region Snake Movement
        if (!endGame)
        {
            if (_direction.x != 0f)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _input = Vector2.up;
                    Quaternion rotationUp = Quaternion.Euler(0f, 0f, 180f);
                    transform.rotation = rotationUp;
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _input = Vector2.down;
                    Quaternion rotationDown = Quaternion.Euler(0f, 0f, 0f);
                    transform.rotation = rotationDown;
                }
            }
            else if (_direction.y != 0f)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    _input = Vector2.left;
                    Quaternion rotationLeft = Quaternion.Euler(0f, 0f, 270f);
                    transform.rotation = rotationLeft;
                }
                else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    _input = Vector2.right;
                    Quaternion rotationRight = Quaternion.Euler(0f, 0f, 90f);
                    transform.rotation = rotationRight;
                }
            }
        }
        #endregion
    }
    private void FixedUpdate()
    {
        #region Snake Segments
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
        #endregion
    }
    private void GrowSnake()
    {
        Transform segment = Instantiate(this.snakePrefab);
        segment.position = segmentsSnake[segmentsSnake.Count-1].position;
        segmentsSnake.Add(segment);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region Collision Types
        if (collision.tag == "Food")
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
        #endregion
    }

    private void TraverseDirection(Transform wall)
    {
        #region Traverse Any Direction
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
        #endregion
    }
    public bool OccupyPosition(int x, int y)
    {
        foreach (Transform segment in segmentsSnake)
        {
            if(Mathf.RoundToInt(segment.position.x) == x && Mathf.RoundToInt(segment.position.y) == y)
            {
                return true;
            }
        }
        return false;
    }
}
