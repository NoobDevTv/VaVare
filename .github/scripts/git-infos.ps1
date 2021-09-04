param($path)

if(!(Test-Path $path)){
    exit;
}

$name = "GitInfo"
$enable = "!#USE_$name"
Write-Host "Use $name"

$childItems = Get-ChildItem -Recurse -Path $path
$length = $childItems.Length
Write-Host "Found $length files"
$selection = $childItems | Select-String $enable -List
$length = $selection.Length
Write-Host "Select $length files"
$paths = $selection | Select-Object Path

Write-Host "fetch current repo"
git fetch
Write-Host "-----------------------------"

Write-Host "get current branch"
$current_branch = git branch --show-current
Write-Host $current_branch
Write-Host "-----------------------------"

Write-Host "get commit count"
$count = git rev-list --count HEAD
Write-Host $count
Write-Host "-----------------------------"

Write-Host "get current hash"
$longHash = git rev-parse HEAD
Write-Host $longHash
Write-Host "-----------------------------"

Write-Host "get current short hash"
$shortHash = git rev-parse --short HEAD
Write-Host $shortHash
Write-Host "-----------------------------"

Write-Host "Execute"
$paths | ForEach-Object {    
    $p = $_.Path;
    Write-Host "Replace Git placeholder in $p"
    $fileContent = Get-Content $_.Path
    $fileContent = $fileContent -replace "{{BRANCH}}", $current_branch
    $fileContent = $fileContent -replace "{{COMMIT_COUNT}}", $count
    $fileContent = $fileContent -replace "{{COMMIT_HASH}}", $longHash
    $fileContent = $fileContent -replace "{{COMMIT_HASH_SHORT}}", $shortHash
    $fileContent | Set-Content $_.Path
}