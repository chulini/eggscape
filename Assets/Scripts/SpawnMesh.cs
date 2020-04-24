using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMesh : MonoBehaviour
{
    private MeshRenderer[] meshes;

    void Start()
    {
        meshes = GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            foreach (MeshRenderer mesh in meshes) {
                mesh.enabled = true;
            }
        }
    }
}
