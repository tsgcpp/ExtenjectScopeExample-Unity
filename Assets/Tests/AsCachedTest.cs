using Zenject;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AsCachedTest : ZenjectUnitTestFixture
    {
        private const string Identifier01 = "ID_01";
        private const string Identifier02 = "ID_02";

        [Inject]
        private IGreeter _targetMain = default;

        [Inject]
        private IGreeter _targetSub = default;

        [Inject(Id = Identifier01)]
        private IGreeter _targetId01 = default;

        [Inject(Id = Identifier01)]
        private IGreeter _targetId01Ex = default;

        [Inject(Id = Identifier02)]
        private IGreeter _targetId02 = default;

        [Inject(Id = Identifier02)]
        private IGreeter _targetId02Ex = default;

        [SetUp]
        public void SetUp()
        {
            _targetMain = null;
            _targetSub = null;
            _targetId01 = null;
            _targetId01Ex = null;
            _targetId02 = null;
            _targetId02Ex = null;
        }

        [Test]
        public void AsCached_インスタンスがIDごとに同一であること()
        {
            // setup, when
            Container
                .Bind<IGreeter>()
                .To<ResultTypeGreeter>()
                .AsCached();
            Container
                .Bind<IGreeter>()
                .WithId(Identifier01)
                .To<ResultTypeGreeter>()
                .AsCached();
            Container
                .Bind<IGreeter>()
                .WithId(Identifier02)
                .To<ResultTypeGreeter>()
                .AsCached();
            Container.Inject(this);

            // then

            // IDなし(デフォルト)
            Assert.NotNull(_targetMain);
            Assert.AreEqual(_targetMain, _targetSub);  // ID指定なしで同じインスタンス
            Assert.AreNotEqual(_targetMain, _targetId01);
            Assert.AreNotEqual(_targetMain, _targetId02);

            // ID_01
            Assert.NotNull(_targetId01);
            Assert.AreEqual(_targetId01, _targetId01Ex);  // 同一IDでは同じインスタンス
            Assert.AreNotEqual(_targetId01, _targetId02);

            // ID_02
            Assert.NotNull(_targetId02);
            Assert.AreEqual(_targetId02, _targetId02Ex);  // 同一IDでは同じインスタンス
        }
    }
}