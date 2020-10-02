using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    GameObject[] SpawnPoints;
    //List<GameObject> SpawnPoints
    public Sprite[] DominoesSprites;
    List<GameObject> PlayerDominoes = new List<GameObject>();
    public GameObject GameManager;
    GameManagementScript GMscript;
    List<bool> WhatDominoesCanBePlayed = new List<bool>();


    private bool first = true;
    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {


        //Script Communication with GameManager and Creating the player start dominoes
        GMscript = GameManager.GetComponent<GameManagementScript>();

        SpawnPoints = GameObject.FindGameObjectsWithTag("PlayerPoints");


        foreach (GameObject SP in SpawnPoints)
        {
            int i = Random.Range(0, DominoesSprites.Length);
            GameObject go = new GameObject("PlayerDominoes"); ;
            go.transform.SetParent(this.transform);
            go.transform.position = SP.transform.position;
            go.transform.localScale = new Vector2(0.6f, 0.6f);
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = DominoesSprites[i];
            PlayerDominoes.Add(go);


        }
        for (int k = 0; k < PlayerDominoes.Count; k++)
        {
            bool CanBePlayed = true;
            PlayerDominoes[k].name = "PlayerDominoes" + k.ToString();
            PlayerDominoes[k].GetComponent<SpriteRenderer>().sortingOrder = 1;
            PlayerDominoes[k].tag = "Dominoes";
            PlayerDominoes[k].AddComponent<BoxCollider2D>();
            WhatDominoesCanBePlayed.Add(CanBePlayed);
        }


    }

    // Update is called once per frame
    void Update()
    {
        ClickASpecificDominoe();
        if (once == true)
        {
            CheckDominoes();

        }

    }
    

    //Click A player Dominoe
    void ClickASpecificDominoe()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        if (Input.GetMouseButtonDown(0) && first)
        {
            Debug.Log("here");
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            for (int k = 0; k < PlayerDominoes.Count; k++)
            {
                if (hit.collider == PlayerDominoes[k].GetComponent<BoxCollider2D>() && WhatDominoesCanBePlayed[k] == true)
                {

                    CheckNumbers(k);


                    GMscript.TheRunningDominoe = PlayerDominoes[k];
                    GMscript.SpriteOfRunningDominoe = PlayerDominoes[k].GetComponent<SpriteRenderer>().sprite;
                    GMscript.PlayerTurn = false;
                    RemoveDominoe(k);

                }
            }
            first = false;
            once = true;
        }

    }

    //Remove Player Dominoes
    void RemoveDominoe(int k)
    {

        PlayerDominoes[k].GetComponent<SpriteRenderer>().sprite = null;
        PlayerDominoes.RemoveAt(k);
    }

    void AddDominoes()
    {

    }

    //Check Dominoes for Animating
    void CheckDominoes()
    {


        if (first == false)
        {



            for (int k = 0; k < PlayerDominoes.Count; k++)
            {

                string fp = PlayerDominoes[k].GetComponent<SpriteRenderer>().sprite.ToString();

                char[] characters = new char[3];
                for (int i = 0; i < 3; i++)
                {
                    characters[i] = System.Convert.ToChar(fp[i]);

                }


                for (int j = 0; j < GMscript.DominoesTree.Count; j++)
                {
                    for (int r = 0; r < GMscript.DominoesTree[j].AvailableNumbersOfDominoes.Count; r++)
                    {
                        if ((int)char.GetNumericValue(characters[0]) == GMscript.DominoesTree[j].AvailableNumbersOfDominoes[r] || (int)char.GetNumericValue(characters[2]) == GMscript.DominoesTree[j].AvailableNumbersOfDominoes[r])
                        {
                            Debug.Log("Ew");
                            PlayerDominoes[k].transform.position = new Vector2(PlayerDominoes[k].transform.position.x, -3.60f);
                            WhatDominoesCanBePlayed[k] = true;

                            once = false;
                            first = true;
                        }
                        else
                        {
                           
                        }

                    }

                }

            }

        }

    }

    //Check Number of Dominoes with Dominoe Tree
    void CheckNumbers(int k)
    {


            string fp = PlayerDominoes[k].GetComponent<SpriteRenderer>().sprite.ToString();

            char[] characters = new char[3];
            for (int i = 0; i < 3; i++)
            {
                characters[i] = System.Convert.ToChar(fp[i]);

            }


            for (int j = 0; j < GMscript.DominoesTree.Count; j++)
            {
               
                    if ((int)char.GetNumericValue(characters[0]) == GMscript.DominoesTree[j].AvailableNumbersOfDominoes[0])
                    {
                        GMscript.DominoesTree[j].AvailableNumbersOfDominoes[0] = -35;
                        GMscript.CreatingLeftOrRight[0] = true;
                         GMscript.Rotation[0] = true;
                        Debug.Log("L " + GMscript.CreatingLeftOrRight[0]);
                        
                    }

                    else if ((int)char.GetNumericValue(characters[2]) == GMscript.DominoesTree[j].AvailableNumbersOfDominoes[0])
                    {
                        GMscript.DominoesTree[j].AvailableNumbersOfDominoes[0] = -35;
                        GMscript.CreatingLeftOrRight[0] = true;
                        GMscript.Rotation[1] = true;
                        Debug.Log("L " + GMscript.CreatingLeftOrRight[0]);
                        
                    }
                    if ((int)char.GetNumericValue(characters[0]) == GMscript.DominoesTree[j].AvailableNumbersOfDominoes[1])
                    {
                        GMscript.DominoesTree[j].AvailableNumbersOfDominoes[0] = -35;
                        GMscript.CreatingLeftOrRight[1] = true;
                        GMscript.Rotation[2] = true;
                        Debug.Log("R " + GMscript.CreatingLeftOrRight[1]);
                    }

                    else if ((int)char.GetNumericValue(characters[2]) == GMscript.DominoesTree[j].AvailableNumbersOfDominoes[1])
                    {
                        GMscript.DominoesTree[j].AvailableNumbersOfDominoes[0] = -35;
                        GMscript.CreatingLeftOrRight[1] = true;
                        GMscript.Rotation[3] = true;
                        Debug.Log("R " + GMscript.CreatingLeftOrRight[1]);
                    }


            }
        
    }
}