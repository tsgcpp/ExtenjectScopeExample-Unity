using Zenject;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AsSingleTest : ZenjectUnitTestFixture
    {
        private const string Identifier01 = "ID_01";
        private const string Identifier02 = "ID_02";

        [Inject]
        private IGreeter _targetMain = default;

        [Inject]
        private IGreeter _targetSub = default;

        [Inject(Id = Identifier01)]
        private IGreeter _targetId01 = default;

        [Inject(Id = Identifier02)]
        private IGreeter _targetId02 = default;

        [SetUp]
        public void SetUp()
        {
            _targetMain = null;
            _targetSub = null;
            _targetId01 = null;
            _targetId02 = null;
        }

        [Test]
        public void AsSingle_ResultTypeが複数登録される場合に例外を出すこと()
        {
            // setup
            Container
                .Bind<IGreeter>()
                .To<ResultTypeGreeter>()
                .AsSingle();
            Container  
                .Bind<IGreeter>()
                .WithId(Identifier01)
                .To<ResultTypeGreeter>()
                .AsSingle();

            // when, then
            Assert.Throws<ZenjectException>(() => {
                Container.Inject(this);
            });
        }

        [Test]
        public void AsSingle_ResultTypeがAsTransientでも登録される場合に例外を出すこと()
        {
            // setup
            Container
                .Bind<IGreeter>()
                .To<ResultTypeGreeter>()
                .AsSingle();
            Container  
                .Bind<IGreeter>()
                .WithId(Identifier01)
                .To<ResultTypeGreeter>()
                .AsTransient();

            // when, then
            Assert.Throws<ZenjectException>(() => {
                Container.Inject(this);
            });
        }

        [Test]
        public void AsSingle_ResultTypeがAsTransientでID指定無しでも登録される場合に例外を出すこと()
        {
            // setup
            Container
                .Bind<IGreeter>()
                .To<ResultTypeGreeter>()
                .AsSingle();
            Container  
                .Bind<IGreeter>()
                .To<ResultTypeGreeter>()
                .AsTransient();

            // when, then
            Assert.Throws<ZenjectException>(() => {
                Container.Inject(this);
            });
        }

        [Test]
        public void AsSingle_ResultTypeがAsCachedでも登録される場合に例外を出すこと()
        {
            // setup
            Container
                .Bind<IGreeter>()
                .To<ResultTypeGreeter>()
                .AsSingle();
            Container  
                .Bind<IGreeter>()
                .WithId(Identifier01)
                .To<ResultTypeGreeter>()
                .AsCached();

            // when, then
            Assert.Throws<ZenjectException>(() => {
                Container.Inject(this);
            });
        }

        [Test]
        public void AsSingle_ResultTypeがAsCachedでID指定無しでも登録される場合に例外を出すこと()
        {
            // setup
            Container
                .Bind<IGreeter>()
                .To<ResultTypeGreeter>()
                .AsSingle();
            Container  
                .Bind<IGreeter>()
                .To<ResultTypeGreeter>()
                .AsCached();

            // when, then
            Assert.Throws<ZenjectException>(() => {
                Container.Inject(this);
            });
        }
    }
}