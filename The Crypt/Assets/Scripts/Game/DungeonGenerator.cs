using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    private int numberOfRooms = 8;
    private Room[,] rooms;
    public GameObject cubeRoom;
    public GameObject hallway;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject teleporterPrefab;
    private GameManager gameManager;

    private int minEnemies = 2;
    private int maxEnemies = 6;

    void Start() {
        GenerateDungeon();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SetGameActive(true);
    }

    private Room GenerateDungeon() {
        int gridSize = 30 * numberOfRooms;
        rooms = new Room[gridSize, gridSize];

        Vector2Int initialRoomCoordinate = new Vector2Int(gridSize/2, gridSize/2);

        // Setup and add spots for all open adjacent spots
        Queue<Room> roomsToCreate = new Queue<Room>();
        roomsToCreate.Enqueue(new Room(initialRoomCoordinate.x, initialRoomCoordinate.y));
        List<Room> createdRooms = new List<Room>();
        while (roomsToCreate.Count > 0 && createdRooms.Count < numberOfRooms) {
            Room currentRoom = roomsToCreate.Dequeue();
            this.rooms[currentRoom.roomCoordinate.x, currentRoom.roomCoordinate.y] = currentRoom;
            createdRooms.Add(currentRoom);
            AddNeighbors(currentRoom, roomsToCreate);
        }

        // Create rooms and find furthest room for teleporter
        List<Vector2> usedCoordinates = new List<Vector2>();
        foreach (Room room in createdRooms) {
            GameObject child = Instantiate(cubeRoom, new Vector2(room.roomCoordinate.x, room.roomCoordinate.y), Quaternion.identity);
            child.transform.parent = GameObject.Find("Floor").transform;
            List<Vector2Int> neighborCoordinates = room.NeighborCoordinates();
            foreach (Vector2Int coordinate in neighborCoordinates) {
                Room neighbor = this.rooms[coordinate.x, coordinate.y];
                // Create hallways
                if (neighbor != null) {
                    // Create horizontal hallway
                    if (room.roomCoordinate.x > coordinate.x && room.roomCoordinate.y == coordinate.y) {
                        GameObject childHall = Instantiate(hallway, new Vector2(coordinate.x + 10, coordinate.y), Quaternion.identity);
                        childHall.transform.parent = GameObject.Find("Floor").transform;
                    }
                    // Create vertical hallway
                    if (room.roomCoordinate.x == coordinate.x && room.roomCoordinate.y > coordinate.y) {
                        GameObject childHall = Instantiate(hallway, new Vector2(coordinate.x, coordinate.y + 10), Quaternion.identity);
                        childHall.transform.parent = GameObject.Find("Floor").transform;
                    }
                }
            }
        }

        // Random spawn player
        int randomNumRoom = Random.Range(1, createdRooms.Count) - 1;
        Room playersRoom = createdRooms[randomNumRoom];
        PopulatePrefab(playersRoom, playerPrefab);

        // Random spawn enemies and teleporter
        int maximumDistanceToPlayersRoom = 0;
        Room finalRoom = null;
        
        foreach (Room room in createdRooms) {
            int randomNumEnemies = Random.Range(minEnemies, maxEnemies);
            for (int i = 0; i < randomNumEnemies; i++) {
                GameObject child = PopulatePrefab(room, enemyPrefab);
                child.transform.parent = GameObject.Find("Enemies").transform;
            }
            int distanceToPlayersRoom = Mathf.Abs(room.roomCoordinate.x - playersRoom.roomCoordinate.x) + Mathf.Abs(room.roomCoordinate.y - playersRoom.roomCoordinate.y);
            if (distanceToPlayersRoom > maximumDistanceToPlayersRoom) {
                maximumDistanceToPlayersRoom = distanceToPlayersRoom;
                finalRoom = room;
            }
        }

        // Random spawn teleporter in furthest room
        PopulatePrefab(finalRoom, teleporterPrefab);

        return this.rooms[initialRoomCoordinate.x, initialRoomCoordinate.y];
    }

    // Check what neighboor coordinates are available and select a random one to create
    private void AddNeighbors(Room currentRoom, Queue<Room> roomsToCreate) {
        List<Vector2Int> neighborCoordinates = currentRoom.NeighborCoordinates();
        List<Vector2Int> availableNeighbors = new List<Vector2Int>();

        // Add empty neighbors if no room is already there
        foreach (Vector2Int coordinate in neighborCoordinates) {
            if (this.rooms[coordinate.x, coordinate.y] == null && !InQueue(coordinate, roomsToCreate)) {
                availableNeighbors.Add(coordinate);
            }
        }

        // Select a random number of the rooms to add
        int numberofNeighbors = (int) Random.Range(1, availableNeighbors.Count);
        for (int neighborIndex = 0; neighborIndex < numberofNeighbors; neighborIndex++) {
            float randomNumber = Random.value;
            float roomFraction = 1f / (float) availableNeighbors.Count;
            // Randomly choose which neighbor to add
            Vector2Int chosenNeighbor;
            foreach (Vector2Int coordinate in availableNeighbors) {
                if (randomNumber < roomFraction) {
                    chosenNeighbor = coordinate;
                    roomsToCreate.Enqueue(new Room(chosenNeighbor));
                    availableNeighbors.Remove(chosenNeighbor);
                    break;
                } else {
                    roomFraction += 1f / (float) availableNeighbors.Count;
                }
            }
            
        }
    }

    // Create a prefab given the certain rooms 
    private GameObject PopulatePrefab(Room givenRoom, GameObject prefab) {
        if (prefab.CompareTag("Player")) {
            Vector2 roomCoord = new Vector2(givenRoom.roomCoordinate.x, givenRoom.roomCoordinate.y);
            return Instantiate(prefab, roomCoord, Quaternion.identity);
        } else {
            // Find an open space
            Vector2 enemyCoord = FindFreeRegion(givenRoom);
            return Instantiate(prefab, enemyCoord, Quaternion.identity);
        }
    }

    // Finds and returns a random free space in a given room 
    // Uses a hit collider to ensure objects are not inside each other
    private Vector2 FindFreeRegion(Room givenRoom) {
        Vector2 region;
        Collider2D[] hitColliders;
        LayerMask layerMask = 9;
        do {
            // The size of a room is 15x15, with a buffer gives us 6.5 in each direction
            region = new Vector2(Random.Range(givenRoom.roomCoordinate.x - 6.5f, givenRoom.roomCoordinate.x + 6.5f),
                Random.Range(givenRoom.roomCoordinate.y - 6.5f, givenRoom.roomCoordinate.y + 6.5f));
            hitColliders = Physics2D.OverlapBoxAll(region, new Vector2(1,1), 0, layerMask);
        } while(hitColliders.Length != 0);
        return region;
    }

    private bool InQueue(Vector2 coordinate, Queue<Room> roomCoordinate) {
        foreach (Room room in roomCoordinate) {
            if (room.roomCoordinate.x == coordinate.x && room.roomCoordinate.y == coordinate.y) {
                return true;
            }
        }
        return false;
    }
}
