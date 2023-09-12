using QueryStress.App.Arguments;
using QueryStress.App.Factories;
using QueryStress.App.Interfaces;
using QueryStress.App.ScriptSourceCreator;
using QueryStress.Core.Interfaces;
using QueryStress.Core.ScriptSource;
using System.Collections.Generic;
using Xunit;

namespace QueryStress.Tests
{
    public class ScriptSourceFactoryTests
    {
        private readonly SettingsFactory<IScriptSource> _factory;

        public ScriptSourceFactoryTests()
        {
            _factory = new SettingsFactory<IScriptSource>("script", new ICreator<IScriptSource>[]
            {
                new FileScriptSourceCreator()
            });
        }

        [Fact]
        public void Create_FileScriptSource_IsCreated()
        {
            var appArguments = new ApplicationArguments()
            {
                ["script"] = new()
                {
                    Type = "file",
                    Arguments = new Dictionary<string, string>()
                    {
                        ["path"] = "file.sql"
                    }
                }
            };

            Assert.IsType<FileScriptSource>(_factory.Create(appArguments));
        }
    }
}
