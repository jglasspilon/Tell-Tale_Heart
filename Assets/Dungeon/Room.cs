// Summary:
//
// Created by: Julian Glass-Pilon
//
// (C)2016 TwinSoft Studios

using UnityEngine;
using System.Collections;

public class Room
{
    public int xPos;
    public int yPos;
    public int roomWidth;
    public int roomLength;

    public Direction enteringCorridor;

    public void SetupRoom(IntRange widthRange, IntRange lengthRange, int columns, int rows)
    {
        roomWidth = widthRange.Random;
        roomLength = lengthRange.Random;

        //coordinates of the lower left tile of the room
        xPos = Mathf.RoundToInt(columns / 2f - roomWidth / 2f);
        yPos = Mathf.RoundToInt(rows / 2f - roomLength / 2f);
    }

    public void SetupRoom(IntRange widthRange, IntRange lengthRange, int columns, int rows, Corridor corridor)
    {
        enteringCorridor = corridor.direction;

        roomWidth = widthRange.Random;
        roomLength = lengthRange.Random;

        switch (corridor.direction)
        {
            case Direction.North:
                roomLength = Mathf.Clamp(roomLength, 1, rows - corridor.EndPositionY);
                yPos = corridor.EndPositionY;
                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;
            case Direction.East:
                roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridor.EndPositionX);
                xPos = corridor.EndPositionX;

                yPos = Random.Range(corridor.EndPositionY - roomLength + 1, corridor.EndPositionY);
                yPos = Mathf.Clamp(yPos, 0, rows - roomLength);
                break;
            case Direction.South:
                roomLength = Mathf.Clamp(roomLength, 1, corridor.EndPositionY);
                yPos = corridor.EndPositionY - roomLength + 1;

                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;
            case Direction.West:
                roomWidth = Mathf.Clamp(roomWidth, 1, corridor.EndPositionX);
                xPos = corridor.EndPositionX - roomWidth + 1;

                yPos = Random.Range(corridor.EndPositionY - roomLength + 1, corridor.EndPositionY);
                yPos = Mathf.Clamp(yPos, 0, rows - roomLength);
                break;
        }
    }

    public Vector2 GetCenterPointInRoom()
    {
        int centerX = (xPos * DungeonCreator.unitSize) + ((roomWidth - 1)/2 * DungeonCreator.unitSize);
        int centerY = (yPos * DungeonCreator.unitSize) + ((roomLength - 1)/2 * DungeonCreator.unitSize);
        return new Vector2(centerX, centerY);
    }
}
