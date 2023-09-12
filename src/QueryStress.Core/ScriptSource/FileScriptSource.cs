using QueryStress.Core.Interfaces;

namespace QueryStress.Core.ScriptSource
{
    public class FileScriptSource : IScriptSource
    {
        private readonly string _path;

        public FileScriptSource(string path)
        {
            _path = path;
        }

        public async Task<IScript> GetScriptAsync(CancellationToken cancellationToken)
        {
            return new TextScript(await File.ReadAllTextAsync(_path, cancellationToken));
        }
    }
}
