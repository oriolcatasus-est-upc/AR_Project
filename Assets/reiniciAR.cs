using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
    
public class reiniciAR : MonoBehaviour {
    
    public void reiniciARte() {
        //reinicia la escena actual
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
    
}
