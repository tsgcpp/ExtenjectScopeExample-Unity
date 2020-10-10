using Zenject;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ResultTypeGreeterZenjectConstructionTest : ZenjectUnitTestFixture
    {
        [Inject]
        IGreeter _target = default;

        [Test]
        public void WithArgumentsなしでBindしてInjectした場合()
        {
            /*
             [Inject]が指定された引数2つのコンストラクタが使用され、
             第1, 2引数はデフォルトの"Hello", "Everybody"

             コンストラクタにデフォルト引数がない場合は `Container.Inject(this)` で例外が飛ぶ
             */

            // setup, when
            Container
                .Bind<IGreeter>()
                .To<ResultTypeGreeter>()
                .AsTransient();  // テスト時はScopeの明示が必要
            Container.Inject(this);

            // then
            Assert.AreEqual("Hello Everybody!", _target.Greeting);
        }

        [Test]
        public void WithArgumentsで引数1つを指定してBindしてInjectした場合()
        {
            /*
             [Inject]が指定された引数2つのコンストラクタが使用され、
             第2引数はデフォルトの"Everybody"。

             コンストラクタにデフォルト引数がない場合は `Container.Inject(this)` で例外が飛ぶ
             */

            // setup, when
            Container
                .Bind<IGreeter>()
                .To<ResultTypeGreeter>()
                .AsTransient()  // テスト時はScopeの明示が必要
                .WithArguments("Hi");
            Container.Inject(this);

            // then
            Assert.AreEqual("Hi Everybody!", _target.Greeting);
        }

        [Test]
        public void WithArgumentsで引数2つを指定してBindしてInjectした場合()
        {
            /*
             [Inject]が指定された引数2つのコンストラクタが使用される
             */

            // setup, when
            Container
                .Bind<IGreeter>()
                .To<ResultTypeGreeter>()
                .AsTransient()  // テスト時はScopeの明示が必要
                .WithArguments("Hey", "Guys");
            Container.Inject(this);

            // then
            Assert.AreEqual("Hey Guys!", _target.Greeting);
        }
    }
}