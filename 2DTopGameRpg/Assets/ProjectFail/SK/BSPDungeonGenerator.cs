using System.Collections.Generic;
using UnityEngine;

public class BSPDungeonGenerator : MonoBehaviour
{
    public int width = 64;
    public int height = 64;
    public int minRoomSize = 6;
    public GameObject floorPrefab;
    public GameObject wallPrefab;

    private List<Room> rooms;

    void Start()
    {
        rooms = new List<Room>();
        Leaf rootLeaf = new Leaf(0, 0, width, height);
        rootLeaf.Split(minRoomSize);
        rootLeaf.CreateRooms(rooms);

        DrawDungeon();
    }

    void DrawDungeon()
    {
        foreach (Room room in rooms)
        {
            for (int x = room.x; x < room.x + room.width; x++)
            {
                for (int y = room.y; y < room.y + room.height; y++)
                {
                    Instantiate(floorPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
                }
            }

            // 벽 만들기
            for (int x = room.x - 1; x <= room.x + room.width; x++)
            {
                for (int y = room.y - 1; y <= room.y + room.height; y++)
                {
                    if (x < room.x || x >= room.x + room.width || y < room.y || y >= room.y + room.height)
                    {
                        Instantiate(wallPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
                    }
                }
            }
        }
    }
}

class Leaf
{
    public int x, y, width, height;
    public Leaf left, right;
    private Room room;

    public Leaf(int x, int y, int width, int height)
    {
        this.x = x; this.y = y; this.width = width; this.height = height;
    }

    public bool Split(int minSize)
    {
        if (left != null || right != null) return false;

        bool splitH = Random.value > 0.5f;
        if (width > height && width / height >= 1.25f) splitH = false;
        else if (height > width && height / width >= 1.25f) splitH = true;

        int max = (splitH ? height : width) - minSize;
        if (max <= minSize) return false;

        int split = Random.Range(minSize, max);
        if (splitH)
        {
            left = new Leaf(x, y, width, split);
            right = new Leaf(x, y + split, width, height - split);
        }
        else
        {
            left = new Leaf(x, y, split, height);
            right = new Leaf(x + split, y, width - split, height);
        }

        left.Split(minSize);
        right.Split(minSize);
        return true;
    }

    public void CreateRooms(List<Room> roomList)
    {
        if (left != null || right != null)
        {
            left?.CreateRooms(roomList);
            right?.CreateRooms(roomList);
        }
        else
        {
            int roomWidth = Random.Range(width / 2, width - 2);
            int roomHeight = Random.Range(height / 2, height - 2);
            int roomX = x + Random.Range(1, width - roomWidth - 1);
            int roomY = y + Random.Range(1, height - roomHeight - 1);
            room = new Room(roomX, roomY, roomWidth, roomHeight);
            roomList.Add(room);
        }
    }
}

class Room
{
    public int x, y, width, height;

    public Room(int x, int y, int width, int height)
    {
        this.x = x; this.y = y; this.width = width; this.height = height;
    }
}
