
using UnityEngine;
using System.Collections;

public class ReachHeart : MonoBehaviour 
{
    private GameManager game;

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider collision)
    {
        game.LevelComplete();
        GetComponent<Collider>().enabled = false;
    }
}
