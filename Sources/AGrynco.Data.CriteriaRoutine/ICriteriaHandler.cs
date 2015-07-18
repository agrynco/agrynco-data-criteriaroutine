namespace AGrynCo.Data.CriteriaRoutine
{
    public interface ICriteriaHandler
    {
        #region Abstract Methods
        void BeginCriteria(Criteria criteria);

        void EndCriteria();

        void Process(CriteriaItem criteriaItem, int nestingLevel, int index);

        void Reset();
        #endregion
    }
}