using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static void SpawnObjects(List<Room> createdRooms) 
    {
        // Random spawn player
        int randomNumRoom = Random.Range(1, createdRooms.Count) - 1;
        Room playersRoom = createdRooms[randomNumRoom];
        PopulatePrefab(playersRoom, GameAssets.i.playerPrefab);

        // Random spawn enemies and teleporter
        int maximumDistanceToPlayersRoom = 0;
        Room finalRoom = null;
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        foreach (Room room in createdRooms)
        {
            int randomNumEnemies = Random.Range(gameManager.minEnemies, gameManager.maxEnemies);
            for (int i = 0; i < randomNumEnemies; i++)
            {
                int enemyPicker = Random.Range(0, 100);
                GameObject child;
                if (enemyPicker < gameManager.diamondSpawnChance)
                {
                    child = PopulatePrefab(room, gameManager.allEnemies[1]);
                } else 
                {
                    child = PopulatePrefab(room, gameManager.allEnemies[0]);
                }
                child.transform.parent = GameObject.Find("Enemies").transform;
            }
            int distanceToPlayersRoom = Mathf.Abs(room.roomCoordinate.x - playersRoom.roomCoordinate.x) + Mathf.Abs(room.roomCoordinate.y - playersRoom.roomCoordinate.y);
            if (distanceToPlayersRoom > maximumDistanceToPlayersRoom)
            {
                maximumDistanceToPlayersRoom = distanceToPlayersRoom;
                finalRoom = room;
            }
        }

        // Random spawn teleporter in furthest room
        PopulatePrefab(finalRoom, GameAssets.i.teleporterPrefab);
    }

    // Create a prefab given the certain rooms 
    private static GameObject PopulatePrefab(Room givenRoom, GameObject prefab)
    {
        if (prefab.CompareTag("Player"))
        {
            Vector2 roomCoord = new Vector2(givenRoom.roomCoordinate.x, givenRoom.roomCoordinate.y);
            return Instantiate(prefab, roomCoord, Quaternion.identity);
        }
        else
        {
            // Find an open space
            Vector2 enemyCoord = FindFreeRegion(givenRoom);
            return Instantiate(prefab, enemyCoord, Quaternion.identity);
        }
    }

    // Finds and returns a random free space in a given room 
    // Uses a hit collider to ensure objects are not inside each other
    private static Vector2 FindFreeRegion(Room givenRoom)
    {
        Vector2 region;
        Collider2D[] hitColliders;
        LayerMask layerMask = 9;
        do
        {
            // The size of a room is 15x15, with a buffer gives us 6.5 in each direction
            region = new Vector2(Random.Range(givenRoom.roomCoordinate.x - 6.5f, givenRoom.roomCoordinate.x + 6.5f),
                Random.Range(givenRoom.roomCoordinate.y - 6.5f, givenRoom.roomCoordinate.y + 6.5f));
            hitColliders = Physics2D.OverlapBoxAll(region, new Vector2(1, 1), 0, layerMask);
        } while (hitColliders.Length != 0);
        return region;
    }
    
}
