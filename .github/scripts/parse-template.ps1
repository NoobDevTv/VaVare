param($path, $litgit, $config)

if(!(Test-Path $path)){
    exit;
}

$enable = "!#USE_"+"LitGit"
Write-Host "Use LitGit"
Get-ChildItem -Recurse -Path $path | Select-String $enable -List | Select-Object Path | ForEach-Object {    
    Write-Host "Parse template $_.Path"
    & $litgit -c $config -t $_.Path
}