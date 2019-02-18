using System.Linq.Expressions;
using System.Reflection;

namespace CostControl.BusinessLogic.Mapper.Helper
{
    public class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;
        private readonly Expression _replacement;

        private ParameterReplacer(ParameterExpression parameter, Expression replacement)
        {
            _parameter = parameter;
            _replacement = replacement;
        }

        public static Expression Replace(Expression expression, ParameterExpression parameter, Expression replacement)
        {
            return new ParameterReplacer(parameter, replacement).Visit(expression);
        }

        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            if (parameter == _parameter)
            {
                return _replacement;
            }
            return base.VisitParameter(parameter);
        }
        
        ///////////////////////////////////////////////////
        protected override Expression VisitBinary(BinaryExpression node)
        {
            var memberLeft = node.Left as MemberExpression;
            if (memberLeft != null && memberLeft.Expression is ParameterExpression)
            {

                var f = Expression.Lambda(node.Right).Compile();
                var value = f.DynamicInvoke();
            }

            return base.VisitBinary(node);
        }

        protected override Expression VisitMember(MemberExpression memberExpression)
        {
            // Recurse down to see if we can simplify...
            var expression = Visit(memberExpression.Expression);

            // If we've ended up with a constant, and it's a property or a field,
            // we can simplify ourselves to a constant
            if (expression is ConstantExpression)
            {
                object container = ((ConstantExpression)expression).Value;
                var member = memberExpression.Member;
                if (member is FieldInfo)
                {
                    object value = ((FieldInfo)member).GetValue(container);
                    return Expression.Constant(value);
                }
                if (member is PropertyInfo)
                {
                    object value = ((PropertyInfo)member).GetValue(container, null);
                    return Expression.Constant(value);
                }
            }
            return base.VisitMember(memberExpression);
        }

        //protected override Expression VisitConstant(ConstantExpression node)
        //{
        //    MemberExpression prevNode;
        //    var val = node.Value;
        //    while ((prevNode = PreviousNode as MemberExpression) != null)
        //    {
        //        var fieldInfo = prevNode.Member as FieldInfo;
        //        var propertyInfo = prevNode.Member as PropertyInfo;

        //        if (fieldInfo != null)
        //            val = fieldInfo.GetValue(val);
        //        if (propertyInfo != null)
        //            val = propertyInfo.GetValue(val);
        //        Nodes.Pop();
        //    }
        //    // we got the value
        //    // now val = constant we was looking for

        //    return node;
        //}
    }
}