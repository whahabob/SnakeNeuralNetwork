using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int width = 20;
    public int height = 20;
    public Cell[,] board;
    [SerializeField]
    private GameObject[] prefabs;
    // Start is called before the first frame update
    void Awake()
    {
        board = new Cell[width,height];
        //Canvas canvas = FindObjectOfType<Canvas>();
        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Cell cell;
                if (y == 0 || x == 0 || y == height - 1 || x == width - 1)
                {
                    cell = new Cell(new Vector2(x, y), Cell.Space.wall, prefabs);
                    Instantiate(prefabs[0], new Vector3(x, y, 0), Quaternion.identity);
                }
                else
                {
                    cell = new Cell(new Vector2(x, y), Cell.Space.empty, prefabs);
                }
                board[x,y] = cell;
            }
        }
        
    }
    
    public class Cell
    {
        public enum Space {empty, apple, wall, snake };
        public Space space;
        public Vector2 position;
        GameObject[] prefabs;
        
        public Cell(Vector2 position, Space space, GameObject[] prefabs)
        {
            this.position = position;
            this.space = space;
            this.prefabs = prefabs;
            //VisualizeCell();
        }

        //private void VisualizeCell()
        //{
        //    if(space == Space.empty)
        //    {
               
        //    }
        //    if (space == Space.wall)
        //    {
                
        //    }
        //}

    }
}
