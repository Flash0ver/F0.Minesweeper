using System.Diagnostics;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;

IConfig config = Debugger.IsAttached
	? CreateDebugConfiguration()
	: CreateBenchmarkConfiguration();

_ = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);

static IConfig CreateBenchmarkConfiguration()
	=> DefaultConfig.Instance
		.WithOptions(ConfigOptions.StopOnFirstError)
		.AddColumn(StatisticColumn.Min, StatisticColumn.Max, StatisticColumn.Median, RankColumn.Arabic)
		.AddDiagnoser(MemoryDiagnoser.Default);

static IConfig CreateDebugConfiguration()
	=> new DebugInProcessConfig();
