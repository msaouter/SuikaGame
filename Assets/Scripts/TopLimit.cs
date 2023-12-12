using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopLimit : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Fruit fruitScript = collision.gameObject.GetComponent<Fruit>();

        if(fruitScript.hasBeenDropped )
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
