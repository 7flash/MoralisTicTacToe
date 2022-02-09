using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OneScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // testing
        // print("Start");
        // foreach (MeshFilter mf in GetComponentsInChildren<MeshFilter>())
        // {
        //     print(mf.name);
        //     mf.mesh.SetIndices(mf.mesh.GetIndices(0).Concat(mf.mesh.GetIndices(0).Reverse()).ToArray(), MeshTopology.Triangles, 0);
        // }

        // rotate
        // transform.Rotate(new Vector3(0, 0, 45));
    }

    // Update is called once per frame
    void Update()
    {
    }

    // upon collision
    void onTriggerEnter(Collider other) {
        print("collision on " + gameObject.name + " " + other.gameObject.name);
    }    
}
