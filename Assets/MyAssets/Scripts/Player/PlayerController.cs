using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _playerRB;

    #region Variables Movimiento
    [SerializeField]
    private float _maxSpeed = 9f;
    private float _speed;
    private float _forwardInput, _horizontalInput;
    #endregion

    #region Variables Brinco
    private bool _jumpRequest=false;
    [SerializeField]
    private float _jumpforce = 5;
    [SerializeField]
    private int _availableJumps = 0, _maxJumps = 2;
    #endregion

    #region Variables Animation
    private PlayerAnimation _playerAnimation;
    private bool _isRunning;
    #endregion

    private void Start()
    {
        #region Obtener RibgidBody
        _playerRB = GetComponent<Rigidbody>();

        if(_playerRB == null)
        {
            Debug.LogWarning("El jugador no tiene un RigidBody");
        }
        #endregion


        #region Obtener Player anim
        _playerAnimation = GetComponent<PlayerAnimation>();

        if(_playerAnimation == null)
        {
            Debug.LogWarning("No se encontro el player animation del jugador");
        }
        #endregion

        _isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {



        #region Caminar/Correr

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isRunning = !_isRunning;
        }

        if (_isRunning)
        {
            _speed = _maxSpeed;
        }
        else
        {
            _speed = _maxSpeed / 3;
        }
        #endregion

        #region Movimiento
        _horizontalInput = Input.GetAxis("Horizontal"); //Left, Right,A,D   
        _forwardInput = Input.GetAxis("Vertical"); //Up, Down, W, S

        #region animacion Correr/Caminar
        float velocity = Mathf.Max(Mathf.Abs(_horizontalInput),Mathf.Abs(_forwardInput));
        
        if (_isRunning)
        {
            _playerAnimation.SetSpeed(velocity);
        }
        else
        {
            _playerAnimation.SetSpeed(velocity/3);
        }
        #endregion


        Vector3 movement = new Vector3(_horizontalInput,0,_forwardInput);
        transform.Translate(movement * _speed * Time.deltaTime);
        #endregion

        #region Peticion de brinco
        if (Input.GetKeyDown(KeyCode.Space) && _availableJumps > 0)
        {
            _jumpRequest = true;
        }
        #endregion

      
    }

    private void FixedUpdate()
    {
        #region Brincar
        if (_jumpRequest)
        {
            _playerRB.velocity = Vector3.up * _jumpforce;
            _availableJumps--;
            _jumpRequest = false;
        }
        #endregion
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _availableJumps = _maxJumps;

        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("Se acerco al objeto para interactuar");
            Interactable interaction = collider.GetComponent<Interactable>();
            if(interaction == null)
            {
                Debug.Log("Objeto no Responde");
            }
            else
            {
                interaction.Interact();
            }
        }
    }
}
