using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class ScenePortal : MonoBehaviour
{
    [SerializeField] int nextScene = -1;
    [SerializeField] Transform movePlayerTo;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(nextScene);
        }
    }

}
