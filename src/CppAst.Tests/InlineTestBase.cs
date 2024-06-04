using System;
using System.IO;
using NUnit.Framework;

namespace CppAst.Tests
{
    public class InlineTestBase
    {
        public static CppParserOptions ConfigureForGCC(String target, Action<CppParserOptions> configure)
        {

        var options = new CppParserOptions();

        // These options should work for either x86_64 or aarch64
        if (target.Equals("x86_64"))
        {
            options.TargetCpu = CppTargetCpu.X86_64;
            options.TargetVendor = "pc";
            options.TargetAbi = "x86_64-linux-gnu";
        }
        else if (target.Equals("aarch64"))
        {
            options.TargetCpu = CppTargetCpu.ARM64;
            options.TargetVendor = "arm";
            options.TargetAbi = "aarch64-linux-gnu";
        }
        else
        {
            throw new ArgumentException($"Invalid target architecture {target}. Supported values are x86_64 and aarch64.");
        }

        options.TargetCpuSub = string.Empty;
        options.TargetSystem = "linux";
        options.AdditionalArguments.Add("-m64");
        options.AdditionalArguments.Add("-O0");

        // These options are to handle a few warnings that we generally don't want
        // bindr to be reporting. The -Qunused-arguments is actually to fix a bug with
        // libClangSharp that reports errors on line 0.
        options.AdditionalArguments.Add("-Qunused-arguments");
        options.AdditionalArguments.Add("-Wno-backslash-newline-escape");
        options.AdditionalArguments.Add("-Wno-deprecated");
        options.AdditionalArguments.Add("-Wno-attributes");
        
        configure(options);
        return options;
        }

        public void ParseAssert(string text, Action<CppCompilation> assertCompilation, CppParserOptions options = null)
        {
            if (assertCompilation == null) throw new ArgumentNullException(nameof(assertCompilation));

            options = options ?? new CppParserOptions();
            var currentDirectory = Environment.CurrentDirectory;
            var headerFilename = $"{TestContext.CurrentContext.Test.FullName}-{TestContext.CurrentContext.Test.ID}.h";
            var headerFile = Path.Combine(currentDirectory, headerFilename);

            // Parse in memory
            var compilation = CppParser.Parse(text, options, headerFilename);
            foreach (var diagnosticsMessage in compilation.Diagnostics.Messages)
            {
                Console.WriteLine(diagnosticsMessage);
            }

            assertCompilation(compilation);

            // Parse single file from disk
            File.WriteAllText(headerFile, text);
            compilation = CppParser.ParseFile(headerFile, options);
            assertCompilation(compilation);
        }
    }
}