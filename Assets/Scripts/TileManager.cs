using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public float ZSpawn;
    public float TileLength;
    public GameObject[] TilePrefabs;
    public int NumberOfTiles;
    public Transform PlayerTransform;

    private List<GameObject> activeTiles;

    // Start is called before the first frame update
    private void Start()
    {
        activeTiles = new List<GameObject>();

        for (int i = 0; i < NumberOfTiles; ++i)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, TilePrefabs.Length));
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (PlayerTransform.position.z - 35f > ZSpawn - (NumberOfTiles * TileLength))
        {
            SpawnTile(Random.Range(0, TilePrefabs.Length));
            DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(TilePrefabs[tileIndex], transform.forward * ZSpawn, transform.rotation);
        activeTiles.Add(go);

        ZSpawn += TileLength;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
