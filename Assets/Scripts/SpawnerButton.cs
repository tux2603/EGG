using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerButton : MonoBehaviour
{
    public GameObject spawnPrefab;
    bool isCollided = false;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (isCollided) {
             // Spawn a new copy of the prefab at a random point in the scene
            Vector3 location = new Vector3(Random.Range(-14, 14), Random.Range(4, 12), Random.Range(-14, 14));
            GameObject newObject = Instantiate(spawnPrefab, location, Quaternion.identity);

            // Set the scale, rotation, and mass of the new object to some random values
            float baseScale = Random.Range(0.1f, 2.0f);
            newObject.transform.localScale = new Vector3(baseScale * Random.Range(0.75f, 1.5f), baseScale * Random.Range(0.75f, 1.5f), baseScale * Random.Range(0.75f, 1.5f));
            newObject.GetComponent<Rigidbody>().mass = Random.Range(0.01f, 0.1f);
            newObject.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        }
    }

    void OnCollisionEnter(Collision collision) {
        isCollided = true;
    }

    void OnCollisionExit(Collision collision) {
        isCollided = false;
    }
}
