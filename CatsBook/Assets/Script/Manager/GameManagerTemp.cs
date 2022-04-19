using GameUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodsId
{
	Default = 0,
	Cash = 1,
	Gold = 2,
	Exp = 3,
}

public enum FunitureAttribute
{
	Default = 0,
	Basic = 1,
}

public enum FurnitureType
{
	Normal = 0,
	Table = 1,
	Shelf = 2,
}

public class GameManagerTemp : MonoBehaviour
{

	private static GameManagerTemp instance = null;
	public static GameManagerTemp Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType(typeof(GameManagerTemp)) as GameManagerTemp;
				if (instance == null)
				{
					instance = new GameObject("CGameManager", typeof(GameManagerTemp)).GetComponent<GameManagerTemp>();
				}
			}
			return instance;
		}
	}

	public ChartDataLoad dataLoad;

	public List<FurnitureData> lifurniture = null;
	
	public Dictionary<string, List<GameObject>> dicFurnitursCurrent = new Dictionary<string, List<GameObject>>(); // In Game Furniture
	public Dictionary<string, List<GameObject>> dicFurnitursRedundancy = new Dictionary<string, List<GameObject>>(); // Out Game Furniture (Bag)

	public Dictionary<string, GameObject> dicNPCs = new Dictionary<string, GameObject>();
	public Dictionary<string, GameObject> dicAgents = new Dictionary<string, GameObject>();

	public List<FurnitureChartData> liFurniture = new List<FurnitureChartData>();
	public List<FurnitureChartData> liExteriorItem = new List<FurnitureChartData>();

	public int NPC_count = 5;
	public List<string> liNPC = new List<string>();
	public Queue<string> queNPC = new Queue<string>();

	public Tile[,] tiles;
	public int floor1_width = 15;
	public int floor1_length = 15;




	// Start is called before the first frame update
	void Start()
    {
		//DontDestroyOnLoad(gameObject);
		instance = this;
		dataLoad.setDataInit();

		// 임시코드, 삭제
		// NPC Read, Spawn
		ReadNPCList();
		SpawnNPC();


		
		addListExteriorItem();

	}


	// Temp (삭제)

	public void addListExteriorItem()
    {

		FurnitureChartData furniture = new FurnitureChartData();
	}




	// end

	public void ReadNPCList() // Move Data Load Code
    {
		// To Do..

		for(int i=1; i<=6; i++)
        {
			string name = "cat" + i.ToString();
			liNPC.Add(name);
        }

		liNPC = CGameUtils.GetShuffleList(liNPC);

		for(int i=0; i < liNPC.Count; i++)
        {
			queNPC.Enqueue(liNPC[i]);
        }
	}

	public void SpawnNPC()
    {
		CTimeManager.Instance.addTimeEventListener(TIMEER_TYPE.CONTINUITY, 5,
		 (time) => {
			 if (time.Equals(0) && NPC_count > 0 && queNPC.Count > 0)
			 {
				 NPC_count--;
				 string name = queNPC.Dequeue();
				 SpawnManager.Instance.spawnUnit<CharacterData>(name);
			 }
		}, "NPC_Spawn");
	}

	public void DeSpawnNPC(string modelName)
    {
		NPC_count++;
		queNPC.Enqueue(modelName);
	}

	public void AddLiFurniture(FurnitureData furniture)
	{
		if (!(furniture.furnitureType == FurnitureType.Table))
			return;

		if(lifurniture != null)
        {
			for (int i = 0; i < lifurniture.Count; i++)
			{
				if (lifurniture[i].furnitureData.key == furniture.furnitureData.key)
                {
					lifurniture[i] = furniture;
					return;
				}
			}

			lifurniture.Add(furniture);

		}
		else
        {
			lifurniture = new List<FurnitureData>();
			lifurniture.Add(furniture);
		}

	}
}
