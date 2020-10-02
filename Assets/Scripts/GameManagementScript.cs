using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagementScript : MonoBehaviour
{

    public GameObject CenterPoint;

    public struct Dominoes
    {
        public GameObject ThePlayingDominoes;
        public List<int> AvailableNumbersOfDominoes;
    }


    public bool[] Rotation = new bool[] { false, false,false,false };

    public List<Dominoes> DominoesTree = new List<Dominoes>();

    public bool[] CreatingLeftOrRight = new bool[]{ false,false };

    public GameObject TheRunningDominoe;
    public Sprite SpriteOfRunningDominoe;
    public bool PlayerTurn = true;
    public bool EnemyAITurn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTurn == false)
        {
            //Checking the Created dominoe's rotation and location
            Dominoes RunningLocalDominoe;
            List<int> AvailableLocalNumbers = new List<int>();
            TheRunningDominoe = new GameObject("TreeDominoes");
            TheRunningDominoe.transform.rotation = Quaternion.Euler(0, 0, 90.0f);
            if (CreatingLeftOrRight[0] == true)
            {
                CenterPoint.transform.position = new Vector2((CenterPoint.transform.position.x - 1.3f)* DominoesTree.Count, CenterPoint.transform.position.y);
                CreatingLeftOrRight[0] = false;
                if (Rotation[0] == true)
                {
                    TheRunningDominoe.transform.rotation = Quaternion.Euler(0, 0, -90.0f);
                    Rotation[0] = false;
                }
                if (Rotation[1] == true)
                {
                    TheRunningDominoe.transform.rotation = Quaternion.Euler(0, 0, 90.0f);
                    Rotation[1] = false;
                }
                
            }
            if (CreatingLeftOrRight[1] == true)
            {
                CenterPoint.transform.position = new Vector2((CenterPoint.transform.position.x + 1.3f) * DominoesTree.Count, CenterPoint.transform.position.y);
                CreatingLeftOrRight[1] = false;
                if (Rotation[2] == true)
                {
                    TheRunningDominoe.transform.rotation = Quaternion.Euler(0, 0, 90.0f);
                    Rotation[2] = false;
                }
                if (Rotation[3] == true)
                {
                    TheRunningDominoe.transform.rotation = Quaternion.Euler(0, 0, -90.0f);
                    Rotation[3] = false;
                }
                
                
            }

            
            TheRunningDominoe.transform.SetParent(this.transform);
            TheRunningDominoe.transform.position = CenterPoint.transform.position;
            
            TheRunningDominoe.transform.localScale = new Vector2(0.6f, 0.6f);
            SpriteRenderer renderer = TheRunningDominoe.AddComponent<SpriteRenderer>();
            renderer.sprite = SpriteOfRunningDominoe;
            TheRunningDominoe.GetComponent<SpriteRenderer>().sortingOrder = 1;
            
            string fp = TheRunningDominoe.GetComponent<SpriteRenderer>().sprite.ToString();

            char[] characters = new char[3];
            for (int i = 0; i < 3; i++)
            {
                characters[i] = System.Convert.ToChar(fp[i]);

            }
           //Add our created dominoes in the Dominoe Tree
            AvailableLocalNumbers.Add((int)char.GetNumericValue(characters[0]));
            AvailableLocalNumbers.Add((int)char.GetNumericValue(characters[2]));

            RunningLocalDominoe.ThePlayingDominoes = TheRunningDominoe;
            RunningLocalDominoe.AvailableNumbersOfDominoes = AvailableLocalNumbers;

            DominoesTree.Add(RunningLocalDominoe);

            foreach (Dominoes d in DominoesTree)
            {
                Debug.Log(d.AvailableNumbersOfDominoes[0]);
                Debug.Log(d.AvailableNumbersOfDominoes[1]);
            }
           
            PlayerTurn = true;
        }


    }

}
