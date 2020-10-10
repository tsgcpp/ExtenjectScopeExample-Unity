using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ResultTypeGreeterTest
    {
        [Test]
        public void コンストラクタ_引数なし()
        {
            // setup, when
            var target = new ResultTypeGreeter();
            
            // then
            Assert.AreEqual("Good Afternoon Ladies and gentlemen!", target.Greeting);
        }

        [Test]
        public void コンストラクタ_引数1つ()
        {
            // setup, when
            var target = new ResultTypeGreeter("Hi");
            
            // then
            Assert.AreEqual("Hi Ladies and gentlemen!", target.Greeting);
        }

        [Test]
        public void コンストラクタ_引数2つ()
        {
            // setup, when
            var target = new ResultTypeGreeter("Hey", "Guys");
            
            // then
            Assert.AreEqual("Hey Guys!", target.Greeting);
        }
    }
}
