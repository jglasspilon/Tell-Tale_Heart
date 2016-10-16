// Summary:
//
// Created by: Julian Glass-Pilon
//

using UnityEngine;
using System.Collections;

public class DungeonCreator : MonoBehaviour 
{
	#region Fields
	//Constant/Static Fields

	//Public Fields
    public enum TileType
    {
        Wall, Floor,
    }
    public GameObject player;
    public GameObject heart;
    private GameObject heartInstance;
    public static int unitSize = 2;
    public int columns = 100;
    public int rows = 100;

    public IntRange numRooms = new IntRange(15, 20);
    public IntRange roomWidth = new IntRange(15, 20);
    public IntRange roomLength = new IntRange(15, 20);
    public IntRange corridorLength = new IntRange(15, 20);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;

    private TileType[][] tiles;
    private Room[] rooms;
    private Corridor[] corridors;
    private GameObject boardHolder;

    //Protected Fields

    //Private Fields

    #endregion


    #region Unity Functions

    private void Start()
    {
        GenerateDungeon();
    }
    #endregion

    #region Public Functions
    public void GenerateDungeon()
    {
        boardHolder = new GameObject("Dungeon");

        SetupTilesArray();

        CreateRoomsAndCorridors();

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();

        InstantiateTiles();
    }

    public void ReGenerateDungeon()
    {
        Destroy(boardHolder.gameObject);
        Destroy(heartInstance.gameObject);
        GenerateDungeon();
    }

    void SetupTilesArray()
    {
        tiles = new TileType[columns][];

        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[rows];
        }
    }

    void CreateRoomsAndCorridors()
    {
        rooms = new Room[numRooms.Random];
        corridors = new Corridor[rooms.Length - 1];

        rooms[0] = new Room();
        corridors[0] = new Corridor();

        rooms[0].SetupRoom(roomWidth, roomLength, columns, rows);
        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomLength, columns, rows, true);
        
        for(int i = 1; i < rooms.Length; i++)
        {
            rooms[i] = new Room();
            rooms[i].SetupRoom(roomWidth, roomLength, columns, rows, corridors[i - 1]);

            if(i < corridors.Length)
            {
                corridors[i] = new Corridor();
                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomLength, columns, rows, false);
            }
        }

        //set the player to the center of the first room
        player.transform.position = new Vector3(rooms[0].GetCenterPointInRoom().x, player.transform.position.y, rooms[0].GetCenterPointInRoom().y);

        //set the player direction based on the first corridor direction
        switch (corridors[0].direction)
        {
            case Direction.North:
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.East:
                player.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case Direction.South:
                player.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case Direction.West:
                player.transform.rotation = Quaternion.Euler(0, 180, 270);
                break;
        }

        //set the location of the heart to the last room
        Vector3 heartPosition = new Vector3(rooms[rooms.Length - 1].GetCenterPointInRoom().x, heart.transform.position.y, rooms[rooms.Length - 1].GetCenterPointInRoom().y);
        heartInstance = Instantiate(heart, heartPosition, Quaternion.identity) as GameObject;
    }

    void SetTilesValuesForRooms()
    {
        for(int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];

            for(int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;

                for(int k = 0; k < currentRoom.roomLength; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }

    void SetTilesValuesForCorridors()
    {
        for(int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];

            for(int j = 0; j < currentCorridor.corridorLength; j++)
            {
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                switch (currentCorridor.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }

    void InstantiateTiles()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            for(int j = 0; j < tiles[i].Length; j++)
            {
                if(tiles[i][j] == TileType.Floor)
                {
                    InstantiateFromArray(floorTiles, i, j);
                }

                if(tiles[i][j] == TileType.Wall)
                {
                    InstantiateFromArray(wallTiles, i, j);
                }
            }
        }
    }

    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        int randomIndex = Random.Range(0, prefabs.Length);
        Vector3 position = new Vector3(xCoord * unitSize, prefabs[randomIndex].transform.position.y, yCoord * unitSize);
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;
        tileInstance.transform.parent = boardHolder.transform;
    }
    #endregion
}
