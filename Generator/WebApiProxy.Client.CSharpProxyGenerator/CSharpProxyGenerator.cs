using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WebApiProxy.Common.Model;

namespace WebApiProxy.Client.CSharpProxyGenerator
{
    public class CSharpProxyGenerator
    {
        private readonly ProxyGeneratorConfiguration _proxyGeneratorConfiguration;
        private readonly WebApiDescription _webApiDescription;
        
        public CSharpProxyGenerator(ProxyGeneratorConfiguration configuration, WebApiDescription description)
        {
            _proxyGeneratorConfiguration = configuration;
            _webApiDescription = description;
        }

        public string Generate()
        {
            var sb = new StringBuilder();

            AddUsings(sb);

            sb.AppendLine($"namespace {_proxyGeneratorConfiguration.Namespace}");
            sb.AppendLine("{");

            int tabs = 1;

            if (_proxyGeneratorConfiguration.GenerateModel)
            {
                foreach (var modelDescription in _webApiDescription.ModelDescriptions)
                {
                    AddModel(sb, modelDescription, tabs);
                }
                foreach (var enumDescription in _webApiDescription.EnumDescriptions)
                {
                    AddEnum(sb, enumDescription, tabs);
                }
            }

            foreach (var controllerDescription in _webApiDescription.ControllerDescriptions)
            {
                AddProxyInterface(sb, controllerDescription, tabs);
                AddProxyImplementation(sb, controllerDescription, tabs);
            }

            sb.AppendLine("}");

            return sb.ToString();
        }

