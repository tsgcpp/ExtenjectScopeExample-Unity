using Zenject;

/// <summary>
/// ResultType としてのサンプルIGreeter継承クラス
/// </summary>
public class ResultTypeGreeter : IGreeter
{
    private string _greeting;

    /// <summary>
    /// 引数なしコンストラクタ
    /// </summary>
    public ResultTypeGreeter() : this("Good Afternoon")
    {
    }

    /// <summary>
    /// messageのみ指定するコンストラクタ
    /// </summary>
    public ResultTypeGreeter(string message) : this(message, "Ladies and gentlemen")
    {
    }

    /// <summary>
    /// messageとtargetを指定するコンストラクタ
    /// </summary>
    /*
     ZenjectによるDI対象がコンストラクターを複数持っている場合、
     [Inject]を指定して、どのコンストラクタを使用するかをZenjectに教える必要あり。
     */
    [Inject]
    public ResultTypeGreeter(string message = "Hello", string target = "Everybody")
    {
        _greeting = string.Format("{0} {1}!", message, target);
    }

    /*
    // デフォルト引数なし
    [Inject]
    public ResultTypeGreeter(string message, string target)
    {
        _greeting = string.Format("{0} {1}!", message, target);
    }
     */

    public string Greeting => _greeting;
}
