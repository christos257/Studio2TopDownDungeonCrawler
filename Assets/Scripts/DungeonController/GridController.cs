using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Room room;

    [System.Serializable]

    public struct Grid
    {
        public int columns, rows;

        public float verticalOffset, horizontalOffset;
    }

    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();

    private void Awake()
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.Width - 1;
        grid.rows = room.Height - 1;
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;

        for (int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.columns; x++)
            {
                GameObject obj = Instantiate(gridTile, transform);
                obj.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontalOffset), y - (grid.rows - grid.verticalOffset));
                obj.name = "X:" + x + ", Y: " + y;
                availablePoints.Add(obj.transform.position);
                obj.SetActive(false);
            }
        }

        GetComponentInParent<ObjectRoomSpawner>().InitialiseObjectSpawning();
    }
}
