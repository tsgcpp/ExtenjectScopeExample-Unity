using System.Collections.Generic;
using Zenject;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CollectionInjectionExampleTest : ZenjectUnitTestFixture
    {
        [Inject]
        private ICollection<int> _targetMainList = default;

        [Inject]
        private ICollection<int> _targetSubList = default;

        [Test]
        public void AsTransient_CollectionインスタンスがDI先で異なること()
        {
            // setup, when
            Container
                .Bind<ICollection<int>>()
                .To<List<int>>()
                .AsTransient();
            Container.Inject(this);

            _targetMainList.Add(123);

            // then
            Assert.AreEqual(1, _targetMainList.Count);
            Assert.AreEqual(0, _targetSubList.Count);  // Listが別物

            Assert.AreNotEqual(_targetMainList, _targetSubList);
        }

        [Test]
        public void AsTransient_ListではなくLinkedListをResultTypeとして登録()
        {
            // setup, when
            Container
                .Bind<ICollection<int>>()
                .To<LinkedList<int>>()
                .AsTransient();
            Container.Inject(this);

            _targetMainList.Add(123);

            // then
            Assert.AreEqual(1, _targetMainList.Count);
            Assert.AreEqual(0, _targetSubList.Count);  // Listが別物

            Assert.AreNotEqual(_targetMainList, _targetSubList);
        }

        [Test]
        public void AsCached_CollectionインスタンスがDI先が同じこと()
        {
            // setup, when
            Container
                .Bind<ICollection<int>>()
                .To<List<int>>()
                .AsCached();
            Container.Inject(this);

            _targetMainList.Add(123);

            // then
            Assert.AreEqual(1, _targetMainList.Count);
            Assert.AreEqual(1, _targetSubList.Count);

            var enumerator = _targetSubList.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual(123, enumerator.Current);

            Assert.AreEqual(_targetMainList, _targetSubList);
        }

        [Test]
        public void AsSingle_CollectionインスタンスがDI先が同じこと()
        {
            // setup, when
            Container
                .Bind<ICollection<int>>()
                .To<List<int>>()
                .AsSingle();
            Container.Inject(this);

            _targetMainList.Add(123);

            // then
            Assert.AreEqual(1, _targetMainList.Count);
            Assert.AreEqual(1, _targetSubList.Count);

            var enumerator = _targetSubList.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual(123, enumerator.Current);

            Assert.AreEqual(_targetMainList, _targetSubList);
        }
    }
}