using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    private DialogueManager _instance; //Para crear singleton

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    #region Variables del panel de diálogos
    [SerializeField]
    private GameObject _dialoguePnl;

    private Button _nextBttn;
    private TextMeshProUGUI _dialogueTxt, _nameTxt, _nextTxt;
    #endregion


    #region Variables NPC
    private string _name;
    private List<string> _dialogueList;
    private int _dialogueIdx;
    #endregion

    private void Start()
    {
        

        #region Obtener componendes del panel
        if (_dialoguePnl == null)
        {
            Debug.LogError("No se asignó el panel de dialogos al manejador");
        }
        else
        {
            #region Obtener text de dialogo
            _dialogueTxt = _dialoguePnl.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            //_dialogueTxt = _dialoguePnl.GetComponentInChildren<TextMeshProUGUI>();
            if(_dialogueTxt == null)
            {
                Debug.LogError("El panel de diálogo no tiene un primer hijo TMP");
            }
            else
            {
                _dialogueTxt.text = "Diálogos inicializados";
            }
            #endregion

            #region Obtener text de nombre NPC
            //Buscar el homponente TMP en el primer hijo del segundo hijo del panel
            _nameTxt = _dialoguePnl.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
            if (_nameTxt == null)
            {
                Debug.LogWarning("No hay un TMP como primer hijo del segundo hijo del panel");
            }
            else
            {
                _dialogueTxt.text = "Nombre inicializado";
            }

            #endregion

            #region Obtener botón y texto
            //Buscar el homponente botón en el tercer hijo del panel
            _nextBttn= _dialoguePnl.transform.GetChild(2).GetComponent<Button>();
            if (_nextBttn == null)
            {
                Debug.LogWarning("No hay botón como tercer hijo del panel de diálogos");
            }
            else
            {
                _nextBttn.onClick.AddListener(delegate {
                    ContinueDialogue();
                });

                #region Obtener Texto dek botón
                _nextTxt = _nextBttn.GetComponentInChildren<TextMeshProUGUI>();
                
                if(_nextTxt == null)
                {
                    Debug.LogWarning("El botón de continuar no tiene texto");
                }
                else
                {
                    _nextTxt.text = "Bóton inicializado";
                }
                
                #endregion
            }

            #endregion
        }
        #endregion

        _dialoguePnl.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setDialogue(string name, string[] dialogue)
    {
        _name = name;
        _dialogueList = new List<string>(dialogue.Length);
        _dialogueList.AddRange(dialogue);
        _dialogueIdx = 0;

        _dialogueTxt.text = _dialogueList[_dialogueIdx];
        _nameTxt.text = _name;
        _nextTxt.text = "Continuar";
        _dialoguePnl.SetActive(true);
    }

    private void ShowDialogue()
    {
        _dialogueTxt.text = _dialogueList[_dialogueIdx];
    }

    private void ContinueDialogue()
    {
        if(_dialogueIdx == _dialogueList.Count - 1) //Se termino la conversacion
        {
            Debug.Log("Termina conversación");
            _dialoguePnl.SetActive(false);
        } 
        else if( _dialogueIdx == _dialogueList.Count -2) //Ultimo dialogo
        {
            _dialogueIdx++;
            ShowDialogue();
            _nextTxt.text = "Salir";
        }
        else
        {
            _dialogueIdx++;
            ShowDialogue();
        }
        
    }
}
