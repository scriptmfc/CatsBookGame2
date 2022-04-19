using GameUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc_Move : MonoBehaviour
{
    private Sorter sorter;
    private Tiles tiles;

    public NavMeshAgent agent;
    public GameObject npc;
    bool isMove = false;
    bool isEnd = false;
    //List<Furniture> liFurniture;

    public class Pos
    {
        public float x { get; set; }
        public float y { get; set; }

        public Pos(float _x, float _y)
        {
            x = _x;
            y = _y;
        }
    }

    public Pos des = new Pos(0, 0);

    void Awake()
    {
        sorter = GameObject.Find("Unit").GetComponent<Sorter>();
        tiles = GameObject.Find("Tiles").GetComponent<Tiles>();

        //liFurniture = new List<Furniture>(); // Move to

    }

    void Update()
    {
        var pos = agent.gameObject.transform.position;
        npc.transform.position = new Vector2((pos.x - pos.z) * 2, -(pos.z + pos.x)); ;

        var unit = npc.GetComponent<CharacterData>();
        var tile = tiles.GetTileByPoint(npc.transform.position);
        unit.UpdateTile(tile);
        sorter.SortUnit(unit);

        npc.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = agent.gameObject.transform.eulerAngles.y < 180f;

        if (!isMove)
        {
            if(UnityEngine.Random.Range(1, 100) > 50) // To DO..
            {
                NpcMove();
            }
            else
            {
                ReturnOrigin();
            }
            
        }
        else
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        isMove = false;
                        //ToDo..
                    }
                }

                if (isEnd && (Mathf.Abs(transform.position.x) <= 1.0f && Mathf.Abs(transform.position.z) <= 1.0f)) // To Do..
                {
                    isEnd = false;
                    gameObject.SetActive(false);
                    npc.SetActive(false);

                    GameManagerTemp.Instance.DeSpawnNPC(npc.GetComponent<CharacterData>().name);

                }
            }
        }

    }

    public void NpcMove()

    {
        isMove = true;
        
        int x = UnityEngine.Random.Range(0, 14);
        int y = UnityEngine.Random.Range(0, 14);
        var position = new Vector3(y, 0, x);

        agent.SetDestination(position);

        //if(GameManagerTemp.Instance.lifurniture != null)
        //{
        //    liFurniture = GameManagerTemp.Instance.lifurniture;
        //    liFurniture = CGameUtils.GetShuffleList<Furniture>(liFurniture);
        //    Furniture furniture = liFurniture[0];
        //    Pos des = new Pos(furniture.furnitureData.x, furniture.furnitureData.y);

        //    agent.SetDestination(new Vector3(des.x, 0, des.y));

        //}
        //else
        //{
        //    isMove = false;
        //}
    }

    public void ReturnOrigin()
    {
        isMove = true;

        var position = new Vector3(0, 0, 0);
        agent.SetDestination(position);
        isEnd = true;

    }


}
