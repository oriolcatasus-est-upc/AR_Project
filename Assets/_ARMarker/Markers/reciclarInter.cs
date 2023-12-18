using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class reciclarInter : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Lista de objetos con los que este objeto puede interactuar
    private List<reciclarInter> _objectsInteractingWith = new List<reciclarInter>();
    private Vector3 posi; 
    private GameObject tapa, txt, obj;
    private string msg;

    private bool interaction = false;

    // Agregar el objeto activado a la lista
    private void OnTriggerEnter(Collider other)
    {
        reciclarInter otherObject = other.gameObject.GetComponent<reciclarInter>();
        obj = other.gameObject;

        if(otherObject != null && other.tag != tag)
        {
            // Agregar objeto a la lista de interacciones
            AddObject(otherObject, other.tag, obj);
        }
    }

    // Eliminar el objeto de la lista activa
    private void OnTriggerExit(Collider other)
    {
        reciclarInter otherObject = other.gameObject.GetComponent<reciclarInter>();
        if(otherObject != null)
        {
            // Eliminar objeto de la lista de interacciones
            RemoveObject(otherObject);
        }
    }

    private void AddObject(reciclarInter recIn, string T, GameObject obj)
    {
        _objectsInteractingWith.Add(recIn);
        // Realizar la interacción
        Interact(T, obj);
    }

    private void RemoveObject(reciclarInter recIn)
    {
        _objectsInteractingWith.Remove(recIn);  
        if(_objectsInteractingWith.Count == 0)
        {
            // Desactivar la interacción si no hay objetos en la lista
            Sleep();
        }
    }

    // Eliminar el objeto cuando es deshabilitado
    private void OnDisable()
    {
        if(_objectsInteractingWith.Count > 0)
        {
            // Eliminar este objeto de la lista de interacciones
            foreach (reciclarInter rcIn in _objectsInteractingWith)
            {
                rcIn.RemoveObject(this);
            }
        }
    }

    private void OnEnable()
    {
        // Inicializar la interacción
        Sleep();
    }

    // Iniciar interacción (puede ser vacío si no hay acciones iniciales)
    private void Sleep()
    {
        
    }

    // Evaluar la interacción entre objetos con diferentes tags, evaluar colores de contenedores
    private void Interact(string T, GameObject obj)
    {
        txt = GameObject.FindWithTag("fb_texto");
        text = txt.gameObject.GetComponent<TextMeshProUGUI>();

        if (T.Substring(0,2) == tag.Substring(0,2))
        {
            // Realizar interacción si las etiquetas coinciden
            if (T.Substring(0,2) == "bl")
            {
                tapa = GameObject.FindWithTag("modelObject");
            }
            else if(T.Substring(0,2) == "ye")
            {
                tapa = GameObject.FindWithTag("modelAmarillo");
            }
            else if(T.Substring(0,2) == "gr")
            {
                tapa = GameObject.FindWithTag("modelVerde");
            }
            // Evaluar si la tapa está abierta
            if(tapa.transform.localRotation.x != 0)
            {
                // Realizar acciones si la tapa está abierta
                if (interaction)
                {
                    return;
                }
                interaction = true;
                text.color = Color.green;
                msg = "¡Correcto!";
                StartCoroutine(msgWait(msg));
                if (obj.tag.Substring(3,1) == "o")
                {
                    StartCoroutine(pre(tapa, obj));
                    var counterText = GameObject.FindWithTag("counter_texto").GetComponent<TextMeshProUGUI>();
                    var counterNum = int.Parse(counterText.text);
                    ++counterNum;
                    counterText.text = counterNum.ToString();
                }
                /*else
                {
                    StartCoroutine(pre(tapa, this.gameObject));
                }*/
            }
            else
            {
                // Indicar que la tapa está cerrada
                text.color = Color.yellow;
                msg = "¡Cerca, pero primero abre la tapa!";
                StartCoroutine(msgWait(msg));
            }
        }
        else
        {
            // Indicar que la interacción no es correcta
            text.color = Color.red;
            msg = "¡Aquí No!";
            StartCoroutine(msgWait(msg));
        }
    }

    // Rutina de espera para mostrar mensajes
    private IEnumerator msgWait(string msg)
    {
        txt = GameObject.FindWithTag("fb_texto");
        text = txt.gameObject.GetComponent<TextMeshProUGUI>();
        text.text = msg;
        yield return new WaitForSeconds(3);
        text.text = "";
    }

    // Ejecutar para que animación dure dos segundos antes de desactivar el objeto (entra al contendedor)
    private IEnumerator pre(GameObject tapa, GameObject obj)
    {
        Coroutine co;
        var scale = obj.transform.localScale;
        var rotation = obj.transform.rotation;
        co = StartCoroutine(entra(obj));
        yield return new WaitForSeconds(1);
        StopCoroutine(co);
        obj.SetActive(false);
        obj.transform.localScale = scale;
        obj.transform.rotation = rotation;
        interaction = false;
    }

    // Animación de entrada (rotación continua)
    private IEnumerator entra(GameObject obj)
    {
        while(true)
        {
            obj.transform.Rotate(new Vector3(-12, 0, 0));
            var scale = obj.transform.localScale;
            obj.transform.localScale = new Vector3(scale.x * 0.98f, scale.y * 0.98f, scale.z * 0.98f);
            yield return new WaitForFixedUpdate();
        }
    }
}
