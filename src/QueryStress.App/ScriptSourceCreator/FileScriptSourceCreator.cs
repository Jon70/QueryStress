using QueryStress.App.Arguments;
using QueryStress.App.Interfaces;
using QueryStress.Core.Interfaces;
using QueryStress.Core.ScriptSource;

namespace QueryStress.App.ScriptSourceCreator
{
    public class FileScriptSourceCreator : ICreator<IScriptSource>
    {
        public string TypeName => "file";
        public IScriptSource Create(ArgumentsSection argumentsSection)
        {
            return new FileScriptSource(argumentsSection.ExtractStringArgumentOrThrow("path"));
        }
    }
}
