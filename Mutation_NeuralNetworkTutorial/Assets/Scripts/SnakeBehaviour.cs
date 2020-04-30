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

        private List<Transform> tail = new List<Transform>();
        public Cell[,] board;
        // Start is called before the first frame update
        protected override void Start()
        {
            gameInformation = FindObjectOfType<GameInformation>();
            tail = new List<Transform>();
            board = gameInformation.board;
            gameObject.transform.position = board[5, 5].position;
            // Move the Snake every 300ms
            InvokeRepeating("Move", 1.0f, 1.0f);
            base.Start();
        }

        private void AddTaill()
        {
            Debug.Log("Added tail");
            Vector2 v = transform.position;
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            //ate = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                direction = -Vector2.right;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                direction = Vector2.up;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                direction = Vector2.right;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                direction = -Vector2.up;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddTaill();
            }
        }

        void Move()
        {
            // Save current position (gap will be here)
            Vector2 v = transform.position;

            // Move head into new direction (now there is a gap)
            transform.Translate(direction);

            // Do we have a Tail?
            if (tail.Count > 0)
            {
                // Move last Tail Element to where the Head was
                tail[tail.Count -1].position = v;

                // Add to front of list, remove from the back
                tail.Insert(0, tail[tail.Count -1]);
                tail.RemoveAt(tail.Count - 1);
            }
        }

        protected override void OnCantMove<T>(T component)
        {
            throw new System.NotImplementedException();
        }
    }
}

