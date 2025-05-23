using UnityEngine;
using UnityEngine.Tilemaps;

public class Node : MonoBehaviour
{
    private Color startColor;
    public Color hoverColor;

    private Renderer rend;
    private TilemapCollider2D tileColl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tileColl = GetComponent<TilemapCollider2D>();
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    void OnMouseDown()
    {
        Debug.Log("Please work");
    }

    void OnCollisionEnter2D()
    {
       Debug.Log("Come on man");
       rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
