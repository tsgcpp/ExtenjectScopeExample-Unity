using Zenject;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AsTransientTest : ZenjectUnitTestFixture
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
        public void AsTransient_インスタンスがDI先のクラスごとに異なること()
        {
            // setup, when
            Container
                .Bind<IGreeter>()
                .To<ResultTypeGreeter>()
                .AsTransient();
            Container
                .Bind<IGreeter>()
                .WithId(Identifier01)
                .To<ResultTypeGreeter>()
                .AsTransient();
            Container
                .Bind<IGreeter>()
                .WithId(Identifier02)
                .To<ResultTypeGreeter>()
                .AsTransient();
            Container.Inject(this);

            // then
            Assert.NotNull(_targetMain);  // インジェクションされていることの確認
            Assert.AreNotEqual(_targetMain, _targetSub);
            Assert.AreNotEqual(_targetMain, _targetId01);
            Assert.AreNotEqual(_targetMain, _targetId02);
        }
    }
}