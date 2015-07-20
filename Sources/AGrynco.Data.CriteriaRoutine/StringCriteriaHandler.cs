#region Usings
using System.Collections.Generic;
using System.Text;
using AGrynco.Lib.ToStringConverters;
#endregion

namespace AGrynCo.Data.CriteriaRoutine
{
    public class StringCriteriaHandler : ICriteriaHandler
    {
        #region Constructors
        public StringCriteriaHandler(ToStringConverter valueToStringConverter)
        {
            strClauses = new Dictionary<Clause, string>();
            strLogicalOperators = new Dictionary<LogicalOperator, string>();
            this.valueToStringConverter = valueToStringConverter;
            RegisterAllClauses();
            RegisterAllLogicalOperators();
            expression = new StringBuilder();
        }
        #endregion

        #region Properties (public)
        public string Expression
        {
            get
            {
                return expression.ToString();
            }
        }
        #endregion

        #region Constants
        private const char _LEFT_ROUND_BRACKET = '(';

        private const char _RIGHT_ROUND_BRACKET = ')';

        private const char _SPACE = ' ';
        #endregion

        #region Fields (private)
        private readonly StringBuilder expression;

        private readonly Dictionary<Clause, string> strClauses;

        private readonly Dictionary<LogicalOperator, string> strLogicalOperators;

        private readonly ToStringConverter valueToStringConverter;
        #endregion

        #region ICriteriaHandler Methods
        public void BeginCriteria(Criteria criteria)
        {
            AppendLogicaloperator(criteria.LogicalOperator);
            expression.Append(_LEFT_ROUND_BRACKET);
        }

        public void EndCriteria()
        {
            expression.Append(_RIGHT_ROUND_BRACKET);
        }

        public void Process(CriteriaItem criteriaItem, int nestingLevel, int index)
        {
            AppendLogicaloperator(criteriaItem.LogicalOperator);
            expression.Append(GetColumnName(criteriaItem));
            expression.Append(_SPACE);
            expression.Append(strClauses[criteriaItem.Clause]);
            switch (criteriaItem.Clause)
            {
                case Clause.IsNotNull:
                    break;
                case Clause.IsNull:
                    break;
                default:
                    expression.Append(_SPACE);
                    expression.Append(GetValue(criteriaItem, nestingLevel, index));
                    break;
            }
        }

        public virtual void Reset()
        {
            expression.Length = 0;
        }
        #endregion

        #region Methods (private)
        private void AppendLogicaloperator(LogicalOperator logicalOperator)
        {
            if (logicalOperator != LogicalOperator.None)
            {
                expression.Append(_SPACE);
            }
            expression.Append(strLogicalOperators[logicalOperator]);
            if (logicalOperator != LogicalOperator.None)
            {
                expression.Append(_SPACE);
            }
        }

        private void RegisterAllClauses()
        {
            RegisterClause(Clause.Eq, "=");
            RegisterClause(Clause.Grate, ">");
            RegisterClause(Clause.IsNull, "IS NULL");
            RegisterClause(Clause.Less, "<");
            RegisterClause(Clause.Like, "LIKE");
            RegisterClause(Clause.NotEq, "<>");
            RegisterClause(Clause.NotGrate, "<=");
            RegisterClause(Clause.IsNotNull, "IS NOT NULL");
            RegisterClause(Clause.NotLess, ">=");
            RegisterClause(Clause.NotLike, "NOT LIKE");
            RegisterClause(Clause.NotIn, "NOT IN");
            RegisterClause(Clause.In, "IN");
        }

        private void RegisterAllLogicalOperators()
        {
            RegisterLogicalOperator(LogicalOperator.None, string.Empty);
            RegisterLogicalOperator(LogicalOperator.And, "AND");
            RegisterLogicalOperator(LogicalOperator.Or, "OR");
        }

        private void RegisterClause(Clause clause, string value)
        {
            strClauses.Add(clause, value);
        }

        private void RegisterLogicalOperator(LogicalOperator logicalOperator, string value)
        {
            strLogicalOperators.Add(logicalOperator, value);
        }
        #endregion

        #region Methods (protected)
        protected virtual string GetColumnName(CriteriaItem criteriaItem)
        {
            return criteriaItem.Parameter;
        }

        protected virtual string GetValue(CriteriaItem criteriaItem, int nestingLevel, int index)
        {
            return valueToStringConverter.Convert(criteriaItem.Value);
        }
        #endregion
    }
}