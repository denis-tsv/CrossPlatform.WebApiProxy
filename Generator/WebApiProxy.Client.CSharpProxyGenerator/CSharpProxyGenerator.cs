using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiProxy.Common.Model;

namespace WebApiProxy.Client.CSharpProxyGenerator
{
    public class CSharpProxyGenerator
    {
        private Configuration _configuration;
        private WebApiDescription _webApiDescription;
        public string Generate(Configuration configuration, WebApiDescription webApiDescription)
        {
            _configuration = configuration;
            _webApiDescription = webApiDescription;

            var sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine();

            if (!string.IsNullOrEmpty(configuration.AdditionalNamespaces))
            {
                sb.AppendLine("// additional namespaces");
                foreach (var addNamespace in configuration.AdditionalNamespaces
                        .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sb.AppendLine($"using {addNamespace};");
                }

                sb.AppendLine();
            }

            sb.AppendLine($"namespace {configuration.Namespace}");
            sb.AppendLine("{");

            if (configuration.GenerateModel)
            {
                foreach (var modelDescription in webApiDescription.ModelDescriptions)
                {
                    sb.AppendLine(GetModel(modelDescription));
                }
                foreach (var enumDescription in webApiDescription.EnumDescriptions)
                {
                    sb.AppendLine(GetEnum(enumDescription));
                }

            }

            foreach (var controllerDescription in webApiDescription.ControllerDescriptions)
            {
                sb.AppendLine(GetProxyInterface(controllerDescription));
                sb.AppendLine(GetProxyImplementation(controllerDescription));
            }

            sb.AppendLine("}");

            //TODO расставить отступы

            return sb.ToString();
        }

