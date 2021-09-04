param($path)

if(!(Test-Path $path)){
    exit;
}

$enable = "!#USE_"+"Nuget"
Get-ChildItem -Recurse -Path $path -Include *.csproj | Select-String $enable -List | Select-Object Path | ForEach-Object {    
    Write-Host "Pack Project" $_.Path
    dotnet pack $_.Path -c Release --no-build 
}