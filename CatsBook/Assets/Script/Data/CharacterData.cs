using UnityEngine;
using UnityEngine.UI;

public class CharacterData : BaseUnit
{

    public void UpdateTile(Tile tile)
    {
        if (origin != tile)
            origin = tile;
    }
}
