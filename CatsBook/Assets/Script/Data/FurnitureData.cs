using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Direction : int
{
    right,
    left,
}

public class FurnitureInfo
{

    public FurnitureType type { get; set; }
    public string key { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public bool isPlace = false;
}

public class FurnitureData : BaseUnit
{
    public GameObject[] ListDirectionItem = new GameObject[2];
    public FurnitureType furnitureType;
    public FurnitureInfo furnitureData = new FurnitureInfo();

    private Direction direction = Direction.right;
    public HistoricalData previous { get; private set; }
    private List<GameObject> blocks;

    private const string Unit_LAYER = "Unit";
    private const string PREVIEW_LAYER = "Preview";


    public void Rotate()
    {
        direction = (Direction)(((int)direction + 1) % 2);

        foreach (var dir in ListDirectionItem)
            dir.SetActive(false);

        ListDirectionItem[(int)direction].SetActive(true);
        var temp = width;
        width = length;
        length = temp;
    }

    public void Rotate(Direction dir)
    {

        if (Mathf.Abs(dir - direction) % 2 == 1)
        {
            var temp = width;
            width = length;
            length = temp;
        }
        direction = dir;

        foreach (var diritem in ListDirectionItem)
            diritem.SetActive(false);

        ListDirectionItem[(int)dir].SetActive(true);
    }

    public void Move(Tile tile)
    {
        gameObject.transform.position = tile.transform.position;
        origin = tile;
    }

    public void SetColor(Color color)
    {
        foreach (var dir in ListDirectionItem)
            dir.GetComponent<SpriteRenderer>().color = color;
    }

    public void Place(List<Tile> tiles)
    {
        base.tiles = tiles;
        base.tiles.ForEach(tile => tile.isBlock = true);

        foreach (var dir in ListDirectionItem)
            dir.GetComponent<Renderer>().sortingLayerName = Unit_LAYER;

        previous = new HistoricalData(origin, direction);
        //create box collider in 3D.
        Block(tiles);

        makeFurnitureData();
    }


    private void makeFurnitureData()
    {
        furnitureData.key = gameObject.name;
        furnitureData.x = gameObject.transform.position.x - 2;
        furnitureData.y = -(gameObject.transform.position.y * 2 + 4);
        furnitureData.type = furnitureType;
        furnitureData.isPlace = true;
    }

    public void Unplaced()
    {
        tiles.ForEach(tile => tile.isBlock = false);
        tiles = new List<Tile>();

        foreach (var dir in ListDirectionItem)
            dir.GetComponent<Renderer>().sortingLayerName = PREVIEW_LAYER;

        UnBlock();
    }

    private void Block(List<Tile> tiles)
    {
        blocks = new List<GameObject>();
        foreach (var tile in tiles)
        {
            var block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block.transform.SetParent(GameObject.Find("NavMesh").transform);
            block.transform.localEulerAngles = new Vector3(0, 0, 0);
            block.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            block.transform.position = new Vector3(tile.y, 0, tile.x);
            block.AddComponent<NavMeshObstacle>().carving = true;
            block.GetComponent<Renderer>().enabled = false;
            blocks.Add(block);
        }
    }

    private void UnBlock()
    {
        if (blocks != null)
        {
            blocks.ForEach(block => DestroyImmediate(block));
            blocks = null;
        }
    }
}