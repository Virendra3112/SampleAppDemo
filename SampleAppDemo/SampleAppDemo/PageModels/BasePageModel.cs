using FreshMvvm;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SampleAppDemo.PageModels
{
    public class BasePageModel : FreshBasePageModel, INotifyPropertyChanged, IDisposable
    {

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #region Raise Property Changed        
        public void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpr)
        {
            var propertyName = GetMemberInfo(propertyExpr).Name;
            RaisePropertyChangedExplicit(propertyName);
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            RaisePropertyChangedExplicit(propertyName);
        }


      
        protected void RaisePropertyChangedExplicit(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion

        #region Set Property Value

       
        protected bool SetPropertyValue<T>(ref T storageField, T newValue, Expression<Func<T>> propertyExpr)
        {
            if (Equals(storageField, newValue))
                return false;

            storageField = newValue;

            var propertyName = GetMemberInfo(propertyExpr).Name;
            RaisePropertyChangedExplicit(propertyName);

            return true;
        }

       
        protected bool SetPropertyValue<T>(ref T storageField, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(storageField, newValue))
                return false;

            storageField = newValue;
            RaisePropertyChangedExplicit(propertyName);

            return true;
        }

        #endregion

        #region Helper Methods

        private MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression operand;
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            if (lambdaExpression.Body as UnaryExpression != null)
            {
                UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
                operand = (MemberExpression)body.Operand;
            }
            else
            {
                operand = (MemberExpression)lambdaExpression.Body;
            }
            return operand.Member;
        }

        #endregion

        #endregion

        #region IDisposable implementation

        public virtual void Dispose()
        {
        }

        #endregion
    }
}
