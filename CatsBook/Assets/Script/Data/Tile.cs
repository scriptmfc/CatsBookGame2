using UnityEngine;
public class Tile : MonoBehaviour
{
    public int x { get; private set; }
    public int y { get; private set; }

    public bool enable = false;

    [SerializeField]
    public bool isBlock = false;

    public void Set(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}