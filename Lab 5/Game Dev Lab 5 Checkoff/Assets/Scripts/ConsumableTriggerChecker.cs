using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ConsumableTriggerChecker : MonoBehaviour
{
    public Powerup stats;
    public CustomPowerupEvent onCollected;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collected Mushroom" + stats.aboluteSpeedBooster);
            onCollected.Invoke(stats);
			Destroy(this.gameObject);
        }
    }
}