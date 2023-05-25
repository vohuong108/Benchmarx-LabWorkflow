using CommandLine;
using NMF.Models.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows.Commandline
{
    internal abstract class SolveVerbBase : VerbBase
    {

        private ModelRepository repository;

        private string Scenario;
        private string ModelPath;
        private string RunIndex;
        private int Sequences;
        private string Tool;
        private string Model;

        private readonly Stopwatch _stopwatch = new Stopwatch();

        protected abstract ISolution CreateSolution();

        private ISolution _solution;

        protected override void ExecuteCore()
        {
            Initialize();
            Load();
            Initial();
            // for(int i = 1; i <= Sequences; i++)
            // {
            //     Update( i );
            // }
        }

        private void Load()
        {
            _stopwatch.Restart();
            _solution.Load( repository.Resolve( Path.Combine( ModelPath, "initial.xmi" ) ).RootElements[0] as JobRequest );
            _stopwatch.Stop();
            Report( BenchmarkPhase.Load );
        }

        private void Initialize()
        {
            _stopwatch.Restart();
            repository = new ModelRepository();

            ModelPath = Environment.GetEnvironmentVariable( nameof( ModelPath ) );
            RunIndex = Environment.GetEnvironmentVariable( nameof( RunIndex ) );
            Sequences = int.Parse( Environment.GetEnvironmentVariable( nameof( Sequences ) ) );
            Tool = Environment.GetEnvironmentVariable( nameof( Tool ) );
            Model = Environment.GetEnvironmentVariable( nameof( Model ) );
            Scenario = Environment.GetEnvironmentVariable( nameof( Scenario ) );

            _solution = CreateSolution();

            _stopwatch.Stop();
            Report( BenchmarkPhase.Initialization );
        }

        private void Initial()
        {
            _stopwatch.Restart();
            var result = _solution.Initial();
            _stopwatch.Stop();
            Report( BenchmarkPhase.Initial, null );
            MakeSureModelPathExists();
            var path = Path.Combine( ModelPath, "results", $"initialResult-{Tool}.xmi" );
            using(var target = File.Create( path ))
            {
                result.ModelUri = new Uri( path );
                repository.Serializer.Serialize( result, target );
            }
        }

        private void MakeSureModelPathExists()
        {
            if (!Directory.Exists(Path.Combine(ModelPath, "results")))
            {
                Directory.CreateDirectory( Path.Combine( ModelPath, "results" ) );
            }
        }

        private void Update( int iteration )
        {
            var changes = File.ReadAllLines( Path.Combine( ModelPath, $"change{iteration:00}.txt" ) );
            var actualActions = _solution.ComputeChanges( changes );
            _stopwatch.Restart();
            var result = actualActions();
            _stopwatch.Stop();
            Report( BenchmarkPhase.Update, iteration );
            using(var target = File.Create( Path.Combine( ModelPath, "results", $"change{iteration:00}Result-{Tool}.xmi" ) ))
            {
                repository.Serializer.Serialize( result, target );
            }
        }

        private void Report( BenchmarkPhase phase, int? iteration = null )
        {
            Console.WriteLine( $"{Tool};{Scenario};{Model};{RunIndex};{iteration ?? 0};{phase};Time;{_stopwatch.Elapsed.Ticks * 100}" );
            Console.WriteLine( $"{Tool};{Scenario};{Model};{RunIndex};{iteration ?? 0};{phase};Memory;{Environment.WorkingSet}" );
        }
    }
}
