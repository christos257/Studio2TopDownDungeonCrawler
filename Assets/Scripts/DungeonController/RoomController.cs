using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;



public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}
public class RoomController : MonoBehaviour
{

    public static RoomController instance;

    string currentWorldName = "FirstLevel";


    RoomInfo currentLoadRoomData;

    Room currRoom;
    Room prevRoom;
    Room bossRoom;
    Room questRoom;

    Boss boss;
    private GameObject bossObj;
    

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRoom = new List<Room>();

    bool isLoadingRoom = false;
    bool spawnBossRoom = false;
    bool spawnQuestRoom = false;
    bool updatedRooms = false;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //LoadRoom("Start", 0, 0);
        //LoadRoom("Empty", 1, 0);
        //LoadRoom("Empty", -1, 0);
        //LoadRoom("Empty", 0, 1);
        //LoadRoom("Empty", 0, -1);
        boss = GetComponent<Boss>();
       
        //boss.bossHealthBar.SetActive(false);
    }

    void FixedUpdate()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }
        if (loadRoomQueue.Count == 0)
        {
            if (!spawnBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (spawnBossRoom && spawnQuestRoom && !updatedRooms)
            {
                foreach (Room room in loadedRoom)
                {
                    room.RemoveUnconnectedDoors();
                }
                StartCoroutine(RoomCoroutine());
                updatedRooms = true;        
            }
            if (!spawnQuestRoom)
            {
                StartCoroutine(SpawnQuestRoom());
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnBossRoom()
    {
        spawnBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            bossRoom = loadedRoom[loadedRoom.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRoom.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRoom.Remove(roomToRemove);
            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }

    IEnumerator SpawnQuestRoom()
    {
        spawnQuestRoom = true;
        yield return new WaitForSeconds(0.75f);
        if (loadRoomQueue.Count == 0)
        {
            questRoom = loadedRoom[loadedRoom.Count - 2];
            Room tempRoom = new Room(questRoom.X, questRoom.Y);
            Destroy(questRoom.gameObject);
            var roomToRemove = loadedRoom.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRoom.Remove(roomToRemove);
            LoadRoom("Quest", tempRoom.X, tempRoom.Y);
        }
    }

    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y) == true)
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);

    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {

        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(currentLoadRoomData.X * room.Width, currentLoadRoomData.Y * room.Height, 0);


            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRoom.Count == 0)
            {
                CamController.instance.currRoom = room;
            }
            loadedRoom.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRoom.Find(item => item.X == x && item.Y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRoom.Find(item => item.X == x && item.Y == y);
    }

    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[] {
            "Empty",
            "Basic"
        };
        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CamController.instance.currRoom = room;
        currRoom = room;

        StartCoroutine(RoomCoroutine());

    }

    public IEnumerator RoomCoroutine()
    {
        yield return new WaitForSeconds(0.15f);
        UpdateRooms();
        UpdateBossRoom();
    }

    public void UpdateRooms()
    {
        foreach (Room room in loadedRoom)
        {
            if (currRoom != room)
            {
                EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
                if (enemies != null)
                {
                    foreach (EnemyController enemy in enemies)
                    {
                        enemy.notInRoom = true;
                        Debug.Log("Not in room");
                    }

                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }
            }

            else
            {
                EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
                if (enemies.Length > 0)
                {
                    foreach (EnemyController enemy in enemies)
                    {
                        enemy.notInRoom = false;
                        PlayerController.roomComplete = false;
                        Debug.Log("In room");

                    }

                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(true);
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }
                if (enemies.Length <= 0)
                {
                    PlayerController.roomComplete = true;
                    PlayerController.roomsCompleted++;
                    PlayerController.roomComplete = false;
                }

            }
        }
    }

    public void UpdateBossRoom()
    {

        bossObj = GameObject.FindGameObjectWithTag("Boss");
        if (bossObj == null)
        {
            return;
        }
        if (!currRoom.name.Contains("End"))
        {
            if (bossObj.activeSelf == true)
            {
                bossObj.GetComponent<Boss>().notInRoom = true;
            }
        }
        else if (currRoom.name.Contains("End"))
        {
            if (bossObj.activeSelf == true)
            {
                bossObj.GetComponent<Boss>().notInRoom = false;
            }
        }
    }
    
}