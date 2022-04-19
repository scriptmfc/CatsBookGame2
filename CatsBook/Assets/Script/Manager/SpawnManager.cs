using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public bool isFullFurniture = false;
    public bool isFullNPC = false;


    public GameObject Furniture;
    public GameObject NPC;
    public GameObject Agent;

    public GameObject Unit;
    public GameObject NavMesh;


    public bool spawnUnit<T>(string modelName, string attribute = "") where T : BaseUnit
    { 
        string type = typeof(T).ToString();
        

        switch (typeof(T).ToString())
        {
            case "FurnitureData":
                if (!spawnFurniture(modelName, attribute)) { return false; }
                break;

            case "CharacterData":
                if (!spawnNPC(modelName)) { return false; }
                break;
            default:
                break;
        }

        return true;

    }

    public bool spawnFurniture(string modelName, string attribute = "") 
    {
        GameObject unit = null;

        if (!GameManagerTemp.Instance.dicFurnitursCurrent.ContainsKey(modelName))
        {
            unit = Instantiate(Furniture, Unit.transform);
            List<GameObject> liGameObject = new List<GameObject>();
            liGameObject.Add(unit);
            GameManagerTemp.Instance.dicFurnitursCurrent.Add(modelName, liGameObject);

            setImage(unit, modelName, "Furniture/" + attribute + "/");
        }
        else
        {
            if (!GameManagerTemp.Instance.dicFurnitursRedundancy.ContainsKey(modelName) || GameManagerTemp.Instance.dicFurnitursRedundancy[modelName].Count == 0)
            {
                unit = Instantiate(Furniture, Unit.transform);
                List<GameObject> liGameObject = new List<GameObject>();
                liGameObject = GameManagerTemp.Instance.dicFurnitursCurrent[modelName];
                liGameObject.Add(unit);
                GameManagerTemp.Instance.dicFurnitursCurrent[modelName] =  liGameObject;

                setImage(unit, modelName, "Furniture/" + attribute + "/");

            }
            else
            {
                List<GameObject> liGameObjectCurrent = new List<GameObject>();
                List<GameObject> liGameObjectRedundancy = new List<GameObject>();

                liGameObjectCurrent = GameManagerTemp.Instance.dicFurnitursCurrent[modelName];
                liGameObjectRedundancy = GameManagerTemp.Instance.dicFurnitursRedundancy[modelName];

                for(int i=0; i<liGameObjectCurrent.Count; i++)
                {
                    if(liGameObjectCurrent[i].activeSelf == false)
                    {
                        liGameObjectCurrent[i].SetActive(true);
                        liGameObjectRedundancy.Add(liGameObjectCurrent[i]);
                    }
                }

            }

        }
        return true;
    }

    public bool spawnNPC(string modelName)
    {
        GameObject unit = null;

        if (GameManagerTemp.Instance.NPC_count < 0)
        {
            isFullNPC = true;
            return false;
        }

        GameObject agent = null;
        isFullNPC = false;

        if (!GameManagerTemp.Instance.dicNPCs.ContainsKey(modelName))
        {
            unit = Instantiate(NPC, Unit.transform);
            GameManagerTemp.Instance.dicNPCs.Add(modelName, unit);
            unit.name = modelName;

            setImage(unit, modelName, "Character/NPC/");

            agent = Instantiate(Agent, NavMesh.transform);
            GameManagerTemp.Instance.dicAgents.Add(modelName, agent);
            Npc_Move NPC_object = agent.GetComponent<Npc_Move>();
            NPC_object.npc = unit;

            agent.name = modelName;
            agent.transform.position = new Vector3(14, 0, 0);
        }
        else
        {

            unit = GameManagerTemp.Instance.dicNPCs[modelName];
            unit.SetActive(true);

            agent = GameManagerTemp.Instance.dicAgents[modelName];
            agent.SetActive(true);
            agent.transform.position = new Vector3(14, 0, 0);
        }

        return true;

    }

    public void setImage(GameObject unit, string modelName, string path)
    {
        unit.GetComponent<BaseUnit>().SetImage(modelName, path);
        GridManager.Instance.OnBeginDrag((isHold) => GridManager.Instance.dragging = isHold);
    }



}
