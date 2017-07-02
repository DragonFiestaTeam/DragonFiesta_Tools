#region using

using Irony.Compiler;
using ScriptNET.Runtime;
using System;

#endregion using

namespace ScriptNET.Ast
{
    /// <summary>
    ///
    /// </summary>
    internal class ScriptAstTemplate : ScriptAst
    {
        public ScriptAstTemplate(AstNodeArgs args)
          : base(args)
        {
        }

        public override void Evaluate(IScriptContext context)
        {
            throw new NotSupportedException();
        }
    }
}