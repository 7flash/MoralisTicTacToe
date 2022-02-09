using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(Vector3.forward * Time.deltaTime * 2.8f);
    }

    private void onTriggerEnter(Collider other) {
        print("collision on " + gameObject.name + " " + other.gameObject.name);
    }

    private void OnMouseDown() {
        GameObject.Find("WebPageObject").GetComponent<WebPage>().TryLogin();
    }
}
