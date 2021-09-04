param($path)

if(!(Test-Path $path)){
    exit;
}

Get-ChildItem -Recurse -Path $path -Include *.csproj | Select-String "#NUGET#" -List | Select-Object Path | ForEach-Object {    
    Write-Host "Pack Project" $_.Path
    dotnet pack $_.Path -c Release --no-build 
}



