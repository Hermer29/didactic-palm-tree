using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class BootstrapRunner : MonoBehaviour
    {
        private void Awake()
        {
            if (Bootstrapper.Bootstrapped)
                return;

            SceneManager.LoadScene("BootScene");
        }
    }
}