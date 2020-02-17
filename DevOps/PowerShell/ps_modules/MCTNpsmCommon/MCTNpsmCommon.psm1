
# Write-Warning $PSScriptRoot

# Get public and private function definition files.
$Public = @( Get-ChildItem -Path $PSScriptRoot\Public\*.ps1 -ErrorAction SilentlyContinue -Recurse)
$Private = @( Get-ChildItem -Path $PSScriptRoot\Private\*.ps1 -ErrorAction SilentlyContinue -Recurse)

#Dot source the files
Foreach($import in @($Private))
{
    # Write-Warning $import.fullname
    . $import.fullname
}

Foreach($import in @($Public))
{
    # Write-Warning $import.fullname
    . $import.fullname
}

# Here I might...
# Read in or create an initial config file and variable
# Export Public functions ($Public.BaseName) for WIP modules
# Set variables visible to the module and its functions only

Export-ModuleMember -Function GetHTableFromJson, CountKeysFromJson
