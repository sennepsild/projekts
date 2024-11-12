using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikeSpawner : MonoBehaviour
{
    static Tilemap tilemap;
    public GameObject spike;
    // Start is called before the first frame update
    void Start()
    {
        if(tilemap == null) { tilemap = FindObjectOfType<Tilemap>(); }
        Instantiate(spike, transform.position, transform.rotation);
        tilemap.SetTile(tilemap.WorldToCell(transform.position), null);
    }

}
