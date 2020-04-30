using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Grid;


public class SnakeBehaviour : MonoBehaviour
{


    public int score = 0;
    public GameObject tailPrefab;
    private GameInformation gameInformation;
    private Vector2 direction = Vector2.up;
    private float speed = 1f;
    public GameObject foodPrefab;
    public int[] input;

    private List<Transform> tail = new List<Transform>();
    public Cell[,] board;
    // Start is called before the first frame update
    void Start()
    {
        input = new int[6];
        gameInformation = FindObjectOfType<GameInformation>();
        tail = new List<Transform>();
        board = gameInformation.board;
        gameObject.transform.position = board[5, 5].position;
        // Move the Snake every 300ms
        InvokeRepeating("Move", 0.7f, 0.7f);
        AddTaill();
        SpawnNewApple();
            
    }

    private void AddTaill()
    {
        Debug.Log("Added tail");
        Vector2 v = transform.position;
        // Load Prefab into the world
        GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
        board[(int)v.x, (int)v.y].space = Cell.Space.snake;
        // Keep track of it in our tail list
        tail.Insert(0, g.transform);

        // Reset the flag
        //ate = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = transform.position;
            
        Vector2 lookingDirection = v - (Vector2)tail[0].position;
        Vector2 lookingDirectionLeft = v - (Vector2)tail[0].position + Vector2.left;
        Debug.Log(lookingDirectionLeft);

        if (Input.GetKey(KeyCode.A))
        {
            direction = -Vector2.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction = Vector2.up;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction = -Vector2.up;
        }

        //StraightAhead(v, lookingDirection,Cell.Space.wall);
        //StraightAhead(v, lookingDirection, Cell.Space.apple);
        straightAhead = StraightAhead2(v, lookingDirection, Cell.Space.wall);
        appleAhead = StraightAhead2(v, lookingDirection, Cell.Space.apple);

        sideClear = SidesClear(v, lookingDirection);
        //Debug.Log(straightAhead + " : " +sideClear);
       // DebugInputs();
           
    }

    bool sideClear = false;
    bool appleAhead = false;

    private bool SidesClear(Vector2 position, Vector2 lookingDirection)
    {
        return StraightAhead2(position, lookingDirection, Cell.Space.wall);

    }

    private void DebugInputs()
    {
        Debug.Log("(0:" + input[0] + ") (1:" + input[1] + ") (2:" + input[2] + ") (3:" + input[3] + ") (4:" + input[4] + ") (5:" + input[5]+ ")");
    }

    bool straightAhead = false;
    private void StraightAhead(Vector2 position, Vector2 lookingDirection, Cell.Space space)
    {
        Vector2 v = position + lookingDirection;
       
        if (v.x < board.GetLength(0) || v.x > 0 || v.y < board.GetLength(1)|| v.y > 0)
        {
             
            if (board[(int)(v.x), (int)(v.y)].space == space)
            {
                switch(space)
                {
                    case Cell.Space.wall: 
                        input[0] = 1;
                        break;
                    case Cell.Space.apple:
                        input[3] = 1;
                        break;
                    default:
                    break;
                }
            }
            else
            {
                switch (space)
                {
                    case Cell.Space.wall:
                        input[0] = 0;
                        break;
                    case Cell.Space.apple:
                        input[3] = 0;
                        break;
                    default:
                        break;
                }
            }
            if (board[(int)(v.x), (int)(v.y)].space == Cell.Space.empty)
            {
                StraightAhead(v, lookingDirection, space);
            }
        } 
    }
       
    private bool StraightAhead2(Vector2 position, Vector2 lookingDirection, Cell.Space space)
    {
        Vector2 v = position + lookingDirection;
        Debug.Log(v);
        if (v.x < board.GetLength(0) || v.x > 0 || v.y < board.GetLength(1) || v.y > 0)
        {
            if (board[(int)(v.x), (int)(v.y)].space == Cell.Space.empty)
            {
               return StraightAhead2(v, lookingDirection, space);
            }
            else if(board[(int)(v.x), (int)(v.y)].space == space)
            {
                return true;
            }
                return false;   
        }
        else
        {
            return false;
        }
    }
        
    void Move()
    {
        // Save current position (gap will be here)
        Vector2 v = transform.position;
        Vector2 newPosition = v + new Vector2(direction.x, direction.y) * speed;
        if (board[(int)newPosition.x, (int)newPosition.y].space != Cell.Space.empty && board[(int)newPosition.x, (int)newPosition.y].space != Cell.Space.apple)
        {
            Debug.Log(board[(int)newPosition.x, (int)newPosition.y].space);
            GameOver();
        }
        else
        {
            transform.Translate(direction);
            if (board[(int)newPosition.x, (int)newPosition.y].space == Cell.Space.apple)
            {
                    score += 10;
                    AddTaill();
                    SpawnNewApple();
                    board[(int)newPosition.x, (int)newPosition.y].space = Cell.Space.empty;
            }
            // Do we have a Tail?
            if (tail.Count > 0)
            {
                // Move last Tail Element to where the Head was
                tail[tail.Count - 1].position = v;

                // Add to front of list, remove from the back
                tail.Insert(0, tail[tail.Count - 1]);
                tail.RemoveAt(tail.Count - 1);
                foreach(Transform tailPart in tail)
                {
                    board[(int)tailPart.position.x, (int)tailPart.position.y].space = Cell.Space.snake;
                }
                board[(int)tail[tail.Count-1].position.x, (int)tail[tail.Count - 1].position.y].space = Cell.Space.empty;
            }
        }
    }

    private Vector2 positionApple = Vector2.zero;
    private void SpawnNewApple()
    {

        // x position between left & right border
        int x = (int)UnityEngine.Random.Range(1,gameInformation.grid.width - 1);

        // y position between top & bottom border
        int y = (int)UnityEngine.Random.Range(1, gameInformation.grid.height - 1);
            
        if(board[x,y].space != Cell.Space.empty)
        {
            SpawnNewApple();
        }
        else
        {
            board[x, y].space = Cell.Space.apple;
            positionApple = new Vector2(x, y);
            Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation
        }
    }

    private void GameOver()
    {
        CancelInvoke();
        // StopCoroutine("MoveSnake");
        score -= 100;
        Debug.Log("GameOverLoser");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Food")
        {
            collision.gameObject.SetActive(false);
        }
    }

       
}


