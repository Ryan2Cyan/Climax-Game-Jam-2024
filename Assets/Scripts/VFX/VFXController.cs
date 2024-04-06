using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using System;

public class VFXController : MonoBehaviour
{
    public VFX[] visualEffects;
    private Transform player;

    private void Start()
    {
         player = PlayerManager.Instance.transform;
    }

    public void PlayVFX(string name, Vector3 position)
    {
        VFX visualEffect = Array.Find(visualEffects, vfx => vfx.name == name);

        Instantiate(visualEffect.effect, position, Quaternion.identity);
    }
    public void PlayerVFX(string name) //play effect at the players postition
    {
        VFX visualEffect = Array.Find(visualEffects, vfx => vfx.name == name);
        
        GameObject newEffect = Instantiate(visualEffect.effect, player.position, player.rotation, player);

    }
    public void PlayerVFX(string name, Vector3 position) //play effect at any position, as a child of the player
    {
        VFX visualEffect = Array.Find(visualEffects, vfx => vfx.name == name);

        GameObject newEffect = Instantiate(visualEffect.effect, position, player.rotation, player);

    }
}
       
       


   


