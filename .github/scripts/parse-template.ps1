param($path, $litgit, $config)

if(!(Test-Path $path)){
    exit;
}

$name = "LitGit"
$enable = "!#USE_$name"
Write-Host "Use $name"

$childItems = Get-ChildItem -Recurse -Path $path -Include *.csproj
Write-Host "Found $childItems.Length files"
$selection = $childItems | Select-String $enable -List
Write-Host "Select $selection.Length files"
$paths = $selection | Select-Object Path

Write-Host "Execute for "
$paths | ForEach-Object {    
    Write-Host "Parse template $_.Path"
    & $litgit -c $config -t $_.Path
}