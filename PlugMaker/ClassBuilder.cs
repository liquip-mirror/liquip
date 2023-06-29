using System.Reflection;
using System.Text;

namespace PlugMaker;

public static class ClassBuilder
{
    public static Dictionary<Type, HashSet<MethodInfo>> classes = new();

    public static void Add(List<MethodInfo> methods)
    {
        foreach (var item in methods)
        {
            var classType = item.DeclaringType;
            if (!classes.ContainsKey(classType))
            {
                classes.Add(classType, new HashSet<MethodInfo>());
            }

            classes[classType].Add(item);
        }
    }

    public static void Build()
    {
        // foreach (var item in classes)
        // {
        //     Console.WriteLine("{0} | {1} | {2}: {3} {4}", 
        //     item.Key.FullName,
        //     item.Key.Namespace,
        //     item.Key.Name,
        //     string.Join(',', item.Value.Select(i => i.Name)),
        //     Environment.NewLine
        //     );
        // }
        var ii = 0;
        Directory.CreateDirectory("./output");
        Directory.Delete("./output", true);
        Directory.CreateDirectory("./output");

        foreach (var item in classes)
        {
            var sb = new StringBuilder();
            var savePath = "./output";
            var nameSpace = string.IsNullOrWhiteSpace(item.Key.Namespace) ? "" : $@".{item.Key.Namespace}";

            if (!string.IsNullOrWhiteSpace(item.Key.Namespace))
            {
                foreach (var p in item.Key.Namespace.Split('.'))
                {
                    savePath = savePath + '/' + p;
                    Directory.CreateDirectory(savePath);
                }
            }

            sb.AppendLine("using IL2CPU_Plug = IL2CPU.API.Attribs.Plug;");
            sb.AppendLine();

            sb.AppendLine(string.Format("namespace Zarlo.Cosmos.Plug{0};", item.Key.Namespace));
            sb.AppendLine();


            var classNames = item.Key.FullName.Split('.').Last().Split('+');

            savePath = $@"{savePath}/{item.Key.FullName.Split('.').Last()}.{ii++}.cs";

            for (var i = 0; i < classNames.Length; i++)
            {
                var className = classNames[i];
                if (i == classNames.Length - 1)
                {
                    sb.AppendLine($@"[IL2CPU_Plug( typeof({item.Key.FullName.Replace("+", ".")}) )]");
                }

                sb.AppendLine($@"public partial class {className}Plug");
                sb.AppendLine("{");
            }

            foreach (var method in item.Value)
            {
                MakeMethod(sb, method, item.Key.FullName.Replace("+", "."));
            }


            foreach (var className in classNames)
            {
                sb.AppendLine("}");
            }

            // Console.WriteLine(sb.ToString());
            File.WriteAllText(savePath, sb.ToString());
        }

        Console.WriteLine(ii);
    }


    public static string BuildParameter(ParameterInfo parameterInfo)
    {
        var sb = new StringBuilder();

        if (parameterInfo.IsIn)
        {
            sb.Append("in ");
        }

        if (parameterInfo.IsOut)
        {
            sb.Append("out ");
        }

        if (parameterInfo.IsRetval)
        {
            sb.Append("ref ");
        }

        sb.Append(parameterInfo.ParameterType.ToString().Replace("+", "."));
        sb.Append(" ");
        sb.Append(parameterInfo.Name);

        return sb.ToString();
    }

    public static void MakeMethod(StringBuilder sb, MethodInfo method, string cName)
    {
        var returnType = method.ReturnType.ToString().Replace("+", ".");
        if (returnType == "System.Void")
        {
            returnType = "void";
        }

        var args = method.GetParameters().Select(BuildParameter).ToList();

        if (!method.IsStatic)
        {
            args.IndexOf($@"{cName} me", 0);
        }

        sb.AppendLine(string.Format("    public static {0} {1}({2})", returnType, method.Name,
            string.Join(", ", args)));
        sb.AppendLine("    {");
        sb.AppendLine("        throw new NotImplementedException();");
        sb.AppendLine("    }");
        sb.AppendLine();
    }
}
