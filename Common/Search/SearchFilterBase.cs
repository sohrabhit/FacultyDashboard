using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Common.Search
{
    public abstract class SearchFilterBase<T> //: ValidatableViewModelBase
    {
        protected SearchFilterBase()
        {
            var isemptyOp = new Operator("تهی", (expression, expression1) => Expression.Call(expression, typeof(string).GetMethod("IsNullOrWhiteSpace"), null), Operator.TypesToApply.String);
            var notisemptyOp = new Operator("تهی نباشد", (expression, expression1) =>
            {
                var contain = Expression.Call(expression, typeof(string).GetMethod("IsNullOrWhiteSpace"), null);
                return Expression.Not(contain);
            }, Operator.TypesToApply.String);
            var startswithOp = new Operator("شروع با", (expression, expression1) =>
                Expression.Call(expression, typeof(string).GetMethod("StartsWith"), expression1), Operator.TypesToApply.String);
            var endswithOp = new Operator("خاتمه با", (expression, expression1) =>
                Expression.Call(expression, typeof(string).GetMethod("EndsWith"), expression1), Operator.TypesToApply.String);
            var containOp = new Operator("شامل باشد", (expression, expression1) =>
                Expression.Call(expression, typeof(string).GetMethod("Contains"), expression1), Operator.TypesToApply.String);
            var notContainOp = new Operator("شامل نباشد", (expression, expression1) =>
            {
                var contain = Expression.Call(expression, typeof(string).GetMethod("Contains"), expression1);
                return Expression.Not(contain);
            }, Operator.TypesToApply.String);
            var equalOp = new Operator("=", System.Linq.Expressions.Expression.Equal, Operator.TypesToApply.Both);
            var notEqualOp = new Operator("<>", System.Linq.Expressions.Expression.NotEqual, Operator.TypesToApply.Both);
            var lessThanOp = new Operator("<", System.Linq.Expressions.Expression.LessThan, Operator.TypesToApply.Numeric);
            var greaterThanOp = new Operator(">", System.Linq.Expressions.Expression.GreaterThan, Operator.TypesToApply.Numeric);
            var lessThanOrEqual = new Operator("<=", System.Linq.Expressions.Expression.LessThanOrEqual, Operator.TypesToApply.Numeric);
            var greaterThanOrEqual = new Operator(">=", System.Linq.Expressions.Expression.GreaterThanOrEqual, Operator.TypesToApply.Numeric);

            Operators = new ObservableCollection<Operator>
                {
                      isemptyOp,
                      notisemptyOp,
                      startswithOp,
                      endswithOp,
                      equalOp,
                      notEqualOp,
                      containOp,
                      notContainOp,
                      lessThanOp,
                      greaterThanOp,
                      lessThanOrEqual,
                      greaterThanOrEqual,
                };


            SelectedAndOr = AndOrs.FirstOrDefault(a => a.Name == "Suppress");
            SelectedFeild = Feilds.FirstOrDefault();
            SelectedOperator = Operators.FirstOrDefault(a => a.Title == "=");
        }

        public abstract IQueryable<T> GetQuarable(/*IUnitOfWork _uow*/);

        public virtual ObservableCollection<AndOr> AndOrs
        {
            get
            {
                return new ObservableCollection<AndOr>
                    {
                        new AndOr("And","و", System.Linq.Expressions.Expression.AndAlso),
                        new AndOr("Or","یا",System.Linq.Expressions.Expression.OrElse),
                        new AndOr("Suppress","نادیده",(expression, expression1) => expression),
                    };
            }
        }
        public virtual ObservableCollection<Operator> Operators
        {
            get { return _operators; }
            set
            {
                _operators = value; //RaisePropertyChanged("Operators");
            }
        }
        public abstract ObservableCollection<Feild> Feilds { get; }

        public bool IsOtherFilters
        {
            get { return _isOtherFilters; }
            set { _isOtherFilters = value; }
        }
        public string SearchValue
        {
            get { return _searchValue; }
            set
            {
                if (value == null)
                {
                    //MessageBox.Show("no null allow");
                    return;
                }
                else
                    _searchValue = value;
                //RaisePropertyChanged("SearchValue");
            }
        }
        public AndOr SelectedAndOr
        {
            get { return _selectedAndOr; }
            set { _selectedAndOr = value; /*RaisePropertyChanged("SelectedAndOr"); RaisePropertyChanged("SelectedFeildHasSetted");*/ }
        }
        public Operator SelectedOperator
        {
            get { return _selectedOperator; }
            set { _selectedOperator = value; /*RaisePropertyChanged("SelectedOperator");*/ }
        }
        public Feild SelectedFeild
        {
            get { return _selectedFeild; }
            set
            {
                Operators = value.Type == typeof(string) ? new ObservableCollection<Operator>
                    (Operators.Where(a => a.TypeToApply == Operator.TypesToApply.Both ||
                        a.TypeToApply == Operator.TypesToApply.String)) :
                        new ObservableCollection<Operator>(Operators.Where(
                            a => a.TypeToApply == Operator.TypesToApply.Both ||
                            a.TypeToApply == Operator.TypesToApply.Numeric));
                //MessageBox.Show("k");
                if (SelectedOperator == null)
                {
                    SelectedOperator = Operators.FirstOrDefault(a => a.Title == "=");
                }

                //RaisePropertyChanged("SelectedOperator");
                //RaisePropertyChanged("SelectedFeild");
                _selectedFeild = value;
                //RaisePropertyChanged("SelectedFeildHasSetted");
            }
        }
        public bool SelectedFeildHasSetted
        {
            get
            {
                return SelectedFeild != null &&
                       (SelectedAndOr.Name != "Suppress" || !IsOtherFilters);
            }
        }

        private ObservableCollection<Operator> _operators;
        private Feild _selectedFeild;
        private Operator _selectedOperator;
        private AndOr _selectedAndOr;
        private string _searchValue;
        private bool _isOtherFilters = true;
    }
}