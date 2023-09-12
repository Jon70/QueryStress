using System.Reflection;
using Autofac;
using QueryStress.Core.Attributes;

namespace QueryStress.App;

public class ApplicationLoader
{
    private void LoadPlugins(ContainerBuilder builder)
    {
        var dir = new FileInfo(AppContext.BaseDirectory).Directory;
        var dlls = dir?.GetFiles("*.dll");
        var asms = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var dll in dlls.Where(x => IsSuitable(x.FullName)))
        {
            var defaultContext = System.Runtime.Loader.AssemblyLoadContext.Default;
            var loaded = asms.FirstOrDefault(x => string.Equals(x.Location, dll.FullName, StringComparison.InvariantCultureIgnoreCase));

            if (loaded == null)
            {
                defaultContext.LoadFromAssemblyPath(dll.FullName);
            }
        }

        asms = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var asm in asms.Where(x=> x.GetCustomAttribute<QueryStressPluginAttribute>() != null))
        {
            Console.WriteLine(asm.FullName);
            builder.RegisterAssemblyModules(asm);
        }
    }

    private static bool IsSuitable(string path)
    {
        try
        {
            var type = typeof(QueryStressPluginAttribute);
            var asm = Mono.Cecil.AssemblyDefinition.ReadAssembly(path);

            return asm
                .CustomAttributes
                .Any(atr => atr.AttributeType.Name == type.Name && atr.AttributeType.Namespace == type.Namespace);
        }
        catch
        {
            return false;
        }
    }

    public virtual ContainerBuilder Load(ContainerBuilder builder)
    {
        LoadPlugins(builder);
        builder.RegisterModule<AppModule>();

        return builder;
    }
}