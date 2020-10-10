using UnityEngine;
using Zenject;

public class GreeterLogger : MonoBehaviour
{
    [Inject]
    private IGreeter _greeter;

    void Start()
    {
        Debug.Log("Greeter: " + _greeter.Greeting);
    }
}
