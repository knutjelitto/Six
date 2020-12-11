﻿// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SixPeg.Expressions
{
    using System;

    /// <summary>
    /// Represents an expression with a specific type.
    /// </summary>
    public class TypedExpression : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypedExpression"/> class.
        /// </summary>
        /// <param name="type">The specific type of the wrapped expression.</param>
        /// <param name="expression">The wrapped expression.</param>
        public TypedExpression(CodeSpan type, Expression expression)
        {
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
            this.Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        /// <summary>
        /// Gets the wrapped expression.
        /// </summary>
        public Expression Expression { get; }

        /// <summary>
        /// Gets the specific type of the wrapped expression.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Since 'GetType' is defined on the root object type, this is not a confusing usage.")]
        public CodeSpan Type { get; }
    }
}
