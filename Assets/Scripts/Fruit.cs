using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int fruitIndex;
    public bool hasBeenDropped = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasBeenDropped = true;

        if (collision.gameObject.CompareTag("Fruit"))
        {
            Fruit collidedFruit = collision.gameObject.GetComponent<Fruit>();

            if(collidedFruit.fruitIndex == fruitIndex)
            {
                //Prevent getting two of same fruit when 2 fruit collide instead of fusionning
                if (!gameObject.activeSelf || !collidedFruit.gameObject.activeSelf)
                    return;

                //Destroy and deactivate collided fruit
                collision.gameObject.SetActive(false);
                Destroy(collision.gameObject);


                //Instantiate next fruit combinaison at fruit pos
                MoveCloud moveCloudInstance = MoveCloud.instance;
                moveCloudInstance.IncreaseScore(moveCloudInstance.fruitPrefabs[fruitIndex + 1].points);
                GameObject nextfruit = Instantiate(moveCloudInstance.fruitPrefabs[fruitIndex + 1].prefab);
                
                nextfruit.transform.position = transform.position;
                
                //Destroy and deactivate this fruit
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        
    }
}
