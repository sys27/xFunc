using xFunc.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace xFunc.Test
{    
    
    /// <summary>
    ///Это класс теста для TokenTest, в котором должны
    ///находиться все модульные тесты TokenTest
    ///</summary>
    [TestClass()]
    public class TokenTest
    {

        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты теста
        // 
        //При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        //ClassInitialize используется для выполнения кода до запуска первого теста в классе
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //TestInitialize используется для выполнения кода перед запуском каждого теста
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //TestCleanup используется для выполнения кода после завершения каждого теста
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///Тест сравнения объектов.
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            Token expected = new Token(TokenType.Addition); 
            Token actual = new Token(TokenType.Addition);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Тест метода Equals, не сходство объектов.
        /// </summary>
        [TestMethod]
        public void NotEqualsTest()
        {
            Token expected = new Token(TokenType.Number, 10);
            Token actual = new Token(TokenType.Number, 20);

            Assert.AreNotEqual(expected, actual);
        }

    }

}
