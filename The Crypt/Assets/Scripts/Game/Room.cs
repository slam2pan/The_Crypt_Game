using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2Int roomCoordinate;

    public Room (int xCoordinate, int yCoordinate) {
        this.roomCoordinate = new Vector2Int(xCoordinate, yCoordinate);
    }

    public Room (Vector2Int roomCoordinate) {
        this.roomCoordinate = roomCoordinate;
    }

    // Show all possible directions of adjacent rooms
    public List<Vector2Int> NeighborCoordinates() {
        List<Vector2Int> neighborCoordinates = new List<Vector2Int>();
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y - 20));
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x + 20, this.roomCoordinate.y));
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y + 20));
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x - 20, this.roomCoordinate.y));

        return neighborCoordinates;
    }

}
