using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCameraFollow : MonoBehaviour
{
    [SerializeField] Transform _target;

    //in case you have many camera in scene, need to know which one is gonna follow the player
    [SerializeField] Transform _camera;

    // Start is the first frame called
    private void Start() {
        _target = transform;
    }

    // Update is called once per frame
    void Update() {
        _camera.transform.position = new Vector3(_target.position.x, _camera.transform.position.y, _camera.transform.position.z);
    }
}
