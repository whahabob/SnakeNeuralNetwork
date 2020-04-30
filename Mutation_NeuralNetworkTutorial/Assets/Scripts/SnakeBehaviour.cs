using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Grid;

namespace Completed
{

public class SnakeBehaviour : MovingObject
{

    public GameObject tailPrefab;
    private GameInformation gameInformation;
    private Vector2 direction = Vector2.up;
    private float speed = 1f;

    private List<Transform> tail;
    public Cell[,] board;
        // Start is called before the first frame update
    protected override void Start()
    {
        gameInformation = FindObjectOfType<GameInformation>();
        tail = new List<Transform>();
        board = gameInformation.board;
        gameObject.transform.position = board[5, 5].position;
        StartCoroutine("MoveSnake");
        base.Start();
    }

    private void AddTaill()
    {
        GameObject tailPart = Instantiate(tailPrefab, tail[tail.Count - 1].position, Quaternion.identity, this.transform);
        tail.Add(tailPart.transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            direction = new Vector2(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = new Vector2(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = new Vector2(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = new Vector2(0, -1);
        }
    }

    IEnumerator MoveSnake()
    {
        for (; ; )
        {
            transform.position += new Vector3(direction.x, direction.y, 0) * speed;
            board[(int)transform.position.x, (int)transform.position.y].space = Cell.Space.snake;
            for(int i = 0; i < tail.Count -1; i++)
            {
                tail[i + 1].position = tail[i].position;
            }
            yield return new WaitForSeconds(1f);
        }
        
    }

        protected override void OnCantMove<T>(T component)
        {
            throw new System.NotImplementedException();
        }
    }
}

