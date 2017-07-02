#region using

using Irony.Compiler;
using ScriptNET.Runtime;

#endregion using

namespace ScriptNET.Ast
{
    /// <summary>
    /// Script Array Constructor Expression
    /// </summary>
    internal class ScriptArrayConstructor : ScriptExpr
    {
        private ScriptExprList exprList;

        public ScriptArrayConstructor(AstNodeArgs args)
            : base(args)
        {
            exprList = (ScriptExprList)ChildNodes[0];
        }

        public override void Evaluate(IScriptContext context)
        {
            exprList.Evaluate(context);
        }
    }
}