        private string GetEnum(EnumDescription enumDescription)
        {
            var sb = new StringBuilder();

            AddDocumentation(sb, enumDescription.Documentation);

            sb.AppendLine($"public enum {enumDescription.Name}");
            sb.AppendLine("{");

            foreach (var description in enumDescription.PropertyDescriptions)
            {
                AddDocumentation(sb, description.Documentation);
                sb.Append(description.Name);
                if (!string.IsNullOrEmpty(description.Value))
                    sb.Append($" = {description.Value}");
                sb.AppendLine(",");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        private void AddDocumentation(StringBuilder sb, string documentation)
        {
            if (string.IsNullOrEmpty(documentation)) return;

            sb.AppendLine("/// <summary>");
            foreach (var documentationString in documentation.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                sb.Append("/// ");
                sb.AppendLine(documentationString.Trim());
            }

            sb.AppendLine("/// </summary>");
        }

        private void AddMethodDocumentation(StringBuilder sb, MethodDescription methodDescription)
        {
            if (!string.IsNullOrEmpty(methodDescription.Documentation))
            {
                sb.AppendLine("/// <summary>");
                foreach (var documentationString in methodDescription.Documentation
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sb.Append("/// ");
                    sb.AppendLine(documentationString.Trim());
                }
                sb.AppendLine("/// </summary>");
            }

            foreach (var parameterDescription in methodDescription.UrlParameterDescriptions)
            {
                if (!string.IsNullOrEmpty(parameterDescription.Documentation))
                {
                    sb.AppendLine($"/// <param name=\"{parameterDescription.Name}\">{parameterDescription.Documentation}</param>");
                }
            }

            if (!string.IsNullOrEmpty(methodDescription.BodyParameterDescription?.Documentation))
            {
                sb.AppendLine($"/// <param name=\"{methodDescription.BodyParameterDescription.Name}\">{methodDescription.BodyParameterDescription.Documentation}</param>");
            }

            if (!string.IsNullOrEmpty(methodDescription.ReturnDocumentation))
            {
                sb.AppendLine($"/// <returns>{methodDescription.ReturnDocumentation}</returns>");
            }
        }

        private string GetModel(ModelDescription modelDescription)
        {
            var sb = new StringBuilder();

            AddDocumentation(sb, modelDescription.Documentation);

            string abstractValue = modelDescription.IsAbstract ? "abstract" : "";
            string type = modelDescription.IsValueType ? "struct" : "class";
            sb.Append($"public {abstractValue} partial {type} {modelDescription.Name}");
            if (!string.IsNullOrEmpty(modelDescription.BaseModelName))
            {
                sb.Append($" : {modelDescription.BaseModelName}");
            }
            sb.AppendLine();

            sb.AppendLine("{");

            string virtualValue = modelDescription.IsValueType ? "" : "virtual";
            foreach (var prop in modelDescription.PropertyDescriptions)
            {
                AddDocumentation(sb, prop.Documentation);

                sb.Append($"public {virtualValue} {prop.Type} {prop.Name} ");
                sb.AppendLine("{ get; set; }");
            }

            sb.AppendLine("}");

            return sb.ToString();
        }

        private string GetProxyImplementation(ControllerDescription controllerDescription)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"public class {controllerDescription.Name}Client : BaseClient, I{controllerDescription.Name}Client");
            sb.AppendLine("{");

            if (!string.IsNullOrEmpty(_configuration.Ctor))
            {
                sb.AppendLine($"public {controllerDescription.Name}Client{_configuration.Ctor}");
                sb.AppendLine("{ }");
            }

            foreach (var methodDescription in controllerDescription.MethodDescriptions)
            {
                if (IsInvalidType(methodDescription.ReturnType))
                {
                    sb.AppendLine($"// Unable to generate proxy method for {methodDescription.Name}. Please specify the return value by ResponseType attribute");
                    continue;
                }

                var signature = GetProxyMethodSignature(methodDescription);
                sb.Append("public ");
                sb.AppendLine(signature);
                sb.AppendLine("{");

                var url = methodDescription.RelativePath;
                if (methodDescription.RelativePath.Contains("?"))
                {
                    int index = methodDescription.RelativePath.IndexOf("?");
                    url = methodDescription.RelativePath.Substring(0, index);
                }
                sb.AppendLine($"var url = \"{url}\";");


                sb.AppendLine();
                bool manyCustomParams = methodDescription.UrlParameterDescriptions.Count(IsCustomType) > 1;

                foreach (var urlParam in methodDescription.UrlParameterDescriptions
                    .Where(u => !string.IsNullOrEmpty(u.ProxySource)))
                {
                    sb.AppendLine($"var {urlParam.Name} = {urlParam.ProxySource};");
                }

                foreach (var urlParam in methodDescription.UrlParameterDescriptions)
                {
                    if (IsIEnumerable(urlParam.Type))
                    {
                        AppendIEnumerableParameter(urlParam, sb);
                    }
                    else if (IsCustomType(urlParam))
                    {
                        if (_configuration.GenerateModel)
                        {
                            AppendCustomParameter(urlParam, sb, manyCustomParams);
                        }
                        else
                        {
                            sb.AppendLine($"url = AppendParametersFromPropertiesOnRuntime(url, \"{urlParam.Name}\", {urlParam.Name}, {manyCustomParams.ToString().ToLower()});");
                        }
                    }
                    else
                    {
                        string format = string.IsNullOrEmpty(urlParam.ProxyFormat)
                                ? ""
                                : ", \"" + urlParam.ProxyFormat + "\"";

                        if (url.Contains("{" + urlParam.Name + "}"))
                        {
                            sb.AppendLine("url = url.Replace(\"{" + urlParam.Name + "}\", ConvertToString(" + urlParam.Name + ") " + format + ");");
                        }
                        else
                        {
                            sb.AppendLine($"url = AppendParameter(url, \"{urlParam.Name}\", {urlParam.Name}{format});");
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
                    sb.AppendLine($"return {httpMethod}NoResultAsync(url{bodyParam});");
                }
                else
                {
                    sb.AppendLine($"return {httpMethod}Async<{methodDescription.ReturnType}>(url{bodyParam});");
                }

                sb.AppendLine("}");
            }

            sb.AppendLine("}");

            return sb.ToString();
        }

        private bool IsCustomType(ParameterDescription parameterDescription)
        {
            return _webApiDescription.ModelDescriptions.Any(m => m.Name == parameterDescription.Type);
        }

        private void AppendCustomParameter(ParameterDescription urlParam, StringBuilder sb, bool includeParamName)
        {
            var model = _webApiDescription.ModelDescriptions.Find(m => m.Name == urlParam.Type);
            var properties = CollectModelProperties(model);

            string paramName = includeParamName ? $"{urlParam.Name}." : "";

            foreach (var property in properties)
            {
                if (IsIEnumerable(property.Type))
                {
                    sb.AppendLine($"foreach (var item in {urlParam.Name}.{property.Name})");
                    sb.AppendLine("{");
                    sb.AppendLine($"url = AppendParameter(url, \"{paramName}{property.Name}\", item);");
                    sb.AppendLine("}");
                }
                else
                {
                    string format = string.IsNullOrEmpty(property.ProxyFormat)
                                        ? ""
                                        : ", \"" + property.ProxyFormat + "\"";
                    sb.AppendLine($"url = AppendParameter(url, \"{paramName}{property.Name}\", {urlParam.Name}.{property.Name}{format});");
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

        private void AppendIEnumerableParameter(ParameterDescription urlParam, StringBuilder sb)
        {
            sb.AppendLine($"foreach (var item in {urlParam.Name})");
            sb.AppendLine("{");
            sb.AppendLine($"url = AppendParameter(url, \"{urlParam.Name}\", item);");
            sb.AppendLine("}");
        }

        private bool IsIEnumerable(string typeName)
        {
            return typeName.Contains("[]") || typeName.Contains("<");
        }
        private string GetProxyMethodSignature(MethodDescription methodDescription)
        {
            StringBuilder sb = new StringBuilder();

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

            return sb.ToString();
        }


        private string GetProxyInterface(ControllerDescription controllerDescription)
        {
            var sb = new StringBuilder();

            AddDocumentation(sb, controllerDescription.Documentation);

            sb.AppendLine($"public interface I{controllerDescription.Name}Client");
            sb.AppendLine("{");

            foreach (var methodDescription in controllerDescription.MethodDescriptions)
            {
                if (IsInvalidType(methodDescription.ReturnType))
                {
                    sb.AppendLine($"// Unable to generate proxy method for {methodDescription.Name}. Please specify the return value by ResponseType attribute");
                }
                else
                {
                    AddMethodDocumentation(sb, methodDescription);
                    var signature = GetProxyMethodSignature(methodDescription);
                    sb.Append(signature);
                    sb.AppendLine(";");
                }
            }

            sb.AppendLine("}");

            return sb.ToString();
        }

        private bool IsInvalidType(string typeName)
        {
            if (typeName == "HttpResponseMessage") return true;
            if (typeName == "IHttpActionResult") return true;

            return false;
        }
    }
}
