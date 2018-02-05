using InRushCore.Compilers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System;

namespace InRushCoreUnitTests
{
    /// <summary>
    /// Tests DO NOT ATOMIC always RUN ALL SUIT 
    /// </summary>
    [TestClass]
    public class CompilersManagerUnitTest
    {
        public string pathToConfig = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\TestData\compilers_config_unit_test.json");
        private CompilersManager manager;

        [TestInitialize]
        public void InitTest()
        {
            File.WriteAllText(pathToConfig, sourceJson.ToString());

            manager = new CompilersManager
            {
                PathToConfig = pathToConfig
            };
        }

        [TestMethod]
        public void ReadCompilersInfo()
        {
            List<SupportedCompilers> compilers = new List<SupportedCompilers>(manager.GetSupportedCompilers());

            Assert.AreEqual(1, compilers.Count(x => x.Name == ".Net"));
            Assert.AreEqual("Javac", compilers.First(x => x.Lang == "Java").Name);
            Assert.AreEqual("#cd", compilers.First().Commands.First());
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void AddCompilerWithEmptyField()
        {
            SupportedCompilers compiler = new SupportedCompilers("", "test", "test", "test", "test", 1000, new string[] { "1" });

            manager.AddCompiler(compiler);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void AddCompilerWithNullField()
        {
            SupportedCompilers compiler = new SupportedCompilers(null, "test", "test", "test", "test", 1000, new string[] { "1" });

            manager.AddCompiler(compiler);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void AddCompilerWithZeroCountCommandsField()
        {
            SupportedCompilers compiler = new SupportedCompilers("test", "test", "test", "test", "test", 1000, new string[] { });

            manager.AddCompiler(compiler);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddCompilerWithExistedId()
        {
            SupportedCompilers compiler = new SupportedCompilers(".Net c#", "test", "test", "test", "test", 1000, new string[] { "1" });

            manager.AddCompiler(compiler);
        }

        [TestMethod]
        public void AddValidCompiler()
        {
            SupportedCompilers compiler = new SupportedCompilers("test", "test", "test", "test", "test", 1000, new string[] { "1" });

            manager.AddCompiler(compiler);

            SupportedCompilers comp = manager.GetCompiler("test");

            Assert.AreEqual("test", comp.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCompilerWithNotExistedId()
        {
            SupportedCompilers compiler = manager.GetCompiler("some rand id");
        }

        [TestMethod]
        public void UpdateCompiler()
        {
            SupportedCompilers compiler = new SupportedCompilers("Go Go", "test new", "test", "test", "test", 1000, new string[] { "1" });

            manager.UpdateCompiler("Go Go", compiler);            

            SupportedCompilers comp = manager.GetCompiler("Go Go");

            Assert.AreEqual("test new", comp.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateCompilerWithNotExistedId()
        {
            SupportedCompilers compiler = new SupportedCompilers("test", "test new", "test", "test", "test", 1000, new string[] { "1" });

            manager.UpdateCompiler("blah", compiler);

            SupportedCompilers comp = manager.GetCompiler("test");

            Assert.AreEqual("test new", comp.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteCompiler()
        {
            manager.DeleteCompiler("Go Go");

            manager.GetCompiler("Go Go");
        }

        #region source json
        private const string sourceJson = @"{
  'compilers': [
    {
      'name': '.Net',
      'lang': 'c#',
      'id': '.Net c#',
      'invocation': 'dotnet',
      'version': '--version',
      'commands': [
        '#cd',
        'rm -r *',
        'new console',
        '#copy',
        'clean',
        'build',
        'run',
        '#read_output'
      ],
      'timeout': '10000'
    },
    {
      'name': 'GCC',
      'lang': 'c++',
      'id': 'GCC c++',
      'invocation': 'g++',
      'version': '--version',
      'commands': [
        '#cd',
        'rm -r *',
        '#copy',
        'program.cpp',
        './a', // Linux: ./a.out
        '#read_output'
      ],
      'timeout': '10000'
    },
    {
      'name': 'GCC',
      'lang': 'c',
      'id': 'GCC c',
      'invocation': 'g++',
      'version': '--version',
      'commands': [
        '#cd',
        'rm -r *',
        '#copy',
        'program.c',
        './a',
        '#read_output'
      ],
      'timeout': '10000'
    },
    {
      'name': 'FPC',
      'lang': 'Pascal Delphi',
      'id': 'FPC Pascal Delphi',
      'invocation': 'fpc',
      'version': '-version', // Linux: 'The program 'fpc' is ...' || Windows: ''
      'commands': [
        '#cd',
        'rm -r *',
        '#copy',
        'program -Mdelphi',
        './program', // Linux: ./program
        '#read_output'
      ],
      'timeout': '10000'
    },
    {
      'name': 'FPC',
      'lang': 'Object Pascal',
      'id': 'FPC Object Pascal',
      'invocation': 'fpc',
      'version': '-version', // Linux: 'The program 'fpc' is ...' || Windows: ''
      'commands': [
        '#cd',
        'rm -r *',
        '#copy',
        'program -Mobjfpc',
        './program', // Linux: ./program
        '#read_output'
      ],
      'timeout': '10000'
    },
    {
      'name': 'FPC',
      'lang': 'Free Pascal',
      'id': 'FPC Free Pascal',
      'invocation': 'fpc',
      'version': '-version', // Linux: 'The program 'fpc' is ...' || Windows: ''
      'commands': [
        '#cd',
        'rm -r *',
        '#copy',
        'program',
        './program', // Linux: ./program
        '#read_output'
      ],
      'timeout': '10000'
    },
    {
      'name': 'Javac',
      'lang': 'Java',
      'id': 'Javac Java',
      'invocation': 'javac',
      'version': '-version', // Linux: 'The program 'fpc' is ...' || Windows: ''
      'commands': [
        '#cd',
        'rm -r *',
        '#copy',
        'program.java',
        '#execute [!WL: java program]',
        '#read_output'
      ],
      'timeout': '10000'
    },
    {
      'name': 'Go',
      'lang': 'Go',
      'id': 'Go Go',
      'invocation': 'go',
      'version': 'version', // Linux: 'The program 'fpc' is ...' || Windows: ''
      'commands': [
        '#cd',
        'rm -r *',
        '#copy',
        'build',
        './golang',
        '#read_output'
      ],
      'timeout': '10000'
    }
  ]
}";
        #endregion
    }
}
