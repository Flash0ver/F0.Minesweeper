using System.Diagnostics;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

IConfig config = Debugger.IsAttached
	? CreateDebugConfiguration()
	: CreateBenchmarkConfiguration();

_ = BenchmarkRunner.Run(typeof(Program).Assembly, config, args);

static IConfig CreateBenchmarkConfiguration()
	=> DefaultConfig.Instance
		.AddJob(Job.InProcess.WithRuntime(CoreRuntime.Core60))
		.AddColumn(StatisticColumn.Min, StatisticColumn.Max, StatisticColumn.Median)
		.AddDiagnoser(MemoryDiagnoser.Default)
		.AddExporter(DefaultExporters.AsciiDoc)
		.AddValidator(ExecutionValidator.FailOnError);

static IConfig CreateDebugConfiguration()
	=> new DebugInProcessConfig();
