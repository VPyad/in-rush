using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InRushCore.Compilers
{
    internal static class CompilersConfigHelper
    {
        internal static string ValidateNullOrEmpty(string s)
        {
            if (string.IsNullOrEmpty(s))
                throw new ValidationException($"var {nameof(s)} is null of empty");

            return s;
        }

        internal static IEnumerable<string> ValidateNullOrEmpty(IEnumerable<string> list)
        {
            if (list == null || !list.GetEnumerator().MoveNext())
                throw new ValidationException($"var {nameof(list)} is null of empty");

            return list;
        }

        internal static void ValidateCompilerObject(SupportedCompilers compiler)
        {
            ValidateNullOrEmpty(compiler.Id);
            ValidateNullOrEmpty(compiler.Name);
            ValidateNullOrEmpty(compiler.Lang);
            ValidateNullOrEmpty(compiler.Invocation);
            ValidateNullOrEmpty(compiler.VersionCommand);
            ValidateNullOrEmpty(compiler.Commands);
        }

        internal static JObject GenerateCompilerJson(SupportedCompilers compiler)
        {
            JTokenWriter writer = new JTokenWriter();

            writer.WriteStartObject();

            writer.WritePropertyName("id");
            writer.WriteValue(compiler.Id);

            writer.WritePropertyName("name");
            writer.WriteValue(compiler.Name);

            writer.WritePropertyName("lang");
            writer.WriteValue(compiler.Lang);

            writer.WritePropertyName("invocation");
            writer.WriteValue(compiler.Invocation);

            writer.WritePropertyName("version");
            writer.WriteValue(compiler.VersionCommand);

            writer.WritePropertyName("timeout");
            writer.WriteValue(compiler.TimeOut);

            writer.WritePropertyName("commands");
            writer.WriteStartArray();
            foreach (var item in compiler.Commands)
                writer.WriteValue(item);
            writer.WriteEndArray();

            writer.WriteEndObject();

            return (JObject)writer.Token;
        }
    }
}
