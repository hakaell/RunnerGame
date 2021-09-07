using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class obstackle : MonoBehaviour
{ private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
           var temp = other.transform.GetChild(0).GetComponent<Animator>();
           temp.SetTrigger("Injured");
            Debug.Log("Player carptı");
            GameManager.manager.health--;
            Debug.Log(GameManager.manager.score);
            
            if (GameManager.manager.health <= 0)
            {
                GameManager.manager.CurrentGameState = GameManager.GameState.DeadGame;
            }
        }
    }
}
