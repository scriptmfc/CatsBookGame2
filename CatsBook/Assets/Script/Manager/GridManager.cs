using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoSingleTon<GridManager> {

    private Tiles tiles;
    private Sorter sorter;

    private FurnitureData SelectedFurniture;

    public bool dragging = false;

    public Transform interactBtnGroup;
    public Button placeButton;
    public Button rotateButton;
	public Button undoButton;
    public Toggle mode;
    public SpriteRenderer grids;

    void Awake ()
    {
        sorter = GameObject.Find("Unit").GetComponent<Sorter>();
        tiles = GameObject.Find("Tiles").GetComponent<Tiles>();
    }

    void Start () {
        placeButton.onClick.AddListener(() => {
			
            OnPlaceFurniture(SelectedFurniture);
            sorter.SortAll();
            interactBtnGroup.gameObject.SetActive(false);
        });
        rotateButton.onClick.AddListener(() => {
                List<Tile> area;
                RotateItem(out area);
        });
        undoButton.onClick.AddListener(() => {
            OnUndo(SelectedFurniture);
            sorter.SortAll();
			interactBtnGroup.gameObject.SetActive(false);
		});
        mode.onValueChanged.AddListener(value => grids.enabled = value);
    }
	
	void Update () {
        sorter.SortAll();
        //if (!mode.isOn)
        //    return;
        mode.interactable = SelectedFurniture == null;
        
        if (Input.GetMouseButtonDown(0))
            OnBeginDrag(isHold => dragging = isHold);

        else if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            OnEndDrag();
        }

        if (dragging)
            OnDrag();
    }

    public void OnBeginDrag(Action<bool> isHold) {

        if (SelectedFurniture == null)
        {
            var furniture = OnSelect(child => child.transform.parent.GetComponent<FurnitureData>() != null);
            if (furniture != null)
            {
                SelectedFurniture = furniture.transform.parent.GetComponent<FurnitureData>();
                SelectedFurniture.Unplaced();
            }
            isHold(furniture != null);
        }
        else
        {
            var furniture = OnSelect(child => child.transform.parent.GetComponent<FurnitureData>() != null);
            isHold(furniture != null && furniture.transform.parent.GetComponent<FurnitureData>() == SelectedFurniture);
        }

    }

    private void OnDrag()
    {
        if (SelectedFurniture == null)
            return;

        var tile = OnSelect(obj => obj.GetComponent<Tile>() != null);
        if (tile != null)
        {
            interactBtnGroup.gameObject.SetActive(false);
            SelectedFurniture.Move(tile.GetComponent<Tile>());

            List<Tile> area;
            OnInvalid(SelectedFurniture, out area);
        }
    }

    private void OnEndDrag()
    {
        if (SelectedFurniture == null)
            return;

        Vector3 pos = new Vector3();
        pos = SelectedFurniture.transform.position;

        var centerPoint = new Vector3(pos.x, pos.y - 5); //Camera.main.WorldToScreenPoint(SelectedFurniture.transform.position);
        interactBtnGroup.position = centerPoint;
        interactBtnGroup.gameObject.SetActive(true);

        List<Tile> area;
        placeButton.interactable = !(OnInvalid(SelectedFurniture, out area));
        undoButton.interactable = SelectedFurniture.previous != null;
    }

    private void RotateItem(out List<Tile> area)
    {
        area = new List<Tile>();
		if (SelectedFurniture != null)
        {
            SelectedFurniture.Rotate();
			placeButton.interactable = !(OnInvalid(SelectedFurniture, out area));
        }
    }

    private GameObject OnSelect(Predicate<GameObject> condition)
	{	
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray, Mathf.Infinity);
        foreach (var hit in hits)
            if (condition(hit.transform.gameObject))
                return hit.transform.gameObject;
        return null;
    }

    private void OnPlaceFurniture(FurnitureData furniture)
    {
        if (furniture == null)
            return;

        List<Tile> area;
        if (!OnInvalid(furniture, out area))
        {
            furniture.Place(area);
            furniture.SetColor(Color.white);
            SelectedFurniture = null;

            //temp
            GameManagerTemp.Instance.AddLiFurniture(furniture);
        }
    }

    private bool OnInvalid(FurnitureData furniture, out List<Tile> area)
    {
        List<Tile> area_temp = new List<Tile>();
        area = new List<Tile>();

        for (int i =0; i< (furniture.width); i++)
        {
            for (int j =0; j < (furniture.length); j++)
            {
                var tile = tiles.GetTileByCoordinate(furniture.origin.x - j, furniture.origin.y - i);
                if (tile == null || tile.isBlock)
                {
                    furniture.SetColor(Color.red);
                    return true;
                }

                area_temp.Add(tile);
            }
        }

        area = area_temp;
        furniture.SetColor(Color.green);
        return false;
    }

    private void SizeOnvalid(Tile tile, FurnitureData furniture, int x, int y)
    {

    }

	private void OnUndo (FurnitureData furniture)
	{
        if (furniture.previous == null)
            return;

        furniture.Move (furniture.previous.tile);
        furniture.Rotate (furniture.previous.direction);
        OnPlaceFurniture(furniture);
    }
}
