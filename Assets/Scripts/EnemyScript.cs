using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    GameObject[] SpawnPoints;
    //List<GameObject> SpawnPoints
    public Sprite[] DominoesSprites;
    List<Sprite> EnemiesDominoes;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("EnemyPoints");
        
        foreach (GameObject SP in SpawnPoints)
        {
            int i = Random.Range(0, DominoesSprites.Length);
            GameObject go = new GameObject("EnemiesDominoes"); ;
            go.transform.SetParent(this.transform);
            go.transform.position = SP.transform.position;
            go.transform.localScale = new Vector2(0.6f, 0.6f);
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = DominoesSprites[i];

            
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
