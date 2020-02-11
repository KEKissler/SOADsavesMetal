using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image heart1;
    [SerializeField] private Image heart2;
    [SerializeField] private Image heart3;
    [SerializeField] private Sprite full;
    [SerializeField] private Sprite broken;
    [SerializeField] private Player player;
    private void Awake(){
        player = FindObjectOfType<Player>();
        heart1.sprite = full;
        heart2.sprite = full;
        heart3.sprite = full;
    }
    private void Update(){
        if(player.Health == 2){
            heart3.sprite = broken;
        }
        else if(player.Health == 1){
            heart3.sprite = broken;
            heart2.sprite = broken;
        }
        else if(player.Health <= 0){
            heart3.sprite = broken;
            heart2.sprite = broken;
            heart1.sprite = broken;
        }
    }
}
