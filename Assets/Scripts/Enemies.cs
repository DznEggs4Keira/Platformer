using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    //Base Class for all Enemy actions, the common stuff can be added here

    public enum EnemyTypes {
        Fly,
        Slime,
        Traps
    }

    [Header("Enemy Type")]
    public EnemyTypes _enemy;
    protected bool isDead = false;

    #region MOVEMENT

    Vector2 _startPosition;
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;
    

    [Header("Enemy Movement")]
    [SerializeField] Vector2 _direction = Vector2.up;
    [SerializeField] float _maxDistance = 3f;
    [SerializeField] float _speed = 3f;



    //Slime
    float _slimeDirection = -1f;

    [Header("Slime Sensors")]
    [SerializeField] Transform _leftSensor = null;
    [SerializeField] Transform _rightSensor = null;

    // Start is called before the first frame update
    protected virtual void Start() {
        _startPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update() {
        if(!isDead) {
            switch (_enemy) {
                case EnemyTypes.Fly: {
                        transform.Translate(_direction.normalized * Time.deltaTime * _speed);
                        //var distance = (_startPosition - (Vector2)transform.position).sqrMagnitude;
                        var distance = Vector2.Distance(_startPosition, transform.position);

                        if (distance >= _maxDistance) {

                            transform.position = _startPosition + (_direction.normalized * _maxDistance);
                            _direction *= -1;
                            _spriteRenderer.flipX = _direction.x > 0;
                        }
                        break;
                    }

                case EnemyTypes.Slime: {

                        _rigidbody.velocity = new Vector2(_slimeDirection, _rigidbody.velocity.y);

                        if (_slimeDirection < 0) {
                            ScanSensor(_leftSensor);
                        } else {
                            ScanSensor(_rightSensor);
                        }
                        break;
                    }
            }
        }
    }

    private void ScanSensor(Transform sensor) {
        Debug.DrawRay(sensor.position, Vector2.down * 0.1f, Color.red);
        var hit = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        if (hit.collider == null) {
            TurnAround();
        }

        Debug.DrawRay(sensor.position, new Vector2(_slimeDirection, 0) * 0.1f, Color.red);
        var sideHit = Physics2D.Raycast(sensor.position, new Vector2(_slimeDirection, 0), 0.1f);
        if (sideHit.collider != null) {
            TurnAround();
        }
    }

    void TurnAround() {
        _slimeDirection *= -1;
        var sr = GetComponent<SpriteRenderer>();
        sr.flipX = _slimeDirection > 0;
    }

    #endregion

    #region ATTACK

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (!enabled) return;

        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("Player Died. Game Over!");
            collision.GetComponent<Animator>().SetTrigger("Hit");
            collision.GetComponent<Player>().ResetToStart();
        }
    }

    protected virtual void OnParticleCollision(GameObject other) {
        if (!enabled) return;
    }

    #endregion
}
