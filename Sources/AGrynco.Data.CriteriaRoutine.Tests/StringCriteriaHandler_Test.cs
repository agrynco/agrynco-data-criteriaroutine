#region Usings
using AGrynco.Lib.ToStringConverters;
using AGrynCo.Data.CriteriaRoutine;
using NUnit.Framework;
#endregion

namespace AGrynco.Data.CriteriaRoutine.Tests
{
    /// <summary>
    ///This is a test class for StringCriteriaHandler_Test and is intended
    ///to contain all StringCriteriaHandler_Test Unit Tests
    ///</summary>
    [TestFixture]
    public class StringCriteriaHandler_Test
    {
        #region Methods (public)
        /// <summary>
        ///A test for Process
        ///</summary>
        [Test]
        public void Process()
        {
            // (A = 5 AND ((A > 4 OR A < 5) AND (B <> 7 OR C IS NOT NULL)) AND S LIKE 'qwert' OR S NOT LIKE 'qwerty' AND A <= 9 AND Q >= 8)
            Criteria criteria = new Criteria("A", Clause.Eq, 5);
            Criteria criteria1 = new Criteria("A", Clause.Grate, 4).Or("A", Clause.Less, 5);
            Criteria criteria2 = new Criteria(LogicalOperator.And, "B", Clause.NotEq, 7).Or("C", Clause.IsNotNull);
            criteria.And(new Criteria(criteria1, criteria2));
            criteria.And("S", Clause.Like, "qwert");
            criteria.Or("S", Clause.NotLike, "qwerty");
            criteria.And("A", Clause.NotGrate, 9).And("Q", Clause.NotLess, 8);
            criteria.And("E", Clause.NotIn, new[] { 1, 2, 3 });

            StringCriteriaHandler criteriaHandler = new StringCriteriaHandler(ToStringConverter.Instance);
            criteria.Process(criteriaHandler);
            const string EXPECTED = "(A = 5 AND ((A > 4 OR A < 5) AND (B <> 7 OR C IS NOT NULL)) AND S LIKE 'qwert' OR S NOT LIKE 'qwerty' AND A <= 9 AND Q >= 8 AND E NOT IN (1, 2, 3))";
            string actual = criteriaHandler.Expression;
            Assert.AreEqual(EXPECTED, actual);
        }
        #endregion
    }
}