using UnityEngine;
using Zenject;

public class GreeterInstaller : MonoInstaller
{
    [Tooltip("Greeterのコンストラクタに渡すmessage引数")]
    [SerializeField] private string message = "Hello";

    [Tooltip("Greeterのコンストラクタに渡すtarget引数")]
    [SerializeField] private string target = "Everyone";

    public override void InstallBindings()
    {
        Container
            .Bind<IGreeter>()
            .To<ResultTypeGreeter>()
            .AsSingle()
            .WithArguments(message, target);
    }
}