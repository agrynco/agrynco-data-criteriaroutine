#region Usings
using System;
using System.Collections.Generic;
#endregion

namespace AGrynCo.Data.CriteriaRoutine
{
    public class Criteria : BaseCriteria
    {
        #region Fields (private)
        private readonly List<BaseCriteria> _criteriaList;
        #endregion

        #region Methods (internal)
        internal override void Process(ICriteriaHandler criteriaHandler, int nestingLevel, int index)
        {
            criteriaHandler.BeginCriteria(this);
            for (int criteraIndex = 0; criteraIndex < _criteriaList.Count; criteraIndex++)
            {
                _criteriaList[criteraIndex].Process(criteriaHandler, nestingLevel + 1, criteraIndex);
            }
            criteriaHandler.EndCriteria();
        }
        #endregion

        #region Constructors
        public Criteria()
            : base(LogicalOperator.None)
        {
            _criteriaList = new List<BaseCriteria>();
        }

        public Criteria(string parameter)
            : this(parameter, Clause.IsNull)
        {
        }

        public Criteria(params Criteria[] criterias)
            : this()
        {
            _criteriaList.AddRange(criterias);
        }

        public Criteria(string parameter, Clause clause)
            : this(parameter, clause, null)
        {
            switch (clause)
            {
                case Clause.IsNull:
                case Clause.IsNotNull:
                    break;
                default:
                    throw new ArgumentException(string.Format("Only {0} and {1} are allowed", Clause.IsNull, Clause.IsNotNull));
            }
        }

        public Criteria(string parameter, Clause clause, object value)
            : this()
        {
            var criteriaItem = new CriteriaItem(LogicalOperator.None, parameter, clause, value);
            _criteriaList.Add(criteriaItem);
        }

        public Criteria(LogicalOperator logicalOperator, string parameter, Clause clause, object value)
            : this(parameter, clause, value)
        {
            LogicalOperator = logicalOperator;
        }
        #endregion

        #region Methods (public)
        public Criteria And(string parameter, Clause clause, object value)
        {
            var criteriaItem = new CriteriaItem(LogicalOperator.And, parameter, clause, value);
            _criteriaList.Add(criteriaItem);
            return this;
        }

        public Criteria And(Criteria criteria)
        {
            criteria.LogicalOperator = LogicalOperator.And;
            _criteriaList.Add(criteria);
            return this;
        }

        public Criteria Or(string parameter, Clause clause, object value)
        {
            var criteriaItem = new CriteriaItem(LogicalOperator.Or, parameter, clause, value);
            _criteriaList.Add(criteriaItem);
            return this;
        }

        public Criteria Or(string parameter, Clause clause)
        {
            return Or(parameter, clause, null);
        }

        public Criteria Or(Criteria criteria)
        {
            criteria.LogicalOperator = LogicalOperator.Or;
            _criteriaList.Add(criteria);
            return this;
        }

        public void Process(ICriteriaHandler criteriaHandler)
        {
            Process(criteriaHandler, 0, 0);
        }
        #endregion
    }
}