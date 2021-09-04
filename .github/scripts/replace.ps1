param($path,$placeholder,$value)

if(!(Test-Path $path)){
    exit;
}

$enable = "!#USE_"+"PLACEHOLDER"
Get-ChildItem -Recurse -Path $path | Select-String $enable -List | Select-Object Path | ForEach-Object {    
    Write-Host "Replace {{$placeholder}} with $value in $_.Path"
    (Get-Content $_.Path) -replace "{{$placeholder}}", $value | Set-Content $_.Path
}
