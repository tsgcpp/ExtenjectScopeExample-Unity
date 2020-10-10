using System.Collections.Generic;
using Zenject;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class QueueInjectionExampleTest : ZenjectUnitTestFixture
    {
        private const string Group01 = "GROUP_01";
        private const string Group02 = "GROUP_02";

        [Inject(Id = Group01)]
        private Queue<string> _targetMessageQueue01 = null;

        [Inject(Id = Group02)]
        private Queue<string> _targetMessageQueue02 = null;

        [Inject]
        private DITarget01 _target01 = null;

        [Inject]
        private DITarget01Ex _target01Ex = null;

        [Inject]
        private DITarget02 _target02 = null;

        [Test]
        public void AsCached_CollectionインスタンスがDI先で異なること()
        {
            // setup
            Container
                .Bind<Queue<string>>()  // Queueは残念ながら専用interfaceなし
                .WithId(Group01)
                // .To<Queue<string>>()  // ContractTypeとResultTypeが同じならToは不要
                .AsCached();
            Container
                .Bind<Queue<string>>()
                .WithId(Group02)
                .AsCached();

            Container
                .Bind<DITarget01>()
                .AsSingle();  // DI先のクラスのScopeは気にしない(確認用のため何でも良い)
            Container
                .Bind<DITarget01Ex>()
                .AsSingle();  // DI先のクラスのScopeは気にしない(確認用のため何でも良い)
            Container
                .Bind<DITarget02>()
                .AsSingle();
            
            Container.Inject(this);

            // when
            _target01.Enqueue();
            _target01Ex.Enqueue();
            _target02.Enqueue();

            // then
            Assert.AreEqual(2, _targetMessageQueue01.Count);
            Assert.AreEqual(1, _targetMessageQueue02.Count);

            // GROUP_01
            {
                var message = _targetMessageQueue01.Dequeue();
                Assert.AreEqual("Hello", message);
            }
            {
                var message = _targetMessageQueue01.Dequeue();
                Assert.AreEqual("Hey!", message);
            }

            // GROUP_02
            {
                var message = _targetMessageQueue02.Dequeue();
                Assert.AreEqual("Hi...", message);
            }
        }

        /// <summary>
        /// GROUP_01用
        /// </summary>
        public class DITarget01
        {
            private Queue<string> _queue;

            // MonoBehaviourでない場合はIdは引数で指定
            public DITarget01([Inject(Id = Group01)] Queue<string> queue)
            {
                _queue = queue;
            }

            public void Enqueue()
            {
                _queue.Enqueue("Hello");
            }
        }

        /// <summary>
        /// GROUP_01用
        /// </summary>
        public class DITarget01Ex
        {
            private Queue<string> _queue;

            public DITarget01Ex([Inject(Id = Group01)] Queue<string> queue)
            {
                _queue = queue;
            }

            public void Enqueue()
            {
                _queue.Enqueue("Hey!");
            }
        }

        /// <summary>
        /// GROUP_02用
        /// </summary>
        public class DITarget02
        {
            private Queue<string> _queue;

            public DITarget02([Inject(Id = Group02)] Queue<string> queue)
            {
                _queue = queue;
            }

            public void Enqueue()
            {
                _queue.Enqueue("Hi...");
            }
        }
    }
}