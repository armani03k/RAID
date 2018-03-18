using System.Collections;
using UnityEngine;

public class Abyss : MonoBehaviour {
    [Header("Player")]
    public PlayerStats pStats;
    public Transform RespawnArea;

    public float Dmg;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            pStats.TakeDmg(Dmg);
            pStats.gameObject.transform.position = RespawnArea.transform.position;
            pStats.UIUpdate();
        }
    }
}