        private void AddUsings(StringBuilder sb)
        {
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;"); 
            sb.AppendLine("using System.Collections.ObjectModel;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine();

            if (_proxyGeneratorConfiguration.AdditionalNamespaces != null)
            {
                sb.AppendLine("// additional namespaces");
                foreach (var addNamespace in _proxyGeneratorConfiguration.AdditionalNamespaces)
                {
                    sb.AppendLine($"using {addNamespace};");
                }

                sb.AppendLine();
            }
        }

        private void AddEnum(StringBuilder sb, EnumDescription enumDescription, int tabs)
        {
            AddDocumentation(sb, enumDescription.Documentation, tabs);

            sb.AppendLine(tabs, $"public enum {enumDescription.Name}");
            sb.AppendLine(tabs, "{");

            foreach (var description in enumDescription.PropertyDescriptions)
            {
                AddDocumentation(sb, description.Documentation, tabs + 1);
                sb.Append(tabs + 1, description.Name);
                if (!string.IsNullOrEmpty(description.Value))
                    sb.Append($" = {description.Value}");
                sb.AppendLine(",");
            }

            sb.AppendLine(tabs, "}");
        }

        private void AddDocumentation(StringBuilder sb, string documentation, int tabs)
        {
            if (string.IsNullOrEmpty(documentation)) return;

            sb.AppendLine(tabs, "/// <summary>");
            foreach (var documentationString in documentation.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                sb.Append(tabs, "/// ");
                sb.AppendLine(documentationString.Trim());
            }

            sb.AppendLine(tabs, "/// </summary>");
        }

        private void AddMethodDocumentation(StringBuilder sb, MethodDescription methodDescription, int tabs)
        {
            if (!string.IsNullOrEmpty(methodDescription.Documentation))
            {
                sb.AppendLine(tabs, "/// <summary>");
                foreach (var documentationString in methodDescription.Documentation
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sb.Append(tabs, "/// ");
                    sb.AppendLine(documentationString.Trim());
                }
                sb.AppendLine(tabs, "/// </summary>");
            }

            foreach (var parameterDescription in methodDescription.UrlParameterDescriptions)
            {
                if (!string.IsNullOrEmpty(parameterDescription.Documentation))
                {
                    sb.AppendLine(tabs, $"/// <param name=\"{parameterDescription.Name}\">{parameterDescription.Documentation}</param>");
                }
            }

            if (!string.IsNullOrEmpty(methodDescription.BodyParameterDescription?.Documentation))
            {
                sb.AppendLine(tabs, $"/// <param name=\"{methodDescription.BodyParameterDescription.Name}\">{methodDescription.BodyParameterDescription.Documentation}</param>");
            }

            if (!string.IsNullOrEmpty(methodDescription.ReturnDocumentation))
            {
                sb.AppendLine(tabs, $"/// <returns>{methodDescription.ReturnDocumentation}</returns>");
            }
        }

        private void AddModel(StringBuilder sb, ModelDescription modelDescription, int tabs)
        {
            AddDocumentation(sb, modelDescription.Documentation, tabs);

            string abstractValue = modelDescription.IsAbstract ? "abstract" : "";
            string type = modelDescription.IsValueType ? "struct" : "class";
            sb.Append(tabs, $"public {abstractValue} partial {type} {modelDescription.Name}");
            if (!string.IsNullOrEmpty(modelDescription.BaseModelName))
            {
                sb.Append($" : {modelDescription.BaseModelName}");
            }
            sb.AppendLine();

            sb.AppendLine(tabs, "{");

            string virtualValue = modelDescription.IsValueType ? "" : "virtual";
            foreach (var prop in modelDescription.PropertyDescriptions)
            {
                AddDocumentation(sb, prop.Documentation, tabs + 1);

                sb.Append(tabs + 1, $"public {virtualValue} {prop.Type} {prop.Name} ");
                sb.AppendLine("{ get; set; }");
            }

            sb.AppendLine(tabs, "}");
        }

        private void AddProxyImplementation(StringBuilder sb, ControllerDescription controllerDescription, int tabs)
        {
            sb.AppendLine(tabs, $"public class {controllerDescription.Name}{_proxyGeneratorConfiguration.Suffix} : {_proxyGeneratorConfiguration.BaseProxyClass}, I{controllerDescription.Name}{_proxyGeneratorConfiguration.Suffix}");
            sb.AppendLine(tabs, "{");

            tabs ++;

            if (!string.IsNullOrEmpty(_proxyGeneratorConfiguration.ProxyConstructor))
            {
                sb.AppendLine(tabs, $"public {controllerDescription.Name}Client{_proxyGeneratorConfiguration.ProxyConstructor}");
                sb.AppendLine(tabs, "{ }");
            }

            foreach (var methodDescription in controllerDescription.MethodDescriptions)
            {
                if (IsInvalidType(methodDescription.ReturnType))
                {
                    sb.AppendLine(tabs, $"// Unable to generate proxy method for {methodDescription.Name}. Please specify the return value by ResponseType attribute");
                    continue;
                }
                sb.Append(tabs, "public ");
                AddProxyMethodSignature(sb, methodDescription);
                sb.AppendLine();
                sb.AppendLine(tabs, "{");
                tabs ++;
                var url = methodDescription.RelativePath;
                if (methodDescription.RelativePath.Contains("?"))
                {
                    int index = methodDescription.RelativePath.IndexOf("?", StringComparison.InvariantCulture);
                    url = methodDescription.RelativePath.Substring(0, index);
                }
                sb.AppendLine(tabs, $"var url = \"{url}\";");


                sb.AppendLine();
                bool manyCustomParams = methodDescription.UrlParameterDescriptions.Count(IsCustomType) > 1;

                foreach (var urlParam in methodDescription.UrlParameterDescriptions
                    .Where(u => !string.IsNullOrEmpty(u.ProxySource)))
                {
                    sb.AppendLine(tabs, $"var {urlParam.Name} = {urlParam.ProxySource};");
                }

                foreach (var urlParam in methodDescription.UrlParameterDescriptions)
                {
                    if (IsIEnumerable(urlParam.Type))
                    {
                        AppendIEnumerableParameter(urlParam, sb, tabs);
                    }
                    else if (IsCustomType(urlParam))
                    {
                        if (_proxyGeneratorConfiguration.GenerateModel)
                        {
                            AppendCustomParameter(urlParam, sb, manyCustomParams, tabs);
                        }
                        else
                        {
                            sb.AppendLine(tabs, $"url = AppendParametersFromPropertiesOnRuntime(url, \"{urlParam.Name}\", {urlParam.Name}, {manyCustomParams.ToString().ToLower()});");
                        }
                    }
                    else
                    {
                        string format = string.IsNullOrEmpty(urlParam.ProxyFormat)
                                ? ""
                                : ", \"" + urlParam.ProxyFormat + "\"";

                        if (url.Contains("{" + urlParam.Name + "}"))
                        {
                            sb.AppendLine(tabs, "url = url.Replace(\"{" + urlParam.Name + "}\", ConvertToString(" + urlParam.Name + ") " + format + ");");
                        }
                        else
                        {
                            sb.AppendLine(tabs, $"url = AppendParameter(url, \"{urlParam.Name}\", {urlParam.Name}{format});");
                        }

                    }
                    sb.AppendLine();
                }


                string bodyParam = methodDescription.BodyParameterDescription != null ?
                    ", " + methodDescription.BodyParameterDescription.Name :
                    "";
                string httpMethod = methodDescription.HttpMethod.Substring(0, 1) +
                                    methodDescription.HttpMethod.Substring(1).ToLower();
                if (methodDescription.ReturnType == null)
                {
                    sb.AppendLine(tabs, $"return {httpMethod}NoResultAsync(url{bodyParam});");
                }
                else
                {
                    sb.AppendLine(tabs, $"return {httpMethod}Async<{methodDescription.ReturnType}>(url{bodyParam});");
                }
                tabs--;
                sb.AppendLine(tabs, "}");
            }
            tabs--;
            sb.AppendLine(tabs, "}");
        }

        private bool IsCustomType(ParameterDescription parameterDescription)
        {
            return _webApiDescription.ModelDescriptions.Any(m => m.Name == parameterDescription.Type);
        }

        private void AppendCustomParameter(ParameterDescription urlParam, StringBuilder sb, bool includeParamName, int tabs)
        {
            var model = _webApiDescription.ModelDescriptions.Find(m => m.Name == urlParam.Type);
            var properties = CollectModelProperties(model);

            string paramName = includeParamName ? $"{urlParam.Name}." : "";

            foreach (var property in properties)
            {
                if (IsIEnumerable(property.Type))
                {
                    sb.AppendLine(tabs, $"foreach (var item in {urlParam.Name}.{property.Name})");
                    sb.AppendLine(tabs, "{");
                    sb.AppendLine(tabs + 1, $"url = AppendParameter(url, \"{paramName}{property.Name}\", item);");
                    sb.AppendLine(tabs, "}");
                }
                else
                {
                    string format = string.IsNullOrEmpty(property.ProxyFormat)
                                        ? ""
                                        : ", \"" + property.ProxyFormat + "\"";
                    sb.AppendLine(tabs, $"url = AppendParameter(url, \"{paramName}{property.Name}\", {urlParam.Name}.{property.Name}{format});");
                }
            }
        }

        private List<ModelPropertyDescription> CollectModelProperties(ModelDescription model)
        {
            var result = new List<ModelPropertyDescription>();

            result.AddRange(model.PropertyDescriptions);

            if (!string.IsNullOrEmpty(model.BaseModelName))
            {
                var baseModel = _webApiDescription.ModelDescriptions.Find(m => m.Name == model.BaseModelName);
                var baseProperties = CollectModelProperties(baseModel);
                result.AddRange(baseProperties);
            }

            return result;
        }

        private void AppendIEnumerableParameter(ParameterDescription urlParam, StringBuilder sb, int tabs)
        {
            sb.AppendLine(tabs, $"foreach (var item in {urlParam.Name})");
            sb.AppendLine(tabs, "{");
            sb.AppendLine(tabs + 1, $"url = AppendParameter(url, \"{urlParam.Name}\", item);");
            sb.AppendLine(tabs, "}");
        }

        private bool IsIEnumerable(string typeName)
        {
            return typeName.Contains("[]") || typeName.Contains("<");
        }
        private void AddProxyMethodSignature(StringBuilder sb, MethodDescription methodDescription)
        {
            string returnType = methodDescription.ReturnType == null ?
                "Task" :
                $"Task<{methodDescription.ReturnType}>";
            sb.Append($"{returnType} {methodDescription.Name}Async");
            sb.Append("(");

            var paramStrings = new List<string>();
            foreach (var parameterDescription in methodDescription.UrlParameterDescriptions)
            {
                if (!string.IsNullOrEmpty(parameterDescription.ProxySource)) continue;

                string paramString;
                if (parameterDescription.IsOptional)
                {
                    if (parameterDescription.Type == "string")
                    {
                        paramString = $"{parameterDescription.Type} {parameterDescription.Name} = \"{parameterDescription.DefaultValue}\"";
                    }
                    else
                    {
                        paramString = $"{parameterDescription.Type} {parameterDescription.Name} = {parameterDescription.DefaultValue}";
                    }
                }
                else
                {
                    paramString = $"{parameterDescription.Type} {parameterDescription.Name}";
                }
                paramStrings.Add(paramString);
            }
            var parameters = string.Join(", ", paramStrings);
            sb.Append(parameters);

            if (methodDescription.BodyParameterDescription != null)
            {
                string comma = "";
                if (methodDescription.UrlParameterDescriptions.Any(u => string.IsNullOrEmpty(u.ProxySource)))
                {
                    comma = ", ";
                }

                var bodyParam = methodDescription.BodyParameterDescription;
                sb.Append($"{comma}{bodyParam.Type} {bodyParam.Name}");
            }

            sb.Append(")");
        }


        private void AddProxyInterface(StringBuilder sb, ControllerDescription controllerDescription, int tabs)
        {
            AddDocumentation(sb, controllerDescription.Documentation, tabs);

            var baseInterface = !string.IsNullOrEmpty(_proxyGeneratorConfiguration.BaseProxyInterface)
                ? " : " + _proxyGeneratorConfiguration.BaseProxyInterface
                : string.Empty;
            sb.AppendLine(tabs, $"public interface I{controllerDescription.Name}{_proxyGeneratorConfiguration.Suffix}{baseInterface}");
            sb.AppendLine(tabs, "{");

            foreach (var methodDescription in controllerDescription.MethodDescriptions)
            {
                if (IsInvalidType(methodDescription.ReturnType))
                {
                    sb.AppendLine(tabs + 1, $"// Unable to generate proxy method for {methodDescription.Name}. Please specify the return value by ResponseType attribute");
                }
                else
                {
                    AddMethodDocumentation(sb, methodDescription, tabs + 1);
                    sb.Append(tabs + 1, "");
                    AddProxyMethodSignature(sb, methodDescription);
                    sb.AppendLine(";");
                }
            }

            sb.AppendLine(tabs, "}");
        }

        private bool IsInvalidType(string typeName)
        {
            if (typeName == "HttpResponseMessage") return true;
            if (typeName == "IHttpActionResult") return true;

            return false;
        }
    }

    static class SBExt
    {
        private static void AddTabs(StringBuilder sb, int tabs)
        {
            for (int i = 0; i < tabs; i++)
            {
                sb.Append('\t');
            }
        }

        public static void Append(this StringBuilder sb, int level, string value)
        {
            AddTabs(sb, level);
            sb.Append(value);
        }

        public static void AppendLine(this StringBuilder sb, int level, string value)
        {
            AddTabs(sb, level);
            sb.AppendLine(value);
        }
    }
}
