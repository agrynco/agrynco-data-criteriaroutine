#region Usings
using System.Diagnostics;
#endregion

namespace AGrynCo.Data.CriteriaRoutine
{
    public class CriteriaItem : BaseCriteria
    {
        #region Constructors
        public CriteriaItem(LogicalOperator logicalOperator, string parameter, Clause clause, object value)
            : base(logicalOperator)
        {
            Clause newClause = clause;

            if (value == null)
            {
                if (clause == Clause.Eq)
                {
                    newClause = Clause.IsNull;
                }
            }
            this.clause = newClause;
            this.parameter = parameter;
            this.value = value;
        }
        #endregion

        #region Methods (internal)
        internal override void Process(ICriteriaHandler criteriaHandler, int nestingLevel, int index)
        {
            criteriaHandler.Process(this, nestingLevel, index);
        }
        #endregion

        #region Fields (private)
        private readonly Clause clause;

        private readonly string parameter;

        private readonly object value;
        #endregion

        #region Properties (public)
        public Clause Clause
        {
            [DebuggerStepThrough]
            get
            {
                return clause;
            }
        }

        public string Parameter
        {
            [DebuggerStepThrough]
            get
            {
                return parameter;
            }
        }

        public object Value
        {
            [DebuggerStepThrough]
            get
            {
                return value;
            }
        }
        #endregion
    }
}