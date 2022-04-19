using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AIManager : MonoBehaviour {

    private Sorter sorter;
    private Tiles tiles;

    public NavMeshAgent agent;
    public GameObject character;
    public Toggle mode;

    void Awake()
    {
        sorter = GameObject.Find("Unit").GetComponent<Sorter>();
        tiles = GameObject.Find("Tiles").GetComponent<Tiles>();
    }

    void Update ()
    {
        var pos = agent.gameObject.transform.position;
        character.transform.position = new Vector2((pos.x - pos.z) * 2, -(pos.z + pos.x));
        var unit = character.GetComponent<CharacterData>();
        var tile = tiles.GetTileByPoint(character.transform.position);
        unit.UpdateTile(tile);
        sorter.SortUnit(unit);

       if(agent.gameObject.transform.eulerAngles.y > 180)
        {
            character.transform.GetChild(0).gameObject.SetActive(true);
            character.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            character.transform.GetChild(0).gameObject.SetActive(false);
            character.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (!mode.isOn && !CPopupManager.Instance.isOpen && !CPopupManager.Instance.block) 
        {
            if (Input.GetMouseButtonUp(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hits = Physics.RaycastAll(ray, Mathf.Infinity);
                foreach (var hit in hits)
                {
                    var selectedTile = hit.transform.gameObject.GetComponent<Tile>();
                    if (selectedTile != null && selectedTile.enable == true)
                    {
                        var position = new Vector3(selectedTile.y, 0, selectedTile.x);
                        agent.destination = position;
                    }
                }
            }
        }

       
    }
}
