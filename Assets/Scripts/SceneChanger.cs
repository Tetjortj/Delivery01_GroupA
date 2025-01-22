using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Campo para definir el nombre de la escena desde el Inspector

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
