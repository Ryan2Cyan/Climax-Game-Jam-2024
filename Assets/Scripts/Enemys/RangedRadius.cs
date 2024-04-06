using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class RangedRadius : MonoBehaviour
{
    bool playerInRange;

    private void OnEnable()
    {
        playerInRange = false;
    }
    private void OnTriggerStay(Collider other)
    {
      if (other.gameObject.tag == "Player")
      {
            playerInRange = true;
      }
      else
      {
            playerInRange = false;
      }
     
    }
  

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                {
                    playerInRange = false;
                }
                break;

        }

    }


    public bool CanSeePlayer()
    {
        return playerInRange;
    }
}
