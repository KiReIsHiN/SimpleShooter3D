using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour
{
    SC_DamageReceiver player;
    Text playerHealth;

    private void Start()
    {
        playerHealth = gameObject.GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SC_DamageReceiver>();
    }

    private void Update()
    {
        playerHealth.text = "My health:" + " " + player.playerHP.ToString() + "HP";
    }
}
