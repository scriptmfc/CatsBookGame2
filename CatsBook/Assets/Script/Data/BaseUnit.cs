using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BaseUnit : MonoBehaviour
{
    public int width = 1;
    public int length = 1;
    public string name;

    private int sortedOrder;

    [SerializeField] private GameObject right;
    [SerializeField] private GameObject left;

    public void SetImage(string _name, string _path)
    {
        name = _name;
        string where = _path + _name;

        right.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(where);
        left.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(where);
    }


    public int order
    {
        get { return sortedOrder; }
        set
        {
            sortedOrder = value;
            foreach (Transform child in transform)
            {
                child.GetComponent<SortingGroup>().sortingOrder = value;
            }
        }
    }

    public Tile origin { get; protected set; }
    protected List<Tile> tiles = new List<Tile>();

    public List<Tile> GetTiles()
    {
        return tiles;
    }

    public void setVisible(bool visible)
    {

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(visible);
        }
    }

}