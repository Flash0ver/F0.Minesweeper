#Requires -Version 6.0

[CmdletBinding()]
param (
    [Parameter(Mandatory=$false)]
    [Alias('c')]
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Debug',

    [Parameter(Mandatory=$false)]
    [Alias('or','open')]
    [switch]$OpenReports
)

Set-StrictMode -Version 3.0
$ErrorActionPreference = 'Stop'

$RepositoryRootPath = (Get-Item -Path $PSScriptRoot).Parent
$NuGetConfigurationFile = Join-Path -Path $RepositoryRootPath -ChildPath 'nuget.config'
$SolutionFile = Join-Path -Path $RepositoryRootPath -ChildPath 'source' -AdditionalChildPath 'F0.Minesweeper.sln'
$TestResultsDirectory = Join-Path -Path $RepositoryRootPath -ChildPath 'source' -AdditionalChildPath 'test', 'TestResults'
$CoverageReportTargetDirectory = Join-Path -Path $TestResultsDirectory -ChildPath 'coveragereport'
$CoverageReportTargetFile = Join-Path -Path $CoverageReportTargetDirectory -ChildPath 'index.html'
$StrykerConfigurationFile = Join-Path -Path $RepositoryRootPath -ChildPath 'source' -AdditionalChildPath 'test', 'stryker-config.json'

dotnet clean $SolutionFile --configuration $Configuration --nologo --verbosity minimal

dotnet restore $SolutionFile --configfile $NuGetConfigurationFile --verbosity minimal
dotnet tool restore --configfile $NuGetConfigurationFile --verbosity minimal

dotnet build $SolutionFile --configuration $Configuration --no-incremental --no-restore --nologo --verbosity minimal

if (Test-Path -Path $TestResultsDirectory) {
    Remove-Item -Path $TestResultsDirectory -Recurse
}

dotnet test $SolutionFile --configuration $Configuration --no-build --nologo --results-directory $TestResultsDirectory /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./TestResults/

dotnet tool run reportgenerator "-reports:$RepositoryRootPath/**/coverage.cobertura.xml" "-targetdir:$CoverageReportTargetDirectory" -reporttypes:HtmlInline_AzurePipelines

dotnet tool run dotnet-stryker --solution $SolutionFile --config-file $StrykerConfigurationFile

if ($OpenReports) {
    Invoke-Item $CoverageReportTargetFile

    $MutationResultsDirectory = Join-Path -Path (Get-Location).Path -ChildPath 'StrykerOutput'
    $LatestMutationReportDirectory = Get-ChildItem -Path $MutationResultsDirectory -Directory | Sort-Object -Property Name -Descending | Select-Object -First 1
    $LatestMutationReportFile = Join-Path -Path $LatestMutationReportDirectory -ChildPath 'reports' -AdditionalChildPath 'mutation-report.html'
    Invoke-Item -Path $LatestMutationReportFile
}
