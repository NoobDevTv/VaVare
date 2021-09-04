param($path, $litgit, $config)

if(!(Test-Path $path)){
    exit;
}

$name = "LitGit"
$enable = "!#USE_$name"
Write-Host "Use $name"

$childItems = Get-ChildItem -Recurse -Path $path
$length = $childItems.Length
Write-Host "Found $length files"
$selection = $childItems | Select-String $enable -List
$length = $selection.Length
Write-Host "Select $length files"
$paths = $selection | Select-Object Path

Write-Host "Execute"
$paths | ForEach-Object {    
    $p = $_.Path;
    Write-Host "Parse template $p"
    & $litgit -c $config -t $_.Path
}