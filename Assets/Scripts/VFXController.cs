using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VFXController : MonoBehaviour
{
    public VFX[] visualEffects;
    private  Player.PlayerManager player;

    private void Start()
    {
         player = FindObjectOfType<Player.PlayerManager>();
    }

    public void PlayVFX(Transform location, string name)
    {
        VFX visualEffect = Array.Find(visualEffects, vfx => vfx.name == name);

        Instantiate(visualEffect.effect, location.position, Quaternion.identity);
    }
    public void PlayerVFX(string name) 
    {
        VFX visualEffect = Array.Find(visualEffects, vfx => vfx.name == name);
        
        GameObject newEffect = Instantiate(visualEffect.effect, player.transform.position, player.transform.rotation, player.transform);


    }
}
       
       


   


