using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;

namespace RPG.Cinematics
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(PlayableDirector))]
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (alreadyTriggered) return;

            PlayerInput input = other.GetComponent<PlayerInput>();
            if (input == null) return;
            
            PlayableDirector director = GetComponent<PlayableDirector>();
            if (director == null) return;

            input.EnterCinematic(director);
            
            alreadyTriggered = true;
        }      
    }
}
