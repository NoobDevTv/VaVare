param($path)

if(!(Test-Path $path)){
    exit;
}

$name = "Nuget"
$enable = "!#USE_$name"
Write-Host "Use $name"

$childItems = Get-ChildItem -Recurse -Path $path -Include *.csproj
Write-Host "Found $childItems.Length files"
$selection = $childItems | Select-String $enable -List
Write-Host "Select $selection.Length files"
$paths = $selection | Select-Object Path

Write-Host "Execute for "
$paths | ForEach-Object {    
    Write-Host "Pack Project" $_.Path
    dotnet pack $_.Path -c Release --no-build 
}