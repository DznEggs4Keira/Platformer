using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    Vector2 _startPosition;
    [SerializeField] Vector2 _direction = Vector2.up;
    [SerializeField] float _maxDistance = 3f;
    [SerializeField] float _speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction.normalized * Time.deltaTime * _speed);
        //var distance = (_startPosition - (Vector2)transform.position).sqrMagnitude;
        var distance = Vector2.Distance(_startPosition, transform.position);

        if(distance >= _maxDistance) {

            transform.position = _startPosition + (_direction.normalized * _maxDistance);
            _direction *= -1;
        }
        
    }
}
