param($path,$placeholder,$value)

if(!(Test-Path $path)){
    exit;
}

$name = "PLACEHOLDER"
$enable = "!#USE_$name"
Write-Host "Use $name"

$childItems = Get-ChildItem -Recurse -Path $path -Include *.csproj
Write-Host "Found $childItems.Length files"
$selection = $childItems | Select-String $enable -List
Write-Host "Select $selection.Length files"
$paths = $selection | Select-Object Path

Write-Host "Execute for "
$paths | ForEach-Object {    
    Write-Host "Replace {{$placeholder}} with $value in $_.Path"
    (Get-Content $_.Path) -replace "{{$placeholder}}", $value | Set-Content $_.Path
}