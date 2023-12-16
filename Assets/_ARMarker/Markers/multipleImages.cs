using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class multipleImages : MonoBehaviour
{
    // Arreglo de objetos que se instanciarán cuando se detecten las imágenes
    [SerializeField] GameObject[] prefabsToSpawn;

    // Referencia al ARTrackedImageManager
    private ARTrackedImageManager _arTrackedImageManager;
    
    // Diccionario para mapear nombres de objetos a GameObjects asociados con las imágenes
    private Dictionary<string, GameObject> _arObjects;

    // Obtener referencia de ARTrackedImageManager
    private void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        _arObjects = new Dictionary<string, GameObject>();
    }

    // Identificar cambios en TrackedImageManager
    private void Start()
    {
        // Suscribirse al evento de cambios en las imágenes rastreadas
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;

        // Instanciar objetos asociados a imágenes y ocultarlos
        foreach (GameObject prefab in prefabsToSpawn)
        {
            GameObject newARObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newARObject.name = prefab.name;
            newARObject.gameObject.SetActive(false);
            _arObjects.Add(newARObject.name, newARObject);
        }
    }

    // Desuscribirse del evento al destruir el objeto
    private void OnDestroy()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    // Manejar cambios en las imágenes rastreadas
    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateTrackedImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateTrackedImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            // Descomentar para ocultar objetos asociados a imágenes eliminadas, comentado para que permanezcan en la escena
            //_arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
        }
    }

    // Actualizar la posición y visibilidad de los objetos asociados a las imágenes rastreadas
    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if(trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
            // Ocultar objetos si el estado de seguimiento es "Limitado" o "Ninguno", comentado también para que permamezcan.
            //_arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
            return;
        }
        if(prefabsToSpawn != null)
        {
            // Mostrar objetos y actualizar su posición según la posición de la imagen rastreada
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
            _arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
        }
    }
}
