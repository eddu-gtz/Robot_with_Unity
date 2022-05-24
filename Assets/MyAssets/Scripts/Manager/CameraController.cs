using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Componentes cámara
    [SerializeField]
    private Transform _player, _playerCamera, _focusPoint;
    [SerializeField]
    private float _cameraHeight = 5f;
    #endregion

    #region Variables zoom
    [SerializeField]
    private float _zoom = -5;
    [SerializeField]
    private float _zoomSpeed = 8;
    [SerializeField]
    private float _zoomMax = -4, _zoomMin = -15;
    #endregion

    #region Variables rotación
    [SerializeField]
    private float _camRotX, _camRotY;
    [SerializeField]
    private float _mouseSentitivity = 5;
    private float _minCamRotY = -60f;
    private float _maxCamRotY = 89f;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        #region Comprobar asignacion de componentes
        if(_player == null)
        {
            Debug.Log("El jugador no se asigno al CameraController");
        }
        if (_playerCamera == null)
        {
            Debug.Log("La camara no se asigno al CameraController");
        }
        if (_focusPoint == null)
        {
            Debug.Log("Punto focal no se asigno al CameraController");
        }
        #endregion

        #region Asignar parentesco
        _focusPoint.SetParent(_player);
        _playerCamera.SetParent(_focusPoint);
        #endregion

        #region Asignar pos,rot,scale
        _focusPoint.localPosition = new Vector3(0, _cameraHeight, 0);
        _focusPoint.localRotation = Quaternion.Euler(0,0,0);
        _focusPoint.localScale = new Vector3(1, 1, 1);
        _playerCamera.localPosition = new Vector3(0, 0, _zoom);
        _playerCamera.LookAt(_player);
        _playerCamera.localScale = new Vector3(1, 1, 1);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Zoom
        _zoom += Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
        _zoom = Mathf.Clamp(_zoom, _zoomMin, _zoomMax);
        #endregion

        #region Actualizar cámara
        _playerCamera.localPosition = new Vector3(0, 0, _zoom);
        _playerCamera.LookAt(_player);
        #endregion

        #region Rotación
        if (Input.GetMouseButton(1))
        {
            _camRotX += Input.GetAxis("Mouse X")*_mouseSentitivity;
            _camRotY += Input.GetAxis("Mouse Y")*_mouseSentitivity;
            _camRotY = Mathf.Clamp(_camRotY, _minCamRotY, _maxCamRotY);

            _focusPoint.localRotation = Quaternion.Euler(_camRotY,0,0);
            _player.localRotation = Quaternion.Euler(0, _camRotX, 0);
        }

        #endregion
    }
}
