namespace AGrynCo.Data.CriteriaRoutine
{
    public abstract class BaseCriteria
    {
        #region Constructors
        protected BaseCriteria(LogicalOperator logicalOperator)
        {
            LogicalOperator = logicalOperator;
        }
        #endregion

        #region Properties (public)
        public LogicalOperator LogicalOperator { get; protected set; }
        #endregion

        #region Abstract Methods
        internal abstract void Process(ICriteriaHandler criteriaHandler, int nestingLevel, int index);
        #endregion
    }
}