#region using

using Irony.Compiler;
using ScriptNET.Runtime;
using System;

#endregion using

namespace ScriptNET.Ast
{
    /// <summary>
    /// Base Node for Expression
    /// </summary>
    internal class ScriptExpr : ScriptAst
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="args">arguments</param>
        public ScriptExpr(AstNodeArgs args)
          : base(args)
        {
        }

        protected internal static event EventHandler<HandleOperatorArgs> HandleOperator;

        protected static HandleOperatorArgs OnHandleOperator(object sender, IScriptContext context, string symbol, params object[] parameters)
        {
            HandleOperatorArgs args = new HandleOperatorArgs(context, symbol, parameters);

            if (HandleOperator != null)
                HandleOperator.Invoke(sender, args);

            return args;
        }
    }
}