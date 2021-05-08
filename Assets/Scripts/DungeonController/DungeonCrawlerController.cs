using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    Up = 0,

    Left = 1,

    Down = 2,
     
    Right = 3

};
public class DungeonCrawlerController : MonoBehaviour
{
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();
    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>()
    {
        {Direction.Up, Vector2Int.up},
        {Direction.Left, Vector2Int.left},
        {Direction.Down, Vector2Int.down},
        {Direction.Right, Vector2Int.right}
    };

    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData)
    {
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();

        for (int i = 0; i < dungeonData.numberOfCrawlers; i++)
        {
            dungeonCrawlers.Add(new DungeonCrawler(Vector2Int.zero));
        }

        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);
        for (int i = 0; i < iterations; i++)
        {
            foreach (DungeonCrawler dungeonCrawler in dungeonCrawlers)
            {
                Vector2Int newPos = dungeonCrawler.Move(directionMovementMap);
                positionsVisited.Add(newPos);
                
            }
            
        }
        return positionsVisited;
    }
}